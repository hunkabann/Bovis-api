using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_auditoria_cumplimiento")]
    public class TB_Auditoria_Cumplimiento
    {
        [Column("Nukidauditoria_cumplimiento"), NotNull, PrimaryKey, Identity] public int IdAuditoriaCumplimiento { get; set; }
        [Column("Nukidproyecto"), Nullable] public int IdProyecto { get; set; }
        [Column("Nukiddirector"), Nullable] public int IdDirector { get; set; }
        [Column("Nummes"), Nullable] public int Mes { get; set; }
        [Column("Dtfecha"), Nullable] public DateTime Fecha { get; set; }
        [Column("Chpunto"), Nullable] public string Punto { get; set; }
        [Column("Nukidseccion"), Nullable] public int IdSeccion { get; set; }
        [Column("Chcumplimiento"), Nullable] public string Cumplimiento { get; set; }
        [Column("Chdocumento_ref"), Nullable] public string DocumentoRef { get; set; }
        [Column("Boaplica"), NotNull] public bool Aplica { get; set; }
        [Column("Chmotivo"), Nullable] public string Motivo { get; set; }

    }
}
