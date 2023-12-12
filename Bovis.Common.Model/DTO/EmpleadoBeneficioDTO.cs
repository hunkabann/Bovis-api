using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.DTO
{
    public class EmpleadoBeneficioDTO
    {
        public int Id { get; set; }
        public int IdBeneficio { get; set; }
        public int NumEmpleadoRrHh { get; set; }
        public decimal Costo { get; set; }
        public int Mes { get; set; }
        public int Anno { get; set; } 
    }
}
