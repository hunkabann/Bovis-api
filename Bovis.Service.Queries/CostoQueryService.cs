using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class CostoQueryService : ICostoQueryService
    {
        #region base
        private readonly ICostoBusiness _costoBusiness;

        private readonly IMapper _map;

        public CostoQueryService(IMapper _map, ICostoBusiness _costoBusiness)
        {
            this._map = _map;
            this._costoBusiness = _costoBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<Response<(bool Success, string Message)>> AddCosto(JsonObject registro)
        {
            var response = await _costoBusiness.AddCosto(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<List<Costo_Detalle>>> GetCostos(int IdCosto)
        {
            var response = await _costoBusiness.GetCostos(IdCosto);
            return new Response<List<Costo_Detalle>> { Data = _map.Map<List<Costo_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
    }
}

