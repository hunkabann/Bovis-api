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
    [ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class PersonaController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<PersonaController> _logger;
        private readonly IPersonaQueryService _personaQueryService;
        private readonly IMediator _mediator;

        public PersonaController(ILogger<PersonaController> logger, IPersonaQueryService _personaQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._personaQueryService = _personaQueryService;
            this._mediator = _mediator;
        }

        #region Personas
        [HttpGet, Route("Personas/{Activo?}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetPersonas(bool? Activo)
        {
            var query = await _personaQueryService.GetPersonas(Activo);
            return Ok(query);
        }

        [HttpGet, Route("Personas/Libres")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetPersonasLibres()
        {
            var query = await _personaQueryService.GetPersonasLibres();
            return Ok(query);
        }

        [HttpGet, Route("Registro/{idPersona}")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> GetPersona(int idPersona)
        {
            var query = await _personaQueryService.GetPersona(idPersona);
            return Ok(query);
        }

        [HttpPost("Registro/Agregar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AddRegistro([FromBody] JsonObject registro)
        {
            var query = await _personaQueryService.AddRegistro(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut("Registro/Actualizar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateRegistro([FromBody] JsonObject registro)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string token = headers["token"];
            string email = headers["email"];
            string nombre = headers["nombre"];
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", nombre);
            registroJsonObject.Add("Usuario", email);
            registroJsonObject.Add("Roles", string.Empty);
            registroJsonObject.Add("TransactionId", TransactionId);
            registroJsonObject.Add("Rel", 44);

            var query = await _personaQueryService.UpdateRegistro(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut("Estatus/Actualizar")]//, Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> UpdateEstatus([FromBody] JsonObject registro)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string token = headers["token"];
            string email = headers["email"];
            string nombre = headers["nombre"];
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", nombre);
            registroJsonObject.Add("Usuario", email);
            registroJsonObject.Add("Roles", string.Empty);
            registroJsonObject.Add("TransactionId", TransactionId);
            registroJsonObject.Add("Rel", 1048);

            var query = await _personaQueryService.UpdateEstatus(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Personas

    }
}