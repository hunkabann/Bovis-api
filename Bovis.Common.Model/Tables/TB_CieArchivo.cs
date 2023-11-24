using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cie_archivo")]
    public class TB_CieArchivo
    {
        [Column("Nukid_archivo"), NotNull, PrimaryKey, Identity] public int IdArchivo { get; set; }
        [Column("Chnombre_archivo"), Nullable] public string? NombreArchivo { get; set; }
    }
}
