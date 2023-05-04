using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_contrato")]
	public class TB_Cat_TipoContrato
	{
		[Column("NukidtipoContrato"), NotNull, PrimaryKey, Identity] public int IdTipoContrato { get; set; }
		[Column("Chcontrato"), NotNull] public string Contrato { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("ChcveContrato"), Nullable] public byte? Cve { get; set; }
	}
}
