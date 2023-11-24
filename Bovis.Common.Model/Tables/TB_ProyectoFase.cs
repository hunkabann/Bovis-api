using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_proyecto_fase")]
	public class TB_ProyectoFase
	{
		[Column("Nukidfase"), NotNull] public int IdFase { get; set; }
		[Column("NunumProyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("Nuorden"), NotNull] public byte Orden { get; set; }
		[Column("Chfase"), NotNull] public string Fase { get; set; }
		[Column("DtfechaIni"), NotNull] public DateTime FechaIni { get; set; }
		[Column("DtfechaFin"), NotNull] public DateTime FechaFin { get; set; }
	}
}
