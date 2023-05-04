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


    }
}
