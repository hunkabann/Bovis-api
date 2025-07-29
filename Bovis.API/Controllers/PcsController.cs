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





        #region Clientes
        [HttpGet, Route("Clientes")]
        public async Task<IActionResult> ObtenerClientes()
        {
            var business = await _pcsQueryService.GetClientes();
            return Ok(business);
        }
        #endregion Clientes





        #region Empresas
        [HttpGet, Route("Empresas")]
        public async Task<IActionResult> ObtenerEmpresas()
        {
            var business = await _pcsQueryService.GetEmpresas();
            return Ok(business);
        }
        #endregion Empresas




        #region Proyectos
        [HttpGet, Route("Proyectos/{OrdenAlfabetico?}")]
        public async Task<IActionResult> ObtenerProyectos(bool? OrdenAlfabetico)
        {
            var business = await _pcsQueryService.GetProyectos(OrdenAlfabetico);
            return Ok(business);
        }

        //atc 09-11-2024
        [HttpGet, Route("ProyectosNoClose/{OrdenAlfabetico?}")]
        public async Task<IActionResult> ObtenerProyectosNoClose(bool? OrdenAlfabetico)
        {
            var business = await _pcsQueryService.GetProyectosNoClose(OrdenAlfabetico);
            return Ok(business);
        }

        [HttpGet, Route("Proyecto/{numProyecto}")]
        public async Task<IActionResult> ObtenerProyecto(int numProyecto)
        {
            var business = await _pcsQueryService.GetProyecto(numProyecto);
            return Ok(business);
        }

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

        [HttpPut, Route("Proyectos/FechaAuditoria")]
        public async Task<IActionResult> UpdateProyectoFechaAuditoria([FromBody] JsonObject registro)
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

            var query = await _pcsQueryService.UpdateProyectoFechaAuditoria(registroJsonObject);
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
        public async Task<IActionResult> DeleteEmpleado(int IdFase, string NumEmpleado)
        {
            var query = await _pcsQueryService.DeleteEmpleado(IdFase, NumEmpleado);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Empleados






        #region Gastos / Ingresos
        [HttpGet, Route("GastosIngresos/Secciones/{IdProyecto}/{Tipo}")]
        public async Task<IActionResult> GetGastosIngresosSecciones(int IdProyecto, string Tipo)
        {
            var query = await _pcsQueryService.GetGastosIngresosSecciones(IdProyecto, Tipo);
            return Ok(query);
        }

        [HttpGet, Route("GastosIngresos/{IdProyecto}/{Tipo}/{Seccion}")]
        public async Task<IActionResult> GetGastosIngresos(int IdProyecto, string Tipo, string Seccion)
        {
            var query = await _pcsQueryService.GetGastosIngresos(IdProyecto, Tipo, Seccion);
            return Ok(query);
        }

        [HttpGet, Route("TotalesIngresos/{IdProyecto}")]
        public async Task<IActionResult> GetTotalesIngresos(int IdProyecto)
        {
            var query = await _pcsQueryService.GetTotalesIngresos(IdProyecto);
            return Ok(query);
        }

        [HttpPut, Route("GastosIngresos")]
        public async Task<IActionResult> UpdateGastosIngresos([FromBody] JsonObject registro)
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

            var query = await _pcsQueryService.UpdateGastosIngresos(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpGet, Route("GastosIngresos/{IdProyecto}/TotalFacturacion")]
        public async Task<IActionResult> GetTotalFacturacion(int IdProyecto)
        {
            var query = await _pcsQueryService.GetTotalFacturacion(IdProyecto);
            return Ok(query);
        }
        #endregion Gastos / Ingresos




        #region Control
        [HttpGet, Route("GastosIngresos/{IdProyecto}/Control")]
        public async Task<IActionResult> GetControl(int IdProyecto)
        {
            var query = await _pcsQueryService.GetControl(IdProyecto);
            return Ok(query);
        }

        [HttpGet, Route("Control/{IdProyecto}/{Seccion}")]
        public async Task<IActionResult> GetSeccionControl(int IdProyecto, string Seccion)
        {
            var query = await _pcsQueryService.GetSeccionControl(IdProyecto, Seccion);
            return Ok(query);
        }
        #endregion Control
    }
}
