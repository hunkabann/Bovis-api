using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_perfil_usuario")]
	public class TB_PerfilUsuario
	{
        [Column("Nukid_perfil_usuario"), NotNull, PrimaryKey, Identity] public int IdPerfilUsuario { get; set; }
        [Column("Nukidperfil"), NotNull] public int IdPerfil { get; set; }
		[Column("Nukidusuario"), NotNull] public int IdUsuario { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
