using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_empleado")]
	public class TB_Cat_TipoEmpleado
	{
		[Column("Nukidtipo_empleado"), NotNull, PrimaryKey, Identity] public int IdTipoEmpleado { get; set; }
		[Column("Chtipo_empleado"), NotNull] public string TipoEmpleado { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
