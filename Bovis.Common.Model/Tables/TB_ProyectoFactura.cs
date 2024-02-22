using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_proyecto_factura")]
	public class TB_ProyectoFactura
	{
        [Column("nukidfactura"), NotNull, PrimaryKey, Identity] public int Id { get; set; }
        [Column("Chuuid"), NotNull] public string Uuid { get; set; }
		[Column("nunum_proyecto"), NotNull] public int NumProyecto { get; set; }
		[Column("nukidtipo_factura"), NotNull] public string IdTipoFactura { get; set; }
		[Column("nukidmoneda"), NotNull] public string IdMoneda { get; set; }
		[Column("Nuimporte"), NotNull] public decimal Importe { get; set; }
		[Column("Nuiva"), Nullable] public decimal? Iva { get; set; }
		[Column("nuiva_ret"), Nullable] public decimal? IvaRet { get; set; }
		[Column("Nutotal"), Nullable] public decimal? Total { get; set; }
		[Column("Chconcepto"), NotNull] public string Concepto { get; set; }
		[Column("Numes"), NotNull] public int Mes { get; set; }
		[Column("Nuanio"), NotNull] public int Anio { get; set; }
		[Column("dtfecha_emision"), NotNull] public DateTime FechaEmision { get; set; }
		[Column("dtfecha_pago"), Nullable] public DateTime? FechaPago { get; set; }
		[Column("dtfecha_cancelacion"), Nullable] public DateTime? FechaCancelacion { get; set; }
		[Column("chno_factura"), Nullable] public string? NoFactura { get; set; }
		[Column("nutipo_cambio"), Nullable] public decimal? TipoCambio { get; set; }
        [Column("chxml"), NotNull] public string XmlB64 { get; set; }
        [Column("chmotivocancela"), Nullable] public string? MotivoCancelacion { get; set; }
		[Column("dtfecha_transaccion"), Nullable] public DateTime? FechaTransaccion { get; set; }
    }
}
