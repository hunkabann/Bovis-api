using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Responses;
using System.Text.Json.Nodes;
using System.Threading.Tasks;


namespace Bovis.Service.Queries.Interface
{
    public interface IPcsQueryService
    {
        Task<Response<List<Proyecto>>> GetProyectos();
        Task<Response<Proyecto>> GetProyecto(int numProyecto);
        Task<Response<List<InfoCliente>>> GetClientes();
        Task<Response<List<InfoEmpresa>>> GetEmpresas();

        #region Proyectos
        Task<Response<(bool Success, string Message)>> AddProyecto(JsonObject registro);
        Task<Response<List<Proyecto_Detalle>>> GetProyectos(int IdProyecto);
        Task<Response<(bool existe, string mensaje)>> UpdateProyecto(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> DeleteProyecto(int IdProyecto);
        #endregion Proyectos

        #region Etapas
        Task<Response<(bool Success, string Message)>> AddEtapa(JsonObject registro);
        Task<Response<List<PCS_Etapa_Detalle>>> GetEtapas(int IdProyecto);
        Task<Response<(bool existe, string mensaje)>> UpdateEtapa(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> DeleteEtapa(int IdEtapa);
        #endregion Etapas

        #region Empleados
        Task<Response<(bool Success, string Message)>> AddEmpleado(JsonObject registro);
        Task<Response<List<PCS_Empleado_Detalle>>> GetEmpleados(int IdProyecto);
        Task<Response<(bool existe, string mensaje)>> UpdateEmpleado(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> DeleteEmpleado(int IdEmpleado);
        #endregion Empleados
    }
}

