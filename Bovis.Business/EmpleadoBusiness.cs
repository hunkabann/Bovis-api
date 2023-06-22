using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;

namespace Bovis.Business
{
    public class EmpleadoBusiness : IEmpleadoBusiness
    {
        #region base
        private readonly IEmpleadoData _empleadoData;
        public EmpleadoBusiness(IEmpleadoData _empleadoData)
        {
            this._empleadoData = _empleadoData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base


        #region Empleados
        public Task<List<Empleado_Detalle>> GetEmpleados(bool? Activo) => _empleadoData.GetEmpleados(Activo);

        public Task<Empleado_Detalle> GetEmpleado(int idEmpleado) => _empleadoData.GetEmpleado(idEmpleado);

        public Task<Empleado_BasicData> GetEmpleadoByEmail(string email) => _empleadoData.GetEmpleadoByEmail(email);

        public async Task<(bool Success, string Message)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _empleadoData.AddRegistro(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro del Empleado a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        #endregion Empleados

        #region Proyectos
        public Task<List<Proyecto_Detalle>> GetProyectos(int idEmpleado) => _empleadoData.GetProyectos(idEmpleado);
        #endregion Proyectos
    }
}
