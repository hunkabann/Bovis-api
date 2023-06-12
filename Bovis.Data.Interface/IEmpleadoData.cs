using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IEmpleadoData : IDisposable
    {
        #region Empleados
        Task<List<Empleado_Detalle>> GetEmpleados(bool? activo);
        Task<Empleado_Detalle> GetEmpleado(int idEmpleado);
        Task<Empleado_BasicData> GetEmpleadoByEmail(string email);
        Task<(bool existe, string mensaje)> AddRegistro(JsonObject registro);
        #endregion Empleados

        #region Proyectos
        Task<List<Proyecto_Detalle>> GetProyectos(int idEmpleado);
        #endregion Proyectos
    }
}