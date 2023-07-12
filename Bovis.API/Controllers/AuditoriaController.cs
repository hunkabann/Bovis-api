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
    public class AuditoriaController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<AuditoriaController> _logger;
        private readonly IAuditoriaQueryService _auditoriaQueryService;
        private readonly IMediator _mediator;

        public AuditoriaController(ILogger<AuditoriaController> logger, IAuditoriaQueryService _auditoriaQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._auditoriaQueryService = _auditoriaQueryService;
            this._mediator = _mediator;
        }

        #region Auditoria Legal
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        [HttpGet, Route("Cumplimiento/Documentos")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetDocumentosAuditoriaCumplimiento()
        {
            var query = await _auditoriaQueryService.GetDocumentosAuditoriaCumplimiento();
            return Ok(query);
        }

        [HttpPost("Cumplimiento/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AddDocumentosAuditoriaCumplimiento([FromBody] JsonObject registro)
        {
            var query = await _auditoriaQueryService.AddDocumentosAuditoriaCumplimiento(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut("Cumplimiento/Actualizar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateAuditoriaCumplimientoProyecto([FromBody] JsonObject registro)
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

            var query = await _auditoriaQueryService.UpdateAuditoriaCumplimientoProyecto(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        
        #endregion Auditoria de Calidad (Cumplimiento)

    }
}
