using Bovis.Common.Model.Tables;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{

    public class Empleado_Detalle
    {
        public int? nunum_empleado_rr_hh { get; set; }
        public int? nukidpersona { get; set; }
        public string? nombre_persona { get; set; }
        public string? dtfecha_nacimiento { get; set; }
        public int? nukidsexo { get; set; }
        public string? chsexo { get; set; }
        public int? nukidtipo_empleado { get; set; }
        public string? chtipo_emplado { get; set; }
        public int? nukidcategoria { get; set; }
        public string? chcategoria { get; set; }
        public int? nukidtipo_contrato { get; set; }
        public string? chtipo_contrato { get; set; }
        public int? chcve_puesto { get; set; }
        public string? chpuesto { get; set; }
        public int? nukidempresa { get; set; }
        public string? chempresa { get; set; }
        public string? chcalle { get; set; }
        public string? nunumero_interior { get; set; }
        public string? nunumero_exterior { get; set; }
        public string? chcolonia { get; set; }
        public string? chalcaldia { get; set; }        
        public int? nukidciudad { get; set; }
        public string? chciudad { get; set; }
        public int? nukidestado { get; set; }
        public string? chestado { get; set; }
        public string? chcp { get; set; }
        public int? nukidpais { get; set; }
        public string? chpais { get; set; }
        public int? nukidnivel_estudios { get; set; }
        public string? chnivel_estudios { get; set; }
        public int? nukidforma_pago { get; set; }
        public string? chforma_pago { get; set; }
        public int? nukidjornada { get; set; }
        public string? chjornada { get; set; }
        public int? nukiddepartamento { get; set; }
        public string? chdepartamento { get; set; }
        public int? nukidclasificacion { get; set; }
        public string? chclasificacion { get; set; }
        public int? nukidjefe_directo { get; set; }
        public string? chjefe_directo { get; set; }
        public int? nukidunidad_negocio { get; set; }
        public string? chunidad_negocio { get; set; }
        public int? nukidtipo_contrato_sat { get; set; }
        public string? chtipo_contrato_sat { get; set; }
        public int? nunum_empleado { get; set; }
        public string dtfecha_ingreso { get; set; }
        public string? dtfecha_salida { get; set; }
        public string? dtfecha_ultimo_reingreso { get; set; }
        public string? chnss { get; set; }
        public string? chemail_bovis { get; set; }        
        public string? churl_repositorio { get; set; }
        public decimal? nusalario { get; set; }
        public int? nukidprofesion { get; set; }
        public string chprofesion { get; set; }
        public string? nuantiguedad { get; set; }
        public int? nukidturno { get; set; }
        public string? chturno { get; set; }
        public int? nuunidad_medica { get; set; }
        public string? chregistro_patronal { get; set; }
        public string? chcotizacion { get; set; }
        public int? nuduracion { get; set; }
        public bool? boactivo { get; set; }
        public bool? boempleado { get; set; }
        public string? chporcentaje_pension { get; set; }
        public decimal? nudescuento_pension { get; set; }
        public decimal? nuporcentaje_pension { get; set; }
        public decimal? nufondo_fijo { get; set; }
        public string? nucredito_infonavit { get; set; }
        public string? chtipo_descuento { get; set; }
        public decimal? nuvalor_descuento { get; set; }
        public string? nuno_empleado_noi { get; set; }
        public string? chrol { get; set; }
        public string? chexperiencias { get; set; }
        public string? chhabilidades { get; set; }
        public List<Experiencia_Detalle>? experiencias { get; set; }
        public List<Habilidad_Detalle>? habilidades { get; set; }        
    }

    public class Empleado_Responsable
    {
        public int? nunum_empleado_rr_hh { get; set; }
        public string? nombre_persona { get; set; }
    }

    public class Empleado_BasicData
    {
        public int? nukid_empleado { get; set; }
        public string? chnombre { get; set; }
        public string? chap_paterno { get; set; }
        public string? chap_materno { get; set; }
        public string? chpuesto { get; set; }
        public string? chemailbovis {  get; set; }
    }

    public class Experiencia_Detalle
    {
        public int? IdEmpleado { get; set; }
        public int? IdExperiencia { get; set; }
        public string? Experiencia { get;set; }
        public bool? Activo { get; set; }
    }

    public class Habilidad_Detalle
    {
        public int? IdEmpleado { get; set; }
        public int? IdHabilidad { get; set; }
        public string? Habilidad { get; set; }
        public bool? Activo { get; set; }
    }
}
