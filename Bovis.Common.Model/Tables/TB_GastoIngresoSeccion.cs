using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_gasto_ingreso_seccion")]
    public class TB_GastoIngresoSeccion
    {
        [Column("Nukid_seccion"), NotNull, PrimaryKey, Identity] public int IdSeccion { get; set; }
        [Column("Chcodigo"), NotNull] public string Codigo { get; set; }
        [Column("Chseccion"), NotNull] public string Seccion { get; set; }
        [Column("Chtipo"), NotNull] public string Tipo { get; set; }
        [Column("Boactivo"), Nullable] public bool? Activo { get; set; }
    }
}

