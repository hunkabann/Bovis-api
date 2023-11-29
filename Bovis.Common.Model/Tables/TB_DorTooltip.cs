using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_dor_tooltip")]
    public class TB_DorTooltip
    {
        [Column("idTool"), NotNull, PrimaryKey, Identity] public int IdTool { get; set; }
        [Column("UnidadDeNegocio"), Nullable] public string? UnidadDeNegocio { get; set; }
        [Column("Concepto"), Nullable] public string? Concepto { get; set; }
        [Column("Descripcion"), Nullable] public string? Descripcion { get; set; }
        [Column("Tooltip"), Nullable] public string? Tooltip { get; set; }
    }
}
