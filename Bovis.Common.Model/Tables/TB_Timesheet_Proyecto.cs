using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_timesheet_proyecto")]
    public class TB_Timesheet_Proyecto
    {
        [Column("IdTs_Proyecto"), NotNull, PrimaryKey, Identity] public int IdTimesheet_Otro { get; set; }
        [Column("NukidTs"), Nullable] public int IdTimesheet { get; set; }
        [Column("NukidProyecto"), Nullable] public int IdProyecto { get; set; }
        [Column("Chdescripcion"), Nullable] public string Descripcion { get; set; }
        [Column("Numdias"), Nullable] public int Dias { get; set; }
        [Column("NumTdedicacion"), Nullable] public int TDedicacion { get; set; }
        [Column("Numcosto"), Nullable] public int Costo { get; set; }
    }
}
