using Bovis.API.Helper;
using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Bovis.API.Controllers;

[ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class RequerimientoController : ControllerBase
{
    private string TransactionId { get { return HttpContext.TraceIdentifier; } }
    private readonly ILogger<RequerimientoController> _logger;
    private readonly IRequerimientoQueryService _requerimientoQueryService;
    private readonly IMediator _mediator;

    public RequerimientoController(ILogger<RequerimientoController> logger, IRequerimientoQueryService _requerimientoQueryService, IMediator _mediator)
    {
        _logger = logger;
        this._requerimientoQueryService = _requerimientoQueryService;
        this._mediator = _mediator;        
    }

    #region Registros
    [HttpGet, Route("Requerimientos/{Activo?}")]//, Authorize(Roles = "it.full, dev.full")
    public async Task<IActionResult> GetRequerimientos(bool? Activo)
    {
        var query = await _requerimientoQueryService.GetRequerimientos(Activo);
        return Ok(query);
    }
    [HttpPut("Registro/Agregar"), Authorize(Roles = "it.full, dev.full")]
    public async Task<IActionResult> AgregarRegistro(AddRequerimientoCommand registro)
    {
        var response = await _mediator.Send(registro);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }
    #endregion Registros
}

