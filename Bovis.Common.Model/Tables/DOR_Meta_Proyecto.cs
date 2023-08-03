using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "DOR_META_Proyecto")]
    public class DOR_Meta_Proyecto
    {
        [Column("idMETA_Proyecto"), NotNull, PrimaryKey, Identity] public int Id { get; set; }
        [Column("UnidadNegocio"), Nullable] public string? UnidadDeNegocio { get; set; }
        [Column("Concepto"), NotNull] public string Concepto { get; set; }
        [Column("Descripcion"), Nullable] public string? Descripcion { get; set; }
        [Column("NoProyecto"), Nullable] public int? NoProyecto { get; set; }
        [Column("Empleado"), Nullable] public int? Empleado { get; set; }
        [Column("META"), Nullable] public decimal? Meta { get; set; }
        [Column("Real"), Nullable] public decimal? Real { get; set; }
        [Column("Mes"), Nullable] public int? Mes { get; set; }
        [Column("Año"), Nullable] public int? Año { get; set; }
        [Column("Ene"), Nullable] public decimal? Enero { get; set; }
        [Column("Feb"), Nullable] public decimal? Febrero { get; set; }
        [Column("Mar"), Nullable] public decimal? Marzo { get; set; }
        [Column("Abr"), Nullable] public decimal? Abril { get; set; }
        [Column("May"), Nullable] public decimal? Mayo { get; set; }
        [Column("Jun"), Nullable] public decimal? Junio { get; set; }
        [Column("Jul"), Nullable] public decimal? Julio { get; set; }
        [Column("Ago"), Nullable] public decimal? Agosto { get; set; }
        [Column("Sep"), Nullable] public decimal? Septiembre { get; set; }
        [Column("Oct"), Nullable] public decimal? Octubre { get; set; }
        [Column("Nov"), Nullable] public decimal? Noviembre { get; set; }
        [Column("Dic"), Nullable] public decimal? Diciembre { get; set; }
        [Column("EneP"), Nullable] public decimal? ProyectadoEnero { get; set; }
        [Column("FebP"), Nullable] public decimal? ProyectadoFebrero { get; set; }
        [Column("MarP"), Nullable] public decimal? ProyectadoMarzo { get; set; }
        [Column("AbrP"), Nullable] public decimal? ProyectadoAbril { get; set; }
        [Column("MayP"), Nullable] public decimal? ProyectadoMayo { get; set; }
        [Column("JunP"), Nullable] public decimal? ProyectadoJunio { get; set; }
        [Column("JulP"), Nullable] public decimal? ProyectadoJulio { get; set; }
        [Column("AgoP"), Nullable] public decimal? ProyectadoAgosto { get; set; }
        [Column("SepP"), Nullable] public decimal? ProyectadoSeptiembre { get; set; }
        [Column("OctP"), Nullable] public decimal? ProyectadoOctubre { get; set; }
        [Column("NovP"), Nullable] public decimal? ProyectadoNoviembre { get; set; }
        [Column("DicP"), Nullable] public decimal? ProyectadoDiciembre { get; set; }

    }
}

