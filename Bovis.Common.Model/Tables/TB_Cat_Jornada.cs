using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_jornada")]
	public class TB_Cat_Jornada
	{
		[Column("Nukidjornada"), NotNull, PrimaryKey, Identity] public int IdJornada { get; set; }
		[Column("Chjornada"), NotNull] public string Jornada { get; set; }
		[Column("ChdescripcionJornada"), NotNull] public string Descripcion { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("ChcveJornada"), Nullable] public byte? Cve { get; set; }
	}
}
