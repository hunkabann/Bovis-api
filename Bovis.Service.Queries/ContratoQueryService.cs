using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
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
    public class ContratoQueryService : IContratoQueryService
    {
        #region base
        private readonly IContratoBusiness _contratoBusiness;
        private readonly IMapper _map;

        public ContratoQueryService(IMapper _map, IContratoBusiness _contratoBusiness)
        {
            this._map = _map;
            this._contratoBusiness = _contratoBusiness;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Templates
        public async Task<Response<List<TB_Contrato_Template>>> GetTemplates(string Estatus)
        {
            var response = await _contratoBusiness.GetTemplates(Estatus);
            return new Response<List<TB_Contrato_Template>> { Data = _map.Map<List<TB_Contrato_Template>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }

        public async Task<Response<TB_Contrato_Template>> GetTemplate(int IdTemplate)
        {
            var response = await _contratoBusiness.GetTemplate(IdTemplate);
            return new Response<TB_Contrato_Template> { Data = _map.Map<TB_Contrato_Template>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }

        public async Task<Response<(bool Success, string Message)>> AddTemplate(JsonObject registro)
        {
            var response = await _contratoBusiness.AddTemplate(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool Success, string Message)>> UpdateTemplate(JsonObject registro)
        {
            var response = await _contratoBusiness.UpdateTemplate(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool Success, string Message)>> UpdateTemplateEstatus(JsonObject registro)
        {
            var response = await _contratoBusiness.UpdateTemplateEstatus(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Templates

        #region Contratos Empleado
        public async Task<Response<List<TB_Contrato_Empleado>>> GetContratosEmpleado(int IdEmpleado)
        {
            var response = await _contratoBusiness.GetContratosEmpleado(IdEmpleado);
            return new Response<List<TB_Contrato_Empleado>> { Data = _map.Map<List<TB_Contrato_Empleado>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }

        public async Task<Response<TB_Contrato_Empleado>> GetContratoEmpleado(int IdContratoEmpleado)
        {
            var response = await _contratoBusiness.GetContratoEmpleado(IdContratoEmpleado);
            return new Response<TB_Contrato_Empleado> { Data = _map.Map<TB_Contrato_Empleado>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }

        public async Task<Response<(bool Success, string Message)>> AddContratoEmpleado(JsonObject registro)
        {
            var response = await _contratoBusiness.AddContratoEmpleado(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool Success, string Message)>> UpdateContratoEmpleado(JsonObject registro)
        {
            var response = await _contratoBusiness.UpdateContratoEmpleado(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Contratos Empleado
    }
}
