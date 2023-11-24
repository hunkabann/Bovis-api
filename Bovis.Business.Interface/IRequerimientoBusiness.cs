using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface IRequerimientoBusiness : IDisposable
    {
        Task<List<TB_RequerimientoHabilidad>> GetHabilidades(int idRequerimiento);
        Task<List<TB_RequerimientoExperiencia>> GetExperiencias(int idRequerimiento);
        Task<List<Requerimiento_Detalle>> GetRequerimientos(bool? Asignados, int? idDirector, int? idProyecto, int? idPuesto);
        Task<Requerimiento_Detalle> GetRequerimiento(int idRequerimiento);
        Task<(bool Success, string Message)> AddRegistro(JsonObject registro);
        Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro);
        Task<(bool Success, string Message)> DeleteRequerimiento(int idRequerimiento);
        Task<List<Empleado_Detalle>> GetDirectoresEjecutivos();
        Task<List<TB_Proyecto>> GetProyectosByDirectorEjecutivo(int IdDirectorEjecutivo);
    }
}
