using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_empleado_habilidad")]
    public class TB_Empleado_Habilidad
    {
        [Column("Nukidempleado"), Nullable] public int IdEmpleado { get; set; }
        [Column("Nukidhabilidad"), Nullable] public int? IdHabilidad { get; set; }
        [Column("Boactivo"), Nullable] public bool Activo { get; set; }
    }
}
