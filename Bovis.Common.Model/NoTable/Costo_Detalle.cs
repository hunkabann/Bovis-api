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
        public int Mes {  get; set; }
        public int NumEmpleadoRrHh { get; set; }
        public int? NumEmpleadoNoi { get; set; }
        public int? IdPersona { get; set; }
        public string? NombrePersona { get; set; }
        public string? Reubicacion {  get; set; }
        public int? IdPuesto { get; set; }
        public string? Puesto { get; set; }
        public int NumProyecto { get; set; }
        public string Proyecto { get; set; }
        public int IdUnidadNegocio { get; set; }
        public string UnidadNegocio { get; set; }
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public string? Timesheet {  get; set; }
        public string IdEmpleadoJefe {  get; set; }
        public string NombreJefe {  get; set; }
        public DateTime FechaIngreso { get; set; }
        public decimal? Antiguedad {  get; set; }
        public decimal? AvgDescuentoEmpleado { get; set; }
        public decimal? MontoDescuentoMensual {  get; set; }
        public decimal? SueldoNetoMensual { get; set; }
        public decimal? RetencionImss {  get; set; }
        public decimal? Ispt {  get; set; }
        public decimal? SueldoBruto {  get; set; }
        public decimal? Anual {  get; set; }
        public decimal? AguinaldoCantidadMeses {  get; set; }
        public decimal? AguinaldoMontoProvisionMensual { get; set; }
        public decimal? PvDiasVacasAnuales { get; set; }
        public decimal? PvProvisionMensual { get; set; }
        public decimal? IndemProvisionMensual { get; set; }
        public decimal? AvgBonoAnualEstimado {  get; set; }
        public decimal? BonoAnualProvisionMensual { get; set; }
        public decimal? SgmmCostoTotalAnual {  get; set; }
        public decimal? SgmmCostoMensual {  get; set; }
        public decimal? SvCostoTotalAnual {  get; set; }
        public decimal? SvCostoMensual {  get; set; }
        public decimal? VaidCostoMensual {  get; set; }
        public decimal? VaidComisionCostoMensual {  get; set; }
        public decimal? PtuProvision {  get; set; }
        public bool? Beneficios {  get; set; }
        public decimal? Impuesto3sNomina {  get; set; }
        public decimal? Imss {  get; set; }
        public decimal? Retiro2 {  get; set; }
        public decimal? CesantesVejez {  get; set; }
        public decimal? Infonavit {  get; set; }
        public decimal? CargasSociales {  get; set; }
        public decimal? CostoMensualEmpleado {  get; set; }
        public decimal? CostoMensualProyecto {  get; set; }
        public decimal? CostoAnualEmpleado {  get; set; }
        public decimal? IndiceCostoLaboral {  get; set; }
        public decimal? IndiceCargaLaboral {  get; set; }
        public DateTime FechaActualizacion {  get; set; }
        public bool? RegHistorico {  get; set; }
    }

    public class Costo_Detalle
    {
        public int IdCosto { get; set; }
        public int NumEmpleadoRrHh { get; set; }
        public int? NumEmpleadoNoi { get; set; }
        public int? IdPersona { get; set; }
        public string? Reubicacion { get; set; }
        public int? IdPuesto { get; set; }
        public int? NumProyecto { get; set; }
        public int? IdUnidadNegocio { get; set; }
        public int? IdEmpresa { get; set; }
        public string? Timesheet { get; set; }
        public int? IdEmpleadoJefe { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public decimal? Antiguedad { get; set; }
        public decimal? AvgDescuentoEmpleado { get; set; }
        public decimal? MontoDescuentoMensual { get; set; }
        public decimal? SueldoNetoPercibidoMensual { get; set; }
        public decimal? RetencionImss { get; set; }
        public decimal? Ispt { get; set; }
        public decimal? SueldoBruto { get; set; }
        public decimal? Anual { get; set; }
        public decimal? AguinaldoCantMeses { get; set; }
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
        public decimal? CtlCostoMensualProyecto { get; set; }

    }
}
