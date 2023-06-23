using Bovis.Common.Model.Tables;

namespace Bovis.Data.Interface
{
	public interface ICatalogoData : IDisposable
	{
		#region Beneficio

		Task<List<TB_Cat_Beneficio>> GetBeneficio(bool? activo);
		Task<bool> AddBeneficio(TB_Cat_Beneficio beneficio);
		Task<bool> UpdateBeneficio(TB_Cat_Beneficio beneficio);
		Task<bool> DeleteBeneficio(TB_Cat_Beneficio beneficio);

		#endregion

		#region Categoria

		Task<List<TB_Cat_Categoria>> GetCategoria(bool? activo);
		Task<bool> AddCategoria(TB_Cat_Categoria categoria);
		Task<bool> UpdateCategoria(TB_Cat_Categoria categoria);
		Task<bool> DeleteCategoria(TB_Cat_Categoria categoria);

		#endregion

		#region Clsificacion

		Task<List<TB_Cat_Clasificacion>> GetClasificacion(bool? activo);
		Task<bool> AddClasificacion(TB_Cat_Clasificacion clasificacion);
		Task<bool> UpdateClasificacion(TB_Cat_Clasificacion clasificacion);
		Task<bool> DeleteClasificacion(TB_Cat_Clasificacion clasificacion);

		#endregion

		#region Costo Indirecto Salarios

		Task<List<TB_Cat_CostoIndirectoSalarios>> GetCostoIndirectoSalarios(bool? activo);
		Task<bool> AddCostoIndirectoSalarios(TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios);
		Task<bool> UpdateCostoIndirectoSalarios(TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios);
		Task<bool> DeleteCostoIndirectoSalarios(TB_Cat_CostoIndirectoSalarios costoIndirectoSalarios);

		#endregion

		#region Departamento

		Task<List<TB_Cat_Departamento>> GetDepartamento(bool? activo);
		Task<bool> AddDepartamento(TB_Cat_Departamento departamento);
		Task<bool> UpdateDepartamento(TB_Cat_Departamento departamento);
		Task<bool> DeleteDepartamento(TB_Cat_Departamento Departamento);

		#endregion

		#region Documento

		Task<List<TB_Cat_Documento>> GetDocumento(bool? activo);
		Task<bool> AddDocumento(TB_Cat_Documento documento);
		Task<bool> UpdateDocumento(TB_Cat_Documento documento);
		Task<bool> DeleteDocumento(TB_Cat_Documento documento);

		#endregion

		#region Estado Civil

		Task<List<TB_Cat_EdoCivil>> GetEdoCivil(bool? activo);
		Task<bool> AddEdoCivil(TB_Cat_EdoCivil edoCivil);
		Task<bool> UpdateEdoCivil(TB_Cat_EdoCivil edoCivil);
		Task<bool> DeleteEdoCivil(TB_Cat_EdoCivil edoCivil);

		#endregion

		#region Estatus Proyecto

		Task<List<TB_Cat_EstatusProyecto>> GetEstatusProyecto(bool? activo);
		Task<bool> AddEstatusProyecto(TB_Cat_EstatusProyecto estatusProyecto);
		Task<bool> UpdateEstatusProyecto(TB_Cat_EstatusProyecto estatusProyecto);
		Task<bool> DeleteEstatusProyecto(TB_Cat_EstatusProyecto estatusProyecto);

        #endregion

        #region Experiencia
        Task<List<TB_Cat_Experiencia>> GetExperiencia(bool? activo);
        Task<bool> AddExperiencia(TB_Cat_Experiencia experiencia);
        Task<bool> UpdateExperiencia(TB_Cat_Experiencia experiencia);
        Task<bool> DeleteExperiencia(TB_Cat_Experiencia experiencia);
        #endregion Experiencia

        #region Forma Pago

        Task<List<TB_Cat_FormaPago>> GetFormaPago(bool? activo);
		Task<bool> AddFormaPago(TB_Cat_FormaPago formaPago);
		Task<bool> UpdateFormaPago(TB_Cat_FormaPago formaPago);
		Task<bool> DeleteFormaPago(TB_Cat_FormaPago formaPago);

		#endregion

		#region Gasto

		Task<List<TB_Cat_Gasto>> GetGasto(bool? activo);
		Task<bool> AddGasto(TB_Cat_Gasto gasto);
		Task<bool> UpdateGasto(TB_Cat_Gasto gasto);
		Task<bool> DeleteGasto(TB_Cat_Gasto gasto);

