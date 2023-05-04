using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "TB_Cobranza")]
	public class TB_Cobranza
	{
		[Column("Nukidcobranza"), NotNull, PrimaryKey, Identity] public int IdCobranza { get; set; }
		[Column("Chcod"), Nullable] public string? Cod { get; set; }
		[Column("Dtfecha"), Nullable] public string? Fecha { get; set; }
		[Column("Nucantidad"), Nullable] public string? Cantidad { get; set; }
	}
}
