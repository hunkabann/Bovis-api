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
            return new Response<List<Empresa>> { Data = _map.Map<List<Empresa>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }
        #endregion Empresas

        #region Registros
        public async Task<Response<TB_Cie_Data>> GetRegistro(int? idRegistro)
        {
            var response = await _cieBusiness.GetRegistro(idRegistro);
            return new Response<TB_Cie_Data> { Data = _map.Map<TB_Cie_Data>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<List<TB_Cie_Data>>> GetRegistros(byte? Estatus)
        {
            var response = await _cieBusiness.GetRegistros(Estatus);
            return new Response<List<TB_Cie_Data>> { Data = _map.Map<List<TB_Cie_Data>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }

        public async Task<Response<(bool existe, string mensaje)>> AgregarRegistros(JsonObject registros)
        {
            var response = await _cieBusiness.AgregarRegistros(registros);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = true };
        }

        
        #endregion Registros
    }
}

