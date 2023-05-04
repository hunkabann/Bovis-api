using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_proyecto_factura")]
	public class TB_Proyecto_Factura
	{
		[Column("nukidfactura"), NotNull, PrimaryKey, Identity] public int IdFactura { set; get; }
		[Column("nunum_proyecto"), NotNull] public int NumProyecto { set; get; }
		[Column("nukidtipo_factura"), NotNull] public string IdTipoFactura { set; get; }
		[Column("nukidmoneda"), NotNull] public string IdMoneda { set; get; }
		[Column("chuuid"), NotNull] public string Uuid { set; get; }
		[Column("nuimporte"), NotNull] public decimal Importe { set; get; }
		[Column("nuiva"), NotNull] public decimal Iva { set; get; }
		[Column("nuiva"), NotNull] public decimal Iva_ret { set; get; }
		[Column("nutotal"), NotNull] public decimal Total { set; get; }
		[Column("chconcepto"), NotNull] public string Concepto { set; get; }
		[Column("numes"), NotNull] public byte Mes { set; get; }
		[Column("nuanio"), NotNull] public short Anio { set; get; }
		[Column("dtfecha_emision"), NotNull] public DateTime FechaEmision { set; get; }
		[Column("dtfecha_pago"), Nullable] public DateTime? FechaPago { set; get; }
		[Column("dtfecha_cancelacion"), NotNull] public byte FechaCancelacion { set; get; }
		[Column("chno_factura"), NotNull] public string NoFactura { set; get; }
		[Column("nutipo_cambio"), Nullable] public decimal? TipoCambio { set; get; }
		[Column("chxml"), NotNull] public string Xml { set; get; }
		[Column("chmotivocancela"), Nullable] public string? MotivoCancela { set; get; }

	}
}
