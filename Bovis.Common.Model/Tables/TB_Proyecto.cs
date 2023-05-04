using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_proyecto")]
	public class TB_Proyecto
	{
		[Column("nunum_proyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("Chproyecto"), NotNull] public string Proyecto { get; set; }
		[Column("Chalcance"), NotNull] public string Alcance { get; set; }
		[Column("Chcp"), Nullable] public string? Cp { get; set; }
		[Column("Chciudad"), NotNull] public string Ciudad { get; set; }
		[Column("Nukidestatus"), NotNull] public int IdEstatus { get; set; }
		[Column("Nukidsector"), NotNull] public int IdSector { get; set; }
		[Column("nukidtipo_proyecto"), NotNull] public int IdTipoProyecto { get; set; }
		[Column("nukidresponsable_preconstruccion"), Nullable] public int? IdResponsablePreconstruccion { get; set; }
		[Column("nukidresponsable_construccion"), Nullable] public int? IdResponsableConstruccion { get; set; }
		[Column("nukidresponsable_ehs"), Nullable] public int? IdResponsableEhs { get; set; }
		[Column("nukidresponsable_supervisor"), Nullable] public int? IdResponsableSupervisor { get; set; }
		[Column("Nukidcliente"), NotNull] public int IdCliente { get; set; }
		[Column("Nukidempresa"), NotNull] public int IdEmpresa { get; set; }
		[Column("Nukidpais"), NotNull] public int IdPais { get; set; }
		[Column("nukiddirector_ejecutivo"), NotNull] public int IdDirectorEjecutivo { get; set; }
		[Column("nucosto_promedio_m2"), NotNull] public decimal CostoPromedioM2 { get; set; }
		[Column("dtfecha_ini"), NotNull] public DateTime FechaIni { get; set; }
		[Column("dtfecha_fin"), Nullable] public DateTime? FechaFin { get; set; }
	}
}
