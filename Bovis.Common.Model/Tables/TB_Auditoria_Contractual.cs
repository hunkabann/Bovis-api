using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_auditoria_contractual")]
    public class TB_Auditoria_Contractual
    {
        [Column("Nukidauditoria_contractual"), NotNull, PrimaryKey, Identity] public int IdAuditoriaContractual { get; set; }
        [Column("Nukidproyecto"), Nullable] public int IdProyecto { get; set; }
        [Column("Dtfecha_carga"), Nullable] public DateTime FechaCarga { get; set; }

    }
}
