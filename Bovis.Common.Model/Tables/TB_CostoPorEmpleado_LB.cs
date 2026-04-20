using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_costo_por_empleado_lb")]
    public class TB_CostoPorEmpleado_LB
    {
        [Column("nukid_costo"), NotNull, PrimaryKey]
        public int IdCostoEmpleado { get; set; }

        [Column("nukid_persona"), NotNull]
        public int IdPersona { get; set; }

        [Column("nunum_empleado_rr_hh"), NotNull]
        public string NumEmpleadoRrHh { get; set; } = string.Empty;

        [Column("nunum_empleado_noi"), Nullable]
        public int? NumEmpleadoNoi { get; set; }

        [Column("chreubicacion"), Nullable]
        public string? Reubicacion { get; set; }

        [Column("nukid_puesto"), Nullable]
        public int? IdPuesto { get; set; }

        [Column("nunum_proyecto"), NotNull]
        public int NumProyecto { get; set; }

        [Column("nukid_unidad_negocio"), NotNull]
        public int IdUnidadNegocio { get; set; }

        [Column("nukid_empresa"), NotNull]
        public int IdEmpresa { get; set; }

        [Column("chtimesheet"), Nullable]
        public string? Timesheet { get; set; }

        [Column("nukid_empleado_jefe"), NotNull]
        public string IdEmpleadoJefe { get; set; } = string.Empty;

        [Column("dtfecha_ingreso"), NotNull]
        public DateTime FechaIngreso { get; set; }

        [Column("nuantiguedad"), Nullable]
        public double? Antiguedad { get; set; }

        [Column("nuavg_descuento_empleado"), Nullable]
        public double? AvgDescuentoEmpleado { get; set; }

        [Column("numonto_descuento_mensual"), Nullable]
        public double? MontoDescuentoMensual { get; set; }

        [Column("nusueldo_neto_percibido_mensual"), Nullable]
        public double? SueldoNetoPercibidoMensual { get; set; }

        [Column("nuretencion_imss"), Nullable]
        public double? RetencionImss { get; set; }

        [Column("nuispt"), Nullable]
        public double? Ispt { get; set; }

        [Column("nusueldo_bruto"), Nullable]
        public double? SueldoBruto { get; set; }

        [Column("nuanual"), Nullable]
        public double? Anual { get; set; }

        [Column("nuaguinaldo_cant_meses"), Nullable]
        public double? AguinaldoCantMeses { get; set; }

        [Column("nuaguinaldo_monto_provision_mensual"), Nullable]
        public double? AguinaldoMontoProvisionMensual { get; set; }

        [Column("nupv_dias_vacas_anuales"), Nullable]
        public double? PvDiasVacacionesAnuales { get; set; }

        [Column("nupv_provision_mensual"), Nullable]
        public double? PvProvisionMensual { get; set; }

        [Column("nuindem_provision_mensual"), Nullable]
        public double? IndemnizacionProvisionMensual { get; set; }

        [Column("nuavg_bono_anual_estimado"), Nullable]
        public double? AvgBonoAnualEstimado { get; set; }

        [Column("nubono_anual_provision_mensual"), Nullable]
        public double? BonoAnualProvisionMensual { get; set; }

        [Column("nusgmm_costo_total_anual"), Nullable]
        public double? SgmmCostoTotalAnual { get; set; }

        [Column("nusgmm_costo_mensual"), Nullable]
        public double? SgmmCostoMensual { get; set; }

        [Column("nusv_costo_total_anual"), Nullable]
        public double? SvCostoTotalAnual { get; set; }

        [Column("nusv_costo_mensual"), Nullable]
        public double? SvCostoMensual { get; set; }

        [Column("nuvaid_costo_mensual"), Nullable]
        public double? VaidCostoMensual { get; set; }

        [Column("nuvaid_comision_costo_mensual"), Nullable]
        public double? VaidComisionCostoMensual { get; set; }

        [Column("nuptu_provision"), Nullable]
        public double? PtuProvision { get; set; }

        [Column("bobeneficios"), Nullable]
        public bool? Beneficios { get; set; }

        [Column("nuimpuesto_3_s_nomina"), Nullable]
        public double? Impuesto3SNomina { get; set; }

        [Column("nuimss"), Nullable]
        public double? Imss { get; set; }

        [Column("nuretiro_2"), Nullable]
        public double? Retiro2 { get; set; }

        [Column("nucesantes_vejez"), Nullable]
        public double? CesantiaVejez { get; set; }

        [Column("nuinfonavit"), Nullable]
        public double? Infonavit { get; set; }

        [Column("nucargas_sociales"), Nullable]
        public double? CargasSociales { get; set; }

        [Column("nucosto_mensual_empleado"), Nullable]
        public double? CostoMensualEmpleado { get; set; }

        [Column("nucosto_mensual_proyecto"), Nullable]
        public double? CostoMensualProyecto { get; set; }

        [Column("nucosto_anual_empleado"), Nullable]
        public double? CostoAnualEmpleado { get; set; }

        [Column("nucosto_salario_bruto"), Nullable]
        public double? CostoSalarioBruto { get; set; }

        [Column("nucosto_salario_neto"), Nullable]
        public double? CostoSalarioNeto { get; set; }

        [Column("nuanno"), NotNull]
        public int Anio { get; set; }

        [Column("numes"), NotNull]
        public int Mes { get; set; }

        [Column("dtfecha_actualizacion"), NotNull]
        public DateTime FechaActualizacion { get; set; }

        [Column("boreg_historico"), Nullable]
        public bool? RegHistorico { get; set; }

        [Column("dt_fecha_insert"), Nullable]
        public DateTime? FechaInsert { get; set; }

        [Column("dtfecha_vigencia_ini"), Nullable]
        public DateTime? VigenciaIni { get; set; }

        [Column("dtfecha_vigencia_fin"), Nullable]
        public DateTime? VigenciaFin { get; set; }

        [Column("nukidlinea_base"), NotNull]
        public int IdLineaBase { get; set; }
    }
}