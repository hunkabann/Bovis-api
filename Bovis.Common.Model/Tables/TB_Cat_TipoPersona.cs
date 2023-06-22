using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_tipo_persona")]
    public class TB_Cat_TipoPersona
    {
        [Column("Nukidtipo_persona"), NotNull, PrimaryKey, Identity] public int IdTipoPersona { get; set; }
        [Column("Tipo_persona"), NotNull] public string TipoPersona { get; set; }
        [Column("Activo"), NotNull] public bool Activo { get; set; }
    }
}
