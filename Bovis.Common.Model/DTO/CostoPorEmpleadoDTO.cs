using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.DTO
{
    public class CostoPorEmpleadoDTO
    {
        public int IdCostoEmpleado { get; set; }
        public string NumEmpleadoRrHh { get; set; } = string.Empty;
        public int? NuAnno { get; set; }
        public int? NuMes { get; set; }
        public int? NumEmpleadoNoi { get; set; } = 0;
        public int? IdPersona { get; set; } = 0;
        public string? Reubicacion { get; set; }
        public int? IdPuesto { get; set; } = 0;
        public int? NumProyecto { get; set; } = 0;
        public int? IdUnidadNegocio { get; set; } = 0; 
        public int? IdEmpresa { get; set; } = 0;
        public string? Timesheet { get; set; }
        public string? IdEmpleadoJefe { get; set; } = string.Empty;
        public DateTime? FechaIngreso { get; set; }
        public decimal? Antiguedad { get; set; } = 0M;
        public decimal? AvgDescuentoEmpleado { get; set; } = 0M;
        public decimal? MontoDescuentoMensual { get; set; } = 0M;
        public decimal? SueldoNetoPercibidoMensual { get; set; } = 0M;
        public decimal? RetencionImss { get; set; } = 0M;
        public decimal? Ispt { get; set; } = 0M;
        public decimal? SueldoBruto { get; set; } = 0M;
        public decimal? Anual { get; set; } = 0M;
        public decimal? AguinaldoCantMeses { get; set; } = 0M;
        public decimal? AguinaldoMontoProvisionMensual { get; set; } = 0M;
        public decimal? PvDiasVacasAnuales { get; set; } = 0M;
        public decimal? PvProvisionMensual { get; set; } = 0M;
        public decimal? IndemProvisionMensual { get; set; } = 0M;
        public decimal? AvgBonoAnualEstimado { get; set; } = 0M;
        public decimal? BonoAnualProvisionMensual { get; set; } = 0M;
        public decimal? SgmmCostoTotalAnual { get; set; } = 0M;
        public decimal? SgmmCostoMensual { get; set; } = 0M;
        public decimal? SvCostoTotalAnual { get; set; } = 0M;
        public decimal? SvCostoMensual { get; set; } = 0M;
        public decimal? VaidCostoMensual { get; set; } = 0M;
        public decimal? VaidComisionCostoMensual { get; set; } = 0M;
        public decimal? PtuProvision { get; set; } = 0M;
        public bool? Beneficios { get; set; }
        public decimal? Impuesto3sNomina { get; set; } = 0M;
        public decimal? Imss { get; set; } = 0M;
        public decimal? Retiro2 { get; set; } = 0M;
        public decimal? CesantesVejez { get; set; } = 0M;
        public decimal? Infonavit { get; set; } = 0M;
        public decimal? CargasSociales { get; set; } = 0M;
        public decimal? CostoMensualEmpleado { get; set; } = 0M;
        public decimal? CostoMensualProyecto { get; set; } = 0M;
        public decimal? CostoAnualEmpleado { get; set; } = 0M;
        public decimal? IndiceCostoLaboral { get; set; } = 0M;
        public decimal? IndiceCargaLaboral { get; set; } = 0M;
        public DateTime? FechaActualizacion { get; set; }
        public bool? RegHistorico { get; set; } = false;
        public int ImpuestoNomina { get; set; } 
        public int IdCategoria { get; set; } = 0;
        public string? ChCategoria { get; set; }
        //ATC
        public double? cotizacion { get; set; }
        
    }
}
