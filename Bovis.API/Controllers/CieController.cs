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
        [HttpGet, Route("Cies/{Activo?}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetRegitros(byte? Estatus)
        {
            var query = await _cieQueryService.GetRegistros(Estatus);
            return Ok(query);
        }
        [HttpGet("Registro/{idRegistro}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> ObtenerInfoRegistro(int idRegistro)
        {
            var business = await _cieQueryService.GetInfoRegistro(idRegistro);
            return Ok(business);
        }
        [HttpPut("Registro/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AgregarRegistro(AddCieCommand registro)
        {
            var response = await _mediator.Send(registro);
            if (!response.Success)
            {
                var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
                _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
            }
            return Ok(response);
        }
        [HttpPut("Registros/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AgregarRegistros([FromBody] List<TB_Cie> registros)
        {
            var business = await _cieQueryService.AddRegistros(registros);
            return Ok(business);
        }
        #endregion Registros
    }
}
