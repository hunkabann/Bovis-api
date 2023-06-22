using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{

    [Table(Schema = "dbo", Name = "tb_cat_sexo")]
    public class TB_Cat_Sexo
    {
        [Column("idsexo"), NotNull, PrimaryKey, Identity] public int IdSexo { get; set; }
        [Column("sexo"), NotNull] public string Sexo { get; set; }
        [Column("activo"), NotNull] public bool Activo { get; set; }
    }
}
