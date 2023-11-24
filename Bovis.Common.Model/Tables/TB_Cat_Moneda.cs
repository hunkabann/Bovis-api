using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_moneda")]
	public class TB_Cat_Moneda
	{
		[Column("Nukidmoneda"), NotNull, PrimaryKey, Identity] public string IdMoneda { get; set; }
		[Column("Chmoneda"), NotNull] public string Moneda { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
