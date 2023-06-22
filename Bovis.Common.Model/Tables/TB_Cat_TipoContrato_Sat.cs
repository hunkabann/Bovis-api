using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_tipo_contrato_sat")]
    public class TB_Cat_TipoContrato_Sat
    {
        [Column("Nukidtipo_contrato_sat"), NotNull, PrimaryKey, Identity] public int IdTipoContratoSat { get; set; }
        [Column("Chcontrato_sat"), NotNull] public string ContratoSat { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
        [Column("Chcve_contrato_sat"), Nullable] public byte? Cve { get; set; }
    }
}
