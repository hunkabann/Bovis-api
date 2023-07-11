using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_auditoria_cumplimiento_proyecto")]
    public class TB_Auditoria_Cumplimiento_Proyecto
    {
        [Column("Nukidproyecto"), Nullable] public int IdProyecto { get; set; }
        [Column("Nukidauditoria_cumplimiento"), Nullable] public int IdCAuditoriaCumplimiento { get; set; }
    }
}
