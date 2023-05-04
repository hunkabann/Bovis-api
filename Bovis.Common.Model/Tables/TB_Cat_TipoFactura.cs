using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_factura")]
	public class TB_Cat_TipoFactura
	{
		[Column("NukidtipoFactura"), NotNull, PrimaryKey, Identity] public int IdTipoFactura { get; set; }
		[Column("ChtipoFactura"), NotNull] public string TipoFactura { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
