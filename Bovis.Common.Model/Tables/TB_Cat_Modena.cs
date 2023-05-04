using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_modena")]
	public class TB_Cat_Modena
	{
		[Column("Nukidmoneda"), NotNull, PrimaryKey, Identity] public int IdMoneda { get; set; }
		[Column("Chmoneda"), NotNull] public string Moneda { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
