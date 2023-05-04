using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cie")]
	public class TB_Cie
	{
		[Column("Nukidcie"), NotNull, PrimaryKey, Identity] public int IdCie { get; set; }
		[Column("NunumProyecto"), Nullable] public int? NumProyecto { get; set; }
		[Column("NukidtipoCie"), Nullable] public int? IdTipoCie { get; set; }
		[Column("NukidtipoPoliza"), Nullable] public int? IdTipoPoliza { get; set; }
		[Column("Dtfecha"), Nullable] public DateTime? Fecha { get; set; }
		[Column("DtfechaCaptura"), Nullable] public DateTime? FechaCaptura { get; set; }
		[Column("Chconcepto"), Nullable] public string? Concepto { get; set; }
		[Column("NusaldoIni"), Nullable] public decimal? SaldoIni { get; set; }
		[Column("Nudebe"), Nullable] public decimal? Debe { get; set; }
		[Column("Nuhaber"), Nullable] public decimal? Haber { get; set; }
		[Column("Numovimiento"), Nullable] public decimal? Movimiento { get; set; }
		[Column("ChedoResultados"), Nullable] public string? EdoResultados { get; set; }
		[Column("Numes"), Nullable] public byte? Mes { get; set; }
		[Column("NukidcentroCostos"), Nullable] public int? IdCentroCostos { get; set; }
		[Column("NukidtipoCtaContable"), Nullable] public int? IdTipoCtaContable { get; set; }
	}
}
