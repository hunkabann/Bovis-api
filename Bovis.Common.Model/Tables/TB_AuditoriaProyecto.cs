using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_auditoria_proyecto")]
    public class TB_AuditoriaProyecto
    {
        [Column("Nukidauditoria_proyecto"), NotNull, PrimaryKey, Identity] public int IdAuditoriaProyecto { get; set; }
        [Column("Nukidauditoria"), Nullable] public int? IdAuditoria { get; set; }
        [Column("Nukidproyecto"), Nullable] public int? IdProyecto { get; set; }
        [Column("Boaplica"), Nullable] public bool? Aplica { get; set; }
    }
}
