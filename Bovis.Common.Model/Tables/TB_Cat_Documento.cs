using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_documento")]
	public class TB_Cat_Documento
	{
		[Column("Nukiddocumento"), NotNull, PrimaryKey, Identity] public int IdDocumento { get; set; }
		[Column("nukidtipo_documento"), Nullable] public int? IdTipoDocumento { get; set; }
		[Column("Chdocumento"), NotNull] public string Documento { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
