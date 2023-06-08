using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_timesheet")]
    public class TB_Timesheet
    {
        [Column("IdTs"), NotNull, PrimaryKey, Identity] public int IdTimesheet { get; set; }
        [Column("Nukid_empleado"), Nullable] public int IdEmpleado { get; set; }
        [Column("Nummes"), Nullable] public int Mes { get; set; }
        [Column("Numanio"), Nullable] public int Anio { get; set; }
        [Column("Nukid_responsable"), Nullable] public int IdResponsable { get; set; }
        [Column("Bosabados"), Nullable] public bool Sabados { get; set; }
        [Column("Numdias_trabajo"), Nullable] public int DiasTrabajo { get; set; }
    }
}