        #endregion

        #region Habilidad
        Task<List<TB_Cat_Habilidad>> GetHabilidad(bool? activo);
        Task<bool> AddHabilidad(TB_Cat_Habilidad habilidad);
        Task<bool> UpdateHabilidad(TB_Cat_Habilidad habilidad);
        Task<bool> DeleteHabilidad(TB_Cat_Habilidad habilidad);
        #endregion Habilidad

        #region Ingreso

        Task<List<TB_Cat_Ingreso>> GetIngreso(bool? activo);
		Task<bool> AddIngreso(TB_Cat_Ingreso ingreso);
		Task<bool> UpdateIngreso(TB_Cat_Ingreso ingreso);
		Task<bool> DeleteIngreso(TB_Cat_Ingreso ingreso);

		#endregion

		#region Jornada

		Task<List<TB_Cat_Jornada>> GetJornada(bool? activo);
		Task<bool> AddJornada(TB_Cat_Jornada jornada);
		Task<bool> UpdateJornada(TB_Cat_Jornada jornada);
		Task<bool> DeleteJornada(TB_Cat_Jornada jornada);

		#endregion

		#region Modena

		Task<List<TB_Cat_Modena>> GetModena(bool? activo);
		Task<bool> AddModena(TB_Cat_Modena modena);
		Task<bool> UpdateModena(TB_Cat_Modena modena);
		Task<bool> DeleteModena(TB_Cat_Modena modena);

		#endregion

		#region Nivel Estudios

		Task<List<TB_Cat_NivelEstudios>> GetNivelEstudios(bool? activo);
		Task<bool> AddNivelEstudios(TB_Cat_NivelEstudios nivelEstudios);
		Task<bool> UpdateNivelEstudios(TB_Cat_NivelEstudios nivelEstudios);
		Task<bool> DeleteNivelEstudios(TB_Cat_NivelEstudios nivelEstudios);

		#endregion

		#region Nivel Puesto

		Task<List<TB_Cat_NivelPuesto>> GetNivelPuesto(bool? activo);
		Task<bool> AddNivelPuesto(TB_Cat_NivelPuesto nivelPuesto);
		Task<bool> UpdateNivelPuesto(TB_Cat_NivelPuesto nivelPuesto);
		Task<bool> DeleteNivelPuesto(TB_Cat_NivelPuesto nivelPuesto);

		#endregion

		#region Pcs

		Task<List<TB_Cat_Pcs>> GetPcs(bool? activo);
		Task<bool> AddPcs(TB_Cat_Pcs pcs);
		Task<bool> UpdatePcs(TB_Cat_Pcs pcs);
		Task<bool> DeletePcs(TB_Cat_Pcs pcs);

		#endregion

		#region Prestacion

		Task<List<TB_Cat_Prestacion>> GetPrestacion(bool? activo);
		Task<bool> AddPrestacion(TB_Cat_Prestacion prestacion);
		Task<bool> UpdatePrestacion(TB_Cat_Prestacion prestacion);
		Task<bool> DeletePrestacion(TB_Cat_Prestacion prestacion);

        #endregion

        #region Profesion
        Task<List<TB_Cat_Profesion>> GetProfesion(bool? activo);
        Task<bool> AddProfesion(TB_Cat_Profesion profesion);
        Task<bool> UpdateProfesion(TB_Cat_Profesion profesion);
        Task<bool> DeleteProfesion(TB_Cat_Profesion profesion);
        #endregion Profesion

        #region Puesto

        Task<List<TB_Cat_Puesto>> GetPuesto(bool? activo);
		Task<bool> AddPuesto(TB_Cat_Puesto puesto);
		Task<bool> UpdatePuesto(TB_Cat_Puesto puesto);
		Task<bool> DeletePuesto(TB_Cat_Puesto puesto);

		#endregion

		#region Rubro Ingreso Reembolsable

		Task<List<TB_Cat_RubroIngresoReembolsable>> GetRubroIngresoReembolsable(bool? activo);
		Task<bool> AddRubroIngresoReembolsable(TB_Cat_RubroIngresoReembolsable rubro);
		Task<bool> UpdateRubroIngresoReembolsable(TB_Cat_RubroIngresoReembolsable rubro);
		Task<bool> DeleteRubroIngresoReembolsable(TB_Cat_RubroIngresoReembolsable rubro);

