using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cie_data")]
    public class TB_Cie_Data
    {
        [Column("Nukidcie"), NotNull, PrimaryKey, Identity] public int IdCieData { get; set; }
        [Column("Chnombre_cuenta"), Nullable] public string? NombreCuenta { get; set; }
        [Column("Chcuenta"), Nullable] public string? Cuenta { get; set; }
        [Column("Chtipo_poliza"), Nullable] public string? TipoPoliza { get; set; }
        [Column("Numnumero"), Nullable] public int? Numero { get; set; }
        [Column("Dtfecha"), Nullable] public DateTime? Fecha { get; set; }
        [Column("Nummes"), Nullable] public int? Mes { get; set; }
        [Column("Chconcepto"), Nullable] public string? Concepto { get; set; }
        [Column("Chcentro_costos"), Nullable] public string? CentroCostos { get; set; }
        [Column("Chproyectos"), Nullable] public string? Proyectos { get; set; }
        [Column("Nusaldo_inicial"), Nullable] public decimal? SaldoInicial { get; set; }
        [Column("Nudebe"), Nullable] public decimal? Debe { get; set; }
        [Column("Nuhaber"), Nullable] public decimal? Haber { get; set; }
        [Column("Numovimiento"), Nullable] public decimal? Movimiento { get; set; }
        [Column("Chempresa"), Nullable] public string? Empresa { get; set; }
        [Column("Nunum_proyecto"), Nullable] public int? NumProyecto { get; set; }
        [Column("Chedo_resultados"), Nullable] public string? EdoResultados { get; set; }
        [Column("Chresponsable"), Nullable] public string? Responsable { get; set; }
        [Column("Chtipo_responsable"), Nullable] public string? TipoResponsable { get; set; }
        [Column("Chtipo_py"), Nullable] public string? TipoPY { get; set; }
        [Column("Chclasificacion_py"), Nullable] public string? ClasificacionPY { get; set; }
    }
}
