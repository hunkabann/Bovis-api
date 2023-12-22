using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_tabla_ISR")]
    public class TB_Cat_Tabla_ISR
    {
        [Column("nukid_cat_ISR"), NotNull, Identity] public int IdCatISR { get; set; }
        [Column("numes"), Nullable] public int? Mes { get; set; }
        [Column("nuanio"), Nullable] public int? Anio { get; set; }
        [Column("nu_limite_inf"), Nullable] public decimal? LimiteInferior { get; set; }
        [Column("nu_limite_sup"), Nullable] public decimal? LimiteSuperior { get; set; }
        [Column("nu_cuota_fija"), Nullable] public decimal? CuotaFija { get; set; }
        [Column("nu_porcentaje_aplicable"), Nullable] public decimal? PorcentajeAplicable { get; set; }
    }
}
