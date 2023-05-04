using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Dto.Both
{
    public class FacturaPagos
    {
        public string FechaPago { get; set; }
        public string? TipoCambioP { get; set; }
        public List<FacturaPagoDocto> DoctosRelacionados { get; set; }
    }

    public class FacturaPagoDocto
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
}
