using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_reporte_custom")]
    public class TB_ReporteCustom
    {
        [Column("Nukid_reporte"), NotNull, PrimaryKey, Identity] public int IdReporte { get; set; }
        [Column("Chnombre"), NotNull] public string Nombre { get; set; }
        [Column("Chdescripcion"), Nullable] public string? Descripcion { get; set; }
        [Column("Chquery"), NotNull] public string Query { get; set; }
        [Column("Dtfecha_creacion"), NotNull] public DateTime FechaCreacion { get; set; }
        [Column("Nukid_empleado_crea"), NotNull] public string IdEmpleadoCrea { get; set; }
        [Column("Dtfecha_actualizacion"), Nullable] public DateTime? FechaActualizacion { get; set; }
        [Column("Nukid_empleado_actualiza"), Nullable] public string? IdEmpleadoActualiza { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}
