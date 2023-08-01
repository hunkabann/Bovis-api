using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Dor_ObjetivosEmpleado
    {
        public int IdEmpOb { get; set; }
        public string? UnidadDeNegocio { get; set; }
        public string? Concepto { get; set; }
        public string? Descripcion { get; set; }
        public decimal? Meta { get; set; }
        public decimal? Real { get; set; }
        public decimal? PromedioReal { get; set; }
        public decimal? PorcentajeEstimado { get; set; }
        public decimal? PorcentajeReal { get; set; }
        public int? Acepto { get; set; }
        public string? MotivoR { get; set; }
        public DateTime? FechaCarga { get; set; }
        public DateTime? FechaAceptado { get; set; }
        public DateTime? FechaRechazo { get; set; }
        public int? Nivel { get; set; }
        public int? Valor { get; set; }
        public string? Tooltip { get; set; }

    }
}
