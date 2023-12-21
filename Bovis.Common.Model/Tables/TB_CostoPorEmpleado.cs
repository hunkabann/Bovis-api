using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_costo_por_empleado")]
    public class TB_CostoPorEmpleado
    {
        [Column("Nukid_costo"), NotNull, PrimaryKey, Identity] public int IdCosto { get; set; }
        [Column("Nuanno"), NotNull] public int NuAnno { get; set; }
        [Column("Numes"), NotNull] public int NuMes { get; set; }
        [Column("Nunum_empleado_rr_hh"), NotNull] public string NumEmpleadoRrHh { get; set; } = string.Empty;
        [Column("Nunum_empleado_noi"), Nullable] public int? NumEmpleadoNoi { get; set; } = 0;
        [Column("Nukid_persona"), Nullable] public int? IdPersona { get; set; } = 0;
        [Column("Chreubicacion"), Nullable] public string? Reubicacion { get; set; }
        [Column("Nukid_puesto"), Nullable] public int? IdPuesto { get; set; } = 0;
        [Column("Nunum_proyecto"), NotNull] public int NumProyecto { get; set; } = 0;
        [Column("Nukid_unidad_negocio"), NotNull] public int IdUnidadNegocio { get; set; } = 0;
        [Column("Nukid_empresa"), NotNull] public int IdEmpresa { get; set; } = 0;
        [Column("Chtimesheet"), Nullable] public string? Timesheet { get; set; }
        [Column("Nukid_empleado_jefe"), NotNull] public int IdEmpleadoJefe { get; set; } = 0;     
        [Column("Dtfecha_ingreso"), NotNull] public DateTime FechaIngreso { get; set; }
        [Column("Nuantiguedad"), Nullable] public decimal? Antiguedad { get; set; } = 0M;
        [Column("Nuavg_descuento_empleado"), Nullable] public decimal? AvgDescuentoEmpleado { get; set; } = 0M;
        [Column("Numonto_descuento_mensual"), Nullable] public decimal? MontoDescuentoMensual { get; set; } = 0M;
        [Column("Nusueldo_neto_percibido_mensual"), Nullable] public decimal? SueldoNetoPercibidoMensual { get; set; } = 0M;
        [Column("Nuretencion_imss"), Nullable] public decimal? RetencionImss { get; set; } = 0M;
        [Column("Nuispt"), Nullable] public decimal? Ispt { get; set; } = 0M;
        [Column("Nusueldo_bruto"), Nullable] public decimal? SueldoBruto { get; set; } = 0M;
        [Column("Nuanual"), Nullable] public decimal? Anual { get; set; } = 0M;
        [Column("Nuaguinaldo_cant_meses"), Nullable] public decimal? AguinaldoCantMeses { get; set; } = 0M;
        [Column("Nuaguinaldo_monto_provision_mensual"), Nullable] public decimal? AguinaldoMontoProvisionMensual { get; set; } = 0M;
        [Column("Nupv_dias_vacas_anuales"), Nullable] public decimal? PvDiasVacasAnuales { get; set; } = 0M;
        [Column("Nupv_provision_mensual"), Nullable] public decimal? PvProvisionMensual { get; set; } = 0M;
        [Column("Nuindem_provision_mensual"), Nullable] public decimal? IndemProvisionMensual { get; set; } = 0M;
        [Column("Nuavg_bono_anual_estimado"), Nullable] public decimal? AvgBonoAnualEstimado { get; set; } = 0M;
        [Column("Nubono_anual_provision_mensual"), Nullable] public decimal? BonoAnualProvisionMensual { get; set; } = 0M;
        [Column("Nusgmm_costo_total_anual"), Nullable] public decimal? SgmmCostoTotalAnual { get; set; } = 0M;
        [Column("Nusgmm_costo_mensual"), Nullable] public decimal? SgmmCostoMensual { get; set; } = 0M;
        [Column("Nusv_costo_total_anual"), Nullable] public decimal? SvCostoTotalAnual { get; set; } = 0M;
        [Column("Nusv_costo_mensual"), Nullable] public decimal? SvCostoMensual { get; set; } = 0M;
        [Column("Nuvaid_costo_mensual"), Nullable] public decimal? VaidCostoMensual { get; set; } = 0M;
        [Column("Nuvaid_comision_costo_mensual"), Nullable] public decimal? VaidComisionCostoMensual { get; set; } = 0M;
        [Column("Nuptu_provision"), Nullable] public decimal? PtuProvision { get; set; } = 0M;
        [Column("Bobeneficios"), Nullable] public bool? Beneficios { get; set; }
        [Column("Nuimpuesto_3_s_nomina"), Nullable] public decimal? Impuesto3sNomina { get; set; } = 0M;
        [Column("Nuimss"), Nullable] public decimal? Imss { get; set; } = 0M;
        [Column("Nuretiro_2"), Nullable] public decimal? Retiro2 { get; set; } = 0M;
        [Column("Nucesantes_vejez"), Nullable] public decimal? CesantesVejez { get; set; } = 0M;
        [Column("Nuinfonavit"), Nullable] public decimal? Infonavit { get; set; } = 0M;
        [Column("Nucargas_sociales"), Nullable] public decimal? CargasSociales { get; set; } = 0M;
        [Column("Nucosto_mensual_empleado"), Nullable] public decimal? CostoMensualEmpleado { get; set; } = 0M;
        [Column("Nucosto_mensual_proyecto"), Nullable] public decimal? CostoMensualProyecto { get; set; } = 0M;
        [Column("Nucosto_anual_empleado"), Nullable] public decimal? CostoAnualEmpleado { get; set; } = 0M;
        [Column("Nuindice_costo_laboral"), Nullable] public decimal? IndiceCostoLaboral { get; set; } = 0M;
        [Column("Nuindice_carga_laboral"), Nullable] public decimal? IndiceCargaLaboral { get; set; } = 0M;
        [Column("Dtfecha_actualizacion"), NotNull] public DateTime FechaActualizacion { get; set; }
        [Column("Boreg_historico"), NotNull] public bool RegHistorico { get; set; } = false;
    }

}
