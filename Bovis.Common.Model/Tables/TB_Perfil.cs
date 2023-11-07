using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_perfil")]
	public class TB_Perfil
	{
		[Column("Nukidperfil"), NotNull, PrimaryKey, Identity] public int IdPerfil { get; set; }
		[Column("Chperfil"), NotNull] public string Perfil { get; set; }
		[Column("Chdescripcion"), Nullable] public string? Descripcion { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
