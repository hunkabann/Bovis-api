using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_proyecto")]
	public class TB_Proyecto
	{
		[Column("nunum_proyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("Chproyecto"), NotNull] public string Proyecto { get; set; }
		[Column("Chalcance"), Nullable] public string? Alcance { get; set; }
		[Column("Chcp"), Nullable] public string? Cp { get; set; }
		[Column("Chciudad"), Nullable] public string? Ciudad { get; set; }
		[Column("Nukidpais"), Nullable] public int? IdPais { get; set; }
		[Column("Nukidestatus"), Nullable] public int? IdEstatus { get; set; }
		[Column("Nukidsector"), Nullable] public int? IdSector { get; set; }
		[Column("nukidtipo_proyecto"), Nullable] public int? IdTipoProyecto { get; set; }
		[Column("nukidresponsable_preconstruccion"), Nullable] public string? IdResponsablePreconstruccion { get; set; }
		[Column("nukidresponsable_construccion"), Nullable] public string? IdResponsableConstruccion { get; set; }
		[Column("nukidresponsable_ehs"), Nullable] public string? IdResponsableEhs { get; set; }
		[Column("nukidresponsable_supervisor"), Nullable] public string? IdResponsableSupervisor { get; set; }
		[Column("Nukidempresa"), Nullable] public int? IdEmpresa { get; set; }
		[Column("nukiddirector_ejecutivo"), Nullable] public string? IdDirectorEjecutivo { get; set; }
		[Column("nucosto_promedio_m2"), Nullable] public decimal? CostoPromedioM2 { get; set; }
		[Column("dtfecha_ini"), Nullable] public DateTime FechaIni { get; set; }
		[Column("dtfecha_fin"), Nullable] public DateTime? FechaFin { get; set; }
		[Column("dtfecha_auditoria_inicial"), Nullable] public DateTime? FechaAuditoriaInicial { get; set; }
		[Column("dtfecha_prox_auditoria"), Nullable] public DateTime? FechaProxAuditoria { get; set; }
        [Column("Chnombre_responsable_asignado"), Nullable] public string? ResponsableAsignado { get; set; }
        [Column("Nuimpuesto_nomina"), NotNull] public int ImpuestoNomina { get; set; }
    }
}
