using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IPersonaData : IDisposable
    {
        #region Personas
        Task<List<Persona_Detalle>> GetPersonas(bool? activo);
        Task<Persona_Detalle> GetPersona(int idPersona);
        Task<(bool Success, string Message)> AddRegistro(JsonObject registro);
        #endregion Personas
    }
}