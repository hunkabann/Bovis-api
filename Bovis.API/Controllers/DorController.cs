using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

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

        [HttpGet("ConsultarObjetivosGenerales/{nivel}/{unidadNegocio}")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ObtenerDorObjectivosGenerales(string nivel, string unidadNegocio)
        {
            var business = await _dorQueryService.GetDorObjetivosGenerales(nivel, unidadNegocio);
            return Ok(business);
        }

        [HttpGet("ConsultarGPM/{proyecto}")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ObtenerGpmProyecto(int proyecto)
        {
            var business = await _dorQueryService.GetDorGpmProyecto(proyecto);
            return Ok(business);
        }

        [HttpGet("ConsultarMetas/{proyecto}/{nivel}")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ObtenerMetasProyecto(int proyecto,int nivel)
        {
            var business = await _dorQueryService.GetDorMetasProyecto(proyecto,nivel);
            return Ok(business);
        }      


        [HttpGet("ConsultarObjetivosProyecto/{anio}/{proyecto}/{empleado}/{nivel}/{acepto?}")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ObtenerDorObjectivosAnio(int anio, int proyecto, int empleado,int nivel, int? acepto)
        {
            var business = await _dorQueryService.GetDorObjetivoDesepeno(anio, proyecto, empleado, nivel, acepto);
            return Ok(business);
        }

        [HttpPut("ActualizarObjetivos")]//, Authorize(Roles = "eje.full, dev.full")]
        public async Task<IActionResult> ActualizarObjectivosDorAnio(UpdDorObjetivoCommand objetivo)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _mediator.Send(objetivo);
            return Ok(business);
        }
    }
}
