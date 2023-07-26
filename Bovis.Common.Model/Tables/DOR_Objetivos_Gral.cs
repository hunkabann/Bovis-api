using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "DOR_OBJ_GRAL")]
    public class DOR_Objetivos_Gral
    {
        [Column("id"), NotNull, PrimaryKey, Identity] public int Id { get; set; }
        [Column("UnidadNegocio"), Nullable] public string? UnidadDeNegocio { get; set; }
        [Column("Concepto"), Nullable] public string? Concepto { get; set; }
        [Column("Descripcion"), Nullable] public string? Descripcion { get; set; }
        [Column("Meta"), Nullable] public decimal? Meta { get; set; }
        [Column("Metavalor"), Nullable] public decimal? MetaValor { get; set; }
        [Column("Año"), Nullable] public int? Año { get; set; }
        [Column("Mes"), Nullable] public int? Mes { get; set; }
        [Column("Real"), Nullable] public decimal? Real { get; set; }
        [Column("Ene"), Nullable] public decimal? ENE { get; set; }
        [Column("Feb"), Nullable] public decimal? FEB { get; set; }
        [Column("Mar"), Nullable] public decimal? MAR { get; set; }
        [Column("Abr"), Nullable] public decimal? ABR { get; set; }
        [Column("May"), Nullable] public decimal? MAY { get; set; }
        [Column("Jun"), Nullable] public decimal? JUN { get; set; }
        [Column("Jul"), Nullable] public decimal? JUL { get; set; }
        [Column("Ago"), Nullable] public decimal? AGO { get; set; }
        [Column("Sep"), Nullable] public decimal? SEP { get; set; }
        [Column("Oct"), Nullable] public decimal? OCT { get; set; }
        [Column("Nov"), Nullable] public decimal? NOV { get; set; }
        [Column("Dic"), Nullable] public decimal? DIC { get; set; }
    }
}


