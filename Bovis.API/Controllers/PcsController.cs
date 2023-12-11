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
    // Hola mundo

    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class PcsController : ControllerBase
    {
        #region base
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<PcsController> _logger;
        private readonly IPcsQueryService _pcsQueryService;
        private readonly IMediator _mediator;

        public PcsController(ILogger<PcsController> logger, IPcsQueryService _pcsQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._pcsQueryService = _pcsQueryService;
            this._mediator = _mediator;
        }
        #endregion base

        [HttpGet, Route("Proyectos/{OrdenAlfabetico?}")]
        public async Task<IActionResult> ObtenerProyectos(bool? OrdenAlfabetico)
        {
            var business = await _pcsQueryService.GetProyectos(OrdenAlfabetico);
            return Ok(business);
        }

        [HttpGet, Route("Proyecto/{numProyecto}")]
        public async Task<IActionResult> ObtenerProyecto(int numProyecto)
        {
            var business = await _pcsQueryService.GetProyecto(numProyecto);
            return Ok(business);
        }

        [HttpGet, Route("Clientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            var business = await _pcsQueryService.GetClientes();
            return Ok(business);
        }

        [HttpGet, Route("Empresas")]
        public async Task<IActionResult> ObtenerEmpresas()
        {
            var business = await _pcsQueryService.GetEmpresas();
            return Ok(business);
        }



        #region Proyectos
        [HttpPost, Route("Proyectos")]
        public async Task<IActionResult> AddProyecto([FromBody] JsonObject registro)
        {
            var query = await _pcsQueryService.AddProyecto(registro);
            return Ok(query);
        }

        [HttpGet, Route("Proyectos/Info/{IdProyecto}")]
        public async Task<IActionResult> GetProyectos(int IdProyecto)
        {
            var query = await _pcsQueryService.GetProyectos(IdProyecto);
            return Ok(query);
        }

        [HttpGet, Route("Proyectos/Tipo")]
        public async Task<IActionResult> GetTipoProyectos()
        {
            var query = await _pcsQueryService.GetTipoProyectos();
            return Ok(query);
        }

        [HttpPut, Route("Proyectos")]
        public async Task<IActionResult> UpdateProyecto([FromBody] JsonObject registro)
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
            registroJsonObject.Add("Rel", 1053);

            var query = await _pcsQueryService.UpdateProyecto(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete, Route("Proyectos/{IdProyecto}")]
        public async Task<IActionResult> DeleteProyecto(int IdProyecto)
        {
            var query = await _pcsQueryService.DeleteProyecto(IdProyecto);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Proyectos

        #region Etapas
        [HttpPost, Route("Etapas")]
        public async Task<IActionResult> AddEtapa([FromBody] JsonObject registro)
        {
            var query = await _pcsQueryService.AddEtapa(registro);
            return Ok(query);
        }

        [HttpGet, Route("Etapas/{IdProyecto}")]
        public async Task<IActionResult> GetEtapas(int IdProyecto)
        {
            var query = await _pcsQueryService.GetEtapas(IdProyecto);
            return Ok(query);
        }

        [HttpPut, Route("Etapas")]
        public async Task<IActionResult> UpdateEtapa([FromBody] JsonObject registro)
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
            registroJsonObject.Add("Rel", 1053);

            var query = await _pcsQueryService.UpdateEtapa(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete, Route("Etapas/{IdEtapa}")]
        public async Task<IActionResult> DeleteEtapa(int IdEtapa)
        {
            var query = await _pcsQueryService.DeleteEtapa(IdEtapa);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Etapas

        #region Empleados
        [HttpPost, Route("Empleados/Fase")]
        public async Task<IActionResult> AddEmpleado([FromBody] JsonObject registro)
        {
            var query = await _pcsQueryService.AddEmpleado(registro);
            return Ok(query);
        }

        [HttpGet, Route("Empleados/Fase/{IdFase}")]
        public async Task<IActionResult> GetEmpleados(int IdFase)
        {
            var query = await _pcsQueryService.GetEmpleados(IdFase);
            return Ok(query);
        }

        [HttpPut, Route("Empleados/Fase")]
        public async Task<IActionResult> UpdateEmpleado([FromBody] JsonObject registro)
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
            registroJsonObject.Add("Rel", 1053);

            var query = await _pcsQueryService.UpdateEmpleado(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete, Route("Empleados/{NumEmpleado}/Fase/{IdFase}")]
        public async Task<IActionResult> DeleteEmpleado(int IdFase, int NumEmpleado)
        {
            var query = await _pcsQueryService.DeleteEmpleado(IdFase, NumEmpleado);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Empleados
    }
}
