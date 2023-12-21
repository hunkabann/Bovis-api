namespace Bovis.Common.Model.NoTable
{
    public class CfdiPagos
    {
        public string FechaPago { get; set; }
        public string? TipoCambioP { get; set; }
        public CfdiTrasladoP? TrasladoP { get; set; }
        public List<CfdiPagoDocto> DoctosRelacionados { get; set; }
    }

    public class CfdiPagoDocto
    {
        public string Uuid { get; set; }
        public string? Serie { get; set; }
        public string? Folio { get; set; }
        public string? MonedaDR { get; set; }
        public string? ImportePagado { get; set; }
        public string? ImporteSaldoAnt { get; set; }
        public string? ImporteSaldoInsoluto { get; set; }        
        public string? ImporteDR { get; set; }
    }

    public class CfdiTrasladoP
    {
        public string? BaseP { get; set; }

    }
}
