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
        [Column("Meta"), Nullable] public string? Meta { get; set; }
    }
}


