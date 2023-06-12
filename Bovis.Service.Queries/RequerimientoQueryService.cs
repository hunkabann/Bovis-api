using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.Tables;
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
    public class RequerimientoQueryService : IRequerimientoQueryService
    {
        #region base
        private readonly IRequerimientoBusiness _requerimientoBussines;
        private readonly IMapper _map;

        public RequerimientoQueryService(IMapper _map, IRequerimientoBusiness _requerimientoBussines)
        {
            this._map = _map;
            this._requerimientoBussines = _requerimientoBussines;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Habilidades
        public async Task<Response<List<Habilidad>>> GetHabilidades(int idRequerimiento)
        {
            var response = await _requerimientoBussines.GetHabilidades(idRequerimiento);
            return new Response<List<Habilidad>> { Data = _map.Map<List<Habilidad>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }
        #endregion Habilidades

        #region Experiencias
        public async Task<Response<List<Experiencia>>> GetExperiencias(int idRequerimiento)
        {
            var response = await _requerimientoBussines.GetExperiencias(idRequerimiento);
            return new Response<List<Experiencia>> { Data = _map.Map<List<Experiencia>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }
        #endregion Experiencias

        #region Registros
        public async Task<Response<List<Requerimiento>>> GetRequerimientos(bool? Activo)
        {
            var response = await _requerimientoBussines.GetRequerimientos(Activo);
            return new Response<List<Requerimiento>> { Data = _map.Map<List<Requerimiento>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }

        public async Task<Response<Requerimiento>> GetRequerimiento(int idRequerimiento)
        {
            var response = await _requerimientoBussines.GetRequerimiento(idRequerimiento);
            return new Response<Requerimiento> { Data = _map.Map<Requerimiento>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<(bool existe, string mensaje)>> AgregarRegistro(JsonObject registro)
        {
            var response = await _requerimientoBussines.AgregarRegistro(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool existe, string mensaje)>> UpdateRegistro(JsonObject registro)
        {
            var response = await _requerimientoBussines.UpdateRegistro(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Registros
    }
}
