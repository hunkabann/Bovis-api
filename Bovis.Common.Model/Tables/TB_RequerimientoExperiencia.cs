using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_requerimiento_experiencia")]
    public class TB_RequerimientoExperiencia
    {
        [Column("Nukidrequerimiento"), NotNull] public int IdRequerimiento { get; set; }
        [Column("Nukidexperiencia"), NotNull] public int IdExperiencia { get; set; }
        [Column("Boactivo"), Nullable] public bool? Activo { get; set; }
    }
}
