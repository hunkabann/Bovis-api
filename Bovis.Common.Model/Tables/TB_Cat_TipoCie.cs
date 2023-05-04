using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_cie")]
	public class TB_Cat_TipoCie
	{
		[Column("nukidtipo_cie"), NotNull, PrimaryKey, Identity] public int IdTipoCie { get; set; }
		[Column("chtipo_cie"), NotNull] public string TipoCie { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
