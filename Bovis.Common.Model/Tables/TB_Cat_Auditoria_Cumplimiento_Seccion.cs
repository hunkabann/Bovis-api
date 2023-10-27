using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_auditoria_cumplimiento_seccion")]
    public class TB_Cat_Auditoria_Cumplimiento_Seccion
    {
        [Column("Nukidseccion"), NotNull, PrimaryKey, Identity] public int IdSeccion { get; set; }
        [Column("Chseccion"), Nullable] public string Seccion { get; set; }
        [Column("Chtipo_auditoria"), Nullable] public string TipoAuditoria { get; set; }

    }
}
