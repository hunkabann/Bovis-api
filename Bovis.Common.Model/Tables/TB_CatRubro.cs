using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_rubro")]
    public class TB_CatRubro
    {
        [Column("nukid_rubro"), NotNull, PrimaryKey, Identity] public int IdRubro { get; set; }
        [Column("Chrubro"), NotNull] public string Rubro { get; set; }
        [Column("Boactivo"), Nullable] public bool? Activo { get; set; }
    }
}

