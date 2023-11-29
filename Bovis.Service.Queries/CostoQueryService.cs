using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
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

        #region AddCosto
        public async Task<Response<decimal>> AddCosto(CostoPorEmpleadoDTO source)
        {
            var response = await _costoBusiness.AddCosto(source);
            return response; 
        }
        #endregion

        #region GetCostos
        public async Task<Response<List<TB_CostoPorEmpleado>>> GetCostos(bool hist)
        {
            var response = await _costoBusiness.GetCostos(hist);
            return new Response<List<TB_CostoPorEmpleado>> { Data = _map.Map<List<TB_CostoPorEmpleado>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }
        #endregion

        #region GetCosto
        public async Task<Response<TB_CostoPorEmpleado>> GetCosto(int IdCosto)
        {
            var response = await _costoBusiness.GetCosto(IdCosto);
            return new Response<TB_CostoPorEmpleado> { Data = _map.Map<TB_CostoPorEmpleado>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        #endregion

        #region GetCostosEmpleado
        public async Task<Response<List<TB_CostoPorEmpleado>>> GetCostosEmpleado(int NumEmpleadoRrHh, bool hist)
        {
            return await _costoBusiness.GetCostosEmpleado(NumEmpleadoRrHh, hist);
        }
        #endregion

        #region GetCostoEmpleado
        public async Task<Response<List<TB_CostoPorEmpleado>>> GetCostoEmpleado(int NumEmpleadoRrHh, int anno, int mes, bool hist)
        {
            return await _costoBusiness.GetCostoEmpleado(NumEmpleadoRrHh, anno, mes, hist);
        }
        #endregion

        #region GetCostoLaborable
        public async Task<Response<decimal>> GetCostoLaborable(int NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max)
        {
            return await _costoBusiness.GetCostoLaborable(NumEmpleadoRrHh, anno_min, mes_min, anno_max, mes_max);
        }
        #endregion

        #region GetCostBetweenDates
        public async Task<Response<List<TB_CostoPorEmpleado>>> GetCostosBetweenDates(int NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max, bool hist)
        {
            return await _costoBusiness.GetCostosBetweenDates(NumEmpleadoRrHh,anno_min,mes_min,anno_max,mes_max, hist); 

        }
        #endregion

        #region UpdateCostos
        public async Task<Response<TB_CostoPorEmpleado>> UpdateCostos(int costoId, CostoPorEmpleadoDTO source)
        {
            var response = await _costoBusiness.UpdateCostos(costoId, source);
            return response; 
        }
        #endregion

        #region DeleteCosto
        public async Task<Response<bool>> DeleteCosto(int costoId)
        {
            var response = await _costoBusiness.DeleteCosto(costoId);
            return response;
        }
        #endregion 


    }
}

