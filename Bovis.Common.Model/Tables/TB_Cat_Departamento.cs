using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_departamento")]
	public class TB_Cat_Departamento
	{
		[Column("Nukiddepartamento"), NotNull, PrimaryKey, Identity] public int IdDepartamento { get; set; }
		[Column("Chdepartamento"), NotNull] public string Departamento { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("chcve_depto"), Nullable] public string? Cve { get; set; }
	}
}
