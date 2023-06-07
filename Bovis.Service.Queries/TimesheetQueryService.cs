using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class TimesheetQueryService : ITimesheetQueryService
    {
        #region base
        private readonly ITimesheetBusiness _timesheetBusiness;

        private readonly IMapper _map;

        public TimesheetQueryService(IMapper _map, ITimesheetBusiness _timesheetBusiness)
        {
            this._map = _map;
            this._timesheetBusiness = _timesheetBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<Response<Dias_Timesheet_Detalle>> GetDiasHabiles(int mes, int anio, bool sabados)
        {
            var response = await _timesheetBusiness.GetDiasHabiles(mes, anio, sabados);
            return new Response<Dias_Timesheet_Detalle> { Data = _map.Map<Dias_Timesheet_Detalle>(response), Success = true };
        }

    }
}

