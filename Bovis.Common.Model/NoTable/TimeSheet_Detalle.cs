using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class TimeSheet_Detalle
    {
        public int? id { get; set; }
        public string? id_empleado { get; set; }
        public string? empleado { get; set; }
        public int? mes { get; set; }
        public int? anio { get; set; }
        public string? id_responsable { get; set; }
        public string? responsable { get; set; }
        public bool? sabados { get; set; }
        public int? dias_trabajo { get; set; }
        public int? coi_empresa { get; set; }
        public int? noi_empresa { get; set; }
        public string? noi_empleado { get; set; }
        public int? num_empleado { get; set; }
        public List<TB_Timesheet_Otro>? otros { get; set; }
        public List<TB_Timesheet_Proyecto>? proyectos { get; set; }
    }

    public class Detalle_Dias_Timesheet
    {
        public int? id { get; set; }
        public int? mes { get; set; }
        public int? dias { get; set; }
        public int? feriados { get; set; }
        public int? sabados { get; set; }
        public int? anio { get; set; }
        public int? dias_habiles { get; set; }
        public int? sabados_feriados { get; set; }
    }
}
