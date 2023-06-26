using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Requerimiento_Detalle
    {
        public int nukidrequerimiento { get; set; }
        public int nukidcategoria { get; set; }
        public string chcategoria { get; set; }
        public int nukidpuesto { get; set; }
        public string chpuesto { get; set; }
        public int nukidnivel_estudios { get; set; }
        public string chnivel_estudios { get; set; }
        public int nukidprofesion { get;set; }
        public string chprofesion { get; set; }
        public int nukidjornada { get; set; }
        public string chjornada { get; set; }
        public int nusueldo_min { get; set; }
        public int nusueldo_max { get; set; }
        public List<TB_Requerimiento_Habilidad> habilidades { get; set; }
        public List<TB_Requerimiento_Experiencia> experiencias { get; set; }
        public bool boactivo { get; set; }
    }
}
