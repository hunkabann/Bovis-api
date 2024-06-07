using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_timesheet_otro")]
    public class TB_Timesheet_Otro
    {
        [Column("IdTs_Otro"), NotNull, PrimaryKey, Identity] public int IdTimesheet_Otro { get; set; }
        [Column("NukidTs"), NotNull] public int IdTimeSheet { get; set; }
        [Column("Chdescripcion"), NotNull] public string Descripcion { get; set; }
        [Column("Numdias"), NotNull] public float Dias { get; set; }
        [Column("NumTdedicacion"), NotNull] public float TDedicacion { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}
