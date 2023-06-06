using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_nivel_estudios")]
	public class TB_Cat_NivelEstudios
	{
		[Column("Nukidnivel_estudios"), NotNull, PrimaryKey, Identity] public int IdNivelEstudios { get; set; }
		[Column("Chnivel_estudios"), NotNull] public string NivelEstudios { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
