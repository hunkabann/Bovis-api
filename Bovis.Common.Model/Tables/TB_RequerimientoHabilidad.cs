using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_requerimiento_habilidad")]
    public class TB_RequerimientoHabilidad
    {
        [Column("Nukidrequerimiento"), NotNull] public int IdRequerimiento { get; set; }
        [Column("Nukidhabilidad"), NotNull] public int IdHabilidad { get; set; }
        [Column("Boactivo"), Nullable] public bool? Activo { get; set; }
    }
}
