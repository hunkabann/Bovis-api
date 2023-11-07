using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_modulo")]
	public class TB_Modulo
	{
		[Column("Nukidmodulo"), NotNull, PrimaryKey, Identity] public int IdModulo { get; set; }
		[Column("Chmodulo"), NotNull] public string Modulo { get; set; }
		[Column("Chsub_modulo"), NotNull] public string? SubModulo { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("Tab"), NotNull] public bool IsTab { get; set; }
	}
}
