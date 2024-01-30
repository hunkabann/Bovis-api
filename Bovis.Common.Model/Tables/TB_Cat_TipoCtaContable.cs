using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_cta_contable")]
	public class TB_Cat_TipoCtaContable
	{
		[Column("nukidtipo_cta_contable"), NotNull, PrimaryKey, Identity] public int IdTipoCtaContable { get; set; }
        [Column("Chcta_contable"), NotNull] public string CtaContable { get; set; }
        [Column("Chconcepto"), NotNull] public string Concepto { get; set; }
		[Column("chtipo_cta_contable_mayor"), NotNull] public string TipoCtaContableMayor { get; set; }
		[Column("chtipo_cta_contable_primer_nivel"), NotNull] public string TipoCtaContablePrimerNivel { get; set; }
		[Column("chtipo_cta_contable_segundo_nivel"), NotNull] public string TipoCtaContableSegundoNivel { get; set; }
		[Column("nukidtipo_cuenta"), Nullable] public int? IdTipoCuenta { get; set; }
		[Column("nukidtipo_resultado"), Nullable] public int? IdTipoResultado { get; set; }
		[Column("Nukidpcs"), Nullable] public int? IdPcs { get; set; }
        [Column("Nukidpcs2"), Nullable] public int? IdPcs2 { get; set; }
        [Column("Nukidpcs3"), Nullable] public int? IdPcs3 { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
