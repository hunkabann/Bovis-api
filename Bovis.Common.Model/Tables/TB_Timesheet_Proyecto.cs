using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_timesheet_proyecto")]
    public class TB_Timesheet_Proyecto
    {
        [Column("IdTs_Proyecto"), NotNull, PrimaryKey, Identity] public int IdTimesheet_Proyecto { get; set; }
        [Column("NukidTs"), NotNull] public int IdTimesheet { get; set; }
        [Column("NukidProyecto"), NotNull] public int IdProyecto { get; set; }
        [Column("Chdescripcion"), NotNull] public string Descripcion { get; set; }
        [Column("Numdias"), NotNull] public float Dias { get; set; }
        [Column("NumTdedicacion"), NotNull] public float TDedicacion { get; set; }
        [Column("Numcosto"), NotNull] public decimal Costo { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}
