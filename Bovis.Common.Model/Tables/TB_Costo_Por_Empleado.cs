using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_costo_por_empleado")]
    public class TB_Costo_Por_Empleado
    {
        [Column("Nukid_costo"), NotNull, PrimaryKey, Identity] public int IdCosto { get; set; }
        [Column("Nunum_empleado_rr_hh"), NotNull] public int NumEmpleadoRrHh { get; set; }
        [Column("Nunum_empleado_noi"), Nullable] public int? NumEmpleadoNoi { get; set; }
        [Column("Nukid_persona"), Nullable] public int? IdPersona { get; set; }
        [Column("Chreubicacion"), Nullable] public string? Reubicacion { get; set; }
        [Column("Nukid_puesto"), Nullable] public int? IdPuesto { get; set; }
        [Column("Nunum_proyecto"), Nullable] public int? NumProyecto { get; set; }
        [Column("Nukid_unidad_negocio"), Nullable] public int? IdUnidadNegocio { get; set; }
        [Column("Nukid_empresa"), Nullable] public int? IdEmpresa { get; set; }
        [Column("Chtimesheet"), Nullable] public string? Timesheet { get; set; }
        [Column("Nukid_empleado_jefe"), Nullable] public int? IdEmpleadoJefe { get; set; }
        [Column("Dtfecha_ingreso"), Nullable] public DateTime? FechaIngreso { get; set; }
        [Column("Nuantiguedad"), Nullable] public decimal? Antiguedad { get; set; }
        [Column("Nuavg_descuento_empleado"), Nullable] public decimal? AvgDescuentoEmpleado { get; set; }
        [Column("Numonto_descuento_mensual"), Nullable] public decimal? MontoDescuentoMensual { get; set; }
        [Column("Nusueldo_neto_percibido_mensual"), Nullable] public decimal? SueldoNetoPercibidoMensual { get; set; }
        [Column("Nuretencion_imss"), Nullable] public decimal? RetencionImss { get; set; }
        [Column("Nuispt"), Nullable] public decimal? Ispt { get; set; }
        [Column("Nusueldo_bruto"), Nullable] public decimal? SueldoBruto { get; set; }
        [Column("Nuanual"), Nullable] public decimal? Anual { get; set; }
        [Column("Nuaguinaldo_cant_meses"), Nullable] public decimal? AguinaldoCantMeses { get; set; }
        [Column("Nuaguinaldo_monto_provision_mensual"), Nullable] public decimal? AguinaldoMontoProvisionMensual { get; set; }
        [Column("Nupv_dias_vacas_anuales"), Nullable] public decimal? PvDiasVacasAnuales { get; set; }
        [Column("Nupv_provision_mensual"), Nullable] public decimal? PvProvisionMensual { get; set; }
        [Column("Nuindem_provision_mensual"), Nullable] public decimal? IndemProvisionMensual { get; set; }
        [Column("Nuavg_bono_anual_estimado"), Nullable] public decimal? AvgBonoAnualEstimado { get; set; }
        [Column("Nubono_anual_provision_mensual"), Nullable] public decimal? BonoAnualProvisionMensual { get; set; }
        [Column("Nusgmm_costo_total_anual"), Nullable] public decimal? SgmmCostoTotalAnual { get; set; }
        [Column("Nusgmm_costo_mensual"), Nullable] public decimal? SgmmCostoMensual { get; set; }
        [Column("Nusv_costo_total_anual"), Nullable] public decimal? SvCostoTotalAnual { get; set; }
        [Column("Nusv_costo_mensual"), Nullable] public decimal? SvCostoMensual{ get; set; }
        [Column("Nuvaid_costo_mensual"), Nullable] public decimal? VaidCostoMensual { get; set; }
        [Column("Nuvaid_comision_costo_mensual"), Nullable] public decimal? VaidComisionCostoMensual { get; set; }
        [Column("Nuptu_provision"), Nullable] public decimal? PtuProvision { get; set; }
        [Column("Bobeneficios"), Nullable] public bool? Beneficios { get; set; }
        [Column("Nuimpuesto_3_s_nomina"), Nullable] public decimal? Impuesto3sNomina { get; set; }
        [Column("Nuimss"), Nullable] public decimal? Imss { get; set; }
        [Column("Nuretiro_2"), Nullable] public decimal? Retiro2 { get; set; }
        [Column("Nucesantes_vejez"), Nullable] public decimal? CesantesVejez { get; set; }
        [Column("Nuinfonavit"), Nullable] public decimal? Infonavit { get; set; }
        [Column("Nucargas_sociales"), Nullable] public decimal? CargasSociales { get; set; }
        [Column("Nuctl_costo_mensual_proyecto"), Nullable] public decimal? CtlCostoMensualProyecto { get; set; }
    }
}
