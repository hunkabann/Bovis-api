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

namespace Bovis.API.Controllers
{
    [ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class PcsController : ControllerBase
    {
        #region base
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<PcsController> _logger;
        private readonly IPcsQueryService _pcsQueryService;
        private readonly IMediator _mediator;

        public PcsController(ILogger<PcsController> logger, IPcsQueryService _pcsQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._pcsQueryService = _pcsQueryService;
            this._mediator = _mediator;
        }
        #endregion base

        [HttpGet("Proyectos")]//, Authorize(Roles = "dev.full")]
        public async Task<IActionResult> ObtenerProyectos()
        {
            var business = await _pcsQueryService.GetProyectos();
            return Ok(business);
        }

        [HttpGet("Proyecto/{numProyecto}")]//, Authorize(Roles = "dev.full")]
        public async Task<IActionResult> ObtenerProyecto(int numProyecto)
        {
            var business = await _pcsQueryService.GetProyecto(numProyecto);
            return Ok(business);
        }

        [HttpGet("Clientes")]//, Authorize(Roles = "dev.full")]
        public async Task<IActionResult> ObtenerClientes()
        {
            var business = await _pcsQueryService.GetClientes();
            return Ok(business);
        }

        [HttpGet("Empresas")]//, Authorize(Roles = "dev.full")]
        public async Task<IActionResult> ObtenerEmpresas()
        {
            var business = await _pcsQueryService.GetEmpresas();
            return Ok(business);
        }

        #region Etapas
        [HttpPost("Etapas")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AddEtapa([FromBody] JsonObject registro)
        {
            var query = await _pcsQueryService.AddEtapa(registro);
            return Ok(query);
        }

        [HttpGet("Etapas/{IdProyecto}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetEtapas(int IdProyecto)
        {
            var query = await _pcsQueryService.GetEtapas(IdProyecto);
            return Ok(query);
        }

        [HttpPut("Etapas")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateEtapa([FromBody] JsonObject registro)
        {
            ClaimJWTModel claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", claimJWTModel.nombre);
            registroJsonObject.Add("Usuario", claimJWTModel.correo);
            registroJsonObject.Add("Roles", claimJWTModel.roles);
            registroJsonObject.Add("TransactionId", claimJWTModel.transactionId);
            registroJsonObject.Add("Rel", 1053);

            var query = await _pcsQueryService.UpdateEtapa(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete("Etapas/{IdEtapa}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> DeleteEtapa(int IdEtapa)
        {
            var query = await _pcsQueryService.DeleteEtapa(IdEtapa);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Etapas

        #region Empleados
        [HttpPost("Empleados")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AddEmpleado([FromBody] JsonObject registro)
        {
            var query = await _pcsQueryService.AddEmpleado(registro);
            return Ok(query);
        }

        [HttpGet("Empleados/{IdProyecto}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetEmpleados(int IdProyecto)
        {
            var query = await _pcsQueryService.GetEmpleados(IdProyecto);
            return Ok(query);
        }

        [HttpPut("Empleados")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateEmpleado([FromBody] JsonObject registro)
        {
            ClaimJWTModel claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", claimJWTModel.nombre);
            registroJsonObject.Add("Usuario", claimJWTModel.correo);
            registroJsonObject.Add("Roles", claimJWTModel.roles);
            registroJsonObject.Add("TransactionId", claimJWTModel.transactionId);
            registroJsonObject.Add("Rel", 1053);

            var query = await _pcsQueryService.UpdateEmpleado(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete("Empleados/{IdEmpleado}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> DeleteEmpleado(int IdEmpleado)
        {
            var query = await _pcsQueryService.DeleteEmpleado(IdEmpleado);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Empleados
    }
}
