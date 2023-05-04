using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_tipo_sangre")]
	public class TB_Cat_TipoSangre
	{
		[Column("NukidtipoSangre"), NotNull, PrimaryKey, Identity] public int IdTipoSangre { get; set; }
		[Column("ChtipoSangre"), NotNull] public string TipoSangre { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
