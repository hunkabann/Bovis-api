using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado_contrato")]
	public class TB_EmpleadoCorreos
	{
		[Column("NunumEmpleadoRrHh"), NotNull, PrimaryKey, Identity] public int NumEmpleadoRrHh { get; set; }
		[Column("Chcorreo"), NotNull] public string Correo { get; set; }
	}
}
