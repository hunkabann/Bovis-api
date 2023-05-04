using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_bitacora")]
	public class TB_Bitacora
	{
		[Column("Nukidbitacora"), NotNull, PrimaryKey, Identity] public int IdBitacora { get; set; }
		[Column("Chusuario"), NotNull] public string Usuario { get; set; }
		[Column("Chbitacora"), NotNull] public string Bitacora { get; set; }
		[Column("Dtfecha"), NotNull] public DateTime Fecha { get; set; }
		[Column("Nukidoperacion"), NotNull] public int IdOperacion { get; set; }
	}
}
