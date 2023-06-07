using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;

namespace Bovis.Data.Interface
{
    public interface IEmpleadoData : IDisposable
    {
        #region Empleados
        Task<List<TB_Empleado>> GetEmpleados(bool? activo);
        Task<Empleado_Detalle> GetEmpleado(int idEmpleado);
        Task<(bool existe, string mensaje)> AddRegistro(TB_Empleado registro);
        #endregion Empleados
    }
}