using Bovis.Common;
using Bovis.Common.Model.NoTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface IReporteQueryService
    {
        #region Custom Reports
        Task<Response<object>> ExecReportePersonalizado(JsonObject registro);
        Task<Response<(bool Success, string Message)>> AddReportePersonalizado(JsonObject registro);
        Task<Response<List<Reporte_Detalle>>> GetReportesPersonalizados(int IdReporte);
        Task<Response<(bool Success, string Message)>> UpdateReportePersonalizado(JsonObject registro);
        Task<Response<(bool Success, string Message)>> DeleteReportePersonalizado(int IdReporte);
        #endregion Custom Reports
    }
}

