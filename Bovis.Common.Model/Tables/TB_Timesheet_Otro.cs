using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_timesheet_otro")]
    public class TB_Timesheet_Otro
    {
        [Column("IdTs_Otro"), NotNull, PrimaryKey, Identity] public int IdTimesheet_Otro { get; set; }
        [Column("NukidTs"), Nullable] public int IdTimeSheet { get; set; }
        [Column("Chdescripcion"), Nullable] public string Descripcion { get; set; }
        [Column("Numdias"), Nullable] public int Dias { get; set; }
        [Column("NumTdedicacion"), Nullable] public decimal TDedicacion { get; set; }
    }
}
