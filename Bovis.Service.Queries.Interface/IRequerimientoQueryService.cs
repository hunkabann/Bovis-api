using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface IRequerimientoQueryService : IDisposable
    {
        Task<Response<List<Habilidad>>> GetHabilidades(int idRequerimiento);
        Task<Response<List<Experiencia>>> GetExperiencias(int idRequerimiento);
        Task<Response<List<Requerimiento_Detalle>>> GetRequerimientos(bool? Asignados, string? idDirector, int? idProyecto, int? idPuesto);
        Task<Response<Requerimiento_Detalle>> GetRequerimiento(int idRequerimiento);
        Task<Response<(bool Success, string Message)>> AddRegistro(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdateRegistro(JsonObject registro);
        Task<Response<(bool Success, string Message)>> DeleteRequerimiento(int idRequerimiento);
        Task<Response<List<Empleado_Detalle>>> GetDirectoresEjecutivos();
        Task<Response<List<TB_Proyecto>>> GetProyectosByDirectorEjecutivo(string IdDirectorEjecutivo);
    }
}
