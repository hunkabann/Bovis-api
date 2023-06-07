using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Dto.Responses
{
    public class Detalle_Dias_Timesheet
    {
        public int id { get; set; }
        public int mes { get; set; }
        public int dias { get; set; }
        public int feriados { get; set; }
        public bool sabados { get; set; }
        public bool anio { get; set; }
        public int dias_habiles { get; set; }
    }
}
