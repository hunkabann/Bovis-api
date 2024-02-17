using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_cliente_proyecto")]
    public class TB_ClienteProyecto
    {
        [Column("Nukidcliente"), Nullable] public int? IdCliente { get; set; }
        [Column("Nunum_proyecto"), Nullable] public int? NumProyecto { get; set; }
    }
}
