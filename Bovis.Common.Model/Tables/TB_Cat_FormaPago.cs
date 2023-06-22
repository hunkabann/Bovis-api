using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_forma_pago")]
	public class TB_Cat_FormaPago
	{
		[Column("Nukidforma_pago"), NotNull, PrimaryKey, Identity] public int IdFormaPago { get; set; }
		[Column("Chtipo_documento"), NotNull] public string TipoDocumento { get; set; }
		[Column("Chcve_forma_pago"), NotNull] public string Cve { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
