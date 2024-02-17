using LinqToDB.Mapping;

namespace Bovis.Common.Model.Tables
{
	[Table(Schema = "dbo", Name = "tb_dor_empleados")]
	public class TB_DorEmpleados
	{
        [Column("idEmp"), NotNull, PrimaryKey, Identity] public string IdEmp { get; set; }
        [Column("Usuario"), Nullable] public string? Usuario { get; set; }
		[Column("Rol"), Nullable] public string? Rol { get; set; }
		[Column("Nombre"), Nullable] public string? Nombre { get; set; }
		[Column("NoEmpleado"), Nullable] public string? NoEmpleado { get; set; }
		[Column("CorreoElec"), Nullable] public string? CorreoElec { get; set; }
		[Column("Contrasena"), Nullable] public string? Contrasena { get; set; }
		[Column("PersonalaCargo"), Nullable] public string? PersonalaCargo { get; set; }
		[Column("Puesto"), Nullable] public string? Puesto { get; set; }
		[Column("JefeDirecto"), Nullable] public string? JefeDirecto { get; set; }
		[Column("DireccionEjecutiva"), Nullable] public string? DireccionEjecutiva { get; set; }
		[Column("UnidadDeNegocio"), Nullable] public string? UnidadDeNegocio { get; set; }
		[Column("Proyecto"), Nullable] public string? Proyecto { get; set; }
		[Column("LlenoObjetivos"), Nullable] public string? LlenoObjetivos { get; set; }
		[Column("CentrosdeCostos"), Nullable] public string? CentrosdeCostos { get; set; }
		[Column("CambiodeProyecto"), Nullable] public string? CambiodeProyecto { get; set; }
		[Column("FechaIngreso"), Nullable] public string? FechaIngreso { get; set; }
		[Column("MeseAconsiderar"), Nullable] public string? MeseAconsiderar { get; set; }
		[Column("AcumuladoPuntos"), Nullable] public string? AcumuladoPuntos { get; set; }
		[Column("Perfiles"), Nullable] public string? Perfiles { get; set; }
		[Column("Permisos"), Nullable] public string? Permisos { get; set; }
		[Column("PuntosPosibles"), Nullable] public string? PuntosPosibles { get; set; }
		[Column("Aprovechamiento"), Nullable] public string? Aprovechamiento { get; set; }
        [Column("Activo"), Nullable] public bool? Activo { get; set; }
    }
}
