using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_habilidad")]
    public class TB_Cat_Habilidad
    {
        [Column("Nukidhabilidad"), NotNull, PrimaryKey, Identity] public int IdHabilidad { get; set; }
        [Column("Chhabilidad"), NotNull] public string Habilidad { get; set; }
        [Column("Activo"), NotNull] public bool Activo { get; set; }
    }
}