using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_pais")]
	public class TB_Pais
	{
		[Column("Nukidpais"), NotNull, PrimaryKey, Identity] public int IdPais { get; set; }
		[Column("Chpais"), NotNull] public string Pais { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
