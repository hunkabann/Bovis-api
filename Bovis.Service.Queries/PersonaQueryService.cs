using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class PersonaQueryService : IPersonaQueryService
    {
        #region base
        private readonly IPersonaBusiness _personaBusiness;

        private readonly IMapper _map;

        public PersonaQueryService(IMapper _map, IPersonaBusiness _personaBusiness)
        {
            this._map = _map;
            this._personaBusiness = _personaBusiness;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Personas
        public async Task<Response<List<Persona_Detalle>>> GetPersonas(bool? Activo)
        {
            var response = await _personaBusiness.GetPersonas(Activo);
            return new Response<List<Persona_Detalle>> { Data = _map.Map<List<Persona_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }

        public async Task<Response<Persona_Detalle>> GetPersona(int idPersona)
        {
            var response = await _personaBusiness.GetPersona(idPersona);
            return new Response<Persona_Detalle> { Data = _map.Map<Persona_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<(bool Success, string Message)>> AddRegistro(JsonObject registro)
        {
            var response = await _personaBusiness.AddRegistro(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool Success, string Message)>> UpdateRegistro(JsonObject registro)
        {
            var response = await _personaBusiness.UpdateRegistro(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool Success, string Message)>> UpdateEstatus(JsonObject registro)
        {
            var response = await _personaBusiness.UpdateEstatus(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Personas

    }
}

