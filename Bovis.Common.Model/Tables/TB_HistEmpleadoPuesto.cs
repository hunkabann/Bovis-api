using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_hist_empleado_puesto")]
	public class TB_HistEmpleadoPuesto
	{
		[Column("NunumEmpleadoRrHh"), NotNull, PrimaryKey, Identity] public int NumEmpleadoRrHh { get; set; }
		[Column("ChcvePuesto"), NotNull] public string CvePuesto { get; set; }
		[Column("DtfechaIni"), NotNull] public DateTime FechaIni { get; set; }
		[Column("DtfechaFin"), NotNull] public DateTime FechaFin { get; set; }
	}
}
