using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_requerimiento_experiencia")]
    public class TB_Requerimiento_Experiencia
    {
        [Column("Nukidrequerimiento"), Nullable] public int IdRequerimiento { get; set; }
        [Column("Nukidexperiencia"), Nullable] public int IdExperiencia { get; set; }
        [Column("Boactivo"), Nullable] public bool Activo { get; set; }
    }
}
