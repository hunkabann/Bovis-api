using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_puesto")]
	public class TB_Cat_Puesto
	{
		[Column("Nukid_puesto"), NotNull, PrimaryKey, Identity] public string CvePuesto { get; set; }
		[Column("Nukidnivel"), NotNull] public int IdNivel { get; set; }
		[Column("Chpuesto"), NotNull] public string Puesto { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("nusalario_min"), NotNull] public decimal SalarioMin { get; set; }
		[Column("nusalario_max"), Nullable] public decimal? SalarioMax { get; set; }
		[Column("nusalio_prom"), Nullable] public decimal? SalioProm { get; set; }
	}
}
