using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_direccion")]
	public class TB_Direccion
	{
		[Column("Nukiddireccion"), NotNull, PrimaryKey, Identity] public int IdDireccion { get; set; }
		[Column("Nukidpersona"), NotNull] public int IdPersona { get; set; }
		[Column("Nukidcolonia"), NotNull] public int IdColonia { get; set; }
		[Column("Chcalle"), NotNull] public string Calle { get; set; }
		[Column("ChnoExt"), Nullable] public string? NoExt { get; set; }
		[Column("ChnoInt"), Nullable] public string? NoInt { get; set; }
	}
}
