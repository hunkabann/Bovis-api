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
    public interface IPersonaBusiness : IDisposable
    {
        #region Personas
        Task<List<Persona_Detalle>> GetPersonas(bool? activo);
        Task<List<Persona_Detalle>> GetPersonasLibres();
        Task<Persona_Detalle> GetPersona(int idPersona);
        Task<(bool Success, string Message)> AddRegistro(JsonObject registro);
        Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro);
        Task<(bool Success, string Message)> UpdateEstatus(JsonObject registro);
        #endregion Personas
    }

}
