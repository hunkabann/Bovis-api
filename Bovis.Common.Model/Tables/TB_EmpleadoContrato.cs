using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado_contrato")]
	public class TB_EmpleadoContrato
	{
		[Column("NunumEmpleadoRrHh"), NotNull, PrimaryKey, Identity] public int NumEmpleadoRrHh { get; set; }
		[Column("DtfechaIni"), NotNull] public DateTime FechaIni { get; set; }
		[Column("Chnota"), NotNull] public string Nota { get; set; }
		[Column("DtfechaFin"), Nullable] public string? FechaFin { get; set; }
	}
}
