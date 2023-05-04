using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_persona")]
	public class TB_Persona
	{
		[Column("Nukidpersona"), NotNull, PrimaryKey, Identity] public int IdPersona { get; set; }
		[Column("NukidedoCivil"), Nullable] public int? IdEdoCivil { get; set; }
		[Column("NukidtipoSangre"), Nullable] public int? IdTipoSangre { get; set; }
		[Column("Chnombre"), NotNull] public string Nombre { get; set; }
		[Column("ChapPaterno"), Nullable] public string? ApPaterno { get; set; }
		[Column("ChapMaterno"), Nullable] public string? ApMaterno { get; set; }
		[Column("Chsexo"), NotNull] public string Sexo { get; set; }
		[Column("Chrfc"), Nullable] public string? Rfc { get; set; }
		[Column("DtfechaNacimiento"), NotNull] public DateTime FechaNacimiento { get; set; }
		[Column("Chemail"), Nullable] public string? Email { get; set; }
		[Column("Chtelefono"), Nullable] public string? Telefono { get; set; }
		[Column("Chcelular"), Nullable] public string? Celular { get; set; }
		[Column("Chcurp"), Nullable] public string? Curp { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("NutipoPersona"), Nullable] public decimal? TipoPersona { get; set; }
	}
}
