using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface IReporteBusiness : IDisposable
    {
        #region Custom Reports
        Task<object> ExecReportePersonalizado(JsonObject registro);
        Task<(bool Success, string Message)> AddReportePersonalizado(JsonObject registro);
        Task<List<Reporte_Detalle>> GetReportesPersonalizados(int IdReporte);
        Task<(bool Success, string Message)> UpdateReportePersonalizado(JsonObject registro);
        Task<(bool Success, string Message)> DeleteReportePersonalizado(int IdReporte);
        #endregion Custom Reports
    }

}
