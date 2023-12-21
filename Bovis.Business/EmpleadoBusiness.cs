using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using Bovis.Service.Queries.Dto.Commands;

namespace Bovis.Business
{
    public class EmpleadoBusiness : IEmpleadoBusiness
    {
        #region base
        private readonly IEmpleadoData _empleadoData;
        private readonly ITransactionData _transactionData;
        public EmpleadoBusiness(IEmpleadoData _empleadoData, ITransactionData _transactionData)
        {
            this._empleadoData = _empleadoData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base


        #region Empleados
        public Task<List<Empleado_Detalle>> GetEmpleados(bool? Activo) => _empleadoData.GetEmpleados(Activo);

        public Task<Empleado_Detalle> GetEmpleado(string idEmpleado) => _empleadoData.GetEmpleado(idEmpleado);

        public Task<Empleado_BasicData> GetEmpleadoByEmail(string email) => _empleadoData.GetEmpleadoByEmail(email);

        public Task<List<Empleado_BasicData>> GetEmpleadoDetalle() => _empleadoData.GetEmpleadoDetalle();

        public async Task<(bool Success, string Message)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _empleadoData.AddRegistro(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro del Empleado a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {            
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _empleadoData.UpdateRegistro((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro del Empleado"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateEstatus(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _empleadoData.UpdateEstatus((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro del Empleado"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
        #endregion Empleados

        #region Proyectos
        public Task<List<Proyecto_Detalle>> GetProyectos(string idEmpleado) => _empleadoData.GetProyectos(idEmpleado);
        #endregion Proyectos

        #region Ciudades
        public Task<List<TB_Ciudad>> GetCiudades(bool? Activo, int? IdEstado) => _empleadoData.GetCiudades(Activo, IdEstado);
        #endregion Ciudades
    }
}
