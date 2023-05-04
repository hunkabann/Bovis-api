using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cat_DOR_PuestoNivel")]
    public class TB_Cat_Dor_PuestoNivel
    {
        [Column("Puesto"), Nullable] public string Puesto { get; set; }
        [Column("Nivel"), Nullable] public string Nivel { get; set; }
    }
}


