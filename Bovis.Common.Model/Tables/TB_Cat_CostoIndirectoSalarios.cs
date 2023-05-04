using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_costo_indirecto_salarios")]
	public class TB_Cat_CostoIndirectoSalarios
	{
		[Column("nukidcosto_indirecto"), NotNull, PrimaryKey, Identity] public int IdCostoIndirecto { get; set; }
		[Column("chcosto_indirecto"), NotNull] public string CostoIndirecto { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
