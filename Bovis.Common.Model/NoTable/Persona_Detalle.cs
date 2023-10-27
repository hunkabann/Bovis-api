using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Persona
    {
        public int nukidpersona { get; set; }
        public int nukidedo_civil { get; set; }
        public int nukidtipo_sangre { get; set; }
        public string chnombre { get; set; }
        public string chap_paterno { get; set; }
        public string chap_materno { get; set; }
        public int nukidsexo { get; set; }
        public string chrfc { get; set; }
        public DateTime dtfecha_nacimiento { get; set; }
        public string chemail { get; set; }
        public string chtelefono { get; set; }
        public string chcelular { get; set; }
        public string chcurp { get; set; }
        public int nukidtipo_persona { get; set; }
        public bool boactivo { get; set; }
    }
    public class Persona_Detalle
    {
        public int? nukidpersona { get; set; }
        public int? nukidedo_civil { get; set; }
        public string? chedo_civil { get; set; }
        public int? nukidtipo_sangre { get; set; }
        public string? chtipo_sangre { get; set; }
        public string? chnombre { get; set; }
        public string? chap_paterno { get; set; }
        public string? chap_materno { get; set; }
        public int? nukidsexo { get; set; }
        public string? chsexo { get; set; }
        public string? chrfc { get; set; }
        public DateTime? dtfecha_nacimiento { get; set; }
        public string? chemail { get; set; }
        public string? chtelefono { get; set; }
        public string? chcelular { get; set; }
        public string? chcurp { get; set; }
        public int? nukidtipo_persona { get; set; }
        public string? chtipo_persona { get; set; }
        public bool? boempleado { get; set; }
        public bool? boactivo { get; set; }
    }
}
