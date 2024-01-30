using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_tipo_pcs3")]
    public class TB_Cat_TipoPcs3
    {
        [Column("Nukidtipo_pcs3"), NotNull, PrimaryKey, Identity] public int IdTipoPcs3 { get; set; }
        [Column("Chtipo_pcs3"), NotNull] public string TipoPcs3 { get; set; }
        [Column("Chabreviatura"), NotNull] public string Abreviatura { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}