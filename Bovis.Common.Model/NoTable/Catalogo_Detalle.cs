using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Puesto_Detalle
    {
        public int nukid_puesto { get; set; }
        public int nukidnivel { get; set; }
        public string chcvenoi { get; set; }        
        public string chpuesto { get; set; }
        public bool boactivo { get; set; }
        public decimal nusalario_min { get; set; }
        public decimal nusalario_max { get; set; }
        public decimal nusalario_prom { get; set; }
    }

    public class TipoContrato_Detalle
    {
        public int nukid_contrato { get; set; }
        public string chcontrato { get; set; }
        public string chve_contrato { get; set; }
        public bool boactivo { get; set; }
    }
}
