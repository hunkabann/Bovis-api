using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cliente_empresa")]
	public class TB_ClienteEmpresa
	{
		[Column("Nukidcliente"), NotNull, PrimaryKey, Identity] public int IdCliente { get; set; }
		[Column("Nukidempresa"), NotNull] public int IdEmpresa { get; set; }
	}
}
