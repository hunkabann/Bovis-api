using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_puesto_proyecto")]
	public class TB_PuestoProyecto
	{
		[Column("NunumProyecto"), NotNull, PrimaryKey, Identity] public int NumProyecto { get; set; }
		[Column("ChcvePuesto"), NotNull] public string CvePuesto { get; set; }
		[Column("Nucantidad"), NotNull] public byte Cantidad { get; set; }
	}
}
