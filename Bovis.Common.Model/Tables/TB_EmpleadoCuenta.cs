using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado_cuenta")]
	public class TB_EmpleadoCuenta
	{
		[Column("Nukidcuenta"), NotNull, PrimaryKey, Identity] public int IdCuenta { get; set; }
		[Column("NunumEmpleadoRrHh"), NotNull] public int NumEmpleadoRrHh { get; set; }
	}
}