		#endregion

		#region Sector

		Task<List<TB_Cat_Sector>> GetSector(bool? activo);
		Task<bool> AddSector(TB_Cat_Sector sector);
		Task<bool> UpdateSector(TB_Cat_Sector sector);
		Task<bool> DeleteSector(TB_Cat_Sector sector);

        #endregion

        #region Sexo

        Task<List<TB_Cat_Sexo>> GetSexo(bool? activo);
        Task<bool> AddSexo(TB_Cat_Sexo Sexo);
        Task<bool> UpdateSexo(TB_Cat_Sexo Sexo);
        Task<bool> DeleteSexo(TB_Cat_Sexo Sexo);

        #endregion Sexo

        #region Tipo Cie

        Task<List<TB_Cat_TipoCie>> GetTipoCie(bool? activo);
		Task<bool> AddTipoCie(TB_Cat_TipoCie tipoCie);
		Task<bool> UpdateTipoCie(TB_Cat_TipoCie tipoCie);
		Task<bool> DeleteTipoCie(TB_Cat_TipoCie tipoCie);

		#endregion

		#region Tipo Contrato

		Task<List<TB_Cat_TipoContrato>> GetTipoContrato(bool? activo);
		Task<bool> AddTipoContrato(TB_Cat_TipoContrato tipoContrato);
		Task<bool> UpdateTipoContrato(TB_Cat_TipoContrato tipoContrato);
		Task<bool> DeleteTipoContrato(TB_Cat_TipoContrato tipoContrato);

		#endregion

		#region Tipo Cta Contable

		Task<List<TB_Cat_TipoCtaContable>> GetTipoCtaContable(bool? activo);
		Task<bool> AddTipoCtaContable(TB_Cat_TipoCtaContable tipoCtaContable);
		Task<bool> UpdateTipoCtaContable(TB_Cat_TipoCtaContable tipoCtaContable);
		Task<bool> DeleteTipoCtaContable(TB_Cat_TipoCtaContable tipoCtaContable);

		#endregion

		#region Tipo Cuenta

		Task<List<TB_Cat_TipoCuenta>> GetTipoCuenta(bool? activo);
		Task<bool> AddTipoCuenta(TB_Cat_TipoCuenta tipoCuenta);
		Task<bool> UpdateTipoCuenta(TB_Cat_TipoCuenta tipoCuenta);
		Task<bool> DeleteTipoCuenta(TB_Cat_TipoCuenta tipoCuenta);

		#endregion

		#region Tipo Documento

		Task<List<TB_Cat_TipoDocumento>> GetTipoDocumento(bool? activo);
		Task<bool> AddTipoDocumento(TB_Cat_TipoDocumento tipoDocumento);
		Task<bool> UpdateTipoDocumento(TB_Cat_TipoDocumento tipoDocumento);
		Task<bool> DeleteTipoDocumento(TB_Cat_TipoDocumento tipoDocumento);

		#endregion

		#region Tipo Empleado

		Task<List<TB_Cat_TipoEmpleado>> GetTipoEmpleado(bool? activo);
		Task<bool> AddTipoEmpleado(TB_Cat_TipoEmpleado tipoEmpleado);
		Task<bool> UpdateTipoEmpleado(TB_Cat_TipoEmpleado tipoEmpleado);
		Task<bool> DeleteTipoEmpleado(TB_Cat_TipoEmpleado tipoEmpleado);

		#endregion

		#region Tipo Factura

		Task<List<TB_Cat_TipoFactura>> GetTipoFactura(bool? activo);
		Task<bool> AddTipoFactura(TB_Cat_TipoFactura tipoFactura);
		Task<bool> UpdateTipoFactura(TB_Cat_TipoFactura tipoFactura);
		Task<bool> DeleteTipoFactura(TB_Cat_TipoFactura tipoFactura);

		#endregion

		#region Tipo Gasto

		Task<List<TB_Cat_TipoGasto>> GetTipoGasto(bool? activo);
		Task<bool> AddTipoGasto(TB_Cat_TipoGasto tipoGasto);
		Task<bool> UpdateTipoGasto(TB_Cat_TipoGasto tipoGasto);
		Task<bool> DeleteTipoGasto(TB_Cat_TipoGasto tipoGasto);

