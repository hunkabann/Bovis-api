using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Bovis.API.Helper;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;
using Bovis.Service.Queries.Dto.Responses;
using Microsoft.AspNetCore.Http;
using Bovis.Common.Model.Tables;

namespace Bovis.API.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class EmpleadoController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<EmpleadoController> _logger;
        private readonly IEmpleadoQueryService _empleadoQueryService;
        private readonly IMediator _mediator;

        public EmpleadoController(ILogger<EmpleadoController> logger, IEmpleadoQueryService _empleadoQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._empleadoQueryService = _empleadoQueryService;
            this._mediator = _mediator;
        }

        #region Empleados
        [HttpGet, Route("Empleados/{Activo?}")]
        public async Task<IActionResult> GetEmpleados(bool? Activo)
        {
            var query = await _empleadoQueryService.GetEmpleados(Activo);
            return Ok(query);
        }

        [HttpGet, Route("Empleados/All/{Activo?}")]
        public async Task<IActionResult> GetEmpleadosAll(bool? Activo)
        {
            var query = await _empleadoQueryService.GetEmpleadosAll(Activo);
            return Ok(query);
        }

        [HttpGet, Route("Puesto/{idPuesto}")]
        public async Task<IActionResult> GetEmpleadosByIDPuesto(string? idPuesto)
        {
            var query = await _empleadoQueryService.GetEmpleadosByIDPuesto(idPuesto);
            return Ok(query);
        }

        [HttpGet, Route("Registro/{idEmpleado}")]
        public async Task<IActionResult> GetEmpleado(string idEmpleado)
        {
            var query = await _empleadoQueryService.GetEmpleado(idEmpleado);
            return Ok(query);
        }

        [HttpGet, Route("Registro/Email/{email}")]
        public async Task<IActionResult> GetEmpleadoByEmail(string email)
        {
            var query = await _empleadoQueryService.GetEmpleadoByEmail(email);
            return Ok(query);
        }

        [HttpGet, Route("ConsultarDetalle")]
        public async Task<IActionResult> GetEmpleadoDetalle()
        {
            var query = await _empleadoQueryService.GetEmpleadoDetalle();
            return Ok(query);
        }

        [HttpPost, Route("Registro/Agregar")]
        public async Task<IActionResult> AddRegistro([FromBody] JsonObject registro)
        {
            var query = await _empleadoQueryService.AddRegistro(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut, Route("Registro/Actualizar")]
        public async Task<IActionResult> UpdateRegistro([FromBody] JsonObject registro)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string email = headers["email"];
            string nombre = headers["nombre"];
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", nombre);
            registroJsonObject.Add("Usuario", email);
            registroJsonObject.Add("Roles", string.Empty);
            registroJsonObject.Add("TransactionId", TransactionId);
            registroJsonObject.Add("Rel", 45);
            
            var query = await _empleadoQueryService.UpdateRegistro(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut, Route("Estatus/Actualizar")]
        public async Task<IActionResult> UpdateEstatus([FromBody] JsonObject registro)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string email = headers["email"];
            string nombre = headers["nombre"];
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", nombre);
            registroJsonObject.Add("Usuario", email);
            registroJsonObject.Add("Roles", string.Empty);
            registroJsonObject.Add("TransactionId", TransactionId);
            registroJsonObject.Add("Rel", 1047);

            var query = await _empleadoQueryService.UpdateEstatus(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Empleados

        #region Proyectos
        [HttpGet, Route("Proyectos/{idEmpleado}")]
        public async Task<IActionResult> GetProyectos(string idEmpleado)
        {
            var query = await _empleadoQueryService.GetProyectos(idEmpleado);
            return Ok(query);
        }
        #endregion Proyectos

        #region Ciudades
        [HttpGet, Route("Ciudades/{Activo?}/{IdEstado?}")]
        public async Task<IActionResult> GetCiudades(bool? Activo, int? IdEstado)
        {
            var query = await _empleadoQueryService.GetCiudades(Activo, IdEstado);
            return Ok(query);
        }
        #endregion Ciudades
    }
}