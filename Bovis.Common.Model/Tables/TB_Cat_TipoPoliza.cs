using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_poliza")]
	public class TB_Cat_TipoPoliza
	{
		[Column("NukidtipoPoliza"), NotNull, PrimaryKey, Identity] public int IdTipoPoliza { get; set; }
		[Column("ChtipoPoliza"), NotNull] public string TipoPoliza { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
