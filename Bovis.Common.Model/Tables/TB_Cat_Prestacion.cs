using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_prestacion")]
	public class TB_Cat_Prestacion
	{
		[Column("Nukidprestacion"), NotNull, PrimaryKey, Identity] public int IdPrestacion { get; set; }
		[Column("Chviatico"), NotNull] public string Viatico { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
