using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_documento")]
	public class TB_Cat_TipoDocumento
	{
		[Column("nukidtipo_documento"), NotNull, PrimaryKey, Identity] public int IdTipoDocumento { get; set; }
		[Column("chtipo_documento"), NotNull] public string TipoDocumento { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
