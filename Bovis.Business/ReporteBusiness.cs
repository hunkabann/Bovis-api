using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;

namespace Bovis.Business
{
    public class ReporteBusiness : IReporteBusiness
    {
        #region base
        private readonly IReporteData _reporteData;
        private readonly ITransactionData _transactionData;
        public ReporteBusiness(IReporteData _reporteData, ITransactionData _transactionData)
        {
            this._reporteData = _reporteData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Custom Reports
        public Task<object> ExecReportePersonalizado(JsonObject registro) => _reporteData.ExecReportePersonalizado(registro);
        public Task<(bool Success, string Message)> AddReportePersonalizado(JsonObject registro) => _reporteData.AddReportePersonalizado(registro);
        public Task<List<Reporte_Detalle>> GetReportesPersonalizados(int IdReporte) => _reporteData.GetReportesPersonalizados(IdReporte);
        public async Task<(bool Success, string Message)> UpdateReportePersonalizado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _reporteData.UpdateReportePersonalizado((JsonObject)registro["Registro"]);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public Task<(bool Success, string Message)> DeleteReportePersonalizado(int IdReporte) => _reporteData.DeleteReportePersonalizado(IdReporte);
        #endregion Custom Reports
    }
}
