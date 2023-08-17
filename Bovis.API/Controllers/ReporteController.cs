using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Text.Json.Nodes;
using Bovis.API.Helper;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Bovis.API.Controllers
{
    [ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class ReporteController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<ReporteController> _logger;
        private readonly IReporteQueryService _reporteQueryService;
        private readonly IMediator _mediator;

        public ReporteController(ILogger<ReporteController> logger, IReporteQueryService _reporteQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._reporteQueryService = _reporteQueryService;
            this._mediator = _mediator;
        }

        #region Custom Reports
        [HttpPost("Personalizado/Ejecutar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> ExecReportePersonalizado([FromBody] JsonObject registro)
        {
            var query = await _reporteQueryService.ExecReportePersonalizado(registro);
            //if (!query.Data.TrimStart().StartsWith("[ERROR]", StringComparison.OrdinalIgnoreCase)) return Ok(query);

            if (!(query.Data is string)) return Ok(query);
            else return BadRequest(query.Data);
        }

        [HttpPost("Personalizado/Guardar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AddReportePersonalizado([FromBody] JsonObject registro)
        {
            var query = await _reporteQueryService.AddReportePersonalizado(registro);
            return Ok(query);
        }

        [HttpGet, Route("Personalizado/{IdReporte}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetReportesPersonalizados(int IdReporte)
        {
            var query = await _reporteQueryService.GetReportesPersonalizados(IdReporte);
            return Ok(query);
        }

        [HttpPut("Personalizado/Actualizar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateReportePersonalizado([FromBody] JsonObject registro)
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

            var query = await _reporteQueryService.UpdateReportePersonalizado(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete, Route("Personalizado/Borrar/{IdReporte}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> DeleteReportePersonalizado(int IdReporte)
        {
            var query = await _reporteQueryService.DeleteReportePersonalizado(IdReporte);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Custom Reports
    }
}
