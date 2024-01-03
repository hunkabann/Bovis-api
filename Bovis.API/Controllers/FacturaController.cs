using Bovis.API.Helper;
using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace Bovis.API.Controllers
{
    [Authorize]
	[ApiController, Route("api/[controller]")]
	public class FacturaController : ControllerBase
	{
		private string TransactionId { get { return HttpContext.TraceIdentifier; } }
		private readonly ILogger<FacturaController> _logger;
		private readonly IFacturaQueryService _facturaQueryService;
		private readonly IMediator _mediator;

		public FacturaController(ILogger<FacturaController> logger, IFacturaQueryService _facturaQueryService, IMediator _mediator)
		{
			_logger = logger;
			this._facturaQueryService = _facturaQueryService;
			this._mediator = _mediator;
		}


		[HttpPost, Route("Enviar")]
        public async Task<IActionResult> ExtraerInfoFactura(EnviarFactura request)
		{
			var business = await _facturaQueryService.ExtraerInfoFactura(request.B64Xml);
			return Ok(business);
		}



		[HttpPut, Route("Agregar")]
        public async Task<IActionResult> AgregarFactura(AddFacturasCommand objetivo)
		{
			if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
			var business = await _mediator.Send(objetivo);
			return Ok(business);
		}

		[HttpGet, Route("InfoProyecto/{numProyecto}")]
        public async Task<IActionResult> ObtenerInfoProyecto(int numProyecto)
		{
			var business = await _facturaQueryService.GetInfoProyecto(numProyecto);
			return Ok(business);

		}

        [HttpPut, Route("AgregarNC")]
        public async Task<IActionResult> AgregarNotaCredito(AddNotaCreditoCommand objetivo)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _mediator.Send(objetivo);
            return Ok(business);
        }

        [HttpPost, Route("NotaCredito")]
        public async Task<IActionResult> AddNotaCreditoSinFactura([FromBody] JsonObject registro)
        {
            var query = await _facturaQueryService.AddNotaCreditoSinFactura(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpGet, Route("NotaCredito/{NumProyecto}/{Mes}/{Anio}")]
        public async Task<IActionResult> GetNotaCreditoSinFactura(int NumProyecto, int Mes, int Anio)
        {
            var query = await _facturaQueryService.GetNotaCreditoSinFactura(NumProyecto, Mes, Anio);
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut, Route("NotaCredito/AddToFactura")]
        public async Task<IActionResult> AddNotaCreditoSinFacturaToFactura([FromBody] JsonObject registro)
        {
            var query = await _facturaQueryService.AddNotaCreditoSinFacturaToFactura(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut, Route("AgregarCRP")]
        public async Task<IActionResult> AgregarCRP(AddPagosCommand objetivo)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _mediator.Send(objetivo);
            return Ok(business);
        }

        [HttpPost, Route("Cancelar")]
        public async Task<IActionResult> CancelFactura(CancelFacturaCommand factura)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");


            IHeaderDictionary headers = HttpContext.Request.Headers;
            string email = headers["email"];
            string nombre = headers["nombre"];
            JsonObject registroJsonObject = new JsonObject();

            factura.Nombre = email;
            factura.Usuario = nombre;
            factura.Roles = string.Empty;
            factura.TransactionId = TransactionId;
            factura.Rel = 37;
            var response = await _mediator.Send(factura);
            if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(factura)}");
            return Ok(response);
        }

        [HttpPut, Route("Nota/Cancelar")]
        public async Task<IActionResult> CancelNota([FromBody] JsonObject registro)
        {
            var query = await _facturaQueryService.CancelNota(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut, Route("Cobranza/Cancelar")]
        public async Task<IActionResult> CancelCobranza([FromBody] JsonObject registro)
        {
            var query = await _facturaQueryService.CancelCobranza(registro);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPost, Route("Consultar")]
        public async Task<IActionResult> Search(ConsultarFactura request)
        {
            var business = await _facturaQueryService.Search(request);
            return Ok(business);
        }


        //[HttpPut("Almacenar")]
        //public async Task<IActionResult> AlmacenarInfoFactura(InfoFactura request)
        //{
        //    var business = await _facturaQueryService.AgregarFactura(request);
        //    return Ok(business);
        //}
    }
}