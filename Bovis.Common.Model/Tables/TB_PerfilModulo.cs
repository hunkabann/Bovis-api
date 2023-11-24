using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_perfil_modulo")]
	public class TB_PerfilModulo
	{
		[Column("Nukid_perfil_modulo"), NotNull, PrimaryKey, Identity] public int IdPerfilModulo { get; set; }
		[Column("Nukidperfil"), NotNull] public int IdPerfil { get; set; }
		[Column("Nukidmodulo"), NotNull] public int IdModulo { get; set; }
	}
}
