using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_rubro_valor_lb")]
    public class TB_RubroValor_LB
    {
        [Column("nukid"), NotNull] public int Id { get; set; }

        [Column("nukid_rubro"), NotNull] public int IdRubro { get; set; }

        [Column("numes"), Nullable] public int? Mes { get; set; }

        [Column("nuanio"), Nullable] public int? Anio { get; set; }

        [Column("nuporcentaje"), Nullable] public decimal? Porcentaje { get; set; }

        [Column("boactivo"), Nullable] public bool? Activo { get; set; }

        [Column("dtfecha_vigencia_ini"), Nullable] public DateTime? Vigencia_Ini { get; set; }

        [Column("dtfecha_vigencia_fin"), Nullable] public DateTime? Vigencia_Fin { get; set; }

        [Column("nukidlinea_base"), NotNull] public int IdLineaBase { get; set; }
    }
}