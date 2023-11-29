using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_proyecto_fase")]
	public class TB_ProyectoFase
	{
		[Column("Nukidfase"), NotNull] public int IdFase { get; set; }
		[Column("Nunum_proyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("Nuorden"), NotNull] public int Orden { get; set; }
		[Column("Chfase"), NotNull] public string Fase { get; set; }
		[Column("Dtfecha_ini"), NotNull] public DateTime FechaIni { get; set; }
		[Column("Dtfecha_fin"), NotNull] public DateTime FechaFin { get; set; }
	}
}
