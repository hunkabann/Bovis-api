using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_proyecto")]
	public class TB_Cat_TipoProyecto
	{
		[Column("Nukidtipo_proyecto"), NotNull, PrimaryKey, Identity] public int IdTipoProyecto { get; set; }
		[Column("Chtipo_proyecto"), NotNull] public string TipoProyecto { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
