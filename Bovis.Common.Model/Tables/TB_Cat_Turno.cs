using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{

    [Table(Schema = "dbo", Name = "tb_cat_turno")]
    public class TB_Cat_Turno
    {
        [Column("nukidturno"), NotNull, PrimaryKey, Identity] public int IdTurno { get; set; }
        [Column("turno"), NotNull] public string Turno { get; set; }
        [Column("activo"), NotNull] public bool Activo { get; set; }
    }
}
