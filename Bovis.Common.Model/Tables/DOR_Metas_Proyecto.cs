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
        [Column("META"), Nullable] public decimal? Meta { get; set; }
        [Column("Real"), Nullable] public decimal? Real { get; set; }
        [Column("Mes"), Nullable] public int? Mes { get; set; }
        [Column("Año"), Nullable] public int? Año { get; set; }
        [Column("Ene"), Nullable] public decimal? Ene { get; set; }
        [Column("Feb"), Nullable] public decimal? Feb { get; set; }
        [Column("Mar"), Nullable] public decimal? Mar { get; set; }
        [Column("Abr"), Nullable] public decimal? Abr { get; set; }
        [Column("May"), Nullable] public decimal? May { get; set; }
        [Column("Jun"), Nullable] public decimal? Jun { get; set; }
        [Column("Jul"), Nullable] public decimal? Jul { get; set; }
        [Column("Ago"), Nullable] public decimal? Ago { get; set; }
        [Column("Sep"), Nullable] public decimal? Sep { get; set; }
        [Column("Oct"), Nullable] public decimal? Oct { get; set; }
        [Column("Nov"), Nullable] public decimal? Nov { get; set; }
        [Column("Dic"), Nullable] public decimal? Dic { get; set; }

    }
}

