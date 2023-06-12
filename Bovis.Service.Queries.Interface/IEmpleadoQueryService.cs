using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Responses;
using System.Text.Json.Nodes;

namespace Bovis.Service.Queries.Interface
{
    public interface IEmpleadoQueryService
    {
        #region Empleados
        Task<Response<List<Empleado_Detalle>>> GetEmpleados(bool? Activo);
        Task<Response<Empleado_Detalle>> GetEmpleado(int idEmpleado);
        Task<Response<Empleado_BasicData>> GetEmpleadoByEmail(string email);
        Task<Response<(bool existe, string mensaje)>> AddRegistro(JsonObject registro);
        #endregion Empleados

        #region Proyectos
        Task<Response<List<Proyecto_Detalle>>> GetProyectos(int idEmpleado);
        #endregion Proyectos
    }
}

