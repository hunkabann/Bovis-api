using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_contacto")]
	public class TB_Contacto
	{
		[Column("NunumProyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("Chnombre"), NotNull] public string Nombre { get; set; }
		[Column("Chposicion"), NotNull] public string Posicion { get; set; }
		[Column("Chtelefono"), NotNull] public string Telefono { get; set; }
		[Column("Chcorreo"), NotNull] public string Correo { get; set; }
	}
}
