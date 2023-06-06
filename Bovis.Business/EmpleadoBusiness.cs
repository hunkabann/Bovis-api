using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;

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
        public Task<List<TB_Empleado>> GetEmpleados(bool? Activo) => _empleadoData.GetEmpleados(Activo);

        public Task<TB_Empleado> GetEmpleado(int idEmpleado) => _empleadoData.GetEmpleado(idEmpleado);

        public async Task<(bool Success, string Message)> AddRegistro(TB_Empleado registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _empleadoData.AddRegistro(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo agregar el registro del Empleado a la base de datos"; return resp; }
            return resp;
        }
        #endregion Empleados
    }
}
