using Bovis.API.Helper;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Bovis.API.Controllers;

[ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class CatalogoController : ControllerBase
{
	private string TransactionId { get { return HttpContext.TraceIdentifier; } }
	private readonly ILogger<CatalogoController> _logger;
    private readonly ICatalogoQueryService _catalogoQueryService;
	private readonly IMediator _mediator;

	public CatalogoController(ILogger<CatalogoController> logger, ICatalogoQueryService _catalogoQueryService, IMediator _mediator)
    {
        _logger = logger;
        this._catalogoQueryService = _catalogoQueryService;
		this._mediator = _mediator;
	}

	#region Beneficio

	[HttpGet, Route("Beneficio/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Beneficio(bool? Activo)
	{
		var query = await _catalogoQueryService.GetBeneficio(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Beneficio/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddBeneficio(AgregarBeneficioCommand Beneficio)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Beneficio);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Beneficio/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteBeneficio(EliminarBeneficioCommand Beneficio)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Beneficio);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Beneficio/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateBeneficio(ActualizarBeneficioCommand Beneficio)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Beneficio.Nombre = claimJWTModel.nombre;
		Beneficio.Usuario = claimJWTModel.correo;
		Beneficio.Roles = claimJWTModel.roles;
		Beneficio.TransactionId = claimJWTModel.transactionId;
		Beneficio.Rel = 1;
		var response = await _mediator.Send(Beneficio);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Categoria

	[HttpGet, Route("Categoria/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Categoria(bool? Activo)
	{
		var query = await _catalogoQueryService.GetCategoria(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Categoria/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddCategoria(AgregarCategoriaCommand Categoria)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Categoria);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Categoria/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteCategoria(EliminarCategoriaCommand Categoria)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Categoria);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Categoria/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateCategoria(ActualizarCategoriaCommand Categoria)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Categoria.Nombre = claimJWTModel.nombre;
		Categoria.Usuario = claimJWTModel.correo;
		Categoria.Roles = claimJWTModel.roles;
		Categoria.TransactionId = claimJWTModel.transactionId;
		Categoria.Rel = 2;
		var response = await _mediator.Send(Categoria);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Clasificacion

	[HttpGet, Route("Clasificacion/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Clasificacion(bool? Activo)
	{
		var query = await _catalogoQueryService.GetClasificacion(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Clasificacion/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddClasificacion(AgregarClasificacionCommand Clasificacion)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Clasificacion);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Clasificacion/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteClasificacion(EliminarClasificacionCommand Clasificacion)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Clasificacion);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Clasificacion/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateClasificacion(ActualizarClasificacionCommand Clasificacion)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Clasificacion.Nombre = claimJWTModel.nombre;
		Clasificacion.Usuario = claimJWTModel.correo;
		Clasificacion.Roles = claimJWTModel.roles;
		Clasificacion.TransactionId = claimJWTModel.transactionId;
		Clasificacion.Rel = 3;
		var response = await _mediator.Send(Clasificacion);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Costo Indirecto Salarios

	[HttpGet, Route("CostoIndirectoSalarios/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> CostoIndirectoSalarios(bool? Activo)
	{
		var query = await _catalogoQueryService.GetCostoIndirectoSalarios(Activo);
		return Ok(query);
	}

	[HttpPut, Route("CostoIndirectoSalarios/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddCostoIndirectoSalarios(AgregarCostoIndirectoSalariosCommand CostoIndirectoSalarios)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(CostoIndirectoSalarios);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("CostoIndirectoSalarios/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteCostoIndirectoSalarios(EliminarCostoIndirectoSalariosCommand CostoIndirectoSalarios)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(CostoIndirectoSalarios);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("CostoIndirectoSalarios/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateCostoIndirectoSalarios(ActualizarCostoIndirectoSalariosCommand CostoIndirectoSalarios)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		CostoIndirectoSalarios.Nombre = claimJWTModel.nombre;
		CostoIndirectoSalarios.Usuario = claimJWTModel.correo;
		CostoIndirectoSalarios.Roles = claimJWTModel.roles;
		CostoIndirectoSalarios.TransactionId = claimJWTModel.transactionId;
		CostoIndirectoSalarios.Rel = 4;
		var response = await _mediator.Send(CostoIndirectoSalarios);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Departamento

	[HttpGet, Route("Departamento/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Departamento(bool? Activo)
	{
		var query = await _catalogoQueryService.GetDepartamento(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Departamento/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddDepartamento(AgregarDepartamentoCommand Departamento)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Departamento);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Departamento/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteDepartamento(EliminarDepartamentoCommand Departamento)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Departamento);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Departamento/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateDepartamento(ActualizarDepartamentoCommand Departamento)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Departamento.Nombre = claimJWTModel.nombre;
		Departamento.Usuario = claimJWTModel.correo;
		Departamento.Roles = claimJWTModel.roles;
		Departamento.TransactionId = claimJWTModel.transactionId;
		Departamento.Rel = 5;
		var response = await _mediator.Send(Departamento);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Documento

	[HttpGet, Route("Documento/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Documento(bool? Activo)
	{
		var query = await _catalogoQueryService.GetDocumento(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Documento/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddDocumento(AgregarDocumentoCommand Documento)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Documento);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Documento/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteDocumento(EliminarDocumentoCommand Documento)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Documento);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Documento/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateDocumento(ActualizarDocumentoCommand Documento)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Documento.Nombre = claimJWTModel.nombre;
		Documento.Usuario = claimJWTModel.correo;
		Documento.Roles = claimJWTModel.roles;
		Documento.TransactionId = claimJWTModel.transactionId;
		Documento.Rel = 35;
		var response = await _mediator.Send(Documento);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Estado Civil

	[HttpGet, Route("EstadoCivil/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> EdoCivil(bool? Activo)
	{
		var query = await _catalogoQueryService.GetEdoCivil(Activo);
		return Ok(query);
	}

	[HttpPut, Route("EstadoCivil/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddEdoCivil(AgregarEdoCivilCommand EdoCivil)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(EdoCivil);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("EstadoCivil/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteEdoCivil(EliminarEdoCivilCommand EdoCivil)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(EdoCivil);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("EstadoCivil/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateEdoCivil(ActualizarEdoCivilCommand EdoCivil)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		EdoCivil.Nombre = claimJWTModel.nombre;
		EdoCivil.Usuario = claimJWTModel.correo;
		EdoCivil.Roles = claimJWTModel.roles;
		EdoCivil.TransactionId = claimJWTModel.transactionId;
		EdoCivil.Rel = 6;
		var response = await _mediator.Send(EdoCivil);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Estatus Proyecto

	[HttpGet, Route("Estatus/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Estatus(bool? Activo)
	{
		var query = await _catalogoQueryService.GetEstatusProyecto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Estatus/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddEstatusProyecto(AgregarEstatusProyectoCommand EstatusProyecto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(EstatusProyecto);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Estatus/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteEstatusProyecto(EliminarEstatusProyectoCommand EstatusProyecto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(EstatusProyecto);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Estatus/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateEstatusProyecto(ActualizarEstatusProyectoCommand EstatusProyecto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		EstatusProyecto.Nombre = claimJWTModel.nombre;
		EstatusProyecto.Usuario = claimJWTModel.correo;
		EstatusProyecto.Roles = claimJWTModel.roles;
		EstatusProyecto.TransactionId = claimJWTModel.transactionId;
		EstatusProyecto.Rel = 7;
		var response = await _mediator.Send(EstatusProyecto);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Forma Pago

	[HttpGet, Route("FormaPago/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> FormaPago(bool? Activo)
	{
		var query = await _catalogoQueryService.GetFormaPago(Activo);
		return Ok(query);
	}

	[HttpPut, Route("FormaPago/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddFormaPago(AgregarFormaPagoCommand FormaPago)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(FormaPago);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("FormaPago/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteFormaPago(EliminarFormaPagoCommand FormaPago)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(FormaPago);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("FormaPago/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateFormaPago(ActualizarFormaPagoCommand FormaPago)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		FormaPago.Nombre = claimJWTModel.nombre;
		FormaPago.Usuario = claimJWTModel.correo;
		FormaPago.Roles = claimJWTModel.roles;
		FormaPago.TransactionId = claimJWTModel.transactionId;
		FormaPago.Rel = 8;
		var response = await _mediator.Send(FormaPago);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Gasto

	[HttpGet, Route("Gasto/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Gasto(bool? Activo)
	{
		var query = await _catalogoQueryService.GetGasto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Gasto/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddGasto(AgregarGastoCommand Gasto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Gasto);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Gasto/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteGasto(EliminarGastoCommand Gasto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Gasto);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Gasto/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateGasto(ActualizarGastoCommand Gasto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Gasto.Nombre = claimJWTModel.nombre;
		Gasto.Usuario = claimJWTModel.correo;
		Gasto.Roles = claimJWTModel.roles;
		Gasto.TransactionId = claimJWTModel.transactionId;
		Gasto.Rel = 9;
		var response = await _mediator.Send(Gasto);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Ingreso

	[HttpGet, Route("Ingreso/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Ingreso(bool? Activo)
	{
		var query = await _catalogoQueryService.GetIngreso(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Ingreso/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddIngreso(AgregarIngresoCommand Ingreso)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Ingreso);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Ingreso/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteIngreso(EliminarIngresoCommand Ingreso)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Ingreso);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Ingreso/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateIngreso(ActualizarIngresoCommand Ingreso)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Ingreso.Nombre = claimJWTModel.nombre;
		Ingreso.Usuario = claimJWTModel.correo;
		Ingreso.Roles = claimJWTModel.roles;
		Ingreso.TransactionId = claimJWTModel.transactionId;
		Ingreso.Rel = 10;
		var response = await _mediator.Send(Ingreso);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Jornada

	[HttpGet, Route("Jornada/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Jornada(bool? Activo)
	{
		var query = await _catalogoQueryService.GetJornada(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Jornada/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddJornada(AgregarJornadaCommand Jornada)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Jornada);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Jornada/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteJornada(EliminarJornadaCommand Jornada)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Jornada);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Jornada/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateJornada(ActualizarJornadaCommand Jornada)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Jornada.Nombre = claimJWTModel.nombre;
		Jornada.Usuario = claimJWTModel.correo;
		Jornada.Roles = claimJWTModel.roles;
		Jornada.TransactionId = claimJWTModel.transactionId;
		Jornada.Rel = 11;
		var response = await _mediator.Send(Jornada);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Moneda

	[HttpGet, Route("Moneda/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Modena(bool? Activo)
	{
		var query = await _catalogoQueryService.GetModena(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Moneda/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddModena(AgregarModenaCommand Modena)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Modena);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Moneda/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteModena(EliminarModenaCommand Modena)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Modena);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Moneda/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateModena(ActualizarModenaCommand Modena)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Modena.Nombre = claimJWTModel.nombre;
		Modena.Usuario = claimJWTModel.correo;
		Modena.Roles = claimJWTModel.roles;
		Modena.TransactionId = claimJWTModel.transactionId;
		Modena.Rel = 12;
		var response = await _mediator.Send(Modena);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Nivel Estudios

	[HttpGet, Route("NivelEstudios/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> NivelEstudios(bool? Activo)
	{
		var query = await _catalogoQueryService.GetNivelEstudios(Activo);
		return Ok(query);
	}

	[HttpPut, Route("NivelEstudios/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddNivelEstudios(AgregarNivelEstudiosCommand NivelEstudios)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(NivelEstudios);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("NivelEstudios/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteNivelEstudios(EliminarNivelEstudiosCommand NivelEstudios)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(NivelEstudios);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("NivelEstudios/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateNivelEstudios(ActualizarNivelEstudiosCommand NivelEstudios)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		NivelEstudios.Nombre = claimJWTModel.nombre;
		NivelEstudios.Usuario = claimJWTModel.correo;
		NivelEstudios.Roles = claimJWTModel.roles;
		NivelEstudios.TransactionId = claimJWTModel.transactionId;
		NivelEstudios.Rel = 13;
		var response = await _mediator.Send(NivelEstudios);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Nivel Puesto

	[HttpGet, Route("NivelPuesto/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> NivelPuesto(bool? Activo)
	{
		var query = await _catalogoQueryService.GetNivelPuesto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("NivelPuesto/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddNivelPuesto(AgregarNivelPuestoCommand NivelPuesto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(NivelPuesto);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("NivelPuesto/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteNivelPuesto(EliminarNivelPuestoCommand NivelPuesto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(NivelPuesto);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("NivelPuesto/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateNivelPuesto(ActualizarNivelPuestoCommand NivelPuesto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		NivelPuesto.Nombre = claimJWTModel.nombre;
		NivelPuesto.Usuario = claimJWTModel.correo;
		NivelPuesto.Roles = claimJWTModel.roles;
		NivelPuesto.TransactionId = claimJWTModel.transactionId;
		NivelPuesto.Rel = 14;
		var response = await _mediator.Send(NivelPuesto);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Pcs

	[HttpGet, Route("Pcs/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Pcs(bool? Activo)
	{
		var query = await _catalogoQueryService.GetPcs(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Pcs/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddPcs(AgregarPcsCommand Pcs)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Pcs);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Pcs/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeletePcs(EliminarPcsCommand Pcs)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Pcs);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Pcs/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdatePcs(ActualizarPcsCommand Pcs)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Pcs.Nombre = claimJWTModel.nombre;
		Pcs.Usuario = claimJWTModel.correo;
		Pcs.Roles = claimJWTModel.roles;
		Pcs.TransactionId = claimJWTModel.transactionId;
		Pcs.Rel = 15;
		var response = await _mediator.Send(Pcs);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Prestacion

	[HttpGet, Route("Prestacion/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Prestacion(bool? Activo)
	{
		var query = await _catalogoQueryService.GetPrestacion(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Prestacion/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddPrestacion(AgregarPrestacionCommand Prestacion)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Prestacion);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Prestacion/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeletePrestacion(EliminarPrestacionCommand Prestacion)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Prestacion);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Prestacion/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdatePrestacion(ActualizarPrestacionCommand Prestacion)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Prestacion.Nombre = claimJWTModel.nombre;
		Prestacion.Usuario = claimJWTModel.correo;
		Prestacion.Roles = claimJWTModel.roles;
		Prestacion.TransactionId = claimJWTModel.transactionId;
		Prestacion.Rel = 16;
		var response = await _mediator.Send(Prestacion);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Puesto

	[HttpGet, Route("Puesto/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Puesto(bool? Activo)
	{
		var query = await _catalogoQueryService.GetPuesto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Puesto/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddPuesto(AgregarPuestoCommand Puesto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Puesto);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Puesto/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeletePuesto(EliminarPuestoCommand Puesto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Puesto);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Puesto/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdatePuesto(ActualizarPuestoCommand Puesto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Puesto.Nombre = claimJWTModel.nombre;
		Puesto.Usuario = claimJWTModel.correo;
		Puesto.Roles = claimJWTModel.roles;
		Puesto.TransactionId = claimJWTModel.transactionId;
		Puesto.Rel = 36;
		var response = await _mediator.Send(Puesto);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Rubro Ingreso Reembolsable

	[HttpGet, Route("RubroIngresoReembolsable/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> RubroIngresoReembolsable(bool? Activo)
	{
		var query = await _catalogoQueryService.GetRubroIngresoReembolsable(Activo);
		return Ok(query);
	}

	[HttpPut, Route("RubroIngresoReembolsable/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddRubroIngresoReembolsable(AgregarRubroIngresoReembolsableCommand RubroIngresoReembolsable)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(RubroIngresoReembolsable);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("RubroIngresoReembolsable/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteRubroIngresoReembolsable(EliminarRubroIngresoReembolsableCommand RubroIngresoReembolsable)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(RubroIngresoReembolsable);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("RubroIngresoReembolsable/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateRubroIngresoReembolsable(ActualizarRubroIngresoReembolsableCommand RubroIngresoReembolsable)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		RubroIngresoReembolsable.Nombre = claimJWTModel.nombre;
		RubroIngresoReembolsable.Usuario = claimJWTModel.correo;
		RubroIngresoReembolsable.Roles = claimJWTModel.roles;
		RubroIngresoReembolsable.TransactionId = claimJWTModel.transactionId;
		RubroIngresoReembolsable.Rel = 17;
		var response = await _mediator.Send(RubroIngresoReembolsable);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Sector

	[HttpGet, Route("Sector/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Sector(bool? Activo)
	{
		var query = await _catalogoQueryService.GetSector(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Sector/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddSector(AgregarSectorCommand Sector)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Sector);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Sector/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteSector(EliminarSectorCommand Sector)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(Sector);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Sector/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateSector(ActualizarSectorCommand Sector)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		Sector.Nombre = claimJWTModel.nombre;
		Sector.Usuario = claimJWTModel.correo;
		Sector.Roles = claimJWTModel.roles;
		Sector.TransactionId = claimJWTModel.transactionId;
		Sector.Rel = 18;
		var response = await _mediator.Send(Sector);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Cie

	[HttpGet, Route("TipoCie/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoCie(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoCie(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoCie/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoCie(AgregarTipoCieCommand TipoCie)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoCie);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoCie/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoCie(EliminarTipoCieCommand TipoCie)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoCie);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoCie/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoCie(ActualizarTipoCieCommand TipoCie)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoCie.Nombre = claimJWTModel.nombre;
		TipoCie.Usuario = claimJWTModel.correo;
		TipoCie.Roles = claimJWTModel.roles;
		TipoCie.TransactionId = claimJWTModel.transactionId;
		TipoCie.Rel = 19;
		var response = await _mediator.Send(TipoCie);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Contrato

	[HttpGet, Route("TipoContrato/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoContrato(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoContrato(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoContrato/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoContrato(AgregarTipoContratoCommand TipoContrato)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoContrato);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoContrato/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoContrato(EliminarTipoContratoCommand TipoContrato)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoContrato);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoContrato/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoContrato(ActualizarTipoContratoCommand TipoContrato)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoContrato.Nombre = claimJWTModel.nombre;
		TipoContrato.Usuario = claimJWTModel.correo;
		TipoContrato.Roles = claimJWTModel.roles;
		TipoContrato.TransactionId = claimJWTModel.transactionId;
		TipoContrato.Rel = 20;
		var response = await _mediator.Send(TipoContrato);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Cta Contable

	[HttpGet, Route("TipoCtaContable/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoCtaContable(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoCtaContable(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoCtaContable/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoCtaContable(AgregarTipoCtaContableCommand TipoCtaContable)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoCtaContable);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoCtaContable/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoCtaContable(EliminarTipoCtaContableCommand TipoCtaContable)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoCtaContable);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoCtaContable/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoCtaContable(ActualizarTipoCtaContableCommand TipoCtaContable)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoCtaContable.Nombre = claimJWTModel.nombre;
		TipoCtaContable.Usuario = claimJWTModel.correo;
		TipoCtaContable.Roles = claimJWTModel.roles;
		TipoCtaContable.TransactionId = claimJWTModel.transactionId;
		TipoCtaContable.Rel = 21;
		var response = await _mediator.Send(TipoCtaContable);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Cuenta

	[HttpGet, Route("TipoCuenta/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoCuenta(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoCuenta(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoCuenta/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoCuenta(AgregarTipoCuentaCommand TipoCuenta)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoCuenta);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoCuenta/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoCuenta(EliminarTipoCuentaCommand TipoCuenta)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoCuenta);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoCuenta/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoCuenta(ActualizarTipoCuentaCommand TipoCuenta)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoCuenta.Nombre = claimJWTModel.nombre;
		TipoCuenta.Usuario = claimJWTModel.correo;
		TipoCuenta.Roles = claimJWTModel.roles;
		TipoCuenta.TransactionId = claimJWTModel.transactionId;
		TipoCuenta.Rel = 22;
		var response = await _mediator.Send(TipoCuenta);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Documento

	[HttpGet, Route("TipoDocumento/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoDocumento(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoDocumento(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoDocumento/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoDocumento(AgregarTipoDocumentoCommand TipoDocumento)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoDocumento);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoDocumento/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoDocumento(EliminarTipoDocumentoCommand TipoDocumento)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoDocumento);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoDocumento/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoDocumento(ActualizarTipoDocumentoCommand TipoDocumento)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoDocumento.Nombre = claimJWTModel.nombre;
		TipoDocumento.Usuario = claimJWTModel.correo;
		TipoDocumento.Roles = claimJWTModel.roles;
		TipoDocumento.TransactionId = claimJWTModel.transactionId;
		TipoDocumento.Rel = 23;
		var response = await _mediator.Send(TipoDocumento);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Empleado

	[HttpGet, Route("TipoEmpleado/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoEmpleado(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoEmpleado(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoEmpleado/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoEmpleado(AgregarTipoEmpleadoCommand TipoEmpleado)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoEmpleado);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoEmpleado/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoEmpleado(EliminarTipoEmpleadoCommand TipoEmpleado)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoEmpleado);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoEmpleado/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoEmpleado(ActualizarTipoEmpleadoCommand TipoEmpleado)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoEmpleado.Nombre = claimJWTModel.nombre;
		TipoEmpleado.Usuario = claimJWTModel.correo;
		TipoEmpleado.Roles = claimJWTModel.roles;
		TipoEmpleado.TransactionId = claimJWTModel.transactionId;
		TipoEmpleado.Rel = 24;
		var response = await _mediator.Send(TipoEmpleado);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Factura

	[HttpGet, Route("TipoFactura/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoFactura(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoFactura(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoFactura/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoFactura(AgregarTipoFacturaCommand TipoFactura)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoFactura);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoFactura/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoFactura(EliminarTipoFacturaCommand TipoFactura)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoFactura);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoFactura/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoFactura(ActualizarTipoFacturaCommand TipoFactura)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoFactura.Nombre = claimJWTModel.nombre;
		TipoFactura.Usuario = claimJWTModel.correo;
		TipoFactura.Roles = claimJWTModel.roles;
		TipoFactura.TransactionId = claimJWTModel.transactionId;
		TipoFactura.Rel = 25;
		var response = await _mediator.Send(TipoFactura);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Gasto

	[HttpGet, Route("TipoGasto/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoGasto(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoGasto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoGasto/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoGasto(AgregarTipoGastoCommand TipoGasto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoGasto);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoGasto/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoGasto(EliminarTipoGastoCommand TipoGasto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoGasto);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoGasto/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoGasto(ActualizarTipoGastoCommand TipoGasto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoGasto.Nombre = claimJWTModel.nombre;
		TipoGasto.Usuario = claimJWTModel.correo;
		TipoGasto.Roles = claimJWTModel.roles;
		TipoGasto.TransactionId = claimJWTModel.transactionId;
		TipoGasto.Rel = 26;
		var response = await _mediator.Send(TipoGasto);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Ingreso

	[HttpGet, Route("TipoIngreso/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoIngreso(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoIngreso(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoIngreso/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoIngreso(AgregarTipoIngresoCommand TipoIngreso)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoIngreso);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoIngreso/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoIngreso(EliminarTipoIngresoCommand TipoIngreso)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoIngreso);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoIngreso/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoIngreso(ActualizarTipoIngresoCommand TipoIngreso)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoIngreso.Nombre = claimJWTModel.nombre;
		TipoIngreso.Usuario = claimJWTModel.correo;
		TipoIngreso.Roles = claimJWTModel.roles;
		TipoIngreso.TransactionId = claimJWTModel.transactionId;
		TipoIngreso.Rel = 27;
		var response = await _mediator.Send(TipoIngreso);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Pcs

	[HttpGet, Route("TipoPcs/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoPcs(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoPcs(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoPcs/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoPcs(AgregarTipoPcsCommand TipoPcs)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoPcs);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoPcs/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoPcs(EliminarTipoPcsCommand TipoPcs)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoPcs);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoPcs/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoPcs(ActualizarTipoPcsCommand TipoPcs)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoPcs.Nombre = claimJWTModel.nombre;
		TipoPcs.Usuario = claimJWTModel.correo;
		TipoPcs.Roles = claimJWTModel.roles;
		TipoPcs.TransactionId = claimJWTModel.transactionId;
		TipoPcs.Rel = 28;
		var response = await _mediator.Send(TipoPcs);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Poliza

	[HttpGet, Route("TipoPoliza/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoPoliza(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoPoliza(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoPoliza/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoPoliza(AgregarTipoPolizaCommand TipoPoliza)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoPoliza);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoPoliza/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoPoliza(EliminarTipoPolizaCommand TipoPoliza)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoPoliza);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoPoliza/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoPoliza(ActualizarTipoPolizaCommand TipoPoliza)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoPoliza.Nombre = claimJWTModel.nombre;
		TipoPoliza.Usuario = claimJWTModel.correo;
		TipoPoliza.Roles = claimJWTModel.roles;
		TipoPoliza.TransactionId = claimJWTModel.transactionId;
		TipoPoliza.Rel = 29;
		var response = await _mediator.Send(TipoPoliza);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Proyecto

	[HttpGet, Route("TipoProyecto/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoProyecto(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoProyecto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoProyecto/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoProyecto(AgregarTipoProyectoCommand TipoProyecto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoProyecto);
		if(!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoProyecto/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoProyecto(EliminarTipoProyectoCommand TipoProyecto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoProyecto);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoProyecto/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoProyecto(ActualizarTipoProyectoCommand TipoProyecto)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoProyecto.Nombre = claimJWTModel.nombre;
		TipoProyecto.Usuario = claimJWTModel.correo;
		TipoProyecto.Roles = claimJWTModel.roles;
		TipoProyecto.TransactionId = claimJWTModel.transactionId;
		TipoProyecto.Rel = 30;
		var response = await _mediator.Send(TipoProyecto);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Resultado

	[HttpGet, Route("TipoResultado/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoResultado(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoResultado(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoResultado/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoResultado(AgregarTipoResultadoCommand TipoResultado)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoResultado);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoResultado/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoResultado(EliminarTipoResultadoCommand TipoResultado)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoResultado);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoResultado/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoResultado(ActualizarTipoResultadoCommand TipoResultado)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoResultado.Nombre = claimJWTModel.nombre;
		TipoResultado.Usuario = claimJWTModel.correo;
		TipoResultado.Roles = claimJWTModel.roles;
		TipoResultado.TransactionId = claimJWTModel.transactionId;
		TipoResultado.Rel = 31;
		var response = await _mediator.Send(TipoResultado);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Tipo Sangre

	[HttpGet, Route("TipoSangre/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> TipoSangre(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoSangre(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoSangre/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddTipoSangre(AgregarTipoSangreCommand TipoSangre)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoSangre);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("TipoSangre/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteTipoSangre(EliminarTipoSangreCommand TipoSangre)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(TipoSangre);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("TipoSangre/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateTipoSangre(ActualizarTipoSangreCommand TipoSangre)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		TipoSangre.Nombre = claimJWTModel.nombre;
		TipoSangre.Usuario = claimJWTModel.correo;
		TipoSangre.Roles = claimJWTModel.roles;
		TipoSangre.TransactionId = claimJWTModel.transactionId;
		TipoSangre.Rel = 32;
		var response = await _mediator.Send(TipoSangre);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region UnidadNegocio

	[HttpGet, Route("UnidadNegocio/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UnidadNegocio(bool? Activo)
	{
		var query = await _catalogoQueryService.GetUnidadNegocio(Activo);
		return Ok(query);
	}

	[HttpPut, Route("UnidadNegocio/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddUnidadNegocio(AgregarUnidadNegocioCommand UnidadNegocio)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(UnidadNegocio);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("UnidadNegocio/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteUnidadNegocio(EliminarUnidadNegocioCommand UnidadNegocio)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(UnidadNegocio);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("UnidadNegocio/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateUnidadNegocio(ActualizarUnidadNegocioCommand UnidadNegocio)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		UnidadNegocio.Nombre = claimJWTModel.nombre;
		UnidadNegocio.Usuario = claimJWTModel.correo;
		UnidadNegocio.Roles = claimJWTModel.roles;
		UnidadNegocio.TransactionId = claimJWTModel.transactionId;
		UnidadNegocio.Rel = 33;
		var response = await _mediator.Send(UnidadNegocio);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion

	#region Viatico

	[HttpGet, Route("Viatico/{Activo?}"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> Viatico(bool? Activo)
	{
		var query = await _catalogoQueryService.GetViatico(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Viatico/Agregar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> AddViatico(AgregarViaticoCommand viatico)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(viatico);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpDelete, Route("Viatico/Borrar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> DeleteViatico(EliminarViaticoCommand viatico)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var response = await _mediator.Send(viatico);
		if (!response.Success)
		{
			var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
			_logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		}
		return Ok(response);
	}

	[HttpPost, Route("Viatico/Actualizar"), Authorize(Roles = "it.full, dev.full")]
	public async Task<IActionResult> UpdateViatico(ActualizarViaticoCommand viatico)
	{
		if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
		var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
		viatico.Nombre = claimJWTModel.nombre;
		viatico.Usuario = claimJWTModel.correo;
		viatico.Roles = claimJWTModel.roles;
		viatico.TransactionId = claimJWTModel.transactionId;
		viatico.Rel = 34;
		var response = await _mediator.Send(viatico);
		if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
		return Ok(response);
	}

	#endregion
}

