using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_categoria_prestacion")]
	public class TB_Categ_Prestacion
	{
		[Column("Nukidcategoria"), NotNull, PrimaryKey, Identity] public int IdCategoria { get; set; }
		[Column("Nukidprestacion"), NotNull] public int IdPrestacion { get; set; }
	}
}
