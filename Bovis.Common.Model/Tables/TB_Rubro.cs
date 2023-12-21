﻿using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_rubro")]
    public class TB_Rubro
    {
        [Column("Nukid"), NotNull, PrimaryKey, Identity] public int Id { get; set; }
        [Column("Nukid_seccion"), NotNull, ] public int IdSeccion { get; set; }
        [Column("Nukid_rubro"), NotNull] public int IdRubro { get; set; }
        [Column("Chunidad"), Nullable] public string? Unidad { get; set; }
        [Column("Nucantidad"), Nullable] public decimal? Cantidad { get; set; }
        [Column("Boreembolsable"), Nullable] public bool? Reembolsable { get; set; }
        [Column("Boaplica_todos_meses"), Nullable] public bool? AplicaTodosMeses { get; set; }
        [Column("Nunum_proyecto"), NotNull] public int NumProyecto { get; set; }
        [Column("Boactivo"), Nullable] public bool? Activo { get; set; }
    }
}

