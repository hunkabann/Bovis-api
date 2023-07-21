using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Proyecto_Detalle
    {
        public int? nunum_proyecto { get; set; }
        public string? chproyecto { get; set; }
        public string? chalcance { get; set; }
        public string? chcp { get; set; }
        public string? chciudad { get; set; }
        public int? nukidestatus { get; set; }
        public int? nukidsector { get; set; }
        public int? nukidtipo_proyecto { get; set; }
        public int? nukidresponsable_preconstruccion { get; set; }
        public int? nukidresponsable_construiccion { get; set; }
        public int? nukidresponsable_ehs { get; set; }
        public int? nukidresponsable_supervisor { get; set; }
        public int? nukidcliente { get; set; }
        public int? nukidempresa { get; set; }
        public int? nukidpais { get; set; }
        public int? nukiddirector_ejecutivo { get; set; }
        public decimal? nucosto_promedio_m2 { get; set; }
        public DateTime? dtfecha_ini { get; set; }
        public DateTime? dtfecha_fin { get; set; }
        public int? nunum_empleado_rr_hh { get; set; }
        public decimal? nuporcantaje_participacion { get; set; }
        public string? chalias_puesto { get; set; }
        public string? chgrupo_proyecto { get; set; }
    }
}
