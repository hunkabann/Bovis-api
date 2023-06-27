using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_contrato")]
	public class TB_Cat_TipoContrato
	{
		[Column("Nukidtipo_contrato"), NotNull, PrimaryKey, Identity] public int IdTipoContrato { get; set; }
		[Column("Chcontrato"), NotNull] public string Contrato { get; set; }
		[Column("Chve_contrato"), Nullable] public string VeContrato { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
