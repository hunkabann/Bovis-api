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
    public class PcsController : ControllerBase
    {
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

    }
}
