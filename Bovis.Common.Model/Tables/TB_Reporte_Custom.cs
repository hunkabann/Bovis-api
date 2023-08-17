using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_reporte_custom")]
    public class TB_Reporte_Custom
    {
        [Column("Nukid_reporte"), NotNull, PrimaryKey, Identity] public int IdReporte { get; set; }
        [Column("Chquery"), NotNull] public string? Query { get; set; }
        [Column("Chnombre"), NotNull] public string? Nombre { get; set; }
        [Column("Chdescripcion"), NotNull] public string? Descripcion { get; set; }
        [Column("Dtfecha_creacion"), NotNull] public DateTime? FechaCreacion { get; set; }
        [Column("Nukid_empleado_crea"), NotNull] public int? IdEmpleadoCrea { get; set; }
        [Column("Dtfecha_actualizacion"), NotNull] public DateTime? FechaActualizacion { get; set; }
        [Column("Nukid_empleado_actualiza"), NotNull] public int? IdEmpleadoActualiza { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}
