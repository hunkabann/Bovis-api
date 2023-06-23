using Bovis.API.Helper;
using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;

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

    #region Habilidades
    [HttpGet, Route("Habilidades/{idRequerimiento}")]//, Authorize(Roles = "it.full, dev.full")
    public async Task<IActionResult> GetHabilidades(int idRequerimiento)
    {
        var query = await _requerimientoQueryService.GetHabilidades(idRequerimiento);
        return Ok(query);
    }
    #endregion Habilidades

    #region Experiencias
    [HttpGet, Route("Experiencias/{idRequerimiento}")]//, Authorize(Roles = "it.full, dev.full")
    public async Task<IActionResult> GetExperiencias(int idRequerimiento)
    {
        var query = await _requerimientoQueryService.GetExperiencias(idRequerimiento);
        return Ok(query);
    }
    #endregion Experiencias

    #region Registros
    [HttpGet, Route("Requerimientos/{Activo?}")]//, Authorize(Roles = "it.full, dev.full")
    public async Task<IActionResult> GetRequerimientos(bool? Activo)
    {
        var query = await _requerimientoQueryService.GetRequerimientos(Activo);
        return Ok(query);
    }

    [HttpGet, Route("Registro/{idRequerimiento}")]//, Authorize(Roles = "it.full, dev.full")
    public async Task<IActionResult> GetRequerimiento(int idRequerimiento)
    {
        var query = await _requerimientoQueryService.GetRequerimiento(idRequerimiento);
        return Ok(query);
    }

    [HttpPost("Registro/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
    public async Task<IActionResult> AddRegistro([FromBody] JsonObject registro)
    {
        var query = await _requerimientoQueryService.AddRegistro(registro);
        return Ok(query);
    }

    [HttpPut("Registro/Actualizar")]//, Authorize(Roles = "it.full, dev.full")]
    public async Task<IActionResult> UpdateRegistro([FromBody] JsonObject registro)
    {
        var query = await _requerimientoQueryService.UpdateRegistro(registro);
        return Ok(query);
    }

    [HttpDelete, Route("Registro/Borrar/{idRequerimiento}")]//, Authorize(Roles = "it.full, dev.full")
    public async Task<IActionResult> DeleteRequerimiento(int idRequerimiento)
    {
        var query = await _requerimientoQueryService.DeleteRequerimiento(idRequerimiento);
        if (query.Message == string.Empty) return Ok(query);
        return Ok(query);
    }
    #endregion Registros
}

