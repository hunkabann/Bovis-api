using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cie")]
	public class TB_Cie
	{
		[Column("Nukidcie"), NotNull, PrimaryKey, Identity] public int IdCie { get; set; }
		[Column("Nunum_proyecto"), Nullable] public int? NumProyecto { get; set; }
		[Column("Nukidtipo_cie"), Nullable] public int? IdTipoCie { get; set; }
		[Column("Nukidtipo_poliza"), Nullable] public int? IdTipoPoliza { get; set; }
		[Column("Dtfecha"), Nullable] public DateTime? Fecha { get; set; }
		[Column("Dtfecha_captura"), Nullable] public DateTime? FechaCaptura { get; set; }
		[Column("Chconcepto"), Nullable] public string? Concepto { get; set; }
		[Column("Nusaldo_ini"), Nullable] public decimal? SaldoIni { get; set; }
		[Column("Nudebe"), Nullable] public decimal? Debe { get; set; }
		[Column("Nuhaber"), Nullable] public decimal? Haber { get; set; }
		[Column("Numovimiento"), Nullable] public decimal? Movimiento { get; set; }
		[Column("Chedo_resultados"), Nullable] public string? EdoResultados { get; set; }
		[Column("Numes"), Nullable] public byte? Mes { get; set; }
		[Column("Nukidcentro_costos"), Nullable] public int? IdCentroCostos { get; set; }
		[Column("Nukidtipo_cta_contable"), Nullable] public int? IdTipoCtaContable { get; set; }
		[Column("Nuestatus"), Nullable] public byte? Estatus { get; set; }
	}
}
