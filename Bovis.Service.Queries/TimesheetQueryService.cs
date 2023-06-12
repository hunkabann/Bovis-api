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
using System.Text.Json.Nodes;
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
            return new Response<Dias_Timesheet_Detalle> { Data = _map.Map<Dias_Timesheet_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<(bool existe, string mensaje)>> AddRegistro(JsonObject registro)
        {
            var response = await _timesheetBusiness.AddRegistro(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<List<TimeSheet_Detalle>>> GetTimeSheets(bool? Activo)
        {
            var response = await _timesheetBusiness.GetTimeSheets(Activo);
            return new Response<List<TimeSheet_Detalle>> { Data = _map.Map<List<TimeSheet_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }
        public async Task<Response<TimeSheet_Detalle>> GetTimeSheet(int idTimeSheet)
        {
            var response = await _timesheetBusiness.GetTimeSheet(idTimeSheet);
            return new Response<TimeSheet_Detalle> { Data = _map.Map<TimeSheet_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<(bool existe, string mensaje)>> UpdateRegistro(JsonObject registro)
        {
            var response = await _timesheetBusiness.UpdateRegistro(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool existe, string mensaje)>> DeleteTimeSheet(int idTimeSheet)
        {
            var response = await _timesheetBusiness.DeleteTimeSheet(idTimeSheet);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }

    }
}

