namespace Bovis.Common.Model.NoTable
{
    public class BaseCFDI
    {
        public BaseCFDI() 
        {
            Conceptos = new List<string>();
			CfdiRelacionados = new List<string>();
			Pagos = new List<CfdiPagos>();
		}
        //De comprobante
        public string Version { get; set; }
        public string Serie { get; set; }
        public string Folio { get; set; }
        public string Fecha { get; set; }
        public string SubTotal { get; set; }
        public string Moneda { get; set; }
        public string Total { get; set; }
        public string TipoDeComprobante { get; set; }
        //
        public string RfcEmisor { get; set; }
        public string RfcReceptor { get; set; }
        //
        public List<string> Conceptos { get; set; }
        //de CFDIs Relacionados
        public string? TipoRelacion { get; set; }
        public List<string>? CfdiRelacionados { get; set; }
        //De impuestos
        public string TotalImpuestosTrasladados { get; set; }
        public string TotalImpuestosRetenidos { get; set; }
        //De complemento/TimbreFiscal
        public string UUID { get; set; }
        //De complemento/Pagos
        public List<CfdiPagos>? Pagos { get; set; }
        //de control
        public bool IsVersionValida { get; set; }
        public string? TipoCambio { get; set; } 
        public string XmlB64 { get; set; }
    }

}
