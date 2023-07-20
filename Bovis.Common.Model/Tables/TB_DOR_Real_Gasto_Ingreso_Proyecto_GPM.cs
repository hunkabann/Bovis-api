using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_DOR_REAL_GASTO_INGRESO_Proyecto_GPM")]
    public class TB_DOR_Real_Gasto_Ingreso_Proyecto_GPM
    {
        [Column("IdReal_Proyecto"), NotNull, PrimaryKey, Identity] public int IdRealProyecto { get; set; }
        [Column("UnidadNegocio"), Nullable] public string? UnidadDeNegocio { get; set; }
        [Column("Concepto"), NotNull] public string? Concepto { get; set; }
        [Column("Descripcion"), Nullable] public string? Descripcion { get; set; }
        [Column("Ingreso"), Nullable] public decimal Ingreso { get; set; }
        [Column("Gasto"), Nullable] public decimal Gasto { get; set; }
        [Column("Mes"), Nullable] public int Mes { get; set; }
        [Column("Año"), Nullable] public int Año { get; set; }
    }
}
