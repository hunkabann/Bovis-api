using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_nivel_estudios")]
	public class TB_Cat_NivelEstudios
	{
		[Column("NukidnivelEstudios"), NotNull, PrimaryKey, Identity] public int IdNivelEstudios { get; set; }
		[Column("ChnivelEstudios"), NotNull] public string NivelEstudios { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
