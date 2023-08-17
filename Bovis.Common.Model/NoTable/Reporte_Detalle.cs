using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Reporte_Detalle
    {
        public int IdReporte { get; set; }
        public string? Query { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdEmpleadoCrea { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int? IdEmpleadoActualiza { get; set; }               
        public bool? Activo { get; set; }
    }
}
