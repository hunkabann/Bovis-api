using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_experiencia")]
    public class TB_Cat_Experiencia
    {
        [Column("Nukidexperiencia"), NotNull, PrimaryKey, Identity] public int IdExperiencia { get; set; }
        [Column("Chvexperiencia"), NotNull] public string Experiencia { get; set; }
        [Column("Activo"), NotNull] public bool Activo { get; set; }
    }
}