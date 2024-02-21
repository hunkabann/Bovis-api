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
    public class AuditoriaController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<AuditoriaController> _logger;
        private readonly IAuditoriaQueryService _auditoriaQueryService;
        private readonly IMediator _mediator;

        public AuditoriaController(ILogger<AuditoriaController> logger, IAuditoriaQueryService _auditoriaQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._auditoriaQueryService = _auditoriaQueryService;
            this._mediator = _mediator;
        }

        [HttpGet, Route("Proyectos/{TipoAuditoria}")]
        public async Task<IActionResult> GetProyectos(string TipoAuditoria)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string email = headers["email"];
            string nombre = headers["nombre"];

            var business = await _auditoriaQueryService.GetProyectos(email, TipoAuditoria);
            return Ok(business);
        }


        [HttpGet, Route("{TipoAuditoria}")]
        public async Task<IActionResult> GetAuditorias(string TipoAuditoria)
        {
            var query = await _auditoriaQueryService.GetAuditorias(TipoAuditoria);
            return Ok(query);
        }

        [HttpGet, Route("ByProyecto/{IdProyecto}/{TipoAuditoria}")]
        public async Task<IActionResult> GetAuditoriasByProyecto(int IdProyecto, string TipoAuditoria)
        {            
            var query = await _auditoriaQueryService.GetAuditoriasByProyecto(IdProyecto, TipoAuditoria);
            return Ok(query);
        }

        [HttpGet, Route("TipoComentarios")]
        public async Task<IActionResult> GetTipoComentarios()
        {
            var query = await _auditoriaQueryService.GetTipoComentarios();
            return Ok(query);
        }
        
        [HttpGet, Route("Comentarios/{NumProyecto}")]
        public async Task<IActionResult> GetComentarios(int numProyecto)
        {
            var query = await _auditoriaQueryService.GetComentarios(numProyecto);
            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> AddAuditorias([FromBody] JsonObject registro)
        {
            var query = await _auditoriaQueryService.AddAuditorias(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPost, Route("Comentarios")]
        public async Task<IActionResult> AddComentarios([FromBody] JsonObject registro)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string email = headers["email"];
            string nombre = headers["nombre"];
            var query = await _auditoriaQueryService.AddComentarios(registro, nombre);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuditoriaProyecto([FromBody] JsonObject registro)
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
            registroJsonObject.Add("Rel", 1050);

            var query = await _auditoriaQueryService.UpdateAuditoriaProyecto(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPost, Route("Documento")]
        public async Task<IActionResult> AddAuditoriaDocumento([FromBody] JsonObject registro)
        {
            var query = await _auditoriaQueryService.AddAuditoriaDocumento(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpGet, Route("Documentos/{IdAuditoria}/{offset}/{limit}")]
        public async Task<IActionResult> GetDocumentosAuditoria(int IdAuditoria, int offset, int limit)
        {
            var query = await _auditoriaQueryService.GetDocumentosAuditoria(IdAuditoria, offset, limit);
            return Ok(query);
        }

        [HttpGet, Route("Documento/{IdDocumento}")]
        public async Task<IActionResult> GetDocumentoAuditoria(int IdDocumento)
        {
            var query = await _auditoriaQueryService.GetDocumentoAuditoria(IdDocumento);
            return Ok(query);
        }

        [HttpPut, Route("Documento/Validacion")]
        public async Task<IActionResult> AddAuditoriaDocumentoValidacion([FromBody] JsonObject registro)
        {
            var query = await _auditoriaQueryService.AddAuditoriaDocumentoValidacion(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

    }
}
