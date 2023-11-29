using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_dias_timesheet")]
    public class TB_DiasTimesheet
    {
        [Column("Id"), NotNull, PrimaryKey, Identity] public int Id { get; set; }
        [Column("Mes"), Nullable] public int Mes { get; set; }
        [Column("Dias"), Nullable] public int Dias { get; set; }
        [Column("Feriados"), Nullable] public int Feriados { get; set; }
        [Column("Sabados"), Nullable] public int Sabados { get; set; }
        [Column("Año"), Nullable] public int Anio { get; set; }
        [Column("Sabados_feriados"), Nullable] public int SabadosFeriados { get; set; }
    }
}
