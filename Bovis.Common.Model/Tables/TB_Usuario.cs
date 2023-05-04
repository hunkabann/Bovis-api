using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_usuario")]
	public class TB_Usuario
	{
		[Column("Nukidusuario"), NotNull, PrimaryKey, Identity] public int IdUsuario { get; set; }
		[Column("Nukidperfil"), NotNull] public int IdPerfil { get; set; }
		[Column("Nukidpersona"), NotNull] public int IdPersona { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("Chusuario"), NotNull] public string Usuario { get; set; }
	}
}
