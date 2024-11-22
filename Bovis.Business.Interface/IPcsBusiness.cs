using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Business.Interface
{
    public interface IPcsBusiness : IDisposable
    {
        #region Clientes
        Task<List<TB_Empresa>> GetEmpresas();
        #endregion Clientes




        #region Empreas
        Task<List<TB_Cliente>> GetClientes();
        #endregion Empresas




        #region Proyectos
        Task<List<TB_Proyecto>> GetProyectos(bool? OrdenAlfabetico);
        //atc 09-11-2024
        Task<List<TB_Proyecto>> GetProyectosNoClose(bool? OrdenAlfabetico);
        Task<TB_Proyecto> GetProyecto(int numProyecto);
        Task<(bool Success, string Message)> AddProyecto(JsonObject registro);
        Task<List<Proyecto_Detalle>> GetProyectos(int IdProyecto);
        Task<List<Tipo_Proyecto>> GetTipoProyectos();
        Task<(bool Success, string Message)> UpdateProyecto(JsonObject registro);
        Task<(bool Success, string Message)> DeleteProyecto(int IdProyecto);
        Task<(bool Success, string Message)> UpdateProyectoFechaAuditoria(JsonObject registro);
        #endregion Proyectos




        #region Etapas
        Task<PCS_Etapa_Detalle> AddEtapa(JsonObject registro);
        Task<PCS_Proyecto_Detalle> GetEtapas(int IdProyecto);
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
        Task<GastosIngresos_Detalle> GetGastosIngresos(int IdProyecto, string Tipo);
        Task<(bool Success, string Message)> UpdateGastosIngresos(JsonObject registro);
        Task<GastosIngresos_Detalle> GetTotalFacturacion(int IdProyecto);
        #endregion Gastos / Ingresos




        #region Control
        Task<Control_Detalle> GetControl(int IdProyecto);
        Task<Control_Data> GetSeccionControl(int IdProyecto, string Seccion);
        #endregion Control
    }

}
