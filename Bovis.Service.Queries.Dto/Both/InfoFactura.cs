namespace Bovis.Service.Queries.Dto.Both
{
    public class InfoFactura
    {
        //De comprobante
        public string Serie { get; set; }
        public string Folio { get; set; }
        public string Fecha { get; set; }
        public string SubTotal { get; set; }
        public string Moneda { get; set; }
        public string? TipoCambio { get; set; }

        public string Total { get; set; }
        public string TipoDeComprobante { get; set; }
        //
        public string RfcEmisor { get; set; }
        public string RfcReceptor { get; set; }
        //
        public List<string> Conceptos { get; set; }
        //de CFDIs Relacionados
        public string TipoRelacion { get; set; }
        public List<string> CfdiRelacionados { get; set; }
        //De impuestos
        public string? TotalImpuestosTrasladados { get; set; }

        public string? TotalImpuestosRetenidos { get; set; }
        //De complemento/TimbreFiscal
        public string Uuid { get; set; }
        //De complemento/Pagos
        public List<FacturaPagos>? Pagos { get; set; }
        //de control
        public bool IsVersionValida { get; set; }

    }
}
