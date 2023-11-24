using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_auditoria_documento")]
    public class TB_Auditoria_Documento
    {
        [Column("Nukiddocumento"), NotNull, PrimaryKey, Identity] public int IdDocumento { get; set; }
        [Column("Nukidauditoria_proyecto"), NotNull] public int IdAuditoriaProyecto { get; set; }
        [Column("Chmotivo"), Nullable] public string? Motivo { get; set; }
        [Column("Dtfecha"), NotNull] public DateTime Fecha { get; set; }
        [Column("Chdocumento_base64"), NotNull] public string DocumentoBase64 { get; set; }
        [Column("Bovalido"), Nullable] public bool? Valido { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}
