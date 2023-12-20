using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_usuario_timesheet")]
    public class TB_UsuarioTimesheet
    {
        [Column("IdUserTime"), NotNull, PrimaryKey, Identity] public int IdUserTimesheet { get; set; }
        [Column("Chusuario"), Nullable] public string Usuario { get; set; }
        [Column("Nukid_empleado"), Nullable] public string NumEmpleadoRrHh { get; set; }
        [Column("Nunum_proyecto"), Nullable] public int NumProyecto { get; set; }
    }
}
