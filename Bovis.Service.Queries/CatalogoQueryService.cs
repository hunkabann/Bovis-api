using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;

namespace Bovis.Service.Queries;
public class CatalogoQueryService : ICatalogoQueryService
{
    private readonly ICatalogoBusiness _catalogoBusiness;

	private readonly IMapper _map;

    public CatalogoQueryService(IMapper _map, ICatalogoBusiness _catalogoBusiness)
    {
        this._map = _map;
        this._catalogoBusiness = _catalogoBusiness;
	}

	#region Catalogos
	public async Task<Response<List<Catalogo>>> GetBeneficio(bool? Activo)
	{
		var response = await _catalogoBusiness.GetBeneficio(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetCategoria(bool? Activo)
	{
		var response = await _catalogoBusiness.GetCategoria(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetClasificacion(bool? Activo)
	{
		var response = await _catalogoBusiness.GetClasificacion(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetCostoIndirectoSalarios(bool? Activo)
	{
		var response = await _catalogoBusiness.GetCostoIndirectoSalarios(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetDepartamento(bool? Activo)
	{
		var response = await _catalogoBusiness.GetDepartamento(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetDocumento(bool? Activo)
	{
		var response = await _catalogoBusiness.GetDocumento(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetEdoCivil(bool? Activo)
	{
		var response = await _catalogoBusiness.GetEdoCivil(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetEstatusProyecto(bool? Activo)
	{
		var response = await _catalogoBusiness.GetEstatusProyecto(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

    public async Task<Response<List<Catalogo>>> GetExperiencia(bool? Activo)
    {
        var response = await _catalogoBusiness.GetExperiencia(Activo);
        return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
    }

    public async Task<Response<List<Catalogo>>> GetFormaPago(bool? Activo)
	{
		var response = await _catalogoBusiness.GetFormaPago(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetGasto(bool? Activo)
	{
		var response = await _catalogoBusiness.GetGasto(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

    public async Task<Response<List<Catalogo>>> GetHabilidad(bool? Activo)
    {
        var response = await _catalogoBusiness.GetHabilidad(Activo);
        return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
    }

    public async Task<Response<List<Catalogo>>> GetIngreso(bool? Activo)
	{
		var response = await _catalogoBusiness.GetIngreso(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetJornada(bool? Activo)
	{
		var response = await _catalogoBusiness.GetJornada(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetModena(bool? Activo)
	{
		var response = await _catalogoBusiness.GetModena(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetNivelEstudios(bool? Activo)
	{
		var response = await _catalogoBusiness.GetNivelEstudios(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetNivelPuesto(bool? Activo)
	{
		var response = await _catalogoBusiness.GetNivelPuesto(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetPcs(bool? Activo)
	{
		var response = await _catalogoBusiness.GetPcs(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetPrestacion(bool? Activo)
	{
		var response = await _catalogoBusiness.GetPrestacion(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

    public async Task<Response<List<Catalogo>>> GetProfesion(bool? Activo)
    {
        var response = await _catalogoBusiness.GetProfesion(Activo);
        return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
    }

    public async Task<Response<List<Catalogo>>> GetPuesto(bool? Activo)
	{
		var response = await _catalogoBusiness.GetPuesto(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetRubroIngresoReembolsable(bool? Activo)
	{
		var response = await _catalogoBusiness.GetRubroIngresoReembolsable(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetSector(bool? Activo)
	{
		var response = await _catalogoBusiness.GetSector(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

    public async Task<Response<List<Catalogo>>> GetSexo(bool? Activo)
    {
        var response = await _catalogoBusiness.GetSexo(Activo);
        return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
    }

    public async Task<Response<List<Catalogo>>> GetTipoCie(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoCie(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoContrato(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoContrato(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoCtaContable(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoCtaContable(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoCuenta(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoCuenta(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoDocumento(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoDocumento(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoEmpleado(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoEmpleado(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoFactura(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoFactura(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoGasto(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoGasto(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoIngreso(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoIngreso(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoPcs(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoPcs(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoPoliza(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoPoliza(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoProyecto(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoProyecto(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoResultado(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoResultado(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetTipoSangre(bool? Activo)
	{
		var response = await _catalogoBusiness.GetTipoSangre(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetUnidadNegocio(bool? Activo)
	{
		var response = await _catalogoBusiness.GetUnidadNegocio(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

	public async Task<Response<List<Catalogo>>> GetViatico(bool? Activo)
	{
		var response = await _catalogoBusiness.GetViatico(Activo);
		return new Response<List<Catalogo>> { Data = _map.Map<List<Catalogo>>(response), Success = true };
	}

    #endregion


	#region Destructor

	public void Dispose()
    {
        GC.SuppressFinalize(this);
        GC.Collect();
    }

    #endregion
}

