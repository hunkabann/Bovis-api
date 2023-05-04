using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_banco")]
	public class TB_Banco
	{
		[Column("Nukidbanco"), NotNull, PrimaryKey, Identity] public int IdBanco { get; set; }
		[Column("Chbanco"), NotNull] public string Banco { get; set; }
		[Column("ChclaveSat"), NotNull] public string ClaveSat { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("Chcontrol"), Nullable] public string? Control { get; set; }
		[Column("Nuoperador"), Nullable] public short Operador { get; set; }
	}
}
