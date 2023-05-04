using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_operacion")]
	public class TB_Operacion
	{
		[Column("Nukidoperacion"), NotNull, PrimaryKey, Identity] public int IdOperacion { get; set; }
		[Column("Choperacion"), NotNull] public string Operacion { get; set; }
		[Column("Chdescripcion"), NotNull] public string? Descripcion { get; set; }
	}
}
