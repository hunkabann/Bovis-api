using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_auditoria_cumplimiento_proyecto")]
    public class TB_Auditoria_Cumplimiento_Proyecto
    {
        [Column("Nukidauditoria_cumplimiento_proyecto"), NotNull, PrimaryKey, Identity] public int IdAuditoriaCumplimientoProyecto { get; set; }
        [Column("Nukidauditoria_cumplimiento"), Nullable] public int IdAuditoriaCumplimiento { get; set; }
        [Column("Nukidproyecto"), Nullable] public int IdProyecto { get; set; }
        [Column("Boaplica"), Nullable] public bool Aplica { get; set; }
    }
}
