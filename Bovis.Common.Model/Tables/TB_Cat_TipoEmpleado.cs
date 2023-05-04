using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_empleado")]
	public class TB_Cat_TipoEmpleado
	{
		[Column("NukidtipoEmpleado"), NotNull, PrimaryKey, Identity] public int IdTipoEmpleado { get; set; }
		[Column("ChtipoEmpleado"), NotNull] public string TipoEmpleado { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
