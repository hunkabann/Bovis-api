using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class CieRegistro
    {
        public int? IdCie { get; set; }
        public int? NumProyecto { get; set; }
        public int? IdTipoCie { get; set; }
        public int? IdTipoPoliza { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? FechaCaptura { get; set; }
        public string? Concepto { get; set; }
        public decimal? SaldoIni { get; set; }
        public decimal? Debe { get; set; }
        public decimal? Haber { get; set; }
        public decimal? Movimiento { get; set; }
        public string? EdoResultados { get; set; }
        public byte? Mes { get; set; }
        public int? IdCentroCostos { get; set; }
        public int? IdTipoCtaContable { get; set; }
    }

    public class CuentaContable_Detalle
    {
        public string Cuenta { get; set; }
        public string TipoCuenta { get; set; }
        public string TipoResultado { get; set; }
        public string TipoPY { get; set; }
        public string ClasificacionPY { get; set; }


    }

    public class ProyectoData_Detalle
    {
        public string Proyecto { get; set; }
        public int NumProyecto { get; set; }
        public string Responsable { get; set; }
        public string TipoProyecto { get; set; }
    }
}
