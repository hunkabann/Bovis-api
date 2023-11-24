using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Cie_Registros
    {
        public int TotalRegistros {  get; set; }
        public List<Cie_Detalle> Registros { get; set;}
    }

    public class Cie_Detalle
    {
        public int IdCie { get; set; }
        public string? NombreCuenta { get; set; }
        public string? Cuenta { get; set; }
        public string? TipoPoliza { get; set; }
        public string? Numero { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Mes { get; set; }
        public string? Concepto { get; set; }  
        public string? CentroCostos { get; set; }
        public string? Proyectos { get; set; }
        public decimal? SaldoInicial { get; set; }
        public decimal? Debe { get; set; }
        public decimal? Haber { get; set; }
        public decimal? Movimiento { get; set; }
        public string? Empresa { get; set; }
        public int? NumProyecto { get; set; }
        public string? TipoCuenta { get; set; }
        public string? EdoResultados { get; set; }
        public string? Responsable { get; set; }
        public string? TipoProyecto { get; set; }
        public string? TipoPy { get; set; }
        public string? ClasificacionPy { get; set; }
        public bool? Activo { get; set; }
        public int? IdArchivo { get; set; }
        public string? NombreArchivo { get; set; }
    }

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
        public int? Estatus { set; get; }
    }

    public class CuentaContable_Detalle
    {
        public string? Cuenta { get; set; }
        public string? TipoCuenta { get; set; }
        public string? TipoResultado { get; set; }
        public string? TipoPY { get; set; }
        public string? ClasificacionPY { get; set; }


    }

    public class EmpresaRegistro
    {
        public int nukidempresa { get; set; }
        public string chempresa { get; set; }
        public string rfc { get; set; }
        public int nucoi { set; get; }
        public int nunoi { set; get; }
        public int nusae { set; get; }
        public bool boactivo { set; get; }
    }

    public class ProyectoData_Detalle
    {
        public string? Proyecto { get; set; }
        public int? NumProyecto { get; set; }
        public string? Responsable { get; set; }
        public string? TipoProyecto { get; set; }
    }
}
