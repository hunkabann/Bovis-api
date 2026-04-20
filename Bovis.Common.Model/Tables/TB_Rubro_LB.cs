using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_rubro_lb")]
    public class TB_Rubro_LB
    {
        [Column("nukid"), NotNull]
        public int Id { get; set; }

        [Column("nukid_seccion"), NotNull]
        public int IdSeccion { get; set; }

        [Column("nukid_rubro"), NotNull]
        public int IdRubro { get; set; }

        [Column("chunidad"), Nullable]
        public string? Unidad { get; set; }

        [Column("nucantidad"), Nullable]
        public decimal? Cantidad { get; set; }

        [Column("boreembolsable"), Nullable]
        public bool? Reembolsable { get; set; }

        [Column("boaplica_todos_meses"), Nullable]
        public bool? AplicaTodosMeses { get; set; }

        [Column("nunum_proyecto"), NotNull]
        public int NumProyecto { get; set; }

        [Column("boactivo"), Nullable]
        public bool? Activo { get; set; }

        [Column("dtfecha_vigencia_ini"), Nullable]
        public DateTime? VigenciaIni { get; set; }

        [Column("dtfecha_vigencia_fin"), Nullable]
        public DateTime? VigenciaFin { get; set; }

        [Column("chcomentario"), Nullable]
        public string? Comentario { get; set; }

        [Column("nukidlinea_base"), NotNull]
        public int IdLineaBase { get; set; }
    }
}