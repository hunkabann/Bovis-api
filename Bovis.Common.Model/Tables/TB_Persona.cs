using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_persona")]
	public class TB_Persona
	{
		[Column("Nukidpersona"), NotNull, PrimaryKey, Identity] public int IdPersona { get; set; }
		[Column("Nukidedo_civil"), Nullable] public int? IdEdoCivil { get; set; }
		[Column("Nukidtipo_sangre"), Nullable] public int? IdTipoSangre { get; set; }
		[Column("Chnombre"), NotNull] public string Nombre { get; set; }
		[Column("Chap_paterno"), Nullable] public string? ApPaterno { get; set; }
		[Column("Chap_materno"), Nullable] public string? ApMaterno { get; set; }
		[Column("Nukidsexo"), NotNull] public int IdSexo { get; set; }
		[Column("Chrfc"), Nullable] public string? Rfc { get; set; }
		[Column("Dtfecha_nacimiento"), NotNull] public DateTime FechaNacimiento { get; set; }
		[Column("Chemail"), Nullable] public string? Email { get; set; }
		[Column("Chtelefono"), Nullable] public string? Telefono { get; set; }
		[Column("Chcelular"), Nullable] public string? Celular { get; set; }
		[Column("Chcurp"), Nullable] public string? Curp { get; set; }
		[Column("Nukidtipo_persona"), NotNull] public int IdTipoPersona { get; set; }
		[Column("Boempleado"), Nullable] public bool? EsEmpleado { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
