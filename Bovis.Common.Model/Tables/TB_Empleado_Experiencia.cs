using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_empleado_experiencia")]
    public class TB_Empleado_Experiencia
    {
        [Column("Nukidempleado"), Nullable] public int IdEmpleado { get; set; }
        [Column("Nukidexperiencia"), Nullable] public int? IdExperiencia { get; set; }
        [Column("Boactivo"), Nullable] public bool Activo { get; set; }
    }
}
