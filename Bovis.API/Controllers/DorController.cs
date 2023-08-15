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
    [ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
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

        // Agregado por sebastian.flores
        [HttpPost("DatosPrueba")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ObtenerDatosPrueba(DorEmpCorreoRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _dorQueryService.GetDorEjecutivoCorreo(request.email);
            return Ok(business);
        }

        [HttpPost("DatosEjecutivo")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ObtenerDatosEjecutivo(DorEmpCorreoRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _dorQueryService.GetDorEjecutivoCorreo(request.email);
            return Ok(business);
        }

        [HttpPost("DatosEmpleado")]//, Authorize(Roles = "eje.full, dev.full")]        
        public async Task<IActionResult> ObtenerDatosEmpleado(DorEmpCorreoRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _dorQueryService.GetDorEmpleadoCorreo(request.email);
            return Ok(business);
        }

        [HttpPost("ListaSubordinados")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ObtenerListaSubordinados(DorEmpNombreRequest request)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _dorQueryService.GetDorListaSubordinados(request.nombre);
            return Ok(business);
        }

        [HttpGet("ConsultarObjetivosGenerales/{nivel}/{unidadNegocio}/{mes}/{anio}/{seccion}")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ObtenerDorObjectivosGenerales(int nivel, string unidadNegocio, int mes, int anio, string seccion)
        {
            var business = await _dorQueryService.GetDorObjetivosGenerales(nivel, unidadNegocio, mes, anio, seccion);
            return Ok(business);
        }

        [HttpGet("ConsultarGPM/{proyecto}")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ObtenerGpmProyecto(int proyecto)
        {
            var business = await _dorQueryService.GetDorGpmProyecto(proyecto);
            return Ok(business);
        }

        [HttpGet("ConsultarMetas/{proyecto}/{nivel}/{mes}/{anio}/{empleado}/{seccion}")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ObtenerMetasProyecto(int proyecto, int nivel, int mes, int anio, int empleado, string seccion)
        {
            var business = await _dorQueryService.GetDorMetasProyecto(proyecto, nivel, mes, anio, empleado, seccion);
            return Ok(business);
        }      


        [HttpGet("ConsultarObjetivosProyecto/{anio}/{proyecto}/{empleado}/{nivel}/{acepto}/{mes}")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ObtenerDorObjectivosAnio(int anio, int proyecto, int empleado,int nivel, int? acepto, int mes)
        {
            var business = await _dorQueryService.GetDorObjetivoDesepeno(anio, proyecto, empleado, nivel, acepto, mes);
            return Ok(business);
        }

        [HttpPut("ActualizarObjetivos")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ActualizarObjectivosDorAnio(UpdDorObjetivoCommand objetivo)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _mediator.Send(objetivo);
            return Ok(business);
        }

        [HttpPut("UpdateReal")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> UpdateReal([FromBody] JsonObject registro)
        {
            ClaimJWTModel claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", claimJWTModel.nombre);
            registroJsonObject.Add("Usuario", claimJWTModel.correo);
            registroJsonObject.Add("Roles", claimJWTModel.roles);
            registroJsonObject.Add("TransactionId", claimJWTModel.transactionId);
            registroJsonObject.Add("Rel", 45);

            var query = await _dorQueryService.UpdateReal(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut("UpdateObjetivoPersonal")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> UpdateObjetivoPersonal([FromBody] JsonObject registro)
        {
            ClaimJWTModel claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", claimJWTModel.nombre);
            registroJsonObject.Add("Usuario", claimJWTModel.correo);
            registroJsonObject.Add("Roles", claimJWTModel.roles);
            registroJsonObject.Add("TransactionId", claimJWTModel.transactionId);
            registroJsonObject.Add("Rel", 45);

            var query = await _dorQueryService.UpdateObjetivoPersonal(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
    }
}
