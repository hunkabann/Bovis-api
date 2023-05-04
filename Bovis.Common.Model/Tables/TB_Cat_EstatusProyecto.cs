using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_estatus_proyecto")]
	public class TB_Cat_EstatusProyecto
	{
		[Column("Nukidestatus"), NotNull, PrimaryKey, Identity] public int IdEstatus { get; set; }
		[Column("Chestatus"), NotNull] public string Estatus { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
