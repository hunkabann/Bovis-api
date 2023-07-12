using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_auditoria_cumplimiento_documento")]
    public class TB_Auditoria_Cumplimiento_Documento
    {
        [Column("Nukiddocumento"), NotNull, PrimaryKey, Identity] public int IdDocumento { get; set; }
        [Column("Nukidauditoria_cumplimiento_proyecto"), Nullable] public int IdAuditoriaCumplimientoProyecto { get; set; }
        [Column("Chmotivo"), Nullable] public string Motivo { get; set; }
        [Column("Dtfecha"), Nullable] public DateTime Fecha { get; set; }
        [Column("Chdocumento_base64"), Nullable] public string DocumentoBase64 { get; set; }
    }
}
