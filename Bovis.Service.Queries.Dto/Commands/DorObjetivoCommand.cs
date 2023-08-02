using Bovis.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovis.Service.Queries.Dto.Commands
{
    public class UpdDorObjetivoCommand : IRequest<Response<bool>>
    {
        [Required(ErrorMessage = "El campo IdEmpOb es requerido")]
        public int IdEmpOb { get; set; }

        public string? UnidadDeNegocio { get; set; }
        public string? Concepto { get; set; }
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "El campo meta es requerido")]
        public decimal? Meta { get; set; }
        public int? Valor { get; set; }
        public decimal? Real { get; set; }
        public string? Ponderado { get; set; }
        public string? Calificacion { get; set; }
        //public int? Nivel { get; set; }
        //[Required(ErrorMessage = "El campo año es requerido")]
        public int? Anio { get; set; }
        //[Required(ErrorMessage = "El campo proyecto es requerido")]
        public int? Proyecto { get; set; }
        //[Required(ErrorMessage = "El campo empleado es requerido")]
        public int? Empleado { get; set; }
        [Required(ErrorMessage = "El campo acepto es requerido")]
        public int Acepto { get; set; }
        public string? MotivoR { get; set; }
        public DateTime? FechaCarga { get; set; }
        public DateTime? FechaAceptado { get; set; }
        public DateTime? FechaRechazo { get; set; }

    }
    public class AddDorObjetivoCommand : IRequest<Response<bool>>
    {
        [Required(ErrorMessage = "El campo IdEmpOb es requerido")]
        public int IdEmpOb { get; set; }

        public string? UnidadDeNegocio { get; set; }
        public string? Concepto { get; set; }
        public string? Descripcion { get; set; }
        [Required(ErrorMessage = "El campo meta es requerido")]
        public string? Meta { get; set; }
        public string? Real { get; set; }
        public string? Ponderado { get; set; }
        public string? Calificacion { get; set; }
        public string? Nivel { get; set; }
       
        //[Required(ErrorMessage = "El campo año es requerido")]
        public string? Anio { get; set; }
        //[Required(ErrorMessage = "El campo proyecto es requerido")]
        public string? Proyecto { get; set; }
        //[Required(ErrorMessage = "El campo empleado es requerido")]
        public string? Empleado { get; set; }
        [Required(ErrorMessage = "El campo acepto es requerido")]
        public string? Acepto { get; set; }

    }
}