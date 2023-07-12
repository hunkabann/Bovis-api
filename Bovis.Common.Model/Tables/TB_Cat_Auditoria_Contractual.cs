using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_auditoria_contractual")]
    public class TB_Cat_Auditoria_Contractual
    {
        [Column("Nukidauditoria_contractual"), NotNull, PrimaryKey, Identity] public int IdCampoAuditoriaContractual { get; set; }
        [Column("Chcampo"), NotNull] public string Campo { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
        [Column("Boinicial"), NotNull] public bool Inicial { get; set; }

    }
}
