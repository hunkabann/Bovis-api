using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{ 
    public class Empleado_Detalle
    {
        public int nunum_empleado_rr_hh { get; set; }
        public int nukidpersona { get; set; }
        public string nombre_persona { get; set; }
        public int nukidtipo_empleado { get; set; }
        public int nukidcategoria { get; set; }
        public int nukidtipo_contrato { get; set; }
        public string chcve_puesto { get; set; }
        public int nukidempresa { get; set; }
        public int nukidciudad { get; set; }
        public int nukidnivel_estudios { get; set; }
        public int nukidforma_pago { get; set; }
        public int nukidjornada { get; set; }
        public int nukiddepartamento { get; set; }
        public int nukidclasificacion { get; set; }
        public int nukidjefe_directo { get; set; }
        public int nukidunidad_negocio { get; set; }
        public int nukidtipo_contrato_sat { get; set; }
        public int nunum_empleado { get; set; }
        public DateTime dtfecha_ingreso { get; set; }
        public DateTime dtfecha_salida { get; set; }
        public DateTime dtfecha_ultimo_reingreso { get; set; }
        public decimal chnss { get; set; }
        public string chemail_bovis { get; set; }
        public string chexperiencias { get; set; }
        public string chhabilidades { get; set; }
        public string churl_repositorio { get; set; }
        public decimal nusalario { get; set; }
        public string chprofesion { get; set; }
        public int nuantiguedad { get; set; }
        public string chturno { get; set; }
        public int nuunidad_medica { get; set; }
        public string chregistro_patronal { get; set; }
        public string chcotizacion { get; set; }
        public int nuduracion { get; set; }
        public bool boactivo { get; set; }
        public bool bodescuento_pension { get; set; }
        public decimal nuporcentaje_pension { get; set; }
        public decimal nufondo_fijo { get; set; }
        public int nucredito_infonavit { get; set; }
        public string chtipo_descuento { get; set; }
        public decimal nuvalor_descuento { get; set; }
        public int nuno_empleado_noi { get; set; }
        public string chrol { get; set; }
    }
}
