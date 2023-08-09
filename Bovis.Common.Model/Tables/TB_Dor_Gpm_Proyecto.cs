using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_dor_gpm_proyecto")]
    public class TB_Dor_Gpm_Proyecto
    {
        [Column("idGPM_Proyecto"), NotNull, PrimaryKey, Identity] public int Id { get; set; }
        [Column("UnidadNegocio"), Nullable] public string? UnidadDeNegocio { get; set; }
        [Column("Concepto"), NotNull] public string Concepto { get; set; }
        [Column("Descripcicion"), Nullable] public string? Descripcion { get; set; }
        [Column("NoProyecto"), Nullable] public int Proyecto { get; set; }
        [Column("GPM"), Nullable] public decimal Meta { get; set; }
        [Column("Gasto"), Nullable] public decimal Gasto { get; set; }
    }
}


