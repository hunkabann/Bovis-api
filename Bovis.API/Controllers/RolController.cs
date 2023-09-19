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
    public class RolController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<RolController> _logger;
        private readonly IRolQueryService _rolQueryService;
        private readonly IMediator _mediator;

        public RolController(ILogger<RolController> logger, IRolQueryService _rolQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._rolQueryService = _rolQueryService;
            this._mediator = _mediator;
        }

        [HttpGet("{email}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetRoles(string email)
        {
            var usuario = HttpContext.User;
            var nombreUsuario = usuario.FindFirst(ClaimTypes.Name)?.Value;
            var rolUsuario = usuario.FindFirst(ClaimTypes.Role)?.Value;
            var transactionId = TransactionId;

            //email = "jmmorales@hunkabann.com.mx";

            var query = await _rolQueryService.GetRoles(email);
            return Ok(query);
        }
    }
}
