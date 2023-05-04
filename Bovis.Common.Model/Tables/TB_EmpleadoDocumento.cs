using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado_documento")]
	public class TB_EmpleadoDocumento
	{
		[Column("NunumEmpleadoRrHh"), NotNull, PrimaryKey, Identity] public int NumEmpleadoRrHh { get; set; }
		[Column("Nukiddocumento"), NotNull] public int IdDocumento { get; set; }
		[Column("Chdocumento"), NotNull] public byte[] Documento { get; set; }
	}
}
