using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_DOR_REAL_GASTO_INGRESO_Proyecto_GPM")]
    public class TB_Dor_Real_Gasto_Ingreso_Proyecto_Gpm
    {
        [Column("IdReal_Proyecto"), NotNull, PrimaryKey, Identity] public int IdRealProyecto { get; set; }
        [Column("UnidadNegocio"), Nullable] public string? UnidadDeNegocio { get; set; }
        [Column("Concepto"), NotNull] public string Concepto { get; set; }
        [Column("Descripcion"), Nullable] public string? Descripcion { get; set; }
        [Column("Ingreso"), Nullable] public decimal? Ingreso { get; set; }
        [Column("Gasto"), Nullable] public decimal? Gasto { get; set; }
        [Column("Mes"), Nullable] public int? Mes { get; set; }
        [Column("Año"), Nullable] public int? Año { get; set; }
        [Column("In_Ene"), Nullable] public decimal? InEnero { get; set; }
        [Column("In_Feb"), Nullable] public decimal? InFebrero { get; set; }
        [Column("In_Mar"), Nullable] public decimal? InMarzo { get; set; }
        [Column("In_Abr"), Nullable] public decimal? InAbril { get; set; }
        [Column("In_May"), Nullable] public decimal? InMayo { get; set; }
        [Column("In_Jun"), Nullable] public decimal? InJunio { get; set; }
        [Column("In_Jul"), Nullable] public decimal? InJulio { get; set; }
        [Column("In_Ago"), Nullable] public decimal? InAgosto { get; set; }
        [Column("In_Sep"), Nullable] public decimal? InSeptiembre { get; set; }
        [Column("In_Oct"), Nullable] public decimal? InOctubre { get; set; }
        [Column("In_Nov"), Nullable] public decimal? InNoviembre { get; set; }
        [Column("In_Dic"), Nullable] public decimal? InDiciembre { get; set; }
        [Column("Out_Ene"), Nullable] public decimal? OutEnero { get; set; }
        [Column("Out_Feb"), Nullable] public decimal? OutFebrero { get; set; }
        [Column("Out_Mar"), Nullable] public decimal? OutMarzo { get; set; }
        [Column("Out_Abr"), Nullable] public decimal? OutAbril { get; set; }
        [Column("Out_May"), Nullable] public decimal? OutMayo { get; set; }
        [Column("Out_Jun"), Nullable] public decimal? OutJunio { get; set; }
        [Column("Out_Jul"), Nullable] public decimal? OutJulio { get; set; }
        [Column("Out_Ago"), Nullable] public decimal? OutAgosto { get; set; }
        [Column("Out_Sep"), Nullable] public decimal? OutSeptiembre { get; set; }
        [Column("Out_Oct"), Nullable] public decimal? OutOctubre { get; set; }
        [Column("Out_Nov"), Nullable] public decimal? OutNoviembre { get; set; }
        [Column("Out_Dic"), Nullable] public decimal? OutDiciembre { get; set; }
    }
}
