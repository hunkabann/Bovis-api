using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Business.Interface
{
	public interface ICatalogoBusiness : IDisposable
	{
		#region Beneficio
		Task<List<TB_Cat_Beneficio>> GetBeneficio(bool? activo);
		Task<(bool Success, string Message)> AddBeneficio(TB_Cat_Beneficio beneficio);
		Task<(bool Success, string Message)> UpdateBeneficio(InsertMovApi MovAPI, TB_Cat_Beneficio beneficio);
		Task<(bool Success, string Message)> DeleteBeneficio(TB_Cat_Beneficio beneficio);

		#endregion

		#region Categoria

		Task<List<TB_Cat_Categoria>> GetCategoria(bool? activo);
		Task<(bool Success, string Message)> AddCategoria(TB_Cat_Categoria categoria);
		Task<(bool Success, string Message)> UpdateCategoria(InsertMovApi MovAPI, TB_Cat_Categoria categoria);
		Task<(bool Success, string Message)> DeleteCategoria(TB_Cat_Categoria categoria);

		#endregion

		#region Clasificacion

		Task<List<TB_Cat_Clasificacion>> GetClasificacion(bool? activo);
		Task<(bool Success, string Message)> AddClasificacion(TB_Cat_Clasificacion clasificacion);
		Task<(bool Success, string Message)> UpdateClasificacion(InsertMovApi MovAPI, TB_Cat_Clasificacion clasificacion);
		Task<(bool Success, string Message)> DeleteClasificacion(TB_Cat_Clasificacion clasificacion);

        #endregion

        #region Cliente

        Task<List<TB_Cliente>> GetCliente(bool? activo);
		Task<(bool Success, string Message)> AddCliente(JsonObject registro);
		Task<(bool Success, string Message)> UpdateCliente(JsonObject registro);
		Task<(bool Success, string Message)> DeleteCliente(int idCliente);

        #endregion Cliente

        #region Costo Indirecto Salarios

        Task<List<TB_Cat_CostoIndirectoSalarios>> GetCostoIndirectoSalarios(bool? activo);
		Task<(bool Success, string Message)> AddCostoIndirectoSalarios(TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios);
		Task<(bool Success, string Message)> UpdateCostoIndirectoSalarios(InsertMovApi MovAPI, TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios);
		Task<(bool Success, string Message)> DeleteCostoIndirectoSalarios(TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios);

		#endregion

		#region Departamento

		Task<List<TB_Cat_Departamento>> GetDepartamento(bool? activo);
		Task<(bool Success, string Message)> AddDepartamento(TB_Cat_Departamento departamento);
		Task<(bool Success, string Message)> UpdateDepartamento(InsertMovApi MovAPI, TB_Cat_Departamento departamento);
		Task<(bool Success, string Message)> DeleteDepartamento(TB_Cat_Departamento Departamento);

		#endregion

		#region Documento

		Task<List<TB_Cat_Documento>> GetDocumento(bool? activo);
		Task<(bool Success, string Message)> AddDocumento(TB_Cat_Documento documento);
		Task<(bool Success, string Message)> UpdateDocumento(InsertMovApi MovAPI, TB_Cat_Documento documento);
		Task<(bool Success, string Message)> DeleteDocumento(TB_Cat_Documento documento);

        #endregion

        #region Estado
        Task<List<TB_Estado>> GetEdo(bool? activo);
        Task<(bool Success, string Message)> AddEdo(TB_Estado edo);
        Task<(bool Success, string Message)> UpdateEdo(InsertMovApi MovAPI, TB_Estado edo);
        Task<(bool Success, string Message)> DeleteEdo(TB_Estado edo);

        #endregion Estado

        #region Estado Civil

        Task<List<TB_Cat_EdoCivil>> GetEdoCivil(bool? activo);
		Task<(bool Success, string Message)> AddEdoCivil(TB_Cat_EdoCivil edoCivil);
		Task<(bool Success, string Message)> UpdateEdoCivil(InsertMovApi MovAPI, TB_Cat_EdoCivil edoCivil);
		Task<(bool Success, string Message)> DeleteEdoCivil(TB_Cat_EdoCivil edoCivil);

		#endregion

		#region Estatus Proyecto

		Task<List<TB_Cat_EstatusProyecto>> GetEstatusProyecto(bool? activo);
		Task<(bool Success, string Message)> AddEstatusProyecto(TB_Cat_EstatusProyecto estatusProyecto);
		Task<(bool Success, string Message)> UpdateEstatusProyecto(InsertMovApi MovAPI, TB_Cat_EstatusProyecto estatusProyecto);
		Task<(bool Success, string Message)> DeleteEstatusProyecto(TB_Cat_EstatusProyecto estatusProyecto);

        #endregion

        #region Experiencia
        Task<List<TB_Cat_Experiencia>> GetExperiencia(bool? activo);
        Task<(bool Success, string Message)> AddExperiencia(TB_Cat_Experiencia experiencia);
        Task<(bool Success, string Message)> UpdateExperiencia(InsertMovApi MovAPI, TB_Cat_Experiencia experiencia);
        Task<(bool Success, string Message)> DeleteExperiencia(TB_Cat_Experiencia experiencia);
        #endregion Experiencia

        #region Forma Pago

        Task<List<TB_Cat_FormaPago>> GetFormaPago(bool? activo);
		Task<(bool Success, string Message)> AddFormaPago(TB_Cat_FormaPago formaPago);
		Task<(bool Success, string Message)> UpdateFormaPago(InsertMovApi MovAPI, TB_Cat_FormaPago formaPago);
		Task<(bool Success, string Message)> DeleteFormaPago(TB_Cat_FormaPago formaPago);

		#endregion

		#region Gasto

		Task<List<TB_Cat_Gasto>> GetGasto(bool? activo);
		Task<(bool Success, string Message)> AddGasto(TB_Cat_Gasto gasto);
		Task<(bool Success, string Message)> UpdateGasto(InsertMovApi MovAPI, TB_Cat_Gasto gasto);
		Task<(bool Success, string Message)> DeleteGasto(TB_Cat_Gasto gasto);

        #endregion

        #region Habilidad
        Task<List<TB_Cat_Habilidad>> GetHabilidad(bool? activo);
        Task<(bool Success, string Message)> AddHabilidad(TB_Cat_Habilidad habilidad);
        Task<(bool Success, string Message)> UpdateHabilidad(InsertMovApi MovAPI, TB_Cat_Habilidad habilidad);
        Task<(bool Success, string Message)> DeleteHabilidad(TB_Cat_Habilidad habilidad);
        #endregion Habilidad

        #region Ingreso

        Task<List<TB_Cat_Ingreso>> GetIngreso(bool? activo);
		Task<(bool Success, string Message)> AddIngreso(TB_Cat_Ingreso ingreso);
		Task<(bool Success, string Message)> UpdateIngreso(InsertMovApi MovAPI, TB_Cat_Ingreso ingreso);
		Task<(bool Success, string Message)> DeleteIngreso(TB_Cat_Ingreso ingreso);

		#endregion

		#region Jornada

		Task<List<TB_Cat_Jornada>> GetJornada(bool? activo);
		Task<(bool Success, string Message)> AddJornada(TB_Cat_Jornada jornada);
		Task<(bool Success, string Message)> UpdateJornada(InsertMovApi MovAPI, TB_Cat_Jornada jornada);
		Task<(bool Success, string Message)> DeleteJornada(TB_Cat_Jornada jornada);

		#endregion

		#region Modena

		Task<List<TB_Cat_Moneda>> GetModena(bool? activo);
		Task<(bool Success, string Message)> AddModena(TB_Cat_Moneda modena);
		Task<(bool Success, string Message)> UpdateModena(InsertMovApi MovAPI, TB_Cat_Moneda modena);
		Task<(bool Success, string Message)> DeleteModena(TB_Cat_Moneda modena);

		#endregion

		#region Nivel Estudios

		Task<List<TB_Cat_NivelEstudios>> GetNivelEstudios(bool? activo);
		Task<(bool Success, string Message)> AddNivelEstudios(TB_Cat_NivelEstudios nivelEstudios);
		Task<(bool Success, string Message)> UpdateNivelEstudios(InsertMovApi MovAPI, TB_Cat_NivelEstudios nivelEstudios);
		Task<(bool Success, string Message)> DeleteNivelEstudios(TB_Cat_NivelEstudios nivelEstudios);

		#endregion

		#region Nivel Puesto

		Task<List<TB_Cat_NivelPuesto>> GetNivelPuesto(bool? activo);
		Task<(bool Success, string Message)> AddNivelPuesto(TB_Cat_NivelPuesto nivelPuesto);
		Task<(bool Success, string Message)> UpdateNivelPuesto(InsertMovApi MovAPI, TB_Cat_NivelPuesto nivelPuesto);
		Task<(bool Success, string Message)> DeleteNivelPuesto(TB_Cat_NivelPuesto nivelPuesto);

        #endregion

        #region Pais

        Task<List<TB_Pais>> GetPais(bool? activo);
        Task<(bool Success, string Message)> AddPais(TB_Pais Pais);
        Task<(bool Success, string Message)> UpdatePais(InsertMovApi MovAPI, TB_Pais Pais);
        Task<(bool Success, string Message)> DeletePais(TB_Pais Pais);

        #endregion Pais

        #region Pcs

        Task<List<TB_Cat_Pcs>> GetPcs(bool? activo);
		Task<(bool Success, string Message)> AddPcs(TB_Cat_Pcs pcs);
		Task<(bool Success, string Message)> UpdatePcs(InsertMovApi MovAPI, TB_Cat_Pcs pcs);
		Task<(bool Success, string Message)> DeletePcs(TB_Cat_Pcs pcs);

		#endregion

		#region Prestacion

		Task<List<TB_Cat_Prestacion>> GetPrestacion(bool? activo);
		Task<(bool Success, string Message)> AddPrestacion(TB_Cat_Prestacion prestacion);
		Task<(bool Success, string Message)> UpdatePrestacion(InsertMovApi MovAPI, TB_Cat_Prestacion prestacion);
		Task<(bool Success, string Message)> DeletePrestacion(TB_Cat_Prestacion prestacion);

        #endregion

        #region Profesion
        Task<List<TB_Cat_Profesion>> GetProfesion(bool? activo);
        Task<(bool Success, string Message)> AddProfesion(TB_Cat_Profesion profesion);
        Task<(bool Success, string Message)> UpdateProfesion(InsertMovApi MovAPI, TB_Cat_Profesion profesion);
        Task<(bool Success, string Message)> DeleteProfesion(TB_Cat_Profesion profesion);
        #endregion Profesion

        #region Puesto

        Task<List<Puesto_Detalle>> GetPuesto(bool? activo);
		Task<(bool Success, string Message)> AddPuesto(JsonObject registro);
		Task<(bool Success, string Message)> UpdatePuesto(JsonObject registro);
		Task<(bool Success, string Message)> DeletePuesto(int nukid_puesto);

        #endregion

        #region Rubro Ingreso Reembolsable

        Task<List<TB_Cat_RubroIngresoReembolsable>> GetRubroIngresoReembolsable(bool? activo);
		Task<(bool Success, string Message)> AddRubroIngresoReembolsable(TB_Cat_RubroIngresoReembolsable rubro);
		Task<(bool Success, string Message)> UpdateRubroIngresoReembolsable(InsertMovApi MovAPI, TB_Cat_RubroIngresoReembolsable rubro);
		Task<(bool Success, string Message)> DeleteRubroIngresoReembolsable(TB_Cat_RubroIngresoReembolsable rubro);

		#endregion

		#region Sector

		Task<List<TB_Cat_Sector>> GetSector(bool? activo);
		Task<(bool Success, string Message)> AddSector(TB_Cat_Sector sector);
		Task<(bool Success, string Message)> UpdateSector(InsertMovApi MovAPI, TB_Cat_Sector sector);
		Task<(bool Success, string Message)> DeleteSector(TB_Cat_Sector sector);

        #endregion

        #region Sexo
        Task<List<TB_Cat_Sexo>> GetSexo(bool? activo);
        Task<(bool Success, string Message)> AddSexo(TB_Cat_Sexo Sexo);
        Task<(bool Success, string Message)> UpdateSexo(InsertMovApi MovAPI, TB_Cat_Sexo Sexo);
        Task<(bool Success, string Message)> DeleteSexo(TB_Cat_Sexo Sexo);

        #endregion

        #region Tipo Cie

        Task<List<TB_Cat_TipoCie>> GetTipoCie(bool? activo);
		Task<(bool Success, string Message)> AddTipoCie(TB_Cat_TipoCie tipoCie);
		Task<(bool Success, string Message)> UpdateTipoCie(InsertMovApi MovAPI, TB_Cat_TipoCie tipoCie);
		Task<(bool Success, string Message)> DeleteTipoCie(TB_Cat_TipoCie tipoCie);

		#endregion

		#region Tipo Contrato

		Task<List<TipoContrato_Detalle>> GetTipoContrato(bool? activo);
		Task<(bool Success, string Message)> AddTipoContrato(TB_Cat_TipoContrato tipoContrato);
		Task<(bool Success, string Message)> UpdateTipoContrato(InsertMovApi MovAPI, TB_Cat_TipoContrato tipoContrato);
		Task<(bool Success, string Message)> DeleteTipoContrato(TB_Cat_TipoContrato tipoContrato);

		#endregion

		#region Tipo Cta Contable

		Task<List<TB_Cat_TipoCtaContable>> GetTipoCtaContable(bool? activo);
		Task<(bool Success, string Message)> AddTipoCtaContable(TB_Cat_TipoCtaContable tipoCtaContable);
		Task<(bool Success, string Message)> UpdateTipoCtaContable(InsertMovApi MovAPI, TB_Cat_TipoCtaContable tipoCtaContable);
		Task<(bool Success, string Message)> DeleteTipoCtaContable(TB_Cat_TipoCtaContable tipoCtaContable);

		#endregion

		#region Tipo Cuenta

		Task<List<TB_Cat_TipoCuenta>> GetTipoCuenta(bool? activo);
		Task<(bool Success, string Message)> AddTipoCuenta(TB_Cat_TipoCuenta tipoCuenta);
		Task<(bool Success, string Message)> UpdateTipoCuenta(InsertMovApi MovAPI, TB_Cat_TipoCuenta tipoCuenta);
		Task<(bool Success, string Message)> DeleteTipoCuenta(TB_Cat_TipoCuenta tipoCuenta);

		#endregion

		#region Tipo Documento

		Task<List<TB_Cat_TipoDocumento>> GetTipoDocumento(bool? activo);
		Task<(bool Success, string Message)> AddTipoDocumento(TB_Cat_TipoDocumento tipoDocumento);
		Task<(bool Success, string Message)> UpdateTipoDocumento(InsertMovApi MovAPI, TB_Cat_TipoDocumento tipoDocumento);
		Task<(bool Success, string Message)> DeleteTipoDocumento(TB_Cat_TipoDocumento tipoDocumento);

		#endregion

		#region Tipo Empleado

		Task<List<TB_Cat_TipoEmpleado>> GetTipoEmpleado(bool? activo);
		Task<(bool Success, string Message)> AddTipoEmpleado(TB_Cat_TipoEmpleado tipoEmpleado);
		Task<(bool Success, string Message)> UpdateTipoEmpleado(InsertMovApi MovAPI, TB_Cat_TipoEmpleado tipoEmpleado);
		Task<(bool Success, string Message)> DeleteTipoEmpleado(TB_Cat_TipoEmpleado tipoEmpleado);

		#endregion

		#region Tipo Factura

		Task<List<TB_Cat_TipoFactura>> GetTipoFactura(bool? activo);
		Task<(bool Success, string Message)> AddTipoFactura(TB_Cat_TipoFactura tipoFactura);
		Task<(bool Success, string Message)> UpdateTipoFactura(InsertMovApi MovAPI, TB_Cat_TipoFactura tipoFactura);
		Task<(bool Success, string Message)> DeleteTipoFactura(TB_Cat_TipoFactura tipoFactura);

		#endregion

		#region Tipo Gasto

		Task<List<TB_Cat_TipoGasto>> GetTipoGasto(bool? activo);
		Task<(bool Success, string Message)> AddTipoGasto(TB_Cat_TipoGasto tipoGasto);
		Task<(bool Success, string Message)> UpdateTipoGasto(InsertMovApi MovAPI, TB_Cat_TipoGasto tipoGasto);
		Task<(bool Success, string Message)> DeleteTipoGasto(TB_Cat_TipoGasto tipoGasto);

		#endregion

		#region Tipo Ingreso

		Task<List<TB_Cat_TipoIngreso>> GetTipoIngreso(bool? activo);
		Task<(bool Success, string Message)> AddTipoIngreso(TB_Cat_TipoIngreso tipoIngreso);
		Task<(bool Success, string Message)> UpdateTipoIngreso(InsertMovApi MovAPI, TB_Cat_TipoIngreso tipoIngreso);
		Task<(bool Success, string Message)> DeleteTipoIngreso(TB_Cat_TipoIngreso tipoIngreso);

		#endregion

		#region Tipo Pcs

		Task<List<TB_Cat_TipoPcs>> GetTipoPcs(bool? activo);
		Task<(bool Success, string Message)> AddTipoPcs(TB_Cat_TipoPcs tipoPcs);
		Task<(bool Success, string Message)> UpdateTipoPcs(InsertMovApi MovAPI, TB_Cat_TipoPcs tipoPcs);
		Task<(bool Success, string Message)> DeleteTipoPcs(TB_Cat_TipoPcs tipoPcs);

        #endregion

        #region Tipo Persona
        Task<List<TB_Cat_TipoPersona>> GetTipoPersona(bool? activo);
        Task<(bool Success, string Message)> AddTipoPersona(TB_Cat_TipoPersona tipoPersona);
        Task<(bool Success, string Message)> UpdateTipoPersona(InsertMovApi MovAPI, TB_Cat_TipoPersona tipoPersona);
        Task<(bool Success, string Message)> DeleteTipoPersona(TB_Cat_TipoPersona tipoPersona);

        #endregion Tipo Persona

        #region Tipo Poliza

        Task<List<TB_Cat_TipoPoliza>> GetTipoPoliza(bool? activo);
		Task<(bool Success, string Message)> AddTipoPoliza(TB_Cat_TipoPoliza tipoPoliza);
		Task<(bool Success, string Message)> UpdateTipoPoliza(InsertMovApi MovAPI, TB_Cat_TipoPoliza tipoPoliza);
		Task<(bool Success, string Message)> DeleteTipoPoliza(TB_Cat_TipoPoliza tipoPoliza);

		#endregion

		#region Tipo Proyecto

		Task<List<TB_Cat_TipoProyecto>> GetTipoProyecto(bool? activo);
		Task<(bool Success, string Message)> AddTipoProyecto(TB_Cat_TipoProyecto tipoProyecto);
		Task<(bool Success, string Message)> UpdateTipoProyecto(InsertMovApi MovAPI, TB_Cat_TipoProyecto tipoProyecto);
		Task<(bool Success, string Message)> DeleteTipoProyecto(TB_Cat_TipoProyecto tipoProyecto);

		#endregion

		#region Tipo Resultado

		Task<List<TB_Cat_TipoResultado>> GetTipoResultado(bool? activo);
		Task<(bool Success, string Message)> AddTipoResultado(TB_Cat_TipoResultado tipoResultado);
		Task<(bool Success, string Message)> UpdateTipoResultado(InsertMovApi MovAPI, TB_Cat_TipoResultado tipoResultado);
		Task<(bool Success, string Message)> DeleteTipoResultado(TB_Cat_TipoResultado tipoResultado);

		#endregion

		#region Tipo Sangre

		Task<List<TB_Cat_TipoSangre>> GetTipoSangre(bool? activo);
		Task<(bool Success, string Message)> AddTipoSangre(TB_Cat_TipoSangre tipoSangre);
		Task<(bool Success, string Message)> UpdateTipoSangre(InsertMovApi MovAPI, TB_Cat_TipoSangre tipoSangre);
		Task<(bool Success, string Message)> DeleteTipoSangre(TB_Cat_TipoSangre tipoSangre);

        #endregion

        #region Turno
        Task<List<TB_Cat_Turno>> GetTurno(bool? activo);
        Task<(bool Success, string Message)> AddTurno(TB_Cat_Turno Turno);
        Task<(bool Success, string Message)> UpdateTurno(InsertMovApi MovAPI, TB_Cat_Turno Turno);
        Task<(bool Success, string Message)> DeleteTurno(TB_Cat_Turno Turno);

        #endregion Turno

        #region Unidad Negocio

        Task<List<TB_Cat_UnidadNegocio>> GetUnidadNegocio(bool? activo);
		Task<(bool Success, string Message)> AddUnidadNegocio(TB_Cat_UnidadNegocio unidadNegocio);
		Task<(bool Success, string Message)> UpdateUnidadNegocio(InsertMovApi MovAPI, TB_Cat_UnidadNegocio unidadNegocio);
		Task<(bool Success, string Message)> DeleteUnidadNegocio(TB_Cat_UnidadNegocio unidadNegocio);

		#endregion

		#region Vistico

		Task<List<TB_Cat_Viatico>> GetViatico(bool? activo);
		Task<(bool Success, string Message)> AddViatico(TB_Cat_Viatico viatico);
		Task<(bool Success, string Message)> DeleteViatico(TB_Cat_Viatico viatico);
		Task<(bool Success, string Message)> UpdateViatico(InsertMovApi MovAPI, TB_Cat_Viatico viatico);

		#endregion
	}
}
