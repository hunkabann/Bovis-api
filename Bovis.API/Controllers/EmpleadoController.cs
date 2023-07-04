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
    [ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
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
        [HttpGet, Route("Empleados/{Activo?}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetEmpleados(bool? Activo)
        {
            var query = await _empleadoQueryService.GetEmpleados(Activo);
            return Ok(query);
        }

        [HttpGet, Route("Registro/{idEmpleado}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetEmpleado(int idEmpleado)
        {
            var query = await _empleadoQueryService.GetEmpleado(idEmpleado);
            return Ok(query);
        }

        [HttpGet, Route("Registro/Email/{email}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetEmpleadoByEmail(string email)
        {
            var query = await _empleadoQueryService.GetEmpleadoByEmail(email);
            return Ok(query);
        }

        [HttpPost("Registro/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AddRegistro([FromBody] JsonObject registro)
        {
            var query = await _empleadoQueryService.AddRegistro(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut("Registro/Actualizar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateRegistro([FromBody] JsonObject registro)
        {
            ClaimJWTModel claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", claimJWTModel.nombre);
            registroJsonObject.Add("Usuario", claimJWTModel.correo);
            registroJsonObject.Add("Roles", claimJWTModel.roles);
            registroJsonObject.Add("TransactionId", claimJWTModel.transactionId);
            registroJsonObject.Add("Rel", 45);
            
            var query = await _empleadoQueryService.UpdateRegistro(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut("Estatus/Actualizar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateEstatus([FromBody] JsonObject registro)
        {
            ClaimJWTModel claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", claimJWTModel.nombre);
            registroJsonObject.Add("Usuario", claimJWTModel.correo);
            registroJsonObject.Add("Roles", claimJWTModel.roles);
            registroJsonObject.Add("TransactionId", claimJWTModel.transactionId);
            registroJsonObject.Add("Rel", 1047);

            var query = await _empleadoQueryService.UpdateEstatus(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Empleados

        #region Proyectos
        [HttpGet, Route("Proyectos/{idEmpleado}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetProyectos(int idEmpleado)
        {
            var query = await _empleadoQueryService.GetProyectos(idEmpleado);
            return Ok(query);
        }
        #endregion Proyectos

        #region Ciudades
        [HttpGet, Route("Ciudades/{Activo?}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetCiudades(bool? Activo)
        {
            var query = await _empleadoQueryService.GetCiudades(Activo);
            return Ok(query);
        }
        #endregion Ciudades
    }
}