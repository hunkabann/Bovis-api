using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_ingreso")]
	public class TB_Cat_Ingreso
	{
		[Column("Nukidingreso"), NotNull, PrimaryKey, Identity] public int IdIngreso { get; set; }
		[Column("Chingreso"), NotNull] public string Ingreso { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
