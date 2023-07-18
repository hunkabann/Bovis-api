using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "DOR_ObjetivosDesepeno2")]
    public class DOR_ObjetivosDesepeno
    {
        [Column("idEmpOb"), NotNull, PrimaryKey, Identity] public int IdEmpOb { get; set; }
        [Column("UnidadDeNegocio"), Nullable] public string? UnidadDeNegocio { get; set; }
        [Column("Concepto"), Nullable] public string? Concepto { get; set; }
        [Column("Descripcion"), Nullable] public string? Descripcion { get; set; }
        [Column("Meta"), Nullable] public string? Meta { get; set; }
        [Column("Real"), Nullable] public string? Real { get; set; }
        [Column("Ponderado"), Nullable] public string? Ponderado { get; set; }
        [Column("Calificacion"), Nullable] public string? Calificacion { get; set; }
        [Column("Nivel"), Nullable] public string? Nivel { get; set; }
        [Column("Año"), Nullable] public string? Anio { get; set; }
        [Column("Mes"), Nullable] public int? Mes { get; set; }
        [Column("Proyecto"), Nullable] public string? Proyecto { get; set; }
        [Column("Empleado"), Nullable] public string? Empleado { get; set; }
        //colocar valores 1 o 0
        [Column("Acepto"), Nullable] public string? Acepto { get; set; }
        [Column("MotivoR"), Nullable] public string? MotivoR { get; set; }
        [Column("FechaCarga"), Nullable] public DateTime? FechaCarga { get; set; }
        [Column("FechaAceptado"), Nullable] public DateTime? FechaAceptado { get; set; }
        [Column("FechaRechazo"), Nullable] public DateTime? FechaRechazo { get; set; }

    }
}
