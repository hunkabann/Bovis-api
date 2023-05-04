using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_categoria")]
	public class TB_Cat_Categoria
	{
		[Column("Nukidcategoria"), NotNull, PrimaryKey, Identity] public int IdCategoria { get; set; }
		[Column("Chcategoria"), NotNull] public string Categoria { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
