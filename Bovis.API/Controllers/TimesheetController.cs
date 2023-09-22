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
using Bovis.API.Helper;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Bovis.API.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
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
        [HttpGet, Route("DiasHabiles/{mes}/{anio}/{sabados}")]
        public async Task<IActionResult> GetDiasHabiles(int mes, int anio, bool sabados)
        {            
            var query = await _timesheetQueryService.GetDiasHabiles(mes, anio, sabados);
            return Ok(query);
        }
        #endregion Días Hábiles

        #region TimeSheets
        [HttpPost, Route("Registro/Agregar")]
        public async Task<IActionResult> AddRegistro([FromBody] JsonObject registro)
        {
            var query = await _timesheetQueryService.AddRegistro(registro);
            if(query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpGet, Route("TimeSheets/{Activo?}")]
        public async Task<IActionResult> GetTimeSheets(bool? Activo)
        {
            var query = await _timesheetQueryService.GetTimeSheets(Activo);
            return Ok(query);
        }

        [HttpGet, Route("TimeSheets/Filtro/{idEmpleado}/{idProyecto}/{idUnidadNegocio}/{mes}")]
        public async Task<IActionResult> GetTimeSheetsByFiltro(int idEmpleado, int idProyecto, int idUnidadNegocio, int mes)
        {
            var query = await _timesheetQueryService.GetTimeSheetsByFiltro(idEmpleado, idProyecto, idUnidadNegocio, mes);
            return Ok(query);
        }

        [HttpGet, Route("TimeSheets/Fecha/{mes}/{anio}")]
        public async Task<IActionResult> GetTimeSheetsByFecha(int mes, int anio)
        {
            var query = await _timesheetQueryService.GetTimeSheetsByFecha(mes, anio);
            return Ok(query);
        }

        [HttpGet, Route("Registro/{idTimeSheet}")]
        public async Task<IActionResult> GetTimeSheet(int idTimeSheet)
        {
            var query = await _timesheetQueryService.GetTimeSheet(idTimeSheet);
            return Ok(query);
        }

        [HttpPut, Route("Registro/Actualizar")]
        public async Task<IActionResult> UpdateRegistro([FromBody] JsonObject registro)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", nombre);
            registroJsonObject.Add("Usuario", email);
            registroJsonObject.Add("Roles", string.Empty);
            registroJsonObject.Add("TransactionId", TransactionId);
            registroJsonObject.Add("Rel", 1050);

            var query = await _timesheetQueryService.UpdateRegistro(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete, Route("Registro/Borrar/{idTimeSheet}")]
        public async Task<IActionResult> DeleteTimeSheets(int idTimeSheet)
        {
            var query = await _timesheetQueryService.DeleteTimeSheet(idTimeSheet);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpGet, Route("EmpleadosByResponsable/{EmailResponsable}")]
        public async Task<IActionResult> GetEmpleadosByResponsable(string EmailResponsable)
        {
            var query = await _timesheetQueryService.GetEmpleadosByResponsable(EmailResponsable);
            return Ok(query);
        }

        [HttpGet, Route("ProyectosByResponsable/{EmailResponsable}")]
        public async Task<IActionResult> GetProyectosByResponsable(string EmailResponsable)
        {
            var query = await _timesheetQueryService.GetProyectosByResponsable(EmailResponsable);
            return Ok(query);
        }

        [HttpGet, Route("NotProyectosByEmpleado/{IdEmpleado}")]
        public async Task<IActionResult> GetNotProyectosByEmpleado(int IdEmpleado)
        {
            var query = await _timesheetQueryService.GetNotProyectosByEmpleado(IdEmpleado);
            return Ok(query);
        }

        [HttpPost, Route("ProyectoEmpleado")]
        public async Task<IActionResult> AddProyectoEmpleado([FromBody] JsonObject registro)
        {
            var query = await _timesheetQueryService.AddProyectoEmpleado(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion TimeSheets
    }
}
