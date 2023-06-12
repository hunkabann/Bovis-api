using Bovis.Common;
using Bovis.Service.Queries.Dto.Responses;

namespace Bovis.Service.Queries.Dto.Responses
{
    public class Requerimiento
    {
        public int nukidrequerimiento { get; set; }
        public int nukidcategoria { get; set; }
        public int nukidpuesto { get; set; }
        public int nukidnivel_estudios { get; set; }
        public int nukidprofesion { get; set; }
        public int nukidjornada { get; set; }
        public decimal nusueldo_min { get; set; }
        public decimal nusueldo_max { get; set; }
        public string chhabilidades { get; set; }
        public string chexperiencias { get;set; }
        public bool boactivo { get; set; }
    }

    public class Habilidad
    {
        public int nukidrequerimiento { get; set; }
        public int nukidhabilidad { get; set; }
        public string chhabilidad { get; set; }
    }

    public class Experiencia
    {
        public int nukidrequerimiento { get; set; }
        public int nukidexperiencia { get; set; }
        public string chexperiencia { get; set; }
    }
}
