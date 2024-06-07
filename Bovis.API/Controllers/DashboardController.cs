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
    [Authorize]
    [ApiController, Route("api/[controller]")]

    public class DashboardController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboardQueryService _dashboardQueryService;
        private readonly IMediator _mediator;

        public DashboardController(ILogger<DashboardController> logger, IDashboardQueryService _dashboardQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._dashboardQueryService = _dashboardQueryService;
            this._mediator = _mediator;
        }

        #region Proyectos Documentos
        [HttpGet, Route("Proyectos/Documentos")]
        public async Task<IActionResult> GetProyectosDocumentos()
        {
            var business = await _dashboardQueryService.GetProyectosDocumentos();
            return Ok(business);
        }
        #endregion Proyectos Documentos
    }
}
