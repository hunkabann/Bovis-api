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
        Task<Response<(bool existe, string mensaje)>> AgregarRegistro(JsonObject registro);
    }
}

