using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class CieQueryService : ICieQueryService
    {
        #region base
        private readonly ICieBusiness _cieBusiness;

        private readonly IMapper _map;

        public CieQueryService(IMapper _map, ICieBusiness _cieBusiness)
        {
            this._map = _map;
            this._cieBusiness = _cieBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion


        #region Empresas
        public async Task<Response<List<Empresa>>> GetEmpresas(bool? Activo)
        {
            var response = await _cieBusiness.GetEmpresas(Activo);
            return new Response<List<Empresa>> { Data = _map.Map<List<Empresa>>(response), Success = true };
        }
        #endregion Empresas

        #region Registros
        public async Task<Response<Cie>> GetInfoRegistro(int? idRegistro)
        {
            var response = await _cieBusiness.GetInfoRegistro(idRegistro);

            return new Response<Cie> { Data = _map.Map<Cie>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontro información del registro." : default };
        }

        public async Task<Response<List<Cie>>> GetRegistros(byte? Estatus)
        {
            var response = await _cieBusiness.GetRegistros(Estatus);
            return new Response<List<Cie>> { Data = _map.Map<List<Cie>>(response), Success = true };
        }
        #endregion Registros
    }
}

