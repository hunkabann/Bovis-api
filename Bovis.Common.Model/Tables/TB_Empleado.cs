using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado")]
	public class TB_Empleado
	{
		[Column("NunumEmpleadoRrHh"), NotNull, PrimaryKey, Identity] public int NumEmpleadoRrHh { get; set; }
		[Column("Nukidpersona"), NotNull] public int IdPersona { get; set; }
		[Column("NukidtipoEmpleado"), NotNull] public int IdTipoEmpleado { get; set; }
		[Column("Nukidcategoria"), NotNull] public int IdCategoria { get; set; }
		[Column("NukidtipoContrato"), NotNull] public int IdTipoContrato { get; set; }
		[Column("ChcvePuesto"), NotNull] public string CvePuesto { get; set; }
		[Column("Nukidempresa"), NotNull] public int IdEmpresa { get; set; }
		[Column("Nukidciudad"), NotNull] public int IdCiudad { get; set; }
		[Column("NukidnivelEstudios"), NotNull] public int IdNivelEstudios { get; set; }
		[Column("NukidformaPago"), NotNull] public int IdFormaPago { get; set; }
		[Column("Nukidjornada"), Nullable] public int? IdJornada { get; set; }
		[Column("Nukiddepartamento"), Nullable] public int? IdDepartamento { get; set; }
		[Column("Nukidclasificacion"), Nullable] public int? IdClasificacion { get; set; }
		[Column("NukidjefeDirecto"), Nullable] public int? IdJefeDirecto { get; set; }
		[Column("NukidunidadNegocio"), Nullable] public int? IdUnidadNegocio { get; set; }
		[Column("NunumEmpleado"), Nullable] public int? NumEmpleado { get; set; }
		[Column("DtfechaIngreso"), NotNull] public DateTime FechaIngreso { get; set; }
		[Column("DtfechaSalida"), Nullable] public DateTime? FechaSalida { get; set; }
		[Column("DtfechaUltimoReingreso"), Nullable] public DateTime? FechaUltimoReingreso { get; set; }
		[Column("Chnss"), Nullable] public decimal? Nss { get; set; }
		[Column("ChemailBovis"), Nullable] public string? EmailBovis { get; set; }
		[Column("Chexperiencias"), Nullable] public string? Experiencias { get; set; }
		[Column("Chhabilidades"), Nullable] public string? Habilidades { get; set; }
		[Column("ChurlRepositorio"), Nullable] public string? UrlRepositorio { get; set; }
		[Column("Nusalario"), NotNull] public decimal Salario { get; set; }
		[Column("Chprofesion"), NotNull] public string Profesion { get; set; }
		[Column("Nuantiguedad"), NotNull] public short Antiguedad { get; set; }
		[Column("Chturno"), NotNull] public string Turno { get; set; }
		[Column("NuunidadMedica"), Nullable] public byte? UnidadMedica { get; set; }
		[Column("BodescuentoPension"), NotNull] public bool DescuentoPension { get; set; }
		[Column("ChregistroPatronal"), Nullable] public string? RegistroPatronal { get; set; }
		[Column("Chcotizacion"), Nullable] public string? Cotizacion { get; set; }
		[Column("Nuduracion"), NotNull] public short Duracion { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
