using Bovis.API.Helper;
using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Bovis.API.Controllers
{
	[ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
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


		[HttpPost("Enviar"), Authorize(Roles = "it.full, dev.full")]
		public async Task<IActionResult> ExtraerInfoFactura(EnviarFactura request)
		{
			var business = await _facturaQueryService.ExtraerInfoFactura(request.B64Xml);
			return Ok(business);
		}



		[HttpPut("Agregar"), Authorize(Roles = "it.full, dev.full")]
		public async Task<IActionResult> AgregarFactura(AddFacturasCommand objetivo)
		{
			if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
			var business = await _mediator.Send(objetivo);
			return Ok(business);
		}

		[HttpGet("InfoProyecto/{numProyecto}"), Authorize(Roles = "it.full, dev.full")]
		public async Task<IActionResult> ObtenerInfoProyecto(int numProyecto)
		{
			var business = await _facturaQueryService.GetInfoProyecto(numProyecto);
			return Ok(business);

		}

        [HttpPut("AgregarNC"), Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AgregarNotaCredito(AddNotaCreditoCommand objetivo)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _mediator.Send(objetivo);
            return Ok(business);
        }

        [HttpPut("AgregarCRP"), Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> AgregarCRP(AddPagosCommand objetivo)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var business = await _mediator.Send(objetivo);
            return Ok(business);
        }

        [HttpPost, Route("Cancelar"), Authorize(Roles = "it.full, dev.full")]
        public async Task<IActionResult> CancelFactura(CancelFacturaCommand factura)
        {
            if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            factura.Nombre = claimJWTModel.nombre;
            factura.Usuario = claimJWTModel.correo;
            factura.Roles = claimJWTModel.roles;
            factura.TransactionId = claimJWTModel.transactionId;
            factura.Rel = 37;
            var response = await _mediator.Send(factura);
            if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
            return Ok(response);
        }

        [HttpPost("Consultar"), Authorize(Roles = "eje.full, dev.full")]
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