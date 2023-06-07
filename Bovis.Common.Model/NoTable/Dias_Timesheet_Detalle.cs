using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Dias_Timesheet_Detalle
    {
        public int id { get; set; }
        public int mes { get; set; }
        public int dias { get;set; }
        public int feriados { get; set; }
        public int sabados { get; set; }
        public int anio { get; set; }
        public int dias_habiles { get; set; }
    }
}
