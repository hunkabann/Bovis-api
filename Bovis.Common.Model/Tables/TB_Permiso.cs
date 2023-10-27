using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_permiso")]
    public class TB_Permiso
    {
        [Column("Nukid_permiso"), NotNull, PrimaryKey, Identity] public int IdPermiso { get; set; }
        [Column("Chpermiso"), NotNull] public string Permiso { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}