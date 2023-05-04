using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_edo_civil")]
	public class TB_Cat_EdoCivil
	{
		[Column("NukidedoCivil"), NotNull, PrimaryKey, Identity] public int IdEdoCivil { get; set; }
		[Column("ChedoCivil"), NotNull] public string EdoCivil { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
