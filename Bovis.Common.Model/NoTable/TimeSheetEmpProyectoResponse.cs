using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{//Reporte EmpleadosXProyecto
    public class TimeSheetEmpProyectoResponse
    {
        public TimeSheetEmpProyectoGral general { get; set; }
        public List<TimeSheetEmpProyectoDetalle> detalle { get; set; }
    }

    public class TimeSheetEmpProyectoGral
    {
        public int nunum_proyecto { get; set; }
        public string chproyecto { get; set; }
        public string dtfecha_ini { get; set; }
        public string dtfecha_fin { get; set; }
        public int meses { get; set; }

    }

    public class TimeSheetEmpProyectoDetalle
    {
        public string nukid_empleado { get; set; }
        public string Nombre { get; set; }
        public int nummes { get; set; }
        public int numanio { get; set; }
        public int nukidProyecto { get; set; }
        public string numcosto { get; set; }

    }

}
