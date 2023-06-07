using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Dto.Responses
{
    public class Proyecto
    {
        public int NumProyecto { get; set; }
        public string Nombre { get; set; }
        public string Alcance { get; set; }
        public string? Cp { get; set; }
        public string Ciudad { get; set; }
        public int IdEstatus { get; set; }
        public int IdSector { get; set; }
        public int IdTipoProyecto { get; set; }
        public int? IdResponsablePreconstruccion { get; set; }
        public int? IdResponsableConstruccion { get; set; }
        public int? IdResponsableEhs { get; set; }
        public int? IdResponsableSupervisor { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPais { get; set; }
        public int IdDirectorEjecutivo { get; set; }
        public decimal CostoPromedioM2 { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
    }

    public class Detalle_Proyecto
    {
        public int nunum_proyecto { get; set; }
        public string chproyecto { get; set; }
        public string chalcance { get; set; }
        public string chcp { get; set; }
        public string chciudad { get; set; }
        public int nukidestatus { get; set; }
        public int nukidsector { get; set; }
        public int nukidtipo_proyecto { get; set; }
        public int nukidresponsable_preconstruccion { get; set; }
        public int nukidresponsable_construiccion { get; set; }
        public int nukidresponsable_ehs { get; set; }
        public int nukidresponsable_supervisor { get; set; }
        public int nukidcliente { get; set; }
        public int nukidempresa { get; set; }
        public int nukidpais { get; set; }
        public int nukiddirector_ejecutivo { get; set; }
        public decimal nucosto_promedio_m2 { get; set; }
        public DateTime dtfecha_ini { get; set; }
        public DateTime dtfecha_fin { get; set; }
        public int nunum_empleado_rr_hh { get; set; }
        public decimal nuporcantaje_participacion { get; set; }
        public string chalias_puesto { get; set; }
        public string chgrupo_proyecto { get; set; }        
    }

    public class InfoCliente
    {
        public int IdCliente { get; set; }
        public string Rfc { get; set; }
        public string Cliente { get; set; }
    }

    public class InfoEmpresa
    {
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public string Rfc { get; set; }
    }
}
