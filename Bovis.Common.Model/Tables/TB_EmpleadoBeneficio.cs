using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado_beneficio")]
	public class TB_EmpleadoBeneficio
	{
		[Column("Nukidbeneficio"), NotNull, PrimaryKey, Identity] public int IdBeneficio { get; set; }
		[Column("NunumEmpleadoRrHh"), NotNull] public int NumEmpleadoRrHh { get; set; }
		[Column("Nucosto"), NotNull] public decimal Costo { get; set; }
	}
}
