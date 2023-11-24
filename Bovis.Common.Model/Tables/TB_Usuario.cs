using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_usuario")]
	public class TB_Usuario
	{
		[Column("Nukidusuario"), NotNull, PrimaryKey, Identity] public int IdUsuario { get; set; }
		[Column("Nunum_empleado_rr_hh"), NotNull] public int NumEmpleadoRrHh { get; set; }
		[Column("Boactivo"), NotNull] public bool Activo { get; set; }
		[Column("Chtoken"), Nullable] public string? Token { get; set; }
		[Column("Dtfecha_ultima_session"), Nullable] public DateTime? FechaUltimaSesion { get; set; }
	}
}
