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

[Authorize]
[ApiController, Route("api/[controller]")]
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

	[HttpGet, Route("Beneficio/{Activo?}")]
	public async Task<IActionResult> Beneficio(bool? Activo)
	{
		var query = await _catalogoQueryService.GetBeneficio(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Beneficio/Agregar")]
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

	[HttpDelete, Route("Beneficio/Borrar")]
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

	[HttpPost, Route("Beneficio/Actualizar")]
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
	[HttpGet, Route("Categoria/{Activo?}")]
	public async Task<IActionResult> Categoria(bool? Activo)
	{
		var query = await _catalogoQueryService.GetCategoria(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Categoria/Agregar")]
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

	[HttpDelete, Route("Categoria/Borrar")]
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

	[HttpPost, Route("Categoria/Actualizar")]
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
	[HttpGet, Route("Clasificacion/{Activo?}")]
    public async Task<IActionResult> Clasificacion(bool? Activo)
	{
		var query = await _catalogoQueryService.GetClasificacion(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Clasificacion/Agregar")]
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

	[HttpDelete, Route("Clasificacion/Borrar")]
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

	[HttpPost, Route("Clasificacion/Actualizar")]
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
	[HttpGet, Route("CostoIndirectoSalarios/{Activo?}")]
    public async Task<IActionResult> CostoIndirectoSalarios(bool? Activo)
	{
		var query = await _catalogoQueryService.GetCostoIndirectoSalarios(Activo);
		return Ok(query);
	}

	[HttpPut, Route("CostoIndirectoSalarios/Agregar")]
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

	[HttpDelete, Route("CostoIndirectoSalarios/Borrar")]
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

	[HttpPost, Route("CostoIndirectoSalarios/Actualizar")]
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
	[HttpGet, Route("Departamento/{Activo?}")]
    public async Task<IActionResult> Departamento(bool? Activo)
	{
		var query = await _catalogoQueryService.GetDepartamento(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Departamento/Agregar")]
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

	[HttpDelete, Route("Departamento/Borrar")]
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

	[HttpPost, Route("Departamento/Actualizar")]
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
	[HttpGet, Route("Documento/{Activo?}")]
    public async Task<IActionResult> Documento(bool? Activo)
	{
		var query = await _catalogoQueryService.GetDocumento(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Documento/Agregar")]
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

	[HttpDelete, Route("Documento/Borrar")]
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

	[HttpPost, Route("Documento/Actualizar")]
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

    #region Estado
    [HttpGet, Route("Estado/{Activo?}")]
    public async Task<IActionResult> Edo(bool? Activo)
    {
        var query = await _catalogoQueryService.GetEdo(Activo);
        return Ok(query);
    }

    [HttpPut, Route("Estado/Agregar")]
    public async Task<IActionResult> AddEdo(AgregarEdoCommand Edo)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(Edo);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpDelete, Route("Estado/Borrar")]
    public async Task<IActionResult> DeleteEdo(EliminarEdoCommand Edo)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(Edo);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpPost, Route("Estado/Actualizar")]
    public async Task<IActionResult> UpdateEdo(ActualizarEdoCommand Edo)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
        Edo.Nombre = claimJWTModel.nombre;
        Edo.Usuario = claimJWTModel.correo;
        Edo.Roles = claimJWTModel.roles;
        Edo.TransactionId = claimJWTModel.transactionId;
        Edo.Rel = 6;
        var response = await _mediator.Send(Edo);
        if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        return Ok(response);
    }
    #endregion Estado

    #region Estado Civil
    [HttpGet, Route("EstadoCivil/{Activo?}")]
    public async Task<IActionResult> EdoCivil(bool? Activo)
	{
		var query = await _catalogoQueryService.GetEdoCivil(Activo);
		return Ok(query);
	}

	[HttpPut, Route("EstadoCivil/Agregar")]
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

	[HttpDelete, Route("EstadoCivil/Borrar")]
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

	[HttpPost, Route("EstadoCivil/Actualizar")]
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
	[HttpGet, Route("Estatus/{Activo?}")]
    public async Task<IActionResult> Estatus(bool? Activo)
	{
		var query = await _catalogoQueryService.GetEstatusProyecto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Estatus/Agregar")]
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

	[HttpDelete, Route("Estatus/Borrar")]
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

	[HttpPost, Route("Estatus/Actualizar")]
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

    #region Experiencia
    [HttpGet, Route("Experiencia/{Activo?}")]
    public async Task<IActionResult> Experiencia(bool? Activo)
    {
        var query = await _catalogoQueryService.GetExperiencia(Activo);
        return Ok(query);
    }

    [HttpPut, Route("Experiencia/Agregar")]
    public async Task<IActionResult> AddExperiencia(AgregarExperienciaCommand experiencia)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(experiencia);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpDelete, Route("Experiencia/Borrar")]
    public async Task<IActionResult> DeleteExperiencia(EliminarExperienciaCommand experiencia)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(experiencia);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpPost, Route("Experiencia/Actualizar")]
    public async Task<IActionResult> UpdateExperiencia(ActualizarExperienciaCommand experiencia)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
        experiencia.Nombre = claimJWTModel.nombre;
        experiencia.Usuario = claimJWTModel.correo;
        experiencia.Roles = claimJWTModel.roles;
        experiencia.TransactionId = claimJWTModel.transactionId;
        experiencia.Rel = 7;
        var response = await _mediator.Send(experiencia);
        if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        return Ok(response);
    }
    #endregion Experiencia

    #region Forma Pago
    [HttpGet, Route("FormaPago/{Activo?}")]
    public async Task<IActionResult> FormaPago(bool? Activo)
	{
		var query = await _catalogoQueryService.GetFormaPago(Activo);
		return Ok(query);
	}

	[HttpPut, Route("FormaPago/Agregar")]
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

	[HttpDelete, Route("FormaPago/Borrar")]
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

	[HttpPost, Route("FormaPago/Actualizar")]
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
	[HttpGet, Route("Gasto/{Activo?}")]
    public async Task<IActionResult> Gasto(bool? Activo)
	{
		var query = await _catalogoQueryService.GetGasto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Gasto/Agregar")]
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

	[HttpDelete, Route("Gasto/Borrar")]
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

	[HttpPost, Route("Gasto/Actualizar")]
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

    #region Habilidades
    [HttpGet, Route("Habilidad/{Activo?}")]
    public async Task<IActionResult> Habilidad(bool? Activo)
    {
        var query = await _catalogoQueryService.GetHabilidad(Activo);
        return Ok(query);
    }
    [HttpPut, Route("Habilidad/Agregar")]
    public async Task<IActionResult> AddHabilidad(AgregarHabilidadCommand habilidad)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(habilidad);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpDelete, Route("Habilidad/Borrar")]
    public async Task<IActionResult> DeleteHabilidad(EliminarHabilidadCommand habilidad)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(habilidad);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpPost, Route("Habilidad/Actualizar")]
    public async Task<IActionResult> UpdateHabilidad(ActualizarHabilidadCommand habilidad)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
        habilidad.Nombre = claimJWTModel.nombre;
        habilidad.Usuario = claimJWTModel.correo;
        habilidad.Roles = claimJWTModel.roles;
        habilidad.TransactionId = claimJWTModel.transactionId;
        habilidad.Rel = 7;
        var response = await _mediator.Send(habilidad);
        if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        return Ok(response);
    }
    #endregion Habilidades

    #region Ingreso
    [HttpGet, Route("Ingreso/{Activo?}")]
    public async Task<IActionResult> Ingreso(bool? Activo)
	{
		var query = await _catalogoQueryService.GetIngreso(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Ingreso/Agregar")]
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

	[HttpDelete, Route("Ingreso/Borrar")]
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

	[HttpPost, Route("Ingreso/Actualizar")]
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
	[HttpGet, Route("Jornada/{Activo?}")]
	public async Task<IActionResult> Jornada(bool? Activo)
	{
		var query = await _catalogoQueryService.GetJornada(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Jornada/Agregar")]
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

	[HttpDelete, Route("Jornada/Borrar")]
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

	[HttpPost, Route("Jornada/Actualizar")]
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
	[HttpGet, Route("Moneda/{Activo?}")]
    public async Task<IActionResult> Modena(bool? Activo)
	{
		var query = await _catalogoQueryService.GetModena(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Moneda/Agregar")]
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

	[HttpDelete, Route("Moneda/Borrar")]
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

	[HttpPost, Route("Moneda/Actualizar")]
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
	[HttpGet, Route("NivelEstudios/{Activo?}")]
	public async Task<IActionResult> NivelEstudios(bool? Activo)
	{
		var query = await _catalogoQueryService.GetNivelEstudios(Activo);
		return Ok(query);
	}

	[HttpPut, Route("NivelEstudios/Agregar")]
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

	[HttpDelete, Route("NivelEstudios/Borrar")]
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

	[HttpPost, Route("NivelEstudios/Actualizar")]
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
    [HttpGet, Route("NivelPuesto/{Activo?}")]
    public async Task<IActionResult> NivelPuesto(bool? Activo)
	{
		var query = await _catalogoQueryService.GetNivelPuesto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("NivelPuesto/Agregar")]
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

	[HttpDelete, Route("NivelPuesto/Borrar")]
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

	[HttpPost, Route("NivelPuesto/Actualizar")]
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

    #region Pais
    [HttpGet, Route("Pais/{Activo?}")]
    public async Task<IActionResult> Pais(bool? Activo)
    {
        var query = await _catalogoQueryService.GetPais(Activo);
        return Ok(query);
    }

    [HttpPut, Route("Pais/Agregar")]
    public async Task<IActionResult> AddPais(AgregarPaisCommand Pais)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(Pais);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpDelete, Route("Pais/Borrar")]
    public async Task<IActionResult> DeletePais(EliminarPaisCommand Pais)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(Pais);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpPost, Route("Pais/Actualizar")]
    public async Task<IActionResult> UpdatePais(ActualizarPaisCommand Pais)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
        Pais.Nombre = claimJWTModel.nombre;
        Pais.Usuario = claimJWTModel.correo;
        Pais.Roles = claimJWTModel.roles;
        Pais.TransactionId = claimJWTModel.transactionId;
        Pais.Rel = 14;
        var response = await _mediator.Send(Pais);
        if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        return Ok(response);
    }

    #endregion Pais

    #region Pcs
    [HttpGet, Route("Pcs/{Activo?}")]
    public async Task<IActionResult> Pcs(bool? Activo)
	{
		var query = await _catalogoQueryService.GetPcs(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Pcs/Agregar")]
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

	[HttpDelete, Route("Pcs/Borrar")]
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

	[HttpPost, Route("Pcs/Actualizar")]
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
	[HttpGet, Route("Prestacion/{Activo?}")]
    public async Task<IActionResult> Prestacion(bool? Activo)
	{
		var query = await _catalogoQueryService.GetPrestacion(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Prestacion/Agregar")]
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

	[HttpDelete, Route("Prestacion/Borrar")]
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

	[HttpPost, Route("Prestacion/Actualizar")]
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

    #region Profesion
    [HttpGet, Route("Profesion/{Activo?}")]
    public async Task<IActionResult> Profesion(bool? Activo)
    {
        var query = await _catalogoQueryService.GetProfesion(Activo);
        return Ok(query);
    }
    [HttpPut, Route("Profesion/Agregar")]
    public async Task<IActionResult> AddProfesion(AgregarProfesionCommand profesion)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(profesion);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpDelete, Route("Profesion/Borrar")]
    public async Task<IActionResult> DeleteProfesion(EliminarProfesionCommand profesion)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(profesion);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpPost, Route("Profesion/Actualizar")]
    public async Task<IActionResult> UpdateProfesion(ActualizarProfesionCommand profesion)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
        profesion.Nombre = claimJWTModel.nombre;
        profesion.Usuario = claimJWTModel.correo;
        profesion.Roles = claimJWTModel.roles;
        profesion.TransactionId = claimJWTModel.transactionId;
        profesion.Rel = 7;
        var response = await _mediator.Send(profesion);
        if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        return Ok(response);
    }
    #endregion

    #region Puesto
    [HttpGet, Route("Puesto/{Activo?}")]
	public async Task<IActionResult> Puesto(bool? Activo)
	{
		var query = await _catalogoQueryService.GetPuesto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Puesto/Agregar")]
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

	[HttpDelete, Route("Puesto/Borrar")]
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

	[HttpPost, Route("Puesto/Actualizar")]
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
	[HttpGet, Route("RubroIngresoReembolsable/{Activo?}")]
    public async Task<IActionResult> RubroIngresoReembolsable(bool? Activo)
	{
		var query = await _catalogoQueryService.GetRubroIngresoReembolsable(Activo);
		return Ok(query);
	}

	[HttpPut, Route("RubroIngresoReembolsable/Agregar")]
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

	[HttpDelete, Route("RubroIngresoReembolsable/Borrar")]
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

	[HttpPost, Route("RubroIngresoReembolsable/Actualizar")]
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
	[HttpGet, Route("Sector/{Activo?}")]
    public async Task<IActionResult> Sector(bool? Activo)
	{
		var query = await _catalogoQueryService.GetSector(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Sector/Agregar")]
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

	[HttpDelete, Route("Sector/Borrar")]
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

	[HttpPost, Route("Sector/Actualizar")]
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

    #region Sexo
    [HttpGet, Route("Sexo/{Activo?}")]
    public async Task<IActionResult> Sexo(bool? Activo)
    {
        var query = await _catalogoQueryService.GetSexo(Activo);
        return Ok(query);
    }

    [HttpPut, Route("Sexo/Agregar")]
    public async Task<IActionResult> AddSexo(AgregarSexoCommand Sexo)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(Sexo);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpDelete, Route("Sexo/Borrar")]
    public async Task<IActionResult> DeleteSexo(EliminarSexoCommand Sexo)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(Sexo);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpPost, Route("Sexo/Actualizar")]
    public async Task<IActionResult> UpdateSexo(ActualizarSexoCommand Sexo)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
        Sexo.Nombre = claimJWTModel.nombre;
        Sexo.Usuario = claimJWTModel.correo;
        Sexo.Roles = claimJWTModel.roles;
        Sexo.TransactionId = claimJWTModel.transactionId;
        Sexo.Rel = 18;
        var response = await _mediator.Send(Sexo);
        if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        return Ok(response);
    }

    #endregion Sexo

    #region Tipo Cie
    [HttpGet, Route("TipoCie/{Activo?}")]
    public async Task<IActionResult> TipoCie(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoCie(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoCie/Agregar")]
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

	[HttpDelete, Route("TipoCie/Borrar")]
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

	[HttpPost, Route("TipoCie/Actualizar")]
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
	[HttpGet, Route("TipoContrato/{Activo?}")]
    public async Task<IActionResult> TipoContrato(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoContrato(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoContrato/Agregar")]
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

	[HttpDelete, Route("TipoContrato/Borrar")]
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

	[HttpPost, Route("TipoContrato/Actualizar")]
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
	[HttpGet, Route("TipoCtaContable/{Activo?}")]
    public async Task<IActionResult> TipoCtaContable(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoCtaContable(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoCtaContable/Agregar")]
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

	[HttpDelete, Route("TipoCtaContable/Borrar")]
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

	[HttpPost, Route("TipoCtaContable/Actualizar")]
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
	[HttpGet, Route("TipoCuenta/{Activo?}")]
    public async Task<IActionResult> TipoCuenta(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoCuenta(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoCuenta/Agregar")]
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

	[HttpDelete, Route("TipoCuenta/Borrar")]
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

	[HttpPost, Route("TipoCuenta/Actualizar")]
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
	[HttpGet, Route("TipoDocumento/{Activo?}")]
    public async Task<IActionResult> TipoDocumento(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoDocumento(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoDocumento/Agregar")]
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

	[HttpDelete, Route("TipoDocumento/Borrar")]
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

	[HttpPost, Route("TipoDocumento/Actualizar")]
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
	[HttpGet, Route("TipoEmpleado/{Activo?}")]
    public async Task<IActionResult> TipoEmpleado(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoEmpleado(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoEmpleado/Agregar")]
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

	[HttpDelete, Route("TipoEmpleado/Borrar")]
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

	[HttpPost, Route("TipoEmpleado/Actualizar")]
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
	[HttpGet, Route("TipoFactura/{Activo?}")]
    public async Task<IActionResult> TipoFactura(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoFactura(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoFactura/Agregar")]
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

	[HttpDelete, Route("TipoFactura/Borrar")]
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

	[HttpPost, Route("TipoFactura/Actualizar")]
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
	[HttpGet, Route("TipoGasto/{Activo?}")]
    public async Task<IActionResult> TipoGasto(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoGasto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoGasto/Agregar")]
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

	[HttpDelete, Route("TipoGasto/Borrar")]
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

	[HttpPost, Route("TipoGasto/Actualizar")]
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
	[HttpGet, Route("TipoIngreso/{Activo?}")]
    public async Task<IActionResult> TipoIngreso(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoIngreso(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoIngreso/Agregar")]
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

	[HttpDelete, Route("TipoIngreso/Borrar")]
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

	[HttpPost, Route("TipoIngreso/Actualizar")]
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
	[HttpGet, Route("TipoPcs/{Activo?}")]
    public async Task<IActionResult> TipoPcs(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoPcs(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoPcs/Agregar")]
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

	[HttpDelete, Route("TipoPcs/Borrar")]
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

	[HttpPost, Route("TipoPcs/Actualizar")]
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

    #region Tipo Persona
    [HttpGet, Route("TipoPersona/{Activo?}")]
    public async Task<IActionResult> TipoPersona(bool? Activo)
    {
        var query = await _catalogoQueryService.GetTipoPersona(Activo);
        return Ok(query);
    }

    [HttpPut, Route("TipoPersona/Agregar")]
    public async Task<IActionResult> AddTipoPersona(AgregarTipoPersonaCommand TipoPersona)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(TipoPersona);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpDelete, Route("TipoPersona/Borrar")]
    public async Task<IActionResult> DeleteTipoPersona(EliminarTipoPersonaCommand TipoPersona)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(TipoPersona);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpPost, Route("TipoPersona/Actualizar")]
    public async Task<IActionResult> UpdateTipoPersona(ActualizarTipoPersonaCommand TipoPersona)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
        TipoPersona.Nombre = claimJWTModel.nombre;
        TipoPersona.Usuario = claimJWTModel.correo;
        TipoPersona.Roles = claimJWTModel.roles;
        TipoPersona.TransactionId = claimJWTModel.transactionId;
        TipoPersona.Rel = 24;
        var response = await _mediator.Send(TipoPersona);
        if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        return Ok(response);
    }

    #endregion Tipo Persona

    #region Tipo Poliza
    [HttpGet, Route("TipoPoliza/{Activo?}")]
    public async Task<IActionResult> TipoPoliza(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoPoliza(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoPoliza/Agregar")]
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

	[HttpDelete, Route("TipoPoliza/Borrar")]
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

	[HttpPost, Route("TipoPoliza/Actualizar")]
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
	[HttpGet, Route("TipoProyecto/{Activo?}")]
    public async Task<IActionResult> TipoProyecto(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoProyecto(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoProyecto/Agregar")]
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

	[HttpDelete, Route("TipoProyecto/Borrar")]
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

	[HttpPost, Route("TipoProyecto/Actualizar")]
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
	[HttpGet, Route("TipoResultado/{Activo?}")]
    public async Task<IActionResult> TipoResultado(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoResultado(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoResultado/Agregar")]
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

	[HttpDelete, Route("TipoResultado/Borrar")]
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

	[HttpPost, Route("TipoResultado/Actualizar")]
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
	[HttpGet, Route("TipoSangre/{Activo?}")]
    public async Task<IActionResult> TipoSangre(bool? Activo)
	{
		var query = await _catalogoQueryService.GetTipoSangre(Activo);
		return Ok(query);
	}

	[HttpPut, Route("TipoSangre/Agregar")]
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

	[HttpDelete, Route("TipoSangre/Borrar")]
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

	[HttpPost, Route("TipoSangre/Actualizar")]
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

    #region Turno
    [HttpGet, Route("Turno/{Activo?}")]
    public async Task<IActionResult> Turno(bool? Activo)
    {
        var query = await _catalogoQueryService.GetTurno(Activo);
        return Ok(query);
    }

    [HttpPut, Route("Turno/Agregar")]
    public async Task<IActionResult> Addturno(AgregarTurnoCommand Turno)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(Turno);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpDelete, Route("Turno/Borrar")]
    public async Task<IActionResult> DeleteTurno(EliminarTurnoCommand Turno)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var response = await _mediator.Send(Turno);
        if (!response.Success)
        {
            var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
            _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        }
        return Ok(response);
    }

    [HttpPost, Route("Turno/Actualizar")]
    public async Task<IActionResult> UpdateTurnoSangre(ActualizarTurnoCommand Turno)
    {
        if (!ModelState.IsValid) return BadRequest("Se requieren todos los valores del modelo");
        var claimJWTModel = new ClaimsJWT(TransactionId).GetClaimValues((HttpContext.User.Identity as ClaimsIdentity).Claims);
        Turno.Nombre = claimJWTModel.nombre;
        Turno.Usuario = claimJWTModel.correo;
        Turno.Roles = claimJWTModel.roles;
        Turno.TransactionId = claimJWTModel.transactionId;
        Turno.Rel = 32;
        var response = await _mediator.Send(Turno);
        if (!response.Success) _logger.LogInformation($"Datos de usuario: {JsonConvert.SerializeObject(claimJWTModel)}");
        return Ok(response);
    }

    #endregion Turno

    #region UnidadNegocio
    [HttpGet, Route("UnidadNegocio/{Activo?}")]
    public async Task<IActionResult> UnidadNegocio(bool? Activo)
	{
		var query = await _catalogoQueryService.GetUnidadNegocio(Activo);
		return Ok(query);
	}

	[HttpPut, Route("UnidadNegocio/Agregar")]
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

	[HttpDelete, Route("UnidadNegocio/Borrar")]
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

	[HttpPost, Route("UnidadNegocio/Actualizar")]
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
	[HttpGet, Route("Viatico/{Activo?}")]
    public async Task<IActionResult> Viatico(bool? Activo)
	{
		var query = await _catalogoQueryService.GetViatico(Activo);
		return Ok(query);
	}

	[HttpPut, Route("Viatico/Agregar")]
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

	[HttpDelete, Route("Viatico/Borrar")]
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

	[HttpPost, Route("Viatico/Actualizar")]
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

