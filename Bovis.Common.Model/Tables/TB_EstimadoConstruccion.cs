using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_estado")]
	public class TB_EstimadoConstruccion
	{
		[Column("NunumProyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("Chdescripcion"), NotNull] public string Descripcion { get; set; }
		[Column("Nucosto"), NotNull] public decimal Dosto { get; set; }
		[Column("NuareaM2"), NotNull] public int AreaM2 { get; set; }
		[Column("Numonto"), Nullable] public decimal? Monto { get; set; }
	}
}
