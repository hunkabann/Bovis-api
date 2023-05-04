using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cuenta_banco")]
	public class TB_CuentaBanco
	{
		[Column("Nukidcuenta"), NotNull, PrimaryKey, Identity] public int IdCuenta { get; set; }
		[Column("Nukidbanco"), Nullable] public int? IdBanco { get; set; }
		[Column("ChnoCta"), NotNull] public string NoCta { get; set; }
		[Column("Chclabe"), NotNull] public string Clabe { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("Chsucursal"), NotNull] public string Sucursal { get; set; }
	}
}
