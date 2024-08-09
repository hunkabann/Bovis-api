using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.DTO
{
    public class EmpleadoProyectoBeneficioDTO
    {
        public int Id { get; set; }
        public int NumProyecto { get; set; }
        public string NumEmpleadoRrHh { get; set; } = string.Empty;
        public int IdBeneficio { get; set; }
        public decimal nucostobeneficio { get; set; }
        
        
    }
}