		#endregion

		#region Tipo Ingreso

		Task<List<TB_Cat_TipoIngreso>> GetTipoIngreso(bool? activo);
		Task<bool> AddTipoIngreso(TB_Cat_TipoIngreso tipoIngreso);
		Task<bool> UpdateTipoIngreso(TB_Cat_TipoIngreso tipoIngreso);
		Task<bool> DeleteTipoIngreso(TB_Cat_TipoIngreso tipoIngreso);

		#endregion

		#region Tipo Pcs

		Task<List<TB_Cat_TipoPcs>> GetTipoPcs(bool? activo);
		Task<bool> AddTipoPcs(TB_Cat_TipoPcs tipoPcs);
		Task<bool> UpdateTipoPcs(TB_Cat_TipoPcs tipoPcs);
		Task<bool> DeleteTipoPcs(TB_Cat_TipoPcs tipoPcs);

        #endregion

        #region Tipo Persona

        Task<List<TB_Cat_TipoPersona>> GetTipoPersona(bool? activo);
        Task<bool> AddTipoPersona(TB_Cat_TipoPersona tipoPersona);
        Task<bool> UpdateTipoPersona(TB_Cat_TipoPersona tipoPersona);
        Task<bool> DeleteTipoPersona(TB_Cat_TipoPersona tipoPersona);

        #endregion Tipo Persona

        #region Tipo Poliza

        Task<List<TB_Cat_TipoPoliza>> GetTipoPoliza(bool? activo);
		Task<bool> AddTipoPoliza(TB_Cat_TipoPoliza tipoPoliza);
		Task<bool> UpdateTipoPoliza(TB_Cat_TipoPoliza tipoPoliza);
		Task<bool> DeleteTipoPoliza(TB_Cat_TipoPoliza tipoPoliza);

		#endregion

		#region Tipo Proyecto

		Task<List<TB_Cat_TipoProyecto>> GetTipoProyecto(bool? activo);
		Task<bool> AddTipoProyecto(TB_Cat_TipoProyecto tipoProyecto);
		Task<bool> UpdateTipoProyecto(TB_Cat_TipoProyecto tipoProyecto);
		Task<bool> DeleteTipoProyecto(TB_Cat_TipoProyecto tipoProyecto);

		#endregion

		#region Tipo Resultado

		Task<List<TB_Cat_TipoResultado>> GetTipoResultado(bool? activo);
		Task<bool> AddTipoResultado(TB_Cat_TipoResultado tipoResultado);
		Task<bool> UpdateTipoResultado(TB_Cat_TipoResultado tipoResultado);
		Task<bool> DeleteTipoResultado(TB_Cat_TipoResultado tipoResultado);

		#endregion

		#region Tipo Sangre

		Task<List<TB_Cat_TipoSangre>> GetTipoSangre(bool? activo);
		Task<bool> AddTipoSangre(TB_Cat_TipoSangre tipoSangre);
		Task<bool> UpdateTipoSangre(TB_Cat_TipoSangre tipoSangre);
		Task<bool> DeleteTipoSangre(TB_Cat_TipoSangre tipoSangre);

        #endregion

        #region Turno
        Task<List<TB_Cat_Turno>> GetTurno(bool? activo);
        Task<bool> AddTurno(TB_Cat_Turno Turno);
        Task<bool> UpdateTurno(TB_Cat_Turno Turno);
        Task<bool> DeleteTurno(TB_Cat_Turno Turno);

        #endregion Turno

        #region Unidad Negocio

        Task<List<TB_Cat_UnidadNegocio>> GetUnidadNegocio(bool? activo);
		Task<bool> AddUnidadNegocio(TB_Cat_UnidadNegocio unidadNegocio);
		Task<bool> UpdateUnidadNegocio(TB_Cat_UnidadNegocio unidadNegocio);
		Task<bool> DeleteUnidadNegocio(TB_Cat_UnidadNegocio unidadNegocio);

		#endregion

		#region Vistico

		Task<List<TB_Cat_Viatico>> GetViatico(bool? activo);
		Task<bool> AddViatico(TB_Cat_Viatico viatico);
		Task<bool> UpdateViatico(TB_Cat_Viatico viatico);
		Task<bool> DeleteViatico(TB_Cat_Viatico viatico);

		#endregion
	}
}
