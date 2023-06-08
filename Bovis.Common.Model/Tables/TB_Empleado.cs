using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado")]
	public class TB_Empleado
	{
		[Column("Nunum_empleado_rr_hh"), NotNull, PrimaryKey, Identity] public int NumEmpleadoRrHh { get; set; }
		[Column("Nukidpersona"), NotNull] public int IdPersona { get; set; }
		[Column("Nukidtipo_empleado"), NotNull] public int IdTipoEmpleado { get; set; }
		[Column("Nukidcategoria"), NotNull] public int IdCategoria { get; set; }
		[Column("Nukidtipo_contrato"), NotNull] public int IdTipoContrato { get; set; }
		[Column("Chcve_puesto"), NotNull] public int CvePuesto { get; set; }
		[Column("Nukidempresa"), NotNull] public int IdEmpresa { get; set; }
		[Column("Nukidciudad"), NotNull] public int IdCiudad { get; set; }
		[Column("Nukidnivel_estudios"), NotNull] public int IdNivelEstudios { get; set; }
		[Column("Nukidforma_pago"), NotNull] public int IdFormaPago { get; set; }
		[Column("Nukidjornada"), Nullable] public int IdJornada { get; set; }
		[Column("Nukiddepartamento"), Nullable] public int IdDepartamento { get; set; }
		[Column("Nukidclasificacion"), Nullable] public int IdClasificacion { get; set; }
		[Column("Nukidjefe_directo"), Nullable] public int IdJefeDirecto { get; set; }
		[Column("Nukidunidad_negocio"), Nullable] public int IdUnidadNegocio { get; set; }
		[Column("Nukidtipo_contrato_sat"), Nullable] public int IdTipoContrato_sat { get; set; }
		[Column("Nunum_empleado"), Nullable] public int NumEmpleado { get; set; }
		[Column("Dtfecha_ingreso"), NotNull] public DateTime FechaIngreso { get; set; }
		[Column("Dtfecha_salida"), Nullable] public DateTime FechaSalida { get; set; }
		[Column("Dtfecha_ultimo_reingreso"), Nullable] public DateTime FechaUltimoReingreso { get; set; }
		[Column("Chnss"), Nullable] public decimal Nss { get; set; }
		[Column("Chemail_bovis"), Nullable] public string EmailBovis { get; set; }
		[Column("Chexperiencias"), Nullable] public string Experiencias { get; set; }
		[Column("Chhabilidades"), Nullable] public string Habilidades { get; set; }
		[Column("Churl_repositorio"), Nullable] public string UrlRepositorio { get; set; }
		[Column("Nusalario"), NotNull] public decimal Salario { get; set; }
		[Column("Chprofesion"), NotNull] public string Profesion { get; set; }
		[Column("Nuantiguedad"), NotNull] public short Antiguedad { get; set; }
		[Column("Chturno"), NotNull] public string Turno { get; set; }
		[Column("Nuunidad_medica"), Nullable] public byte UnidadMedica { get; set; }
		[Column("Chregistro_patronal"), Nullable] public string RegistroPatronal { get; set; }
		[Column("Chcotizacion"), Nullable] public string Cotizacion { get; set; }
		[Column("Nuduracion"), NotNull] public short Duracion { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("Bodescuento_pension"), NotNull] public bool DescuentoPension { get; set; }
		[Column("Nuporcentaje_pension"), Nullable] public decimal PorcentajePension { get; set; }
		[Column("Nufondo_fijo"), Nullable] public decimal FondoFijo { get; set; }
		[Column("Nucredito_infonavit"), Nullable] public int CreditoInfonavit { get; set; }
		[Column("Chtipo_descuento"), Nullable] public string TipoDescuento { get; set; }
		[Column("Nuvalor_descuento"), Nullable] public decimal ValorDescuento { get; set; }
		[Column("Nuno_empleado_noi"), Nullable] public int NoEmpleadoNoi { get; set; }
		[Column("Chrol"), Nullable] public string Rol { get; set; }
	}
}
