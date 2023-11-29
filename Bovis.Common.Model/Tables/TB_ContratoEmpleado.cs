using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
    [Table(Schema = "dbo", Name = "tb_contrato_empleado")]
    public class TB_ContratoEmpleado
    {
        [Column("Nukidcontrato_empleado"), NotNull, PrimaryKey, Identity] public int IdContratoEmpleado { get; set; }
        [Column("Chtitulo"), NotNull] public string Titulo { get; set; }
        [Column("Chcontrato"), NotNull] public string Contrato { get; set; }
        [Column("Nunum_empleado_rr_hh"), NotNull] public int NumEmpleadoRrHh { get; set; }
        [Column("Boactivo"), NotNull] public bool Activo { get; set; }
    }
}