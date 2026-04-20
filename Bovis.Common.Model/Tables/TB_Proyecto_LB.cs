using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_proyecto_lb")]
    public class TB_Proyecto_LB
    {
        [Column("nunum_proyecto"), NotNull, PrimaryKey]
        public int NumProyecto { get; set; }

        [Column("chproyecto"), NotNull]
        public string Proyecto { get; set; } = null!;

        [Column("chalcance"), Nullable]
        public string? Alcance { get; set; }

        [Column("chcp"), Nullable]
        public string? Cp { get; set; }

        [Column("chciudad"), Nullable]
        public string? Ciudad { get; set; }

        [Column("nukidpais"), Nullable]
        public int? IdPais { get; set; }

        [Column("nukidestatus"), Nullable]
        public int? IdEstatus { get; set; }

        [Column("nukidsector"), Nullable]
        public int? IdSector { get; set; }

        [Column("nukidtipo_proyecto"), Nullable]
        public int? IdTipoProyecto { get; set; }

        [Column("nukidresponsable_preconstruccion"), Nullable]
        public string? IdResponsablePreconstruccion { get; set; }

        [Column("nukidresponsable_construccion"), Nullable]
        public string? IdResponsableConstruccion { get; set; }

        [Column("nukidresponsable_ehs"), Nullable]
        public string? IdResponsableEhs { get; set; }

        [Column("nukidresponsable_supervisor"), Nullable]
        public string? IdResponsableSupervisor { get; set; }

        [Column("nukidempresa"), Nullable]
        public int? IdEmpresa { get; set; }

        [Column("nukiddirector_ejecutivo"), Nullable]
        public string? IdDirectorEjecutivo { get; set; }

        [Column("nucosto_promedio_m2"), Nullable]
        public decimal? CostoPromedioM2 { get; set; }

        [Column("dtfecha_ini"), Nullable]
        public DateTime? FechaIni { get; set; }

        [Column("dtfecha_fin"), Nullable]
        public DateTime? FechaFin { get; set; }

        [Column("dtfecha_prox_auditoria"), Nullable]
        public DateTime? FechaProxAuditoria { get; set; }

        [Column("dtfecha_auditoria_inicial"), Nullable]
        public DateTime? FechaAuditoriaInicial { get; set; }

        [Column("chnombre_responsable_asignado"), Nullable]
        public string? ResponsableAsignado { get; set; }

        [Column("nuimpuesto_nomina"), Nullable]
        public int? ImpuestoNomina { get; set; }

        [Column("nukidunidaddenegocio"), Nullable]
        public int? IdUnidadDeNegocio { get; set; }

        [Column("dtfecha_vigencia_ini"), Nullable]
        public DateTime? Vigencia_Ini { get; set; }

        [Column("dtfecha_vigencia_fin"), Nullable]
        public DateTime? Vigencia_Fin { get; set; }

        // NUEVO campo exclusivo de línea base
        [Column("nukidlinea_base"), NotNull]
        public int IdLineaBase { get; set; }
    }
}