using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "TB_Cat_tipo_gasto")]
	public class TB_Cat_TipoGasto
	{
		[Column("NukidtipoGasto"), NotNull, PrimaryKey, Identity] public int IdTipoGasto { get; set; }
		[Column("ChtipoGasto"), NotNull] public string TipoGasto { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
