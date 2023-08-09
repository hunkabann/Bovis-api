using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_dor_objetivos_desepeno")]
    public class TB_Dor_Objetivos_Desepeno
    {
        [Column("idEmpOb"), NotNull, PrimaryKey, Identity] public int IdEmpOb { get; set; }
        [Column("UnidadDeNegocio"), Nullable] public string? UnidadDeNegocio { get; set; }
        [Column("Concepto"), Nullable] public string? Concepto { get; set; }
        [Column("Descripcion"), Nullable] public string? Descripcion { get; set; }
        [Column("Meta"), Nullable] public decimal? Meta { get; set; }
        [Column("Real"), Nullable] public decimal? Real { get; set; }
        [Column("Ponderado"), Nullable] public string? Ponderado { get; set; }
        [Column("Calificacion"), Nullable] public string? Calificacion { get; set; }
        [Column("Valor"), Nullable] public int? Valor { get; set; }
        [Column("Año"), Nullable] public int? Anio { get; set; }
        [Column("Mes"), Nullable] public int? Mes { get; set; }
        [Column("Proyecto"), Nullable] public int? Proyecto { get; set; }
        [Column("Empleado"), Nullable] public int? Empleado { get; set; }
        //colocar valores 1 o 0
        [Column("Acepto"), Nullable] public int? Acepto { get; set; }
        [Column("MotivoR"), Nullable] public string? MotivoR { get; set; }
        [Column("FechaCarga"), Nullable] public DateTime? FechaCarga { get; set; }
        [Column("FechaAceptado"), Nullable] public DateTime? FechaAceptado { get; set; }
        [Column("FechaRechazo"), Nullable] public DateTime? FechaRechazo { get; set; }

    }
}
