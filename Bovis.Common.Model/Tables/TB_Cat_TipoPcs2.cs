using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_tipo_pcs2")]
    public class TB_Cat_TipoPcs2
    {
        [Column("Nukidtipo_pcs2"), NotNull, PrimaryKey, Identity] public int IdTipoPcs2 { get; set; }
        [Column("Chtipo_pcs2"), NotNull] public string TipoPcs2 { get; set; }
        [Column("Chabreviatura"), NotNull] public string Abreviatura { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}