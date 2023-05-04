using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_pcs")]
	public class TB_Cat_TipoPcs
	{
		[Column("NukidtipoPcs"), NotNull, PrimaryKey, Identity] public int IdTipoPcs { get; set; }
		[Column("ChtipoPcs"), NotNull] public string TipoPcs { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}