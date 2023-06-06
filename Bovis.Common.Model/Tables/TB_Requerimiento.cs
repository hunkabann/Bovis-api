using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_requerimiento")]
    public class TB_Requerimiento
    {
        [Column("Nukidrequerimiento"), NotNull, PrimaryKey, Identity] public int IdRequerimiento { get; set; }
        [Column("Nukidcategoria"), Nullable] public int? IdCategoria { get; set; }
        [Column("Nukidpuesto"), Nullable] public int? IdPuesto { get; set; }
        [Column("Nukidnivel_estudios"), Nullable] public int? IdNivelEstudios { get; set; }
        [Column("Chprofesion"), Nullable] public string? Profesion { get; set; }
        [Column("Nukidjornada"), Nullable] public int? IdJornada { get; set; }
        [Column("Nusueldo_min"), Nullable] public int? SueldoMin { get; set; }
        [Column("Nusueldo_max"), Nullable] public int? SueldoMax { get; set; }
        [Column("ChHabilidades"), Nullable] public string? Habilidades { get; set; }
        [Column("Chexperiencias"), Nullable] public string? Experiencias { get; set; }
        [Column("Boactivo"), Nullable] public bool? Activo { get; set; }
    }
}
