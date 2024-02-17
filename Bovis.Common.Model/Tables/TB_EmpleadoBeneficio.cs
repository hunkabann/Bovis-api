using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado_beneficio")]
	public class TB_EmpleadoBeneficio
	{
		[Column ("Nukid"), NotNull, PrimaryKey, Identity] public int Id { get; set; }
		[Column("Nukidbeneficio"), NotNull] public int IdBeneficio { get; set; }
		[Column("Nunum_empleado_rr_hh"), NotNull] public string NumEmpleadoRrHh { get; set; }
		[Column("Nucosto"), NotNull] public decimal Costo { get; set; }
		[Column("Numes"), NotNull] public int Mes {  get; set; }
		[Column("Nuanno"), NotNull] public int Anno {  get; set; }
		[Column("Dtfecha_actualizacion"), NotNull] public DateTime FechaActualizacion { get; set; }
		[Column("Boreg_historico"), NotNull] public bool RegHistorico { get; set; }
	}
}
