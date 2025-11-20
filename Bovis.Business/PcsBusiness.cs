using Bovis.Business.Interface;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;
using static System.Collections.Specialized.BitVector32;

namespace Bovis.Business
{
    public class PcsBusiness : IPcsBusiness
    {
        #region base
        private readonly IPcsData _pcsData;
        private readonly ITransactionData _transactionData;
        public PcsBusiness(IPcsData _pcsData, ITransactionData _transactionData)
        {
            this._pcsData = _pcsData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base




        #region Clientes
        public Task<List<TB_Empresa>> GetEmpresas() => _pcsData.GetEmpresas();
        #endregion Clientes




        #region Empresas
        public Task<List<TB_Cliente>> GetClientes() => _pcsData.GetClientes();
        #endregion Empresas




        #region Proyectos
        public Task<List<TB_Proyecto>> GetProyectos(bool? OrdenAlfabetico) => _pcsData.GetProyectos(OrdenAlfabetico);
        //atc 09-11-2024
        public Task<List<TB_Proyecto>> GetProyectosNoClose(bool? OrdenAlfabetico) => _pcsData.GetProyectosNoClose(OrdenAlfabetico);

        public Task<TB_Proyecto> GetProyecto(int numProyecto) => _pcsData.GetProyecto(numProyecto);

        public Task<(bool Success, string Message)> AddProyecto(JsonObject registro) => _pcsData.AddProyecto(registro);

        public Task<List<Proyecto_Detalle>> GetProyectos(int IdProyecto) => _pcsData.GetProyectos(IdProyecto);

        public Task<List<Tipo_Proyecto>> GetTipoProyectos() => _pcsData.GetTipoProyectos();

        public async Task<(bool Success, string Message)> UpdateProyecto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _pcsData.UpdateProyecto((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                //atc
               // _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public Task<(bool Success, string Message)> DeleteProyecto(int IdProyecto) => _pcsData.DeleteProyecto(IdProyecto);

        public async Task<(bool Success, string Message)> UpdateProyectoFechaAuditoria(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _pcsData.UpdateProyectoFechaAuditoria((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
        #endregion Proyectos




        #region Etapas
        public Task<PCS_Etapa_Detalle> AddEtapa(JsonObject registro) => _pcsData.AddEtapa(registro);

        public Task<PCS_GanttData> GetPEtapas(int IdProyecto) => _pcsData.GetPEtapas(IdProyecto);
        public Task<PCS_Proyecto_Detalle> GetEtapas(int IdProyecto) => _pcsData.GetEtapas(IdProyecto);

        public async Task<(bool Success, string Message)> UpdateEtapa(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _pcsData.UpdateEtapa((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public Task<(bool Success, string Message)> DeleteEtapa(int IdEtapa) => _pcsData.DeleteEtapa(IdEtapa);
        #endregion Etapas




        #region Empleados
        public Task<(bool Success, string Message)> AddEmpleado(JsonObject registro) => _pcsData.AddEmpleado(registro);

        public Task<List<PCS_Empleado_Detalle>> GetEmpleados(int IdFase) => _pcsData.GetEmpleados(IdFase);

        public async Task<(bool Success, string Message)> UpdateEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _pcsData.UpdateEmpleado((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public Task<(bool Success, string Message)> DeleteEmpleado(int IdFase, string NumEmpleado) => _pcsData.DeleteEmpleado(IdFase, NumEmpleado);
        #endregion Empleados




        #region Gastos / Ingresos
        public Task<List<Seccion_Detalle>> GetGastosIngresosSecciones(int IdProyecto, string Tipo) => _pcsData.GetGastosIngresosSecciones(IdProyecto, Tipo);
        public Task<GastosIngresos_Detalle> GetGastosIngresos(int IdProyecto, string Tipo, string Seccion) => _pcsData.GetGastosIngresos(IdProyecto, Tipo, Seccion);
        public Task<GastosIngresos_Detalle> GetTotalesIngresos(int IdProyecto) => _pcsData.GetTotalesIngresos(IdProyecto);

        // LDTF
        public async Task<(bool Success, string Message)> UpdateFacturacionCobranza(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _pcsData.UpdateFacturacionCobranza((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
            }
            return resp;

        }   // UpdateFacturacionCobranza


        //LEO inputs para FEEs I
        public async Task<(bool Success, string Message)> UpdateTotalesIngresosFee(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _pcsData.UpdateTotalesIngresosFee((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
               //_transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
        //LEO inputs para FEEs F

        /*
        public async Task<(bool Success, string Message)> UpdateGastosIngresos(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _pcsData.UpdateGastosIngresos((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
        */


        public async Task<(bool Success, string Message)> UpdateGastosIngresos(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            try
            {
                Console.WriteLine(">>> Entró a UpdateGastosIngresos (Business layer)");
                Console.WriteLine($">>> Registro recibido: {registro}");

                var registroObj = registro["Registro"]?.AsObject();
                if (registroObj == null)
                {
                    resp.Success = false;
                    resp.Message = "El campo 'Registro' no tiene un objeto JSON válido.";
                    return resp;
                }

                var respData = await _pcsData.UpdateGastosIngresos(registroObj);

                if (!respData.Success)
                {
                    resp.Success = false;
                    resp.Message = $"No se pudo actualizar el registro en la base de datos: {respData.Message}";
                    return resp;
                }

                resp = respData;

                // Registro de movimiento
                _transactionData.AddMovApi(new Mov_Api
                {
                    Nombre = registro["Nombre"]?.ToString(),
                    Roles = registro["Roles"]?.ToString(),
                    Usuario = registro["Usuario"]?.ToString(),
                    FechaAlta = DateTime.Now,
                    IdRel = Convert.ToInt32(registro["Rel"]?.ToString() ?? "0"),
                    ValorNuevo = registroObj.ToJsonString()
                });

                Console.WriteLine(">>> Actualización completada correctamente");
                return resp;
            }
            catch (Exception ex)
            {
                Console.WriteLine(">>> EXCEPCIÓN en UpdateGastosIngresos (Business layer):");
                Console.WriteLine(ex.ToString());
                return (false, $"Error interno: {ex.Message}");
            }

        }   // UpdateGastosIngresos


        public Task<GastosIngresos_Detalle> GetTotalFacturacion(int IdProyecto) => _pcsData.GetTotalFacturacion(IdProyecto);
        #endregion Gastos / Ingresos




        #region Control
        public Task<Control_Detalle> GetControl(int IdProyecto) => _pcsData.GetControl(IdProyecto);
        public Task<Control_Data> GetSeccionControl(int IdProyecto, string Seccion) => _pcsData.GetSeccionControl(IdProyecto, Seccion);
        #endregion Control
    }
}
