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
        public string? Meta { get; set; }
        public string Real { get; set; }
        public string PorcentajeEstimado { get; set; }
        public string PorcentajeReal { get; set; }
        public string? Acepto { get; set; }
        public string? MotivoR { get; set; }
        public DateTime? FechaCarga { get; set; }
        public DateTime? FechaAceptado { get; set; }
        public DateTime? FechaRechazo { get; set; }
        public string? Nivel { get; set; }
        public string? Valor { get; set; }
        public string? Tooltip { get; set; }

    }
}
