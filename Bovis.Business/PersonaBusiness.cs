using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;

namespace Bovis.Business
{
    public class PersonaBusiness : IPersonaBusiness
    {
        #region base
        private readonly IPersonaData _personaData;
        public PersonaBusiness(IPersonaData _personaData)
        {
            this._personaData = _personaData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base


        #region Personas
        public Task<List<Persona_Detalle>> GetPersonas(bool? Activo) => _personaData.GetPersonas(Activo);

        public Task<Persona_Detalle> GetPersona(int idPersona) => _personaData.GetPersona(idPersona);

        public async Task<(bool Success, string Message)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _personaData.AddRegistro(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro de la persona a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _personaData.UpdateRegistro(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro de la persona a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        #endregion Personas

    }
}
