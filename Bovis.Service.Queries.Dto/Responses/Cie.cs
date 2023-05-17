using Bovis.Common;
using Bovis.Service.Queries.Dto.Responses;

namespace Bovis.Service.Queries.Dto.Responses
{
    public class Empresa
    {
        public int nukidempresa { get; set; }
        public string chempresa { get; set; }
        public string rfc { get; set; }
        public int nucoi { set; get; }
        public int nunoi { set; get; }
        public int nusae { set; get; }
        public bool boactivo { set; get; }
    }

    public class Cie
    {
        public int nukidcie { set; get; }
        public int nunum_proyecto { set; get; }
        public int nukidtipo_cie { set; get; }
        public int nukidtipo_poliza { set; get; }
        public DateTime dtfecha { set; get; }
        public DateTime dtfecha_captura { set; get; }
        public string chconcepto { set; get; }
        public decimal nusaldo_ini { set; get; }
        public decimal nudebe { get; set; }
        public decimal nuhaber { get; set; }
        public decimal numovimiento { set; get; }
        public string chedo_resultados { set; get; }
        public byte numes { get; set; }
        public int nukidcentro_costos { set; get; }
        public int nukidtipo_cta_contable { set; get; }
        public int nuestatus { set; get; }
    }
}
