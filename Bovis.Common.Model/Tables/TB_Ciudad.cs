using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_ciudad")]
	public class TB_Ciudad
	{
		[Column("Nukidciudad"), NotNull, PrimaryKey, Identity] public int IdCiudad { get; set; }
		[Column("Nukidestado"), NotNull] public int IdEstado { get; set; }
		[Column("Chciudad"), NotNull] public string Ciudad { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
