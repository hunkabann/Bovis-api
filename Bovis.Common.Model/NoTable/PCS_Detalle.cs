using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class PCS_Proyecto_Detalle
    {
        public int NumProyecto { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public List<PCS_Etapa_Detalle> Etapas { get; set; }
    }

    public class PCS_Etapa_Detalle
    {
        public int IdFase { get; set; }        
        public int Orden { get; set; }
        public string Fase { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public List<PCS_Empleado_Detalle> Empleados {  get; set; } 
    }

    public class PCS_Empleado_Detalle
    {
        public int Id { get; set; }
        public int IdFase { set; get; }
        public string NumempleadoRrHh { get; set; }
        public string Empleado { get; set; }
        public List<PCS_Fecha_Detalle> Fechas { get; set; }
    }

    public class PCS_Fecha_Detalle
    {
        public int Id { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public int Porcentaje { get; set; }
    }

    public class Tipo_Proyecto
    {
        public int IdTipoProyecto { get; set; }
        public string TipoProyecto { get; set; }
    }
}
