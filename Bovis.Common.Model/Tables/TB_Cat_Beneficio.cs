using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_beneficio")]
	public class TB_Cat_Beneficio
	{
		[Column("Nukidbeneficio"), NotNull, PrimaryKey, Identity] public int IdBeneficio { get; set; }
		[Column("Chbeneficio"), NotNull] public string Beneficio { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
