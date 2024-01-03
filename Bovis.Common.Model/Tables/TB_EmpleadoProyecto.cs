using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado_proyecto")]
	public class TB_EmpleadoProyecto
	{
		[Column("Nunum_proyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("Nunum_empleado_rr_hh"), NotNull] public string NumEmpleadoRrHh { get; set; }
		[Column("Nuporcentaje_participacion"), NotNull] public decimal PorcentajeParticipacion { get; set; }
		[Column("Chalias_puesto"), NotNull] public string AliasPuesto { get; set; }
		[Column("Chgrupo_proyecto"), NotNull] public string GrupoProyecto { get; set; }
		[Column("Dtfecha_ini"), NotNull] public DateTime FechaIni { get; set; }
		[Column("Dtfecha_fin"), Nullable] public DateTime? FechaFin { get; set; }
        [Column("Boactivo"), Nullable] public bool? Activo { get; set; }
    }
}
