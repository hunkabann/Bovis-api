using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class CostoEmpleado_Detalle
    { 
        public int IdCosto { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int NumEmpleadoRrHh { get; set; }
        public int? NumEmpleadoNoi { get; set; }
        public int? IdPersona { get; set; }
        public string? NombrePersona { get; set; }
        public string? Reubicacion { get; set; }
        public int? IdPuesto { get; set; }
        public string? Puesto { get; set; }
        public int NumProyecto { get; set; }
        public string Proyecto { get; set; }
        public int IdUnidadNegocio { get; set; }
        public string UnidadNegocio { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public string? Timesheet { get; set; }
        public string IdEmpleadoJefe { get; set; }
        public string NombreJefe { get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal? Antiguedad { get; set; }
        public decimal? AvgDescuentoEmpleado { get; set; }
        public decimal? MontoDescuentoMensual { get; set; }
        public decimal? SueldoNetoMensual { get; set; }
        public decimal? RetencionImss { get; set; }
        public decimal? Ispt { get; set; }
        public decimal? SueldoBruto { get; set; }
        public decimal? Anual { get; set; }
        public decimal? AguinaldoCantidadMeses { get; set; }
        public decimal? AguinaldoMontoProvisionMensual { get; set; }
        public decimal? PvDiasVacasAnuales { get; set; }
        public decimal? PvProvisionMensual { get; set; }
        public decimal? IndemProvisionMensual { get; set; }
        public decimal? AvgBonoAnualEstimado { get; set; }
        public decimal? BonoAnualProvisionMensual { get; set; }
        public decimal? SgmmCostoTotalAnual { get; set; }
        public decimal? SgmmCostoMensual { get; set; }
        public decimal? SvCostoTotalAnual { get; set; }
        public decimal? SvCostoMensual { get; set; }
        public decimal? VaidCostoMensual { get; set; }
        public decimal? VaidComisionCostoMensual { get; set; }
        public decimal? PtuProvision { get; set; }
        public bool? Beneficios { get; set; }
        public decimal? Impuesto3sNomina { get; set; }
        public decimal? Imss { get; set; }
        public decimal? Retiro2 { get; set; }
        public decimal? CesantesVejez { get; set; }
        public decimal? Infonavit { get; set; }
        public decimal? CargasSociales { get; set; }
        public decimal? CostoMensualEmpleado { get; set; }
        public decimal? CostoMensualProyecto { get; set; }
        public decimal? CostoAnualEmpleado { get; set; }
        public decimal? IndiceCostoLaboral { get; set; }
        public decimal? IndiceCargaLaboral { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool? RegHistorico { get; set; }
        public decimal? SalarioDiarioIntegrado { get; set; }
    }

    public class Costo_Detalle
    {
        public int IdCostoEmpleado { get; set; }
        /*
         *  Empleado
         */
        public int? IdPersona { get; set; } = 0;
        public string NumEmpleadoRrHh { get; set; }
        public int? NumEmpleadoNoi { get; set; } = 0;
        public string? NombreCompletoEmpleado { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? NombreEmpleado { get; set; }
        public int? IdCiudad { get; set; }
        public string? Ciudad { get; set; }
        public string? Reubicacion { get; set; }
        public int? IdPuesto { get; set; } = 0;
        public string? Puesto { get; set; }
        public int? NumProyecto { get; set; } = 0;
        public string? Proyecto { get; set; }
        public int? IdUnidadNegocio { get; set; } = 0;
        public string? UnidadNegocio { get; set; }
        public int? IdEmpresa { get; set; } = 0;
        public string? Empresa { get; set; }
        public string? Timesheet { get; set; }
        public string? IdEmpleadoJefe { get; set; } = string.Empty;
        public string? NombreJefe { get; set; }
        public string? Categoria { get; set; }
        /*
         * Seniority
         */
        public DateTime? FechaIngreso { get; set; }
        public decimal? Antiguedad { get; set; } = 0M;
        /*
         * Sueldo neto mensual (MN)
         */
        public decimal? AvgDescuentoEmpleado { get; set; } = 0M;
        public decimal? MontoDescuentoMensual { get; set; } = 0M;
        public decimal? SueldoNetoPercibidoMensual { get; set; } = 0M;
        public decimal? RetencionImss { get; set; } = 0M;
        public decimal? Ispt { get; set; } = 0M;
        /*
         * Sueldo bruto MN/USD
         */
        public decimal? SueldoBrutoInflacion { get; set; } = 0M;
        public decimal? Anual { get; set; } = 0M;
        /*
         * Aguinaldo
         */
        public decimal? AguinaldoCantidadMeses { get; set; }
        public decimal? AguinaldoMontoProvisionMensual { get; set; } = 0M;
        /*
         * Prima vacacional
         */
        public decimal? PvDiasVacasAnuales { get; set; } = 0M;
        public decimal? PvProvisionMensual { get; set; } = 0M;
        /*
         * IndemnizaciÃ³n
         */
        public decimal? IndemProvisionMensual { get; set; } = 0M;
        /*
         * ProvisiÃ³n bono anual
         */
        public decimal? AvgBonoAnualEstimado { get; set; } = 0M;
        public decimal? BonoAnualProvisionMensual { get; set; } = 0M;
        /*
         * GMM
         */
        public decimal? SgmmCostoTotalAnual { get; set; } = 0M;
        public decimal? SgmmCostoMensual { get; set; } = 0M;
        /*
         * Seguro de vida
         */
        public decimal? SvCostoTotalAnual { get; set; } = 0M;
        public decimal? SvCostoMensual { get; set; } = 0M;
        /*
         * Vales de despensa
         */
        public decimal? VaidCostoMensual { get; set; } = 0M;
        public decimal? VaidComisionCostoMensual { get; set; } = 0M;
        /*
         * PTU
         */
        public decimal? PtuProvision { get; set; } = 0M;
        /*
         * Beneficios
         */
        public List<Beneficio_Costo_Detalle>? Beneficios { get; set; }

        public List<Beneficio_Costo_Detalle>? Beneficiosproyecto { get; set; }
        /*
         * Cargas sociales e impuestos laborales
         */
        public decimal? Impuesto3sNomina { get; set; } = 0M;
        public decimal? Imss { get; set; } = 0M;
        public decimal? Retiro2 { get; set; } = 0M;
        public decimal? CesantesVejez { get; set; } = 0M;
        public decimal? Infonavit { get; set; } = 0M;
        public decimal? CargasSociales { get; set; } = 0M;
        /*
         * Costo total laboral BLL
         */
        public decimal? CostoMensualEmpleado { get; set; } = 0M;
        public decimal? CostoMensualProyecto { get; set; } = 0M;
        public decimal? CostoAnualEmpleado { get; set; } = 0M;
        public decimal? CostoSalarioBruto { get; set; } = 0M;
        public decimal? CostoSalarioNeto { get; set; } = 0M;


        public int? NuAnno { get; set; }
        public int? NuMes { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public bool? RegHistorico { get; set; } = false;
        public string? SalarioDiarioIntegrado { get; set; }

        //ATC
        public int? ImpuestoNomina { get; set; }
        
    }

    public class Beneficio_Costo_Detalle
    {
        public int Id { get; set; }
        public int IdBeneficio { get; set; }
        public string Beneficio { get; set; }
        public string NumEmpleadoRrHh { get; set; }
        public decimal Costo { get; set; }
        public int Mes { get; set; }
        public int Anio { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool RegHistorico { get; set; }
        public decimal nucostobeneficio { get; set; }
        public int NumProyecto { get; set; }

    }
}
