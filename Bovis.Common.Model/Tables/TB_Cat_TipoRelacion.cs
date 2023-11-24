using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_tipo_relacion")]
    public class TB_Cat_TipoRelacion
    {
        [Column("Nukidtipo_relacion"), NotNull, PrimaryKey, Identity] public string IdTipoRelacion { get; set; }
        [Column("Chtipo_relacion"), NotNull] public string TipoRelacion { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}
