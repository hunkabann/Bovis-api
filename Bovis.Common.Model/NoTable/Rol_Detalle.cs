using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Rol_Detalle
    {
        public int nukidusuario { get; set; }
        public string chusuario { get; set; }
        public string chemail { get; set; }
        public List<Permiso_Modulo_Detalle> permisos { get; set; }
    }

    public class Permiso_Modulo_Detalle
    {
        public int nukidmodulo { get; set; }
        public string chmodulo { get; set; }
        public string chmodulo_slug { get; set; }
        public string chsub_modulo { get; set; }
        public string chsub_modulo_slug { get; set; }
        public string chpermiso { get; set; }
        public string chpermiso_slug { get; set; }
        public bool botab { get; set; }
        public string chtab { get; set; }
        public List<string> perfiles { get; set; }
        public List<string> permisos { get; set; }
    }
}
