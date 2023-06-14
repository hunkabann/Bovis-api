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
using System.Net;

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

        #region Dias Hábiles
        [HttpGet, Route("DiasHabiles/{mes}/{anio}/{sabados}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetDiasHabiles(int mes, int anio, bool sabados)
        {
            var query = await _timesheetQueryService.GetDiasHabiles(mes, anio, sabados);
            return Ok(query);
        }
        #endregion Días Hábiles

        #region TimeSheets
        [HttpPost("Registro/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AddRegistro([FromBody] JsonObject registro)
        {
            var query = await _timesheetQueryService.AddRegistro(registro);
            if(query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpGet, Route("TimeSheets/{Activo?}")]//, Authorize(Roles = "it.full, dev.full")
        public async Task<IActionResult> GetTimeSheets(bool? Activo)
        {
            var query = await _timesheetQueryService.GetTimeSheets(Activo);
            return Ok(query);
        }

        [HttpGet, Route("TimeSheets/Empleado/{idEmpleado}")]//, Authorize(Roles = "it.full, dev.full")
        public async Task<IActionResult> GetTimeSheetsByEmpleado(int idEmpleado)
        {
            var query = await _timesheetQueryService.GetTimeSheetsByEmpleado(idEmpleado);
            return Ok(query);
        }

        [HttpGet, Route("TimeSheets/Fecha/{mes}/{anio}")]//, Authorize(Roles = "it.full, dev.full")
        public async Task<IActionResult> GetTimeSheetsByFecha(int mes, int anio)
        {
            var query = await _timesheetQueryService.GetTimeSheetsByFecha(mes, anio);
            return Ok(query);
        }

        [HttpGet, Route("Registro/{idTimeSheet}")]//, Authorize(Roles = "it.full, dev.full")
        public async Task<IActionResult> GetTimeSheet(int idTimeSheet)
        {
            var query = await _timesheetQueryService.GetTimeSheet(idTimeSheet);
            return Ok(query);
        }

        [HttpPut("Registro/Actualizar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateRegistro([FromBody] JsonObject registro)
        {
            var query = await _timesheetQueryService.UpdateRegistro(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete, Route("Registro/Borrar/{idTimeSheet}")]//, Authorize(Roles = "it.full, dev.full")
        public async Task<IActionResult> DeleteTimeSheets(int idTimeSheet)
        {
            var query = await _timesheetQueryService.DeleteTimeSheet(idTimeSheet);
            if (query.Message == string.Empty) return Ok(query);
            return Ok(query);
        }
        #endregion TimeSheets
    }
}
