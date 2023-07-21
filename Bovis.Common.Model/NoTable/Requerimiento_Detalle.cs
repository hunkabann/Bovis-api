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
        public int? nukidrequerimiento { get; set; }
        public int? nukidcategoria { get; set; }
        public string? chcategoria { get; set; }
        public int? nukidpuesto { get; set; }
        public string? chpuesto { get; set; }
        public int? nukidnivel_estudios { get; set; }
        public string? chnivel_estudios { get; set; }
        public int? nukidprofesion { get; set; }
        public string? chprofesion { get; set; }
        public int? nukidjornada { get; set; }
        public string? chjornada { get; set; }
        public decimal? nusueldo_min { get; set; }
        public decimal? nusueldo_max { get; set; }
        public decimal? nusueldo_real { get; set; }
        public int? nunumempleado_rr_hh { get; set; }
        public string? chempleado_rr_hh { get; set; }
        public int? nukiddirector_ejecutivo { get; set; }
        public string? chdirector_ejecutivo { get; set; }
        public int? nukidproyecto { get; set; }
        public string? chproyecto { get; set; }
        public int? nukidjefe_inmediato { get; set; }
        public string? chjefe_inmediato { get; set; }
        public int? nukidtipo_contrato { get; set; }
        public string? chtipo_contrato { get; set; }
        public int? nukidestado { get; set; }
        public string? chestado { get; set; }
        public int? nukidciudad { get; set; }
        public string? chciudad { get; set; }
        public bool? bodisponibilidad_viajar { get; set; }
        public int? nuanios_experiencia { get; set; }
        public string? chnivel_ingles { get;set; }
        public string? chcomentarios { get; set; }
        public List<TB_Requerimiento_Habilidad>? habilidades { get; set; }
        public List<TB_Requerimiento_Experiencia>? experiencias { get; set; }
        public bool? boactivo { get; set; }
    }

    public class Requerimiento
    {
        public int? nukidrequerimiento { get; set; }
        public int? nukidcategoria { get; set; }
        public int? nukidpuesto { get; set; }
        public int? nukidnivel_estudios { get; set; }
        public int? nukidprofesion { get; set; }
        public int? nukidjornada { get; set; }
        public decimal? nusueldo_min { get; set; }
        public decimal? nusueldo_max { get; set; }
        public string? chhabilidades { get; set; }
        public string? chexperiencias { get; set; }
        public bool? boactivo { get; set; }
    }

    public class Habilidad
    {
        public int? nukidrequerimiento { get; set; }
        public int? nukidhabilidad { get; set; }
        public string? chhabilidad { get; set; }
    }

    public class Experiencia
    {
        public int? nukidrequerimiento { get; set; }
        public int? nukidexperiencia { get; set; }
        public string? chexperiencia { get; set; }
    }
}
