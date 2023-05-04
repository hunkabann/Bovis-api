using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_unidad_negocio")]
	public class TB_Cat_UnidadNegocio
	{
		[Column("NukidunidadNegocio"), NotNull, PrimaryKey, Identity] public int IdUnidadNegocio { get; set; }
		[Column("ChunidadNegocio"), NotNull] public string UnidadNegocio { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
