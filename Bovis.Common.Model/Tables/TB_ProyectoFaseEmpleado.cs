using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_proyecto_fase_empleado")]
    public class TB_ProyectoFaseEmpleado
    {
        [Column("Nukid"), NotNull, PrimaryKey, Identity] public int Id { get; set; }
        [Column("Nukidfase"), NotNull] public int IdFase { get; set; }
        [Column("Nunum_empleado_rr_hh"), NotNull] public string NumEmpleado { get; set; }
        [Column("Numes"), NotNull] public int Mes { get; set; }
        [Column("Nuanio"), NotNull] public int Anio { get; set; }
        [Column("Nuporcentaje"), NotNull] public int Porcentaje { get; set; }
        [Column("Nucantidad"), Nullable] public decimal? Cantidad { get; set; }
        [Column("Boaplica_todos_meses"), Nullable] public bool? AplicaTodosMeses { get; set; }
        [Column("Nufee"), Nullable] public decimal? Fee { get; set; }
    }
}
