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
    public class ReporteQueryService : IReporteQueryService
    {
        #region base
        private readonly IReporteBusiness _reporteBusiness;
        private readonly IMapper _map;

        public ReporteQueryService(IMapper _map, IReporteBusiness _reporteBusiness)
        {
            this._map = _map;
            this._reporteBusiness = _reporteBusiness;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Custom Reports
        public async Task<Response<object>> ExecReportePersonalizado(JsonObject registro)
        {
            var response = await _reporteBusiness.ExecReportePersonalizado(registro);
            return new Response<object> { Data = _map.Map<object>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<(bool Success, string Message)>> AddReportePersonalizado(JsonObject registro)
        {
            var response = await _reporteBusiness.AddReportePersonalizado(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<List<Reporte_Detalle>>> GetReportesPersonalizados(int IdReporte)
        {
            var response = await _reporteBusiness.GetReportesPersonalizados(IdReporte);
            return new Response<List<Reporte_Detalle>> { Data = _map.Map<List<Reporte_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<(bool existe, string mensaje)>> UpdateReportePersonalizado(JsonObject registro)
        {
            var response = await _reporteBusiness.UpdateReportePersonalizado(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool existe, string mensaje)>> DeleteReportePersonalizado(int IdReporte)
        {
            var response = await _reporteBusiness.DeleteReportePersonalizado(IdReporte);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Custom Reports
    }
}

