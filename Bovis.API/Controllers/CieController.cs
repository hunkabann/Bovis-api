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
using System.Text.Json.Nodes;
using Microsoft.Win32;
using System.Text;

namespace Bovis.API.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
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
        [HttpGet, Route("Empresas/{Activo?}")]
        public async Task<IActionResult> GetEmpresas(bool? Activo)
        {
            var query = await _cieQueryService.GetEmpresas(Activo);
            return Ok(query);
        }
        #endregion Empresas


        #region Cuenta Data
        [HttpPost("Cuentas")]
        public async Task<IActionResult> GetCuentaData([FromBody] JsonObject cuentas)
        {
            var query = await _cieQueryService.GetCuentaData(cuentas);
            return Ok(query);
        }

        [HttpPost("Cuentas/Agregar")]
        public async Task<IActionResult> AddCuentas([FromBody] JsonObject registros)
        {
            var query = await _cieQueryService.AddCuentas(registros);
            return Ok(query);
        }
        #endregion Cuenta Data


        #region Proyecto
        [HttpPost("Proyectos")]
        public async Task<IActionResult> GetProyectoData([FromBody] JsonObject proyectos)
        {
            var query = await _cieQueryService.GetProyectoData(proyectos);
            return Ok(query);
        }
        #endregion Proyecto

        #region Cat�logos
        [HttpGet, Route("NombresCuenta")]
        public async Task<IActionResult> GetNombresCuenta()
        {
            var query = await _cieQueryService.GetNombresCuenta();
            return Ok(query);
        }

        [HttpGet, Route("Conceptos")]
        public async Task<IActionResult> GetConceptos()
        {
            var query = await _cieQueryService.GetConceptos();
            return Ok(query);
        }

        [HttpGet, Route("NumsProyecto")]
        public async Task<IActionResult> GetNumsProyecto()
        {
            var query = await _cieQueryService.GetNumsProyecto();
            return Ok(query);
        }

        [HttpGet, Route("Responsables")]
        public async Task<IActionResult> GetResponsables()
        {
            var query = await _cieQueryService.GetResponsables();
            return Ok(query);
        }

        [HttpGet, Route("ClasificacionesPY")]
        public async Task<IActionResult> GetClasificacionesPY()
        {
            var query = await _cieQueryService.GetClasificacionesPY();
            return Ok(query);
        }

        [HttpGet, Route("TiposPY")]
        public async Task<IActionResult> GetTiposPY()
        {
            var query = await _cieQueryService.GetTiposPY();
            return Ok(query);
        }
        #endregion Cat�logos


        #region Registros        
        [HttpPost, Route("Registros")]
        public async Task<IActionResult> GetRegitros([FromBody] JsonObject registro)
        {
            var query = await _cieQueryService.GetRegistros(registro);
            return Ok(query);
        }

        [HttpGet("Registro/{idRegistro}")]
        public async Task<IActionResult> GetRegistro(int idRegistro)
        {
            var query = await _cieQueryService.GetRegistro(idRegistro);
            return Ok(query);
        }

        [HttpPost("Registros/Agregar")]
        public async Task<IActionResult> AddRegistros([FromBody] JsonObject registros)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string email = headers["email"];
            string nombre = headers["nombre"];

            var query = await _cieQueryService.AddRegistros(registros);

            if (query.Message == string.Empty)
            {
                ////Se hace env�o de una notificaci�n por Email, indicando que ya se realiz� la carga completa.
                //using (HttpClient client = new HttpClient())
                //{
                //    try
                //    {
                //        var baseUrl = $"{Request.Scheme}://{Request.Host}/api/Email";

                //        var postData = new
                //        {
                //            subject = "Registros de CIE cargados",
                //            body = $"Hola {nombre}.<p>Se han cargado satisfactoriamente al sistema, todos los registros desde archivo Excel.</p>",
                //            emailsTo = new[] { email }
                //        };
                //        var content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8, "application/json");

                //        // Clonar la solicitud original para conservar las credenciales de autorizaci�n
                //        var request = new HttpRequestMessage(HttpMethod.Post, baseUrl);
                //        request.Content = content;

                //        foreach (var header in Request.Headers)
                //        {
                //            request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                //        }

                //        HttpResponseMessage response = await client.SendAsync(request);

                //        if (response.IsSuccessStatusCode) // Verificar si la solicitud fue exitosa (c�digo de estado 200)
                //        {
                //            string apiResponse = await response.Content.ReadAsStringAsync();
                //            query.Message = apiResponse;
                //        }
                //        else
                //        {
                //            Console.WriteLine($"Error en la solicitud: {response.StatusCode} - {response.ReasonPhrase}");
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine($"Error: {ex.Message}");
                //    }
                //}

                return Ok(query);
            }
            else
            {
                return BadRequest(query.Message);
            }
        }


        [HttpPut("Registro/Actualizar")]
        public async Task<IActionResult> UpdateRegistro([FromBody] JsonObject registro)
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
            registroJsonObject.Add("Rel", 1051);

            var query = await _cieQueryService.UpdateRegistro(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete, Route("Registro/Borrar/{idRegistro}")]
        public async Task<IActionResult> DeleteRegistro(int idRegistro)
        {
            var query = await _cieQueryService.DeleteRegistro(idRegistro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete, Route("Archivo")]
        public async Task<IActionResult> DeleteArchivo([FromBody] JsonObject registro)
        {
            var query = await _cieQueryService.DeleteArchivo(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Registros
    }
}
