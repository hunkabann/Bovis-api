using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Text.Json.Nodes;
using Bovis.API.Helper;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Bovis.Common.Model.NoTable;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Permissions;

namespace Bovis.API.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]//, RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class RolController : ControllerBase
    {
        #region base
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
        #endregion base
        
        [HttpGet]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetRoles()
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            var query = await _rolQueryService.GetRoles(email);
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }
    }
}
