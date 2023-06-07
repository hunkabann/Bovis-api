using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;

namespace Bovis.Business
{
    public class TimesheetBusiness : ITimesheetBusiness
    {
        #region base
        private readonly ITimesheetData _timesheetData;
        public TimesheetBusiness(ITimesheetData _timesheetData)
        {
            this._timesheetData = _timesheetData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public Task<Dias_Timesheet_Detalle> GetDiasHabiles(int mes, int anio, bool sabados) => _timesheetData.GetDiasHabiles(mes, anio, sabados);

    }
}
