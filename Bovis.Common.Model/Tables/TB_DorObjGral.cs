using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_dor_obj_gral")]
    public class TB_DorObjGral
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
    }
}


