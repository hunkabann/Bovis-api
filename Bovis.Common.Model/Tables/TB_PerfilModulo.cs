using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_perfil_modulo")]
	public class TB_PerfilModulo
	{
		[Column("Nukidmodulo"), NotNull, PrimaryKey, Identity] public int IdModulo { get; set; }
		[Column("Nukidperfil"), NotNull] public int IdPerfil { get; set; }
	}
}
