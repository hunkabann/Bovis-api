using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Dto.Request
{
    public class ConsultarFactura
    {
        public int? IdProyecto { get; set; }
        public int? IdCliente { get; set; }
        public int? IdEmpresa { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? noFactura { get; set; }
    }
}
