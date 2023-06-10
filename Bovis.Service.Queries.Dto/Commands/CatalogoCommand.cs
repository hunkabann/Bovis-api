using Bovis.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Bovis.Service.Queries.Dto.Commands
{
	public class UpdateBaseCommand
	{
		public string Usuario { get; set; }
		public string Nombre { get; set; }
		public string Roles { get; set; }
		public int Rel { get; set; }
		public string TransactionId { get; set; }
	}

	#region Beneficio
	public class AgregarBeneficioCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarBeneficioCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarBeneficioCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Categoria

	public class AgregarCategoriaCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarCategoriaCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarCategoriaCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Clasificacion

	public class AgregarClasificacionCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarClasificacionCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarClasificacionCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Costo Indirecto Salarios

	public class AgregarCostoIndirectoSalariosCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarCostoIndirectoSalariosCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarCostoIndirectoSalariosCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Departamento

	public class AgregarDepartamentoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarDepartamentoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarDepartamentoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Documento

	public class AgregarDocumentoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarDocumentoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarDocumentoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Estado Civil

	public class AgregarEdoCivilCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarEdoCivilCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarEdoCivilCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Estatus Proyecto

	public class AgregarEstatusProyectoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarEstatusProyectoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarEstatusProyectoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

    #endregion

    #region Experiencia
    public class AgregarExperienciaCommand : IRequest<Response<bool>>
    {
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        public string? descripcion { get; set; }
    }

    public class ActualizarExperienciaCommand : UpdateBaseCommand, IRequest<Response<bool>>
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
        public int id { get; set; }
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        public string? descripcion { get; set; }
    }

    public class EliminarExperienciaCommand : IRequest<Response<bool>>
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
        public int id { get; set; }
    }
    #endregion Experiencia

    #region Forma Pago

    public class AgregarFormaPagoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarFormaPagoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarFormaPagoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Gasto

	public class AgregarGastoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarGastoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarGastoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

    #endregion

    #region Habilidad
    public class AgregarHabilidadCommand : IRequest<Response<bool>>
    {
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        public string? descripcion { get; set; }
    }

    public class ActualizarHabilidadCommand : UpdateBaseCommand, IRequest<Response<bool>>
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
        public int id { get; set; }
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        public string? descripcion { get; set; }
    }

    public class EliminarHabilidadCommand : IRequest<Response<bool>>
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
        public int id { get; set; }
    }
    #endregion Habilidad

    #region Ingreso

    public class AgregarIngresoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarIngresoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarIngresoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Jornada

	public class AgregarJornadaCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarJornadaCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarJornadaCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Moneda

	public class AgregarModenaCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarModenaCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarModenaCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Nivel Estudios

	public class AgregarNivelEstudiosCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarNivelEstudiosCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarNivelEstudiosCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Nivel Puesto

	public class AgregarNivelPuestoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarNivelPuestoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarNivelPuestoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Pcs

	public class AgregarPcsCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarPcsCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarPcsCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Prestacion

	public class AgregarPrestacionCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarPrestacionCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarPrestacionCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

    #endregion

    #region Profesion
    public class AgregarProfesionCommand : IRequest<Response<bool>>
    {
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        public string? descripcion { get; set; }
    }

    public class ActualizarProfesionCommand : UpdateBaseCommand, IRequest<Response<bool>>
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
        public int id { get; set; }
        [Required(ErrorMessage = "El campo descripcion es requerido")]
        public string? descripcion { get; set; }
    }

    public class EliminarProfesionCommand : IRequest<Response<bool>>
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
        public int id { get; set; }
    }
    #endregion Profesion

    #region Puesto

    public class AgregarPuestoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarPuestoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarPuestoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Rubro Ingreso Reembolsable

	public class AgregarRubroIngresoReembolsableCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarRubroIngresoReembolsableCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarRubroIngresoReembolsableCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Sector

	public class AgregarSectorCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarSectorCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarSectorCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Cie

	public class AgregarTipoCieCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoCieCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoCieCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Contrato

	public class AgregarTipoContratoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoContratoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoContratoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Cta Contable

	public class AgregarTipoCtaContableCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoCtaContableCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoCtaContableCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Cuenta

	public class AgregarTipoCuentaCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoCuentaCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoCuentaCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Documento

	public class AgregarTipoDocumentoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoDocumentoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoDocumentoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Empleado

	public class AgregarTipoEmpleadoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoEmpleadoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoEmpleadoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Factura

	public class AgregarTipoFacturaCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoFacturaCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoFacturaCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Gasto

	public class AgregarTipoGastoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoGastoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoGastoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Ingreso

	public class AgregarTipoIngresoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoIngresoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoIngresoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Pcs

	public class AgregarTipoPcsCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoPcsCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoPcsCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Poliza

	public class AgregarTipoPolizaCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoPolizaCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoPolizaCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Proyecto

	public class AgregarTipoProyectoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoProyectoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoProyectoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Resultado

	public class AgregarTipoResultadoCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoResultadoCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoResultadoCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Tipo Sangre

	public class AgregarTipoSangreCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarTipoSangreCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarTipoSangreCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Unidad Negocio

	public class AgregarUnidadNegocioCommand : IRequest<Response<bool>>
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarUnidadNegocioCommand : UpdateBaseCommand, IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarUnidadNegocioCommand : IRequest<Response<bool>>
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

	#region Viatico

	public class AgregarViaticoCommand : IRequest<Response<bool>> 
	{
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class ActualizarViaticoCommand : UpdateBaseCommand, IRequest<Response<bool>> 
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
		[Required(ErrorMessage = "El campo descripcion es requerido")]
		public string? descripcion { get; set; }
	}

	public class EliminarViaticoCommand : IRequest<Response<bool>> 
	{
		[Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
		public int id { get; set; }
	}

	#endregion

}
