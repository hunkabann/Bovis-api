using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_unidad_negocio")]
	public class TB_Cat_UnidadNegocio
	{
		[Column("Nukidunidad_negocio"), NotNull, PrimaryKey, Identity] public int IdUnidadNegocio { get; set; }
		[Column("Chunidad_negocio"), NotNull] public string UnidadNegocio { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
