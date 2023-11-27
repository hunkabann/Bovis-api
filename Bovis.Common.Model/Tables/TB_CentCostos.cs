using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_centro_costos")]
	public class TB_CentCostos
	{
		[Column("NukidcentroCostos"), NotNull, PrimaryKey, Identity] public int IdCentroCostos { get; set; }
		[Column("ChclaveCostos"), NotNull] public string ClaveCostos { get; set; }
		[Column("ChcentroCostos"), NotNull] public string CentroCostos { get; set; }
		[Column("Nusaldo"), Nullable] public decimal? Saldo { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
