using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_proyecto_factura_cobranza")]
	public class TB_ProyectoFacturaCobranza
	{
		[Column("nukidfactura"), NotNull, PrimaryKey, Identity] public int IdFactura { set; get; }
		[Column("chuuid_cobranza"), NotNull] public string UuidCobranza { set; get; }
		[Column("nukidmonedaP"), Nullable] public string? IdMonedaP { set; get; }
		[Column("nuImportePagado"), NotNull] public decimal ImportePagado { set; get; }
		[Column("nuImpSaldoAnt"), NotNull] public decimal ImpSaldoAnt { set; get; }
		[Column("nuImporteSaldoInsoluto"), NotNull] public decimal ImporteSaldoInsoluto { set; get; }
		[Column("nuivaP"), NotNull] public decimal IvaP { set; get; }
		[Column("nutipo_cambioP"), Nullable] public decimal? TipoCambioP { set; get; }
		[Column("dtfecha_pago"), NotNull] public DateTime FechaPago { set; get; }
		[Column("chxml"), NotNull] public string Xml { set; get; }
        [Column("dtfecha_cancelacion"), Nullable] public DateTime? FechaCancelacion { set; get; }
        [Column("chmotivocancela"), Nullable] public string? MotivoCancelacion { set; get; }
        [Column("chcrp"), Nullable] public string? CRP { set; get; }
		[Column("nubase"), Nullable] public decimal? Base { set; get; }
        [Column("dtfecha_transaccion"), Nullable] public DateTime? FechaTransaccion { get; set; }

    }
}
