using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_pcs")]
	public class TB_Cat_Pcs
	{
		[Column("Nukidpcs"), NotNull, PrimaryKey, Identity] public int IdPcs { get; set; }
		[Column("Chpcs"), NotNull] public string Pcs { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("NukidtipoPcs"), Nullable] public int? IdTipoPcs { get; set; }
	}
}
