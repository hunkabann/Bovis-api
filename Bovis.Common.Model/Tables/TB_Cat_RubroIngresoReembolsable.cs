using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_cat_rubro_ingreso_reembolsable_no_reembolsable")]
	public class TB_Cat_RubroIngresoReembolsable
	{
		[Column("nukidrubro_ingreso"), NotNull, PrimaryKey, Identity] public int IdRubroIngreso { get; set; }
		[Column("Chrubro"), NotNull] public string Rubro { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
	}
}
