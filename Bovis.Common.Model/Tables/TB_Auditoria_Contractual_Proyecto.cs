using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_auditoria_contractual_proyecto")]
    public class TB_Auditoria_Contractual_Proyecto
    {
        [Column("Nukidauditoria_contractual"), NotNull] public int IdAuditoriaContractual { get; set; }
        [Column("Nukidproyecto"), NotNull] public int IdProyecto { get; set; }
        [Column("Dtfecha_carga"), Nullable] public DateTime FechaCarga { get; set; }

    }
}
