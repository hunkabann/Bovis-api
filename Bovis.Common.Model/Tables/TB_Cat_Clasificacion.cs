using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_clasificacion")]
	public class TB_Cat_Clasificacion
	{
		[Column("Nukidclasificacion"), NotNull, PrimaryKey, Identity] public int IdClasificacion { get; set; }
		[Column("Chclasificacion"), NotNull] public string Clasificacion { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
