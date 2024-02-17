using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_auditoria_tipo_comentario")]
    public class TB_Cat_AuditoriaTipoComentario
    {
        [Column("Nukid_tipo_comentario"), NotNull, PrimaryKey, Identity] public int IdTipoComentario { get; set; }
        [Column("Chtipo"), Nullable] public string TipoComentario { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}
