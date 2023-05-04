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


    }
}
