﻿using Bovis.API.Helper;
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

[Authorize]
[ApiController, Route("api/[controller]")]
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
    [HttpGet, Route("Habilidades/{idRequerimiento}")]
    public async Task<IActionResult> GetHabilidades(int idRequerimiento)
    {
        var query = await _requerimientoQueryService.GetHabilidades(idRequerimiento);
        return Ok(query);
    }
    #endregion Habilidades

    #region Experiencias
    [HttpGet, Route("Experiencias/{idRequerimiento}")]
    public async Task<IActionResult> GetExperiencias(int idRequerimiento)
    {
        var query = await _requerimientoQueryService.GetExperiencias(idRequerimiento);
        return Ok(query);
    }
    #endregion Experiencias

    #region Registros
    [HttpGet, Route("Requerimientos/{Asignados?}/{idDirector?}/{idProyecto?}/{idPuesto?}")]
    public async Task<IActionResult> GetRequerimientos(bool? Asignados, string? idDirector, int? idProyecto, int? idPuesto)
    {
        var query = await _requerimientoQueryService.GetRequerimientos(Asignados, idDirector, idProyecto, idPuesto);
        return Ok(query);
    }

    [HttpGet, Route("Registro/{idRequerimiento}")]
    public async Task<IActionResult> GetRequerimiento(int idRequerimiento)
    {
        var query = await _requerimientoQueryService.GetRequerimiento(idRequerimiento);
        return Ok(query);
    }

    [HttpPost, Route("Registro/Agregar")]
    public async Task<IActionResult> AddRegistro([FromBody] JsonObject registro)
    {
        var query = await _requerimientoQueryService.AddRegistro(registro);
        if (query.Message == string.Empty) return Ok(query);
        else return BadRequest(query.Message);
    }

    [HttpPut, Route("Registro/Actualizar")]
    public async Task<IActionResult> UpdateRegistro([FromBody] JsonObject registro)
    {
        IHeaderDictionary headers = HttpContext.Request.Headers;        
        string email = headers["email"];
        string nombre = headers["nombre"];
        JsonObject registroJsonObject = new JsonObject();
        registroJsonObject.Add("Registro", registro);
        registroJsonObject.Add("Nombre", nombre);
        registroJsonObject.Add("Usuario", email);
        registroJsonObject.Add("Roles", string.Empty);
        registroJsonObject.Add("TransactionId", TransactionId);
        registroJsonObject.Add("Rel", 1049);

        var query = await _requerimientoQueryService.UpdateRegistro(registroJsonObject);
        if (query.Message == string.Empty) return Ok(query);
        else return BadRequest(query.Message);
    }

    [HttpDelete, Route("Registro/Borrar/{idRequerimiento}")]
    public async Task<IActionResult> DeleteRequerimiento(int idRequerimiento)
    {
        var query = await _requerimientoQueryService.DeleteRequerimiento(idRequerimiento);
        if (query.Message == string.Empty) return Ok(query);
        else return BadRequest(query.Message);
    }
    #endregion Registros

    #region Director Ejecutivo
    [HttpGet, Route("DirectoresEjecutivos")]
    public async Task<IActionResult> GetDirectoresEjecutivos()
    {
        var query = await _requerimientoQueryService.GetDirectoresEjecutivos();
        return Ok(query);
    }
    #endregion Director Ejecutivo

    #region Proyectos
    [HttpGet, Route("Proyectos/DirectorEjecutivo/{IdDirectorEjecutivo}")]
    public async Task<IActionResult> GetProyectosByDirectorEjecutivo(string IdDirectorEjecutivo)
    {
        var query = await _requerimientoQueryService.GetProyectosByDirectorEjecutivo(IdDirectorEjecutivo);
        return Ok(query);
    }
    #endregion Proyectos
}

