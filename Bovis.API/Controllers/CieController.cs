using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Bovis.API.Controllers
{
    [ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class CieController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<CieController> _logger;
        private readonly ICieQueryService _cieQueryService;
        private readonly IMediator _mediator;

        public CieController(ILogger<CieController> logger, ICieQueryService _cieQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._cieQueryService = _cieQueryService;
            this._mediator = _mediator;
        }


    }
}
