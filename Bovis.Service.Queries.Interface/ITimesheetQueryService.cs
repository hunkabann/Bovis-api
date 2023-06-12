using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface ITimesheetQueryService
    {
        Task<Response<Dias_Timesheet_Detalle>> GetDiasHabiles(int mes, int anio, bool sabados);
        Task<Response<(bool existe, string mensaje)>> AddRegistro(JsonObject registro);
        Task<Response<List<TimeSheet_Detalle>>> GetTimeSheets(bool? Activo);
        Task<Response<TimeSheet_Detalle>> GetTimeSheet(int idTimeSheet);
        Task<Response<(bool existe, string mensaje)>> UpdateRegistro(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> DeleteTimeSheet(int idTimeSheet);
    }
}

