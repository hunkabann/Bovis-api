using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class PCS_Proyecto_Detalle
    {
        public int NumProyecto { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public List<PCS_Etapa_Detalle> Etapas { get; set; }
    }

    public class PCS_Etapa_Detalle
    {
        public int IdFase { get; set; }
        public int Orden { get; set; }
        public string Fase { get; set; }
        public DateTime FechaIni { get; set; }
        public DateTime FechaFin { get; set; }
        public List<PCS_Empleado_Detalle> Empleados { get; set; }
    }

    public class PCS_Empleado_Detalle
    {
        public int Id { get; set; }
        public int IdFase { set; get; }
        public string NumempleadoRrHh { get; set; }
        public string Empleado { get; set; }
        public decimal? Cantidad { get; set; }
        public bool? AplicaTodosMeses { get; set; }
        public decimal? Fee { get; set; }
        public List<PCS_Fecha_Detalle> Fechas { get; set; }
    }

    public class PCS_Fecha_Detalle
    {
        public int Id { get; set; }
        public string? Rubro { get; set; }
        public string? ClasificacionPY { get; set; }
        public bool? RubroReembolsable { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public decimal? Porcentaje { get; set; }
    }

    public class Tipo_Proyecto
    {
        public int IdTipoProyecto { get; set; }
        public string TipoProyecto { get; set; }
    }





    public class GastosIngresos_Detalle
    {
        public int NumProyecto { get; set; }
        public DateTime? FechaIni { get; set; }
        public DateTime? FechaFin { get; set; }
        public List<Seccion_Detalle> Secciones { get; set; }
        public List<PCS_Fecha_Totales> Totales { get; set; }
    }

    public class Seccion_Detalle
    {
        public int IdSeccion { get; set; }
        public string Codigo { get; set; }
        public string Seccion { get; set; }
        public List<Rubro_Detalle> Rubros { get; set; }
        public List<PCS_Fecha_Suma> SumaFechas { get; set; }
    }

    public class Rubro_Detalle
    {
        public int Id { get; set; }
        public int IdRubro { get; set; }
        public string Rubro { get; set; }
        public string? Unidad { get; set; }
        public decimal? Cantidad { get; set; }
        public bool? Reembolsable { get; set; }
        public bool? AplicaTodosMeses { get; set; }
        public string? Empleado { get; set; }
        public string? NumEmpleadoRrHh { get; set; }
        public decimal? CostoMensual { get; set; }
        public List<PCS_Fecha_Detalle> Fechas { get; set; }
    }

    public class PCS_Fecha_Suma
    {
        public string? Rubro { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public decimal? SumaPorcentaje { get; set; }
    }

    public class PCS_Fecha_Totales
    {
        public bool? Reembolsable { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public decimal? TotalPorcentaje { get; set; }
    }






    public class Control_Detalle
    {
        public ControlRubro_Detalle Salarios { get; set; }
        public ControlRubro_Detalle Viaticos { get; set; }
        public Gasto_Detalle Gastos { get; set; }
    }

    public class ControlRubro_Detalle
    {
        public string Rubro { get; set; }
        public ValoresRubro_Detalle Previsto { get; set; }
        public ValoresRubro_Detalle Real { get; set; }
    }

    public class ValoresRubro_Detalle
    {
        public decimal SubTotal { get; set; }
        public List<PCS_Fecha_Suma> SumaFechas { get; set; }
    }

    public class Gasto_Detalle
    {
        public List<ValoresRubro_Detalle> Previstos { get; set; }
        public List<ValoresRubro_Detalle> Reales { get; set; }
    }



    public class Control_Data
    {
        public string Seccion { get; set; }
        public bool HasChildren { get; set; }
        public Control_PrevistoReal Previsto { get; set; }
        public Control_PrevistoReal Real { get; set; }
        public List<Control_Subseccion> Subsecciones { get; set; }
    }

    public class Control_PrevistoReal
    {
        public decimal SubTotal { get; set; }
        public List<Control_Fechas> Fechas { get; set; }

    }

    public class Control_Fechas
    {
        public string Rubro { get; set; }
        public string? ClasificacionPY { get; set; }
        public int? Mes { get; set; }
        public int? Anio { get; set; }
        public decimal? Porcentaje { get; set; }
    }

    public class Control_Subseccion
    {
        public string Slug { get; set; }
        public string Seccion { get; set; }
        public Control_PrevistoReal Previsto { get; set; }
        public Control_PrevistoReal Real { get; set; }
    }

}
