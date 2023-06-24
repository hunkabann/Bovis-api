using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Responses;
using System.Text.Json.Nodes;

namespace Bovis.Service.Queries.Interface
{
    public interface IPersonaQueryService
    {
        #region Personas
        Task<Response<List<Persona_Detalle>>> GetPersonas(bool? Activo);
        Task<Response<Persona_Detalle>> GetPersona(int idPersona);
        Task<Response<(bool Success, string Message)>> AddRegistro(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdateRegistro(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdateEstatus(JsonObject registro);
        #endregion Personas
    }
}

