using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;

namespace Bovis.Business
{
    public class ReporteBusiness : IReporteBusiness
    {
        #region base
        private readonly IReporteData _reporteData;
        public ReporteBusiness(IReporteData _reporteData)
        {
            this._reporteData = _reporteData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

    }
}
