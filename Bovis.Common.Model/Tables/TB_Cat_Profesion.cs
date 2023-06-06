using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_profesion")]
    public class TB_Cat_Profesion
    {
        [Column("Nukidprofesion"), NotNull, PrimaryKey, Identity] public int IdProfesion { get; set; }
        [Column("Chprofesion"), NotNull] public string Profesion { get; set; }
        [Column("Activo"), NotNull] public bool Activo { get; set; }
    }
}