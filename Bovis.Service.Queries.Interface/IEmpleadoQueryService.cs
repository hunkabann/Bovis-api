using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Commands;
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
        Task<Response<Empleado_BasicData>> GetEmpleadoDetalle();
        Task<Response<(bool Success, string Message)>> AddRegistro(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdateRegistro(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdateEstatus(JsonObject registro);
        #endregion Empleados

        #region Proyectos
        Task<Response<List<Proyecto_Detalle>>> GetProyectos(int idEmpleado);
        #endregion Proyectos

        #region Ciudades
        Task<Response<List<TB_Ciudad>>> GetCiudades(bool? Activo, int? IdEstado);
        #endregion Ciudades
    }
}

