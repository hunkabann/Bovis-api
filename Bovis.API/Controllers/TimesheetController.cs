using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json.Linq;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.API.Controllers
{
    [ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class TimesheetController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<TimesheetController> _logger;
        private readonly ITimesheetQueryService _timesheetQueryService;
        private readonly IMediator _mediator;

        public TimesheetController(ILogger<TimesheetController> logger, ITimesheetQueryService _timesheetQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._timesheetQueryService = _timesheetQueryService;
            this._mediator = _mediator;
        }

        [HttpGet, Route("DiasHabiles/{mes}/{anio}/{sabados}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetDiasHabiles(int mes, int anio, bool sabados)
        {
            var query = await _timesheetQueryService.GetDiasHabiles(mes, anio, sabados);
            return Ok(query);
        }

        [HttpPost("Registro/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AgregarRegistro([FromBody] JsonObject registro)
        {
            var query = await _timesheetQueryService.AgregarRegistro(registro);
            return Ok(query);
        }
    }
}
