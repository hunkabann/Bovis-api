using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_viatico")]
	public class TB_Cat_Viatico
	{
		[Column("Nukidviatico"), NotNull, PrimaryKey, Identity] public int IdViatico { get; set; }
		[Column("Chviatico"), NotNull] public string Viatico { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
