using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Dor_ObjetivosGenerales
    {
        public int Id { get; set; }
        public string? UnidadDeNegocio { get; set; }
        public string? Concepto { get; set; }
        public string? Descripcion { get; set; }
        public string? Meta { get; set; }
        public string Real { get; set; }
        public string PorcentajeEstimado { get; set; }
        public string PorcentajeReal { get; set; }
        public decimal Ingreso { get; set; }
        public decimal Gasto { get; set; }
        public string? Nivel { get; set; }
        public string? Valor { get; set; }
        public string? Tooltip { get; set; }
    }
}
