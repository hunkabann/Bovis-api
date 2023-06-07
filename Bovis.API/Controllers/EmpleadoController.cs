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

namespace Bovis.API.Controllers
{
    [ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class EmpleadoController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<EmpleadoController> _logger;
        private readonly IEmpleadoQueryService _empleadoQueryService;
        private readonly IMediator _mediator;

        public EmpleadoController(ILogger<EmpleadoController> logger, IEmpleadoQueryService _empleadoQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._empleadoQueryService = _empleadoQueryService;
            this._mediator = _mediator;
        }

        #region Empleados
        [HttpGet, Route("Empleados/{Activo?}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetEmpleados(bool? Activo)
        {
            var query = await _empleadoQueryService.GetEmpleados(Activo);
            return Ok(query);
        }

        [HttpGet, Route("Registro/{idEmpleado}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetEmpleado(int idEmpleado)
        {
            var query = await _empleadoQueryService.GetEmpleado(idEmpleado);
            return Ok(query);
        }

        [HttpPut("Registro/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AgregarRegistro(AddEmpleadoCommand registro)
        {
            var response = await _mediator.Send(registro);
            if (!response.Success)
            {
                var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
                _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
            }
            return Ok(response);
        }
        #endregion Empleados

        #region Proyectos
        [HttpGet, Route("Proyectos/{idEmpleado}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetProyectos(int idEmpleado)
        {
            var query = await _empleadoQueryService.GetProyectos(idEmpleado);
            return Ok(query);
        }
        #endregion Proyectos
    }
}