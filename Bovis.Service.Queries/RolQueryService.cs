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
    public class RolQueryService : IRolQueryService
    {
        #region base
        private readonly IRolBusiness _rolBusiness;
        private readonly IMapper _map;

        public RolQueryService(IMapper _map, IRolBusiness _rolBusiness)
        {
            this._map = _map;
            this._rolBusiness = _rolBusiness;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<Response<Rol_Detalle>> GetRoles(string email)
        {
            var response = await _rolBusiness.GetRoles(email);
            return new Response<Rol_Detalle> { Data = _map.Map<Rol_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
    }
}
