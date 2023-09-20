using Bovis.API.Helper;
using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace Bovis.API.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class DorController : ControllerBase
    {
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<DorController> _logger;
        private readonly IDorQueryService _dorQueryService;
        private readonly IMediator _mediator;

        public DorController(ILogger<DorController> logger, IDorQueryService _dorQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._dorQueryService = _dorQueryService;
            this._mediator = _mediator;
        }

        [HttpPost, Route("DatosEjecutivo")]
        public async Task<IActionResult> ObtenerDatosEjecutivo(DorEmpCorreoRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _dorQueryService.GetDorEjecutivoCorreo(request.email);
            return Ok(business);
        }

        [HttpPost, Route("DatosEmpleado")]       
        public async Task<IActionResult> ObtenerDatosEmpleado(DorEmpCorreoRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _dorQueryService.GetDorEmpleadoCorreo(request.email);
            return Ok(business);
        }

        [HttpPost, Route("ListaSubordinados")]
        public async Task<IActionResult> ObtenerListaSubordinados(DorEmpNombreRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _dorQueryService.GetDorListaSubordinados(request.nombre);
            return Ok(business);
        }

        [HttpGet, Route("ConsultarObjetivosGenerales/{nivel}/{unidadNegocio}/{mes}/{anio}/{seccion}")]
        public async Task<IActionResult> ObtenerDorObjectivosGenerales(int nivel, string unidadNegocio, int mes, int anio, string seccion)
        {
            var business = await _dorQueryService.GetDorObjetivosGenerales(nivel, unidadNegocio, mes, anio, seccion);
            return Ok(business);
        }

        [HttpGet, Route("ConsultarGPM/{proyecto}")]
        public async Task<IActionResult> ObtenerGpmProyecto(int proyecto)
        {
            var business = await _dorQueryService.GetDorGpmProyecto(proyecto);
            return Ok(business);
        }

        [HttpGet, Route("ConsultarMetas/{proyecto}/{nivel}/{mes}/{anio}/{empleado}/{seccion}")]
        public async Task<IActionResult> ObtenerMetasProyecto(int proyecto, int nivel, int mes, int anio, int empleado, string seccion)
        {
            var business = await _dorQueryService.GetDorMetasProyecto(proyecto, nivel, mes, anio, empleado, seccion);
            return Ok(business);
        }      


        [HttpGet, Route("ConsultarObjetivosProyecto/{anio}/{proyecto}/{empleado}/{nivel}/{acepto}/{mes}")]
        public async Task<IActionResult> ObtenerDorObjectivosAnio(int anio, int proyecto, int empleado,int nivel, int? acepto, int mes)
        {
            var business = await _dorQueryService.GetDorObjetivoDesepeno(anio, proyecto, empleado, nivel, acepto, mes);
            return Ok(business);
        }

        [HttpPut, Route("ActualizarObjetivos")]
        public async Task<IActionResult> ActualizarObjectivosDorAnio(UpdDorObjetivoCommand objetivo)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _mediator.Send(objetivo);
            return Ok(business);
        }

        [HttpPut, Route("UpdateReal")]
        public async Task<IActionResult> UpdateReal([FromBody] JsonObject registro)
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
            registroJsonObject.Add("Rel", 1052);

            var query = await _dorQueryService.UpdateReal(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut, Route("UpdateObjetivoPersonal")]
        public async Task<IActionResult> UpdateObjetivoPersonal([FromBody] JsonObject registro)
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
            registroJsonObject.Add("Rel", 1054);

            var query = await _dorQueryService.UpdateObjetivoPersonal(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut, Route("UpdateAcepto")]
        public async Task<IActionResult> UpdateAcepto([FromBody] JsonObject registro)
        {
            var usuario = HttpContext.User;
            var nombreUsuario = usuario.FindFirst(ClaimTypes.Name)?.Value;
            var rolUsuario = usuario.FindFirst(ClaimTypes.Role)?.Value;
            var transactionId = TransactionId;

            IHeaderDictionary headers = HttpContext.Request.Headers;
            string email = headers["email"];
            string nombre = headers["nombre"];
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", nombre);
            registroJsonObject.Add("Usuario", email);
            registroJsonObject.Add("Roles", string.Empty);
            registroJsonObject.Add("TransactionId", TransactionId);
            registroJsonObject.Add("Rel", 1055);

            var query = await _dorQueryService.UpdateAcepto(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
    }
}
