using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_requerimiento")]
    public class TB_Requerimiento
    {
        [Column("Nukidrequerimiento"), NotNull, PrimaryKey, Identity] public int IdRequerimiento { get; set; }
        [Column("Nukidcategoria"), Nullable] public int IdCategoria { get; set; }
        [Column("Nukidpuesto"), Nullable] public int IdPuesto { get; set; }
        [Column("Nukidnivel_estudios"), Nullable] public int IdNivelEstudios { get; set; }
        [Column("Nukidprofesion"), Nullable] public int IdProfesion { get; set; }
        [Column("Nukidjornada"), Nullable] public int IdJornada { get; set; }
        [Column("Nusueldo_min"), Nullable] public decimal SueldoMin { get; set; }
        [Column("Nusueldo_max"), Nullable] public decimal SueldoMax { get; set; }
        [Column("Nusueldo_real"), Nullable] public decimal SueldoReal { get; set; }
        [Column("Nunumempleado_rr_hh"), Nullable] public int NumEmpleadoRrHh { get; set; }
        [Column("Nukiddirector_ejecutivo"), Nullable] public int IdDirectorEjecutivo { get; set; }
        [Column("Nukidproyecto"), Nullable] public int IdProyecto { get; set; }
        [Column("Nukidjefe_inmediato"), Nullable] public int IdJefeInmediato { get; set; }
        [Column("Nukidtipo_contrato"), Nullable] public int IdTipoContrato { get; set; }
        [Column("Nukidestado"), Nullable] public int IdEstado { get; set; }
        [Column("Nukidciudad"), Nullable] public int IdCiudad { get; set; }
        [Column("Bodisponibilidad_viajar"), Nullable] public bool DisponibilidadViajar { get; set; }
        [Column("Nuanios_experiencia"), Nullable] public int AniosExperiencia { get; set; }
        [Column("Chnivel_ingles"), Nullable] public string NivelIngles { get; set; }
        [Column("Chcomentarios"), Nullable] public string Comentarios { get; set; }
        [Column("Boactivo"), Nullable] public bool Activo { get; set; }
    }
}
