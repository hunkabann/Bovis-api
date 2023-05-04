using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_nivel_puesto")]
	public class TB_Cat_NivelPuesto
	{
		[Column("nukidnivel_puesto"), NotNull, PrimaryKey, Identity] public int IdNivelPuesto { get; set; }
		[Column("chnivel_puesto"), NotNull] public string NivelPuesto { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
