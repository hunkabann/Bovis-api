using System.ComponentModel.DataAnnotations.Schema;

namespace Bovis.Service.Queries.Dto.Responses
{
    public class DorEmpleadoCorreo
    {
        public string? Nombre { get; set; }
        public string? Puesto { get; set; }
        public string? Proyecto { get; set; }
        public string? CentrosdeCostos { get; set; }
    }

    public class DorSubordinado
    {
        public string? Nombre { get; set; }
        public string? NoEmpleado { get; set; }
        public string? DireccionEjecutiva { get; set; }
        public string? Proyecto { get; set; }
        public string? CentrosdeCostos { get; set; }
        public string? UnidadDeNegocio { get; set; }
        public string? Puesto { get; set; }
        public string? JefeDirecto { get; set; }
        public string? Nivel { get; set; }
    }
    public class DorObjetivoGeneral
    {
        public int Id { get; set; }
        public string? UnidadDeNegocio { get; set; }
        public string? Concepto { get; set; }
        public string? Descripcion { get; set; }
        public string? Meta { get; set; }
        public string Real { get; set; }
        public string? PromedioReal { get; set; }
        public string? PorcentajeEstimado { get; set; }
        public string? PorcentajeReal { get; set; }
        public decimal? Ingreso { get; set; }
        public decimal? Gasto { get; set; }
        public string? Nivel { get; set; }
        public string? Valor { get; set; }
        public string? Tooltip { get; set; }
        public decimal? ENE { get; set; }
        public decimal? FEB { get; set; }
        public decimal? MAR { get; set; }
        public decimal? ABR { get; set; }
        public decimal? MAY { get; set; }
        public decimal? JUN { get; set; }
        public decimal? JUL { get; set; }
        public decimal? AGO { get; set; }
        public decimal? SEP { get; set; }
        public decimal? OCT { get; set; }
        public decimal? NOV { get; set; }
        public decimal? DIC { get; set; }
        public decimal? ValorMes { get; set; }
    }

    public class DorObjetivoDesepeno
    {
        public string? IdEmpOb { get; set; }
        //public string? Anio { get; set; }
        public string? UnidadDeNegocio { get; set; }
        public string? Concepto { get; set; }
        public string? Descripcion { get; set; }
        public string? Meta { get; set; }
        public string? Real { get; set; }
        public string? PromedioReal { get; set; }
        public string? PorcentajeEstimado { get; set; }
        public string? PorcentajeReal { get; set; }
        public string? Acepto { get; set; }
        public string? MotivoR { get; set; }
        public DateTime? FechaCarga { get; set; }
        public DateTime? FechaAceptado { get; set; }
        public DateTime? FechaRechazo { get; set; }
        public string? Nivel { get; set; }
        public string? Valor { get; set; }
        public string? Tooltip { get; set; }
        //public string? Real { get; set; }
        //public string? Ponderado { get; set; }
        //public string? Calificacion { get; set; }
        //public string? Nivel2 { get; set; }
        //public string? Nivel3 { get; set; }
        //public string? Nivel4 { get; set; }
        //public string? Nivel5 { get; set; }
        //      public string? Proyecto { get; set; }
        //      public string? Empleado { get; set; }

    }
}
