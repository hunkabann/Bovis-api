using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_auditoria_comentario")]
    public class TB_AuditoriaComentario
    {
        [Column("Nukid_comentario"), NotNull, PrimaryKey, Identity] public int IdComentario { get; set; }
        [Column("Nunum_proyecto"), NotNull] public int NumProyecto { get; set; }
        [Column("Chcomentario"), NotNull] public string Comentario { get; set; }
        [Column("Dtfecha"), NotNull] public DateTime Fecha { get; set; }
        [Column("Nukid_tipo_comentario"), NotNull] public int IdTipoComentario { get; set; }
        [Column("Chnombre_auditor"), NotNull] public string NombreAuditor { get; set; }        
    }
}
