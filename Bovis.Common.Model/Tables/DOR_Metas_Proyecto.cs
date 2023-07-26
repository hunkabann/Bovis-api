using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "DOR_META_Proyecto")]
    public class DOR_Metas_Proyecto
    {
        [Column("idMETA_Proyecto"), NotNull, PrimaryKey, Identity] public int Id { get; set; }
        [Column("UnidadNegocio"), Nullable] public string? UnidadDeNegocio { get; set; }
        [Column("Concepto"), NotNull] public string Concepto { get; set; }
        [Column("Descripcion"), Nullable] public string? Descripcion { get; set; }
        [Column("NoProyecto"), Nullable] public int? NoProyecto { get; set; }
        [Column("META"), Nullable] public double? Meta { get; set; }
        [Column("Real"), Nullable] public string Real { get; set; }
        [Column("Mes"), Nullable] public int? Mes { get; set; }
        [Column("Año"), Nullable] public int? Año { get; set; }

    }
}

