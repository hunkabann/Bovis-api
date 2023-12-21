using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IRequerimientoData : IDisposable
    {
        Task<List<TB_RequerimientoHabilidad>> GetHabilidades(int idRequerimiento);
        Task<List<TB_RequerimientoExperiencia>> GetExperiencias(int idRequerimiento);
        Task<List<Requerimiento_Detalle>> GetRequerimientos(bool? Asignados, string? idDirector, int? idProyecto, int? idPuesto);
        Task<Requerimiento_Detalle> GetRequerimiento(int idRequerimiento);
        Task<(bool Success, string Message)> AddRegistro(JsonObject registro);
        Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro);
        Task<(bool Success, string Message)> DeleteRequerimiento(int idRequerimiento);
        Task<List<Empleado_Detalle>> GetDirectoresEjecutivos();
        Task<List<TB_Proyecto>> GetProyectosByDirectorEjecutivo(string IdDirectorEjecutivo);
    }
}
