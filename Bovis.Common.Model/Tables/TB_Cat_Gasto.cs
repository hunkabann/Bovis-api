using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "TB_Cat_Gasto")]
	public class TB_Cat_Gasto
	{
		[Column("Nukidgasto"), NotNull, PrimaryKey, Identity] public int IdGasto { get; set; }
		[Column("Chgasto"), NotNull] public string Gasto { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("NukidtipoGasto"), Nullable] public int? IdTipoGasto { get; set; }
	}
}
