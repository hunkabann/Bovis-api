using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_pcs")]
	public class TB_Cat_TipoPcs
	{
		[Column("Nukidtipo_pcs"), NotNull, PrimaryKey, Identity] public int IdTipoPcs { get; set; }
		[Column("Chtipo_pcs"), NotNull] public string TipoPcs { get; set; }
        [Column("Chabreviatura"), NotNull] public string Abreviatura { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}