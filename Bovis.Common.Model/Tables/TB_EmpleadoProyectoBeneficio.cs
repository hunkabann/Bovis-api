using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado_proyecto_beneficio")]
	public class TB_EmpleadoProyectoBeneficio
	{
		[Column("nunum_proyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("nunum_empleado_rr_hh"), NotNull] public string NumEmpleadoRrHh { get; set; }
		//[Column("NuporcentajeParticipacion"), NotNull] public decimal PorcentajeParticipacion { get; set; }
		//[Column("ChaliasPuesto"), NotNull] public string AliasPuesto { get; set; }
		//[Column("ChgrupoProyecto"), NotNull] public string GrupoProyecto { get; set; }
		//[Column("DtfechaIni"), NotNull] public DateTime FechaIni { get; set; }
		//[Column("DtfechaFin"), Nullable] public DateTime? FechaFin { get; set; }

        [Column("nukidbeneficio"), NotNull] public int IdBeneficio { get; set; }
        [Column("nucostobeneficio"), NotNull] public decimal nucostobeneficio { get; set; }
    }
}
