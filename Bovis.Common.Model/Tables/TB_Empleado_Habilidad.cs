using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_empleado_habilidad")]
    public class TB_Empleado_Habilidad
    {
        [Column("Nukidempleado"), NotNull] public int IdEmpleado { get; set; }
        [Column("Nukidhabilidad"), NotNull] public int IdHabilidad { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}
