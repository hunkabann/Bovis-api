using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_proyecto_viatico")]
	public class TB_ProyectoViatico
	{
		[Column("NunumProyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("Nukidviatico"), NotNull] public int IdViatico { get; set; }
		[Column("Nucosto"), NotNull] public decimal Costo { get; set; }
	}
}
