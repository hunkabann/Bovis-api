using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_empleado_habilidad")]
    public class TB_EmpleadoHabilidad
    {
        [Column("Nukidempleado"), NotNull] public string IdEmpleado { get; set; }
        [Column("Nukidhabilidad"), NotNull] public int IdHabilidad { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}
