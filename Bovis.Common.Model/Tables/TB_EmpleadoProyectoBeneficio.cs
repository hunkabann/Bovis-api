using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_empleado_proyecto_beneficio")]
	public class TB_EmpleadoProyectoBeneficio
	{
        [Column("Nukid"), NotNull, PrimaryKey, Identity] public int Id { get; set; }
        [Column("nukidbeneficio"), NotNull] public int IdBeneficio { get; set; }
       
		[Column("nunum_empleado_rr_hh"), NotNull] public string NumEmpleadoRrHh { get; set; }
        [Column("nunum_proyecto"), NotNull] public int NumProyecto { get; set; }

        [Column("nucostobeneficio"), NotNull] public decimal nucostobeneficio { get; set; }
    }
}
