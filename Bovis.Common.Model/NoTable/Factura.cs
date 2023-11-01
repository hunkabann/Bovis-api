using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class NotaCredito_Detalle
    {
        public int nukidfactura { get; set; }
        public int nunum_proyecto { get; set; }
        public string chproyecto { get; set; }
        public string chuuid_nota_credito { get; set; }
        public string nukidmoneda {  get; set; }
        public string chmoneda { get; set; }
        public string nukidtipo_relacion {  get; set; }
        public string chtipo_relacion { get; set; }
        public string chnota_credito { get; set; }
        public decimal nuimporte { get; set; }
        public decimal nuiva { get; set; }
        public decimal nutotal { get; set; }
        public string chconcepto { get; set; }
        public int numes { get; set; }
        public int nuanio { get; set; }
        public decimal? nutipo_cambio { get; set; }
        public DateTime dtfecha_nota_credito { get; set; }
        public string chxml { get; set; }
        public DateTime? dtfecha_cancelacion { get; set; }
        public string chmotivocancela { get; set; }
    }
}
