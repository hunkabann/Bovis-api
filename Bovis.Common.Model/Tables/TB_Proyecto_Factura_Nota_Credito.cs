using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_proyecto_factura_nota_credito")]
	public class TB_Proyecto_Factura_Nota_Credito
	{
		[Column("nukidfactura"), NotNull, PrimaryKey, Identity] public int IdFactura { set; get; }
		[Column("chuuid_nota_credito"), NotNull] public string UuidNotaCredito { set; get; }
		[Column("nukidmoneda"), Nullable] public string? IdMoneda { set; get; }
		[Column("nukidtipo_relacion"), Nullable] public string? IdTipoRelacion { set; get; }
		[Column("chnota_credito"), NotNull] public string NotaCredito { set; get; }
		[Column("nuimporte"), NotNull] public decimal Importe { set; get; }
		[Column("nuiva"), NotNull] public decimal Iva { set; get; }
		[Column("nutotal"), NotNull] public decimal Total { set; get; }
		[Column("chconcepto"), NotNull] public string Concepto { set; get; }
		[Column("numes"), NotNull] public byte Mes { set; get; }
		[Column("nuanio"), NotNull] public short Anio { set; get; }
		[Column("nutipo_cambio"), Nullable] public decimal? TipoCambio { set; get; }
		[Column("dtfecha_nota_credito"), NotNull] public DateTime FechaNotaCredito { set; get; }
		[Column("chxml"), NotNull] public string Xml { set; get; }
        [Column("dtfecha_cancelacion"), Nullable] public DateTime? FechaCancelacion { set; get; }
        [Column("chmotivocancela"), Nullable] public string? MotivoCancelacion { set; get; }

    }
}
