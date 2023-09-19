using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_perfil_permiso")]
    public class TB_PerfilPermiso
    {
        [Column("Nukid_perfil_permiso"), NotNull, PrimaryKey, Identity] public int IdPerfilPermiso { get; set; }
        [Column("Nukid_perfil"), NotNull] public int IdPerfil { get; set; }
        [Column("Nukid_permiso"), NotNull] public int IdPermiso { get; set; }
    }
}