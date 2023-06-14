using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_cuenta")]
	public class TB_Cat_TipoCuenta
	{
		[Column("nukidtipo_cuenta"), NotNull, PrimaryKey, Identity] public int IdTipoCuenta { get; set; }
        [Column("nukidcta_contable"), NotNull] public int IdTipoCtaContable { get; set; }
        [Column("chtipo_cuenta"), NotNull] public string TipoCuenta { get; set; }
        [Column("chdivision_PCS"), NotNull] public string DivisionPCS { get; set; }
        [Column("chdivision_PCS2"), NotNull] public string DivisionPCS2 { get; set; }
        [Column("chcontrol_gestion"), NotNull] public string ControlGestion { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
