using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_contrato_template")]
    public class TB_Contrato_Template
    {
        [Column("Nukidcontrato_template"), NotNull, PrimaryKey, Identity] public int IdContratoTemplate { get; set; }
        [Column("Chtitulo"), NotNull] public string Titulo { get; set; }
        [Column("Chtemplate"), NotNull] public string Template { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}
