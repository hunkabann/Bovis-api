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
        #region Clientes
        Task<Response<List<InfoCliente>>> GetClientes();
        #endregion Clientes

        #region Empresas
        Task<Response<List<InfoEmpresa>>> GetEmpresas();
        #endregion Empresas

        #region Proyectos
        Task<Response<List<Proyecto>>> GetProyectos(bool? OrdenAlfabetico);
        Task<Response<Proyecto>> GetProyecto(int numProyecto);
        Task<Response<(bool Success, string Message)>> AddProyecto(JsonObject registro);
        Task<Response<List<Proyecto_Detalle>>> GetProyectos(int IdProyecto);
        Task<Response<List<Tipo_Proyecto>>> GetTipoProyectos();
        Task<Response<(bool Success, string Message)>> UpdateProyecto(JsonObject registro);
        Task<Response<(bool Success, string Message)>> DeleteProyecto(int IdProyecto);
        #endregion Proyectos

        #region Etapas
        Task<Response<PCS_Etapa_Detalle>> AddEtapa(JsonObject registro);
        Task<Response<PCS_Proyecto_Detalle>> GetEtapas(int IdProyecto);
        Task<Response<(bool Success, string Message)>> UpdateEtapa(JsonObject registro);
        Task<Response<(bool Success, string Message)>> DeleteEtapa(int IdEtapa);
        #endregion Etapas

        #region Empleados
        Task<Response<(bool Success, string Message)>> AddEmpleado(JsonObject registro);
        Task<Response<List<PCS_Empleado_Detalle>>> GetEmpleados(int IdFase);
        Task<Response<(bool Success, string Message)>> UpdateEmpleado(JsonObject registro);
        Task<Response<(bool Success, string Message)>> DeleteEmpleado(int IdFase, int NumEmpleado);
        #endregion Empleados

        #region Gastos / Ingresos
        Task<Response<GastosIngresos_Detalle>> GetGastosIngresos(int IdProyecto, string Tipo);
        #endregion Gastos / Ingresos
    }
}

