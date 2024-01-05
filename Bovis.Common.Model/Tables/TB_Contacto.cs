using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_contacto")]
	public class TB_Contacto
	{
		[Column("Nunum_proyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("Chnombre"), Nullable] public string Nombre { get; set; }
		[Column("Chposicion"), Nullable] public string Posicion { get; set; }
		[Column("Chtelefono"), Nullable] public string Telefono { get; set; }
		[Column("Chcorreo"), Nullable] public string Correo { get; set; }
	}
}
