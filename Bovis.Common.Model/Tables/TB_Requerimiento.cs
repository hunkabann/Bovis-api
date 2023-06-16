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
        [Column("Nusueldo_min"), Nullable] public int SueldoMin { get; set; }
        [Column("Nusueldo_max"), Nullable] public int SueldoMax { get; set; }
        [Column("Boactivo"), Nullable] public bool Activo { get; set; }
    }
}
