using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Business.Interface
{
    public interface IPcsBusiness : IDisposable
    {
        Task<List<TB_Proyecto>> GetProyectos(bool? OrdenAlfabetico);
        Task<TB_Proyecto> GetProyecto(int numProyecto);
        Task<List<TB_Empresa>> GetEmpresas();
        Task<List<TB_Cliente>> GetClientes();

        #region Proyectos
        Task<(bool Success, string Message)> AddProyecto(JsonObject registro);
        Task<List<Proyecto_Detalle>> GetProyectos(int IdProyecto);
        Task<(bool Success, string Message)> UpdateProyecto(JsonObject registro);
        Task<(bool Success, string Message)> DeleteProyecto(int IdProyecto);
        #endregion Proyectos

        #region Etapas
        Task<(bool Success, string Message)> AddEtapa(JsonObject registro);
        Task<PCS_Proyecto_Detalle> GetEtapas(int IdProyecto);
        Task<(bool Success, string Message)> UpdateEtapa(JsonObject registro);
        Task<(bool Success, string Message)> DeleteEtapa(int IdEtapa);
        #endregion Etapas

        #region Empleados
        Task<(bool Success, string Message)> AddEmpleado(JsonObject registro);
        Task<List<PCS_Empleado_Detalle>> GetEmpleados(int IdFase);
        Task<(bool Success, string Message)> UpdateEmpleado(JsonObject registro);
        Task<(bool Success, string Message)> DeleteEmpleado(int IdFase, int NumEmpleado);
        #endregion Empleados
    }

}
