using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Proyecto
    {
        public int? NumProyecto { get; set; }
        public string? Nombre { get; set; }
        public string? Alcance { get; set; }
        public string? Cp { get; set; }
        public string? Ciudad { get; set; }
        public int? IdEstatus { get; set; }
        public int? IdSector { get; set; }
        public int? IdTipoProyecto { get; set; }
        public int? IdResponsablePreconstruccion { get; set; }
        public int? IdResponsableConstruccion { get; set; }
        public int? IdResponsableEhs { get; set; }
        public int? IdResponsableSupervisor { get; set; }
        public int? IdCliente { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdPais { get; set; }
        public int? IdDirectorEjecutivo { get; set; }
        public decimal? CostoPromedioM2 { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class Proyecto_Detalle
    {
        public int? nunum_proyecto { get; set; }
        public string? chproyecto { get; set; }
        public string? chalcance { get; set; }
        public string? chcp { get; set; }
        public string? chciudad { get; set; }
        public int? nukidpais { get; set; }
        public string? chpais { get; set; }
        public int? nukidestatus { get; set; }
        public string? chestatus { get; set; }
        public int? nukidsector { get; set; }
        public string? chsector { get; set; }
        public int? nukidtipo_proyecto { get; set; }
        public string? chtipo_proyecto { get; set; }
        public string? nukidresponsable_preconstruccion { get; set; }
        public string? chresponsable_preconstruccion { get; set; }
        public string? nukidresponsable_construccion { get; set; }
        public string? chresponsable_construccion { get; set; }
        public string? nukidresponsable_ehs { get; set; }
        public string? chresponsable_ehs { get; set; }
        public string? nukidresponsable_supervisor { get; set; }
        public string? chresponsable_supervisor { get;set; }
        public int? nukidempresa { get; set; }
        public string? chempresa { get; set; }
        public string? nukiddirector_ejecutivo { get; set; }
        public string? chdirector_ejecutivo { get; set; }
        public decimal? nucosto_promedio_m2 { get; set; }
        public DateTime? dtfecha_ini { get; set; }
        public DateTime? dtfecha_fin { get; set; }
        public string? nunum_empleado_rr_hh { get; set; }
        public string? empleado { get; set; }
        public decimal? nuporcantaje_participacion { get; set; }
        public string? chalias_puesto { get; set; }
        public string? chgrupo_proyecto { get; set; }
        public string? chcontacto_nombre { get; set; }
        public string? chcontacto_posicion { get; set; }
        public string? chcontacto_telefono { get; set; }
        public string? chcontacto_correo { get; set; }
        public float? nudias {  get; set; }
        public float? nudedicacion { get; set; }
        public decimal? nucosto { get; set; }

        public List<InfoCliente> Clientes { get; set; }
    }

    public class InfoCliente
    {
        public int? IdCliente { get; set; }
        public string? Rfc { get; set; }
        public string? Cliente { get; set; }
    }

    public class InfoEmpresa
    {
        public int? IdEmpresa { get; set; }
        public string? Empresa { get; set; }
        public string? Rfc { get; set; }
    }
}
