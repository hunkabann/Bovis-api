using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empresa_cuenta")]
	public class TB_EmpresaCuenta
	{
		[Column("Nukidcuenta"), NotNull, PrimaryKey, Identity] public int IdCuenta { get; set; }
		[Column("Nukidempresa"), NotNull] public int IddEmpresa { get; set; }
	}
}
