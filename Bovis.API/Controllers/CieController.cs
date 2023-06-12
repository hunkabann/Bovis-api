using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Bovis.Common.Model.Tables;
using Bovis.API.Helper;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;
using Microsoft.Win32;

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

        #region Empresas
        [HttpGet, Route("Empresas/{Activo?}")]//, Authorize(Roles = "it.full, dev.full")
        public async Task<IActionResult> GetEmpresas(bool? Activo)
        {
            var query = await _cieQueryService.GetEmpresas(Activo);
            return Ok(query);
        }
        #endregion Empresas

        #region Registros        
        [HttpGet, Route("Registros/{Activo?}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetRegitros(bool? Activo)
        {
            var query = await _cieQueryService.GetRegistros(Activo);
            return Ok(query);
        }

        [HttpGet("Registro/{idRegistro}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetRegistro(int idRegistro)
        {
            var query = await _cieQueryService.GetRegistro(idRegistro);
            return Ok(query);
        }

        [HttpPost("Registros/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AddRegistros([FromBody] JsonObject registros)
        {
            var query = await _cieQueryService.AddRegistros(registros);
            return Ok(query);
        }

        [HttpPut("Registro/Actualizar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateRegistro([FromBody] JsonObject registro)
        {
            var query = await _cieQueryService.UpdateRegistro(registro);
            return Ok(query);
        }

        [HttpDelete, Route("Registro/Borrar/{idRegistro}")]//, Authorize(Roles = "it.full, dev.full")
        public async Task<IActionResult> DeleteRegistro(int idRegistro)
        {
            var query = await _cieQueryService.DeleteRegistro(idRegistro);
            if (query.Message == string.Empty) return Ok(query);
            return Ok(query);
        }
        #endregion Registros
    }
}
