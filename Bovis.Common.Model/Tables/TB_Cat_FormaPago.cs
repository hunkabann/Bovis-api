using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_forma_pago")]
	public class TB_Cat_FormaPago
	{
		[Column("NukidformaPago"), NotNull, PrimaryKey, Identity] public int IdFormaPago { get; set; }
		[Column("ChtipoDocumento"), NotNull] public string TipoDocumento { get; set; }
		[Column("ChcveFormaPago"), NotNull] public string Cve { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
