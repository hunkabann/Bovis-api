using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_requerimiento_habilidad")]
    public class TB_Requerimiento_Habilidad
    {
        [Column("Nukidrequerimiento"), Nullable] public int IdRequerimiento { get; set; }
        [Column("Nukidhabilidad"), Nullable] public int IdHabilidad { get; set; }
        [Column("Boactivo"), Nullable] public bool Activo { get; set; }
    }
}
