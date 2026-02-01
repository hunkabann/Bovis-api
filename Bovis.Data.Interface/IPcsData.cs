using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IPcsData : IDisposable
    {
        #region Clientes
        Task<List<TB_Cliente>> GetClientes();
        #endregion Clientes




        #region Empresas
        Task<List<TB_Empresa>> GetEmpresas();
        #endregion Empresas




        #region Proyectos
        Task<List<TB_Proyecto>> GetProyectos(bool? OrdenAlfabetico);
        //atc 09-11-2024
        Task<List<TB_Proyecto>> GetProyectosNoClose(bool? OrdenAlfabetico);
        Task<TB_Proyecto> GetProyecto(int numProyecto);
        Task<(bool Success, string Message)> AddProyecto(JsonObject registro);
        Task<List<Proyecto_Detalle>> GetProyectos(int IdProyecto, string fecha);
        Task<List<Tipo_Proyecto>> GetTipoProyectos();
        Task<(bool Success, string Message)> UpdateProyecto(JsonObject registro);
        Task<(bool Success, string Message)> DeleteProyecto(int IdProyecto);
        Task<(bool Success, string Message)> UpdateProyectoFechaAuditoria(JsonObject registro); 
        #endregion Proyectos




        #region Etapas
        Task<PCS_Etapa_Detalle> AddEtapa(JsonObject registro);
        Task<PCS_Proyecto_Detalle> GetEtapas(int IdProyecto, string fecha);
        Task<PCS_GanttData> GetPEtapas(int IdProyecto);
        Task<(bool Success, string Message)> UpdateEtapa(JsonObject registro);
        Task<(bool Success, string Message)> DeleteEtapa(int IdEtapa);
        #endregion Etapas




        #region Empleados
        Task<(bool Success, string Message)> AddEmpleado(JsonObject registro);
        Task<List<PCS_Empleado_Detalle>> GetEmpleados(int IdFase);
        Task<(bool Success, string Message)> UpdateEmpleado(JsonObject registro);
        Task<(bool Success, string Message)> DeleteEmpleado(int IdFase, string NumEmpleado);
        #endregion Empleados




        #region Gastos / Ingresos
        Task<List<Seccion_Detalle>> GetGastosIngresosSecciones(int IdProyecto, string Tipo);
        Task<GastosIngresos_Detalle> GetGastosIngresos(int IdProyecto, string Tipo, string Seccion);
        Task<GastosIngresos_Detalle> GetTotalesIngresos(int IdProyecto);

        Task<(bool Success, string Message)> UpdateTotalesIngresosFee(JsonObject registro);//LEO inputs para FEEs
        Task<(bool Success, string Message)> UpdateFacturacionCobranza(JsonObject registro); // LDTF
        Task<PCS_Proyecto_Inflacion> GetProyectoInFlacion(int IdProyecto, string? sFecha);  // LDTF
        Task<(bool Success, string Message)> UpdateProyectoInFlacion(JsonObject registro); // LDTF
        Task<(bool Success, string Message)> UpdateRubroValorInflacion(JsonObject registro); // LDTF

        Task<(bool Success, string Message)> UpdateGastosIngresos(JsonObject registro);
        Task<GastosIngresos_Detalle> GetTotalFacturacion(int IdProyecto);

        Task<(bool Success, string Message)> UpdateGastosIngresosFee(JsonObject registro); // FEE libre

        #endregion Gastos / Ingresos




        #region Control
        Task<Control_Detalle> GetControl(int IdProyecto);
        Task<Control_Data> GetSeccionControl(int IdProyecto, string Seccion);
        #endregion Control
    }
}