using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_rubro")]
    public class TB_Cat_Rubro
    {
        [Column("Nukid_rubro"), NotNull, PrimaryKey, Identity] public int IdRubro { get; set; }
        [Column("Chrubro"), NotNull] public string Rubro { get; set; }
        [Column("Nukid_seccion"), Nullable] public int IdSeccion { get; set; }
        [Column("Boactivo"), Nullable] public bool? Activo { get; set; }
    }
}

