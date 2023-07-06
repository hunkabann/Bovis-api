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
    [ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ContratoController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<ContratoController> _logger;
        private readonly IContratoQueryService _contratoQueryService;
        private readonly IMediator _mediator;

        public ContratoController(ILogger<ContratoController> logger, IContratoQueryService _contratoQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._contratoQueryService = _contratoQueryService;
            this._mediator = _mediator;
        }

        #region Templates
        [HttpGet, Route("Templates/{Activo?}")]//, Authorize(Roles = "it.full, dev.full")
        public async Task<IActionResult> GetTemplates(bool? Activo)
        {
            var query = await _contratoQueryService.GetTemplates(Activo);
            return Ok(query);
        }

        [HttpGet, Route("Template/Registro/{idTemplate}")]//, Authorize(Roles = "it.full, dev.full")
        public async Task<IActionResult> GetTemplate(int IdTemplate)
        {
            var query = await _contratoQueryService.GetTemplate(IdTemplate);
            return Ok(query);
        }

        [HttpPost("Template/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AddTemplate([FromBody] JsonObject registro)
        {
            var query = await _contratoQueryService.AddTemplate(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut("Template/Actualizar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateTemplate([FromBody] JsonObject registro)
        {
            ClaimJWTModel claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", claimJWTModel.nombre);
            registroJsonObject.Add("Usuario", claimJWTModel.correo);
            registroJsonObject.Add("Roles", claimJWTModel.roles);
            registroJsonObject.Add("TransactionId", claimJWTModel.transactionId);
            registroJsonObject.Add("Rel", 1050);

            var query = await _contratoQueryService.UpdateTemplate(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        #endregion Templates


        #region Contratos Empleado
        [HttpGet, Route("ContratosEmpleado/{IdEmpleado}")]//, Authorize(Roles = "it.full, dev.full")
        public async Task<IActionResult> GetContratosEmpleado(int IdEmpleado)
        {
            var query = await _contratoQueryService.GetContratosEmpleado(IdEmpleado);
            return Ok(query);
        }

        [HttpGet, Route("ContratoEmpleado/Registro/{IdContratoEmpleado}")]//, Authorize(Roles = "it.full, dev.full")
        public async Task<IActionResult> GetContratoEmpleado(int IdContratoEmpleado)
        {
            var query = await _contratoQueryService.GetContratoEmpleado(IdContratoEmpleado);
            return Ok(query);
        }

        [HttpPost("ContratoEmpleado/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AddContratoEmpleado([FromBody] JsonObject registro)
        {
            var query = await _contratoQueryService.AddContratoEmpleado(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut("ContratoEmpleado/Actualizar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateContratoEmpleado([FromBody] JsonObject registro)
        {
            ClaimJWTModel claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", claimJWTModel.nombre);
            registroJsonObject.Add("Usuario", claimJWTModel.correo);
            registroJsonObject.Add("Roles", claimJWTModel.roles);
            registroJsonObject.Add("TransactionId", claimJWTModel.transactionId);
            registroJsonObject.Add("Rel", 1050);

            var query = await _contratoQueryService.UpdateContratoEmpleado(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        #endregion Contratos Empleado
    }
}
