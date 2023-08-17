using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IReporteData : IDisposable
    {
        #region Custom Reports
        Task<object> ExecReportePersonalizado(JsonObject registro);
        Task<(bool Success, string Message)> AddReportePersonalizado(JsonObject registro);
        Task<List<Reporte_Detalle>> GetReportesPersonalizados(int IdReporte);
        Task<(bool existe, string mensaje)> UpdateReportePersonalizado(JsonObject registro);
        Task<(bool existe, string mensaje)> DeleteReportePersonalizado(int IdReporte);
        #endregion Custom Reports
    }
}