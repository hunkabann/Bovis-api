using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_colonia")]
	public class TB_Colonia
	{
		[Column("Nukidcolonia"), NotNull, PrimaryKey, Identity] public int IdColonia { get; set; }
		[Column("Nukidciudad"), NotNull] public int IdCiudad { get; set; }
		[Column("Chcolonia"), NotNull] public string Colonia { get; set; }
		[Column("Chcp"), NotNull] public string Cp { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
