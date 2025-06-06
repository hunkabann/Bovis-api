﻿using Bovis.Business.Interface;
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
        private readonly ITransactionData _transactionData;
        public PersonaBusiness(IPersonaData _personaData, ITransactionData _transactionData)
        {
            this._personaData = _personaData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base


        #region Personas
        public Task<List<Persona_Detalle>> GetPersonas(bool? Activo) => _personaData.GetPersonas(Activo);

        public Task<List<Persona_Detalle>> GetPersonasLibres() => _personaData.GetPersonasLibres();

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
            var respData = await _personaData.UpdateRegistro((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro de la persona a la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateEstatus(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _personaData.UpdateEstatus((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro de la persona a la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
        #endregion Personas

    }
}
