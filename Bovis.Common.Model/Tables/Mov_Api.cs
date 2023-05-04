using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "Mov_Api")]
	public class Mov_Api
	{
		[Column("IdMov"), NotNull, PrimaryKey, Identity] public int IdMov { get; set; }
		[Column("fkidRelApi"), NotNull] public int IdRel { get; set; }
		[Column("Usuario"), NotNull] public string Usuario { get; set; }
		[Column("Nombre"), NotNull] public string Nombre { get; set; }
		[Column("Roles"), NotNull] public string Roles { get; set; }
		[Column("ValorNuevo"), NotNull] public string ValorNuevo { get; set; }
		[Column("FechaAlta"), NotNull] public DateTime FechaAlta { get; set; }
	}
}
