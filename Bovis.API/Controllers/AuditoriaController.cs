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

        #region Auditoria Legal
        [HttpGet, Route("Contractual")]
        public async Task<IActionResult> GetAuditoriasContractual()
        {
            var query = await _auditoriaQueryService.GetAuditoriasContractual();
            return Ok(query);
        }

        [HttpPost, Route("Contractual/Agregar")]
        public async Task<IActionResult> AddAuditoriasContractual([FromBody] JsonObject registro)
        {
            var query = await _auditoriaQueryService.AddAuditoriasContractual(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        [HttpGet, Route("Cumplimiento")]
        public async Task<IActionResult> GetAuditoriasCumplimiento()
        {
            var query = await _auditoriaQueryService.GetAuditoriasCumplimiento();
            return Ok(query);
        }

        [HttpGet, Route("Cumplimiento/Proyecto/{IdProyecto}")]
        public async Task<IActionResult> GetAuditoriasCumplimientoByProyecto(int IdProyecto)
        {
            var query = await _auditoriaQueryService.GetAuditoriasCumplimientoByProyecto(IdProyecto);
            return Ok(query);
        }

        [HttpPost, Route("Cumplimiento/Agregar")]
        public async Task<IActionResult> AddAuditoriasCumplimiento([FromBody] JsonObject registro)
        {
            var query = await _auditoriaQueryService.AddAuditoriasCumplimiento(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut, Route("Cumplimiento/Actualizar")]
        public async Task<IActionResult> UpdateAuditoriaCumplimientoProyecto([FromBody] JsonObject registro)
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

            var query = await _auditoriaQueryService.UpdateAuditoriaCumplimientoProyecto(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPost, Route("Cumplimiento/Documento")]
        public async Task<IActionResult> AddAuditoriaCumplimientoDocumento([FromBody] JsonObject registro)
        {
            var query = await _auditoriaQueryService.AddAuditoriaCumplimientoDocumento(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpGet, Route("Cumplimiento/Documentos/{IdAuditoriaCumplimiento}/{offset}/{limit}")]
        public async Task<IActionResult> GetDocumentosAuditoriaCumplimiento(int IdAuditoriaCumplimiento, int offset, int limit)
        {
            var query = await _auditoriaQueryService.GetDocumentosAuditoriaCumplimiento(IdAuditoriaCumplimiento, offset, limit);
            return Ok(query);
        }

        [HttpGet, Route("Cumplimiento/Documento/{IdDocumento}")]
        public async Task<IActionResult> GetDocumentoAuditoriaCumplimiento(int IdDocumento)
        {
            var query = await _auditoriaQueryService.GetDocumentoAuditoriaCumplimiento(IdDocumento);
            return Ok(query);
        }

        [HttpPut, Route("Cumplimiento/Documento/Validacion")]
        public async Task<IActionResult> AddAuditoriaCumplimientoDocumentoValidacion([FromBody] JsonObject registro)
        {
            var query = await _auditoriaQueryService.AddAuditoriaCumplimientoDocumentoValidacion(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Auditoria de Calidad (Cumplimiento)

    }
}
