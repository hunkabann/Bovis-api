using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_proyecto_fase_empleado_lb")]
    public class TB_ProyectoFaseEmpleado_LB
    {
        [Column("nukid"), NotNull, PrimaryKey] public int Id { get; set; }

        [Column("nukidfase"), NotNull] public int IdFase { get; set; }

        [Column("nunum_empleado_rr_hh"), NotNull] public string NumEmpleado { get; set; }

        [Column("numes"), NotNull] public int Mes { get; set; }

        [Column("nuanio"), NotNull] public int Anio { get; set; }

        [Column("nuporcentaje"), NotNull] public int Porcentaje { get; set; }

        [Column("nucantidad"), Nullable] public decimal? Cantidad { get; set; }

        [Column("boaplica_todos_meses"), Nullable] public bool? AplicaTodosMeses { get; set; }

        [Column("nucosto_ini"), Nullable] public double? CostoIni { get; set; }

        [Column("nufee"), Nullable] public double? Fee { get; set; }

        [Column("chalias"), Nullable] public string? Alias { get; set; }

        [Column("borembolsable"), Nullable] public bool? Reembolsable { get; set; }

        [Column("dt_fecha_carga"), Nullable] public DateTime? FechaCarga { get; set; }

        [Column("etiqueta"), Nullable] public string? Etiqueta { get; set; }

        [Column("boactivo"), Nullable] public bool? Activo { get; set; }

        [Column("dtfecha_vigencia_ini"), Nullable] public DateTime? VigenciaIni { get; set; }

        [Column("dtfecha_vigencia_fin"), Nullable] public DateTime? VigenciaFin { get; set; }

        [Column("nukidlinea_base"), NotNull] public int IdLineaBase { get; set; }
    }
}