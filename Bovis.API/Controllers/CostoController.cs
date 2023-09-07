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
    public class CostoController : ControllerBase
    {
        #region base
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<CostoController> _logger;
        private readonly ICostoQueryService _costoQueryService;
        private readonly IMediator _mediator;

        public CostoController(ILogger<CostoController> logger, ICostoQueryService _costoQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._costoQueryService = _costoQueryService;
            this._mediator = _mediator;
        }
        #endregion base

        [HttpPost]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AddCosto([FromBody] JsonObject registro)
        {
            var query = await _costoQueryService.AddCosto(registro);
            return Ok(query);
        }

        [HttpGet, Route("{IdCosto}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetCostos(int IdCosto)
        {
            var query = await _costoQueryService.GetCostos(IdCosto);
            return Ok(query);
        }
    }
}
