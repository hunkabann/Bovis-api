using Bovis.Business.Interface;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;

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

        public Task<List<TB_Proyecto>> GetProyectos(bool? OrdenAlfabetico) => _pcsData.GetProyectos(OrdenAlfabetico);
        public Task<TB_Proyecto> GetProyecto(int numProyecto) => _pcsData.GetProyecto(numProyecto);
        public Task<List<TB_Empresa>> GetEmpresas() => _pcsData.GetEmpresas();
        public Task<List<TB_Cliente>> GetClientes() => _pcsData.GetClientes();


        #region Proyectos
        public Task<(bool Success, string Message)> AddProyecto(JsonObject registro) => _pcsData.AddProyecto(registro);

        public Task<List<Proyecto_Detalle>> GetProyectos(int IdProyecto) => _pcsData.GetProyectos(IdProyecto);

        public async Task<(bool Success, string Message)> UpdateProyecto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _pcsData.UpdateProyecto((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public Task<(bool Success, string Message)> DeleteProyecto(int IdProyecto) => _pcsData.DeleteProyecto(IdProyecto);
        #endregion Proyectos

        #region Etapas
        public Task<PCS_Etapa_Detalle> AddEtapa(JsonObject registro) => _pcsData.AddEtapa(registro);

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

        public Task<(bool Success, string Message)> DeleteEmpleado(int IdFase, int NumEmpleado) => _pcsData.DeleteEmpleado(IdFase, NumEmpleado);
        #endregion Empleados
    }
}
