using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_resultado")]
	public class TB_Cat_TipoResultado
	{
		[Column("NukidtipoResultado"), NotNull, PrimaryKey, Identity] public int IdTipoResultado { get; set; }
		[Column("ChtipoResultado"), NotNull] public string TipoResultado { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
