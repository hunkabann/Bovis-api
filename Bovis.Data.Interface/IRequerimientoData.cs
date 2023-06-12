using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IRequerimientoData : IDisposable
    {
        Task<List<TB_Requerimiento_Habilidad>> GetHabilidades(int idRequerimiento);
        Task<List<TB_Requerimiento_Experiencia>> GetExperiencias(int idRequerimiento);
        Task<List<TB_Requerimiento>> GetRequerimientos(bool? activo);
        Task<TB_Requerimiento> GetRequerimiento(int idRequerimiento);
        Task<(bool existe, string mensaje)> AgregarRegistro(JsonObject registro);
        Task<(bool existe, string mensaje)> UpdateRegistro(JsonObject registro);
        Task<(bool existe, string mensaje)> DeleteRequerimiento(int idRequerimiento);
    }
}
