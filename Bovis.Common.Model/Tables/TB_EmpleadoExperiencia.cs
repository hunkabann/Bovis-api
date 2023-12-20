using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_empleado_experiencia")]
    public class TB_EmpleadoExperiencia
    {
        [Column("Nukidempleado"), NotNull] public string IdEmpleado { get; set; }
        [Column("Nukidexperiencia"), NotNull] public int IdExperiencia { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}
