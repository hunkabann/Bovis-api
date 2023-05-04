using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{

	[Table(Schema = "dbo", Name = "tb_cat_sector")]
	public class TB_Cat_Sector
	{
		[Column("nukidsector"), NotNull, PrimaryKey, Identity] public int IdSector { get; set; }
		[Column("chsector"), NotNull] public string Sector { get; set; }
		[Column("boactivo"), NotNull] public bool Activo { get; set; }
		[Column("nuult_valor_pk"), NotNull] public int UltValor { get; set; }
	}
}
