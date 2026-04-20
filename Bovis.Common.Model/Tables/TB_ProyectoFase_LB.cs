using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_proyecto_fase_lb")]
    public class TB_ProyectoFase_LB
    {
        [Column("nukidfase"), NotNull]
        public int IdFase { get; set; }

        [Column("nunum_proyecto"), NotNull]
        public int NumProyecto { get; set; }

        [Column("nuorden"), NotNull]
        public byte Orden { get; set; }

        [Column("chfase"), NotNull]
        public string Fase { get; set; } = null!;

        [Column("dtfecha_ini"), NotNull]
        public DateTime FechaIni { get; set; }

        [Column("dtfecha_fin"), NotNull]
        public DateTime FechaFin { get; set; }

        [Column("dtfecha_vigencia_ini"), Nullable]
        public DateTime? Vigencia_Ini { get; set; }

        [Column("dtfecha_vigencia_fin"), Nullable]
        public DateTime? Vigencia_Fin { get; set; }

        //  NUEVO campo exclusivo línea base
        [Column("nukidlinea_base"), NotNull]
        public int IdLineaBase { get; set; }
    }
}