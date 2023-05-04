using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_ingreso")]
	public class TB_Cat_TipoIngreso
	{
		[Column("NukidtipoIngreso"), NotNull, PrimaryKey, Identity] public int IdTipoIngreso { get; set; }
		[Column("ChtipoIngreso"), NotNull] public string TipoIngreso { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
