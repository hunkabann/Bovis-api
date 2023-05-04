using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empresa")]
	public class TB_Empresa
	{
		[Column("Nukidempresa"), NotNull, PrimaryKey, Identity] public int IdEmpresa { get; set; }
		[Column("Nucoi"), NotNull] public short Coi { get; set; }
		[Column("Nunoi"), NotNull] public short Noi { get; set; }
		[Column("Nusae"), NotNull] public short Sae { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("Chempresa"), NotNull] public string Empresa { get; set; }
        [Column("RFC"), NotNull] public string Rfc { get; set; }
    }
}
