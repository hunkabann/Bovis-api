using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cliente")]
	public class TB_Cliente
	{
		[Column("Nukidcliente"), NotNull, PrimaryKey, Identity] public int IdCliente { get; set; }
		[Column("Chrfc"), NotNull] public string Rfc { get; set; }
		[Column("Chcliente"), NotNull] public string Cliente { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
