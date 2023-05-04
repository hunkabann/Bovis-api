using Bovis.Common;
using Bovis.Service.Queries.Dto.Responses;

namespace Bovis.Service.Queries.Interface;

public interface ICatalogoQueryService : IDisposable
{
	Task<Response<List<Catalogo>>> GetBeneficio(bool? Activo);

	Task<Response<List<Catalogo>>> GetCategoria(bool? Activo);

	Task<Response<List<Catalogo>>> GetClasificacion(bool? Activo);

	Task<Response<List<Catalogo>>> GetCostoIndirectoSalarios(bool? Activo);

	Task<Response<List<Catalogo>>> GetDepartamento(bool? Activo);

	Task<Response<List<Catalogo>>> GetDocumento(bool? Activo);

	Task<Response<List<Catalogo>>> GetEdoCivil(bool? Activo);

	Task<Response<List<Catalogo>>> GetEstatusProyecto(bool? Activo);

	Task<Response<List<Catalogo>>> GetFormaPago(bool? Activo);

	Task<Response<List<Catalogo>>> GetGasto(bool? Activo);

	Task<Response<List<Catalogo>>> GetIngreso(bool? Activo);

	Task<Response<List<Catalogo>>> GetJornada(bool? Activo);

	Task<Response<List<Catalogo>>> GetModena(bool? Activo);

	Task<Response<List<Catalogo>>> GetNivelEstudios(bool? Activo);

	Task<Response<List<Catalogo>>> GetNivelPuesto(bool? Activo);

	Task<Response<List<Catalogo>>> GetPcs(bool? Activo);

	Task<Response<List<Catalogo>>> GetPrestacion(bool? Activo);

	Task<Response<List<Catalogo>>> GetPuesto(bool? Activo);

	Task<Response<List<Catalogo>>> GetRubroIngresoReembolsable(bool? Activo);

	Task<Response<List<Catalogo>>> GetSector(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoCie(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoContrato(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoCtaContable(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoCuenta(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoDocumento(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoEmpleado(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoFactura(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoGasto(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoIngreso(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoPcs(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoPoliza(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoProyecto(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoResultado(bool? Activo);

	Task<Response<List<Catalogo>>> GetTipoSangre(bool? Activo);

	Task<Response<List<Catalogo>>> GetUnidadNegocio(bool? Activo);

	Task<Response<List<Catalogo>>> GetViatico(bool? Activo);

}

