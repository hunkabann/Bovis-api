using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IEmpleadoData : IDisposable
    {
        #region Empleados
        Task<List<Empleado_Detalle>> GetEmpleados(bool? activo, string fecha);
        Task<List<Empleado_Detalle>> GetEmpleadosAll(bool? activo);
        //ATC 03-12-2024
        Task<List<Empleado_Detalle>> GetEmpleadosAllFiltro(bool? activo, int? idEstado, int? idPuesto, int? idProyecto, int? idEmpresa, int? idUnidadNegocio);
        Task<Empleado_Detalle> GetEmpleado(string idEmpleado);
        Task<List<Empleado_Detalle>> GetEmpleadosByIDPuesto(string idPuesto);
        Task<Empleado_BasicData> GetEmpleadoByEmail(string email);
        Task<List<Empleado_BasicData>> GetEmpleadoDetalle();
        Task<(bool Success, string Message)> AddRegistro(JsonObject registro);
        Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro);
        Task<(bool Success, string Message)> UpdateEstatus(JsonObject registro);
        
        #endregion Empleados

        #region Proyectos
        Task<List<Proyecto_Detalle>> GetProyectos(string idEmpleado);
        #endregion Proyectos

        #region Ciudades
        Task<List<TB_Ciudad>> GetCiudades(bool? Activo, int? IdEstado);
        #endregion Ciudades
    }
}