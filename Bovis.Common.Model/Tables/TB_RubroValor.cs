﻿using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_rubro_valor")]
    public class TB_RubroValor
    {
        [Column("Nukid"), NotNull, PrimaryKey, Identity] public int Id { get; set; }
        [Column("Nukid_rubro"), NotNull] public int IdRubro { get; set; }
        [Column("Numes"), Nullable] public int? Mes { get; set; }
        [Column("Nuanio"), Nullable] public int? Anio { get; set; }
        [Column("Nuporcentaje"), Nullable] public decimal? Porcentaje { get; set; }        
        [Column("Boactivo"), Nullable] public bool? Activo { get; set; }
    }
}

