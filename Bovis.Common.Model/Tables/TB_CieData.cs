using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cie_data")]
    public class TB_CieData
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
        [Column("Chproyecto"), NotNull] public string Proyecto { get; set; }
        [Column("Nusaldo_inicial"), Nullable] public decimal? SaldoInicial { get; set; }
        [Column("Nudebe"), Nullable] public decimal? Debe { get; set; }
        [Column("Nuhaber"), Nullable] public decimal? Haber { get; set; }
        [Column("Numovimiento"), Nullable] public decimal? Movimiento { get; set; }
        [Column("Chempresa"), Nullable] public string? Empresa { get; set; }
        [Column("Nunum_proyecto"), NotNull] public int NumProyecto { get; set; }
        [Column("Chtipo_cuenta"), Nullable] public string? TipoCuenta { get; set; }
        [Column("Chedo_resultados"), Nullable] public string? EdoResultados { get; set; }
        [Column("Chresponsable"), Nullable] public string? Responsable { get; set; }
        [Column("Chtipo_proyecto"), Nullable] public string? TipoProyecto { get; set; }
        [Column("Chtipo_py"), Nullable] public string? TipoPY { get; set; }
        [Column("Chclasificacion_py"), Nullable] public string? ClasificacionPY { get; set; }
        [Column("Boactivo"), Nullable] public bool? Activo { get; set; }
        [Column("Nukid_archivo"), Nullable] public int? IdArchivo { get; set; }
    }
}
