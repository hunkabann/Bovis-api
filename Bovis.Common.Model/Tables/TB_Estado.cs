using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_estado")]
	public class TB_Estado
	{
		[Column("Nukidestado"), NotNull, PrimaryKey, Identity] public int IdEstado { get; set; }
		[Column("Nukidpais"), NotNull] public int IdPais { get; set; }
		[Column("Chestado"), NotNull] public string Estado { get; set; }
		[Column("Chcve_entidad"), NotNull] public string CveEntidad { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
