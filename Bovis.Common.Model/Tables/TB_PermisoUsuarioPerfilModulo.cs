using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_permiso_usuario_perfil_modulo")]
	public class TB_PermisoUsuarioPerfilModulo
	{
		[Column("Nukidmodulo"), NotNull, PrimaryKey, Identity] public int IdModulo { get; set; }
		[Column("Nukidperfil"), NotNull] public int IdPerfil { get; set; }
		[Column("Nukidusuario"), NotNull] public int IdUsuario { get; set; }
		[Column("Nupermiso"), NotNull] public byte Permiso { get; set; }
	}
}
