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
        //atc 09-11-2024
        Task<Response<List<Proyecto>>> GetProyectosNoClose(bool? OrdenAlfabetico);        
        Task<Response<Proyecto>> GetProyecto(int numProyecto);
        Task<Response<(bool Success, string Message)>> AddProyecto(JsonObject registro);
        Task<Response<List<Proyecto_Detalle>>> GetProyectos(int IdProyecto, string fecha);
        Task<Response<List<Tipo_Proyecto>>> GetTipoProyectos();
        Task<Response<(bool Success, string Message)>> UpdateProyecto(JsonObject registro);
        Task<Response<(bool Success, string Message)>> DeleteProyecto(int IdProyecto);
        Task<Response<(bool Success, string Message)>> UpdateProyectoFechaAuditoria(JsonObject registro);
        #endregion Proyectos

        #region Etapas
        Task<Response<PCS_Etapa_Detalle>> AddEtapa(JsonObject registro);
        Task<Response<PCS_GanttData>> GetPEtapas(int IdProyecto, string fecha);
        Task<Response<PCS_Proyecto_Detalle>> GetEtapas(int IdProyecto, string fecha);
        Task<Response<(bool Success, string Message)>> UpdateEtapa(JsonObject registro);
        Task<Response<(bool Success, string Message)>> DeleteEtapa(int IdEtapa);
        #endregion Etapas

        #region Empleados
        Task<Response<(bool Success, string Message)>> AddEmpleado(JsonObject registro);
        Task<Response<List<PCS_Empleado_Detalle>>> GetEmpleados(int IdFase);
        Task<Response<(bool Success, string Message)>> UpdateEmpleado(JsonObject registro);
        Task<Response<(bool Success, string Message)>> DeleteEmpleado(int IdFase, string NumEmpleado);
        #endregion Empleados

        #region Gastos / Ingresos
        Task<Response<List<Seccion_Detalle>>> GetGastosIngresosSecciones(int IdProyecto, string Tipo);
        Task<Response<GastosIngresos_Detalle>> GetGastosIngresos(int IdProyecto, string Tipo, string Seccion);
        Task<Response<GastosIngresos_Detalle>> GetTotalesIngresos(int IdProyecto);

        Task<Response<(bool Success, string Message)>> UpdateTotalesIngresosFee(JsonObject registro); //LEO inputs para FEEs
        Task<Response<(bool Success, string Message)>> UpdateFacturacionCobranza(JsonObject registro); //LDTF
        Task<Response<(bool Success, string Message)>> UpdateGastosIngresos(JsonObject registro);
        Task<Response<GastosIngresos_Detalle>> GetTotalFacturacion(int IdProyecto);
        Task<Response<PCS_Proyecto_Inflacion>> GetProyectoInFlacion(int IdProyecto, string? sFecha); //LDTF
        Task<Response<(bool Success, string Message)>> UpdateProyectoInFlacion(JsonObject registro); //LDTF
        Task<Response<(bool Success, string Message)>> UpdateRubroValorInflacion(JsonObject registro); //LDTF

        Task<Response<(bool Success, string Message)>> UpdateGastosIngresosFee(JsonObject registro); //FEE libre

        #endregion Gastos / Ingresos

        #region Control
        Task<Response<Control_Detalle>> GetControl(int IdProyecto);
        Task<Response<Control_Data>> GetSeccionControl(int IdProyecto, string Seccion);
        #endregion Control
    }
}

