using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface ITimesheetBusiness : IDisposable
    {
        Task<Dias_Timesheet_Detalle> GetDiasHabiles(int mes, int anio, bool sabados);
    }

}
