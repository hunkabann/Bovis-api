using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado_proyecto_beneficio")]
	public class TB_EmpleadoProyectoBeneficio
	{
		[Column("NunumProyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("NunumEmpleadoRrHh"), NotNull] public int NumEmpleadoRrHh { get; set; }
		[Column("NuporcentajeParticipacion"), NotNull] public decimal PorcentajeParticipacion { get; set; }
		[Column("ChaliasPuesto"), NotNull] public string AliasPuesto { get; set; }
		[Column("ChgrupoProyecto"), NotNull] public string GrupoProyecto { get; set; }
		[Column("DtfechaIni"), NotNull] public DateTime FechaIni { get; set; }
		[Column("DtfechaFin"), Nullable] public DateTime? FechaFin { get; set; }
	}
}
