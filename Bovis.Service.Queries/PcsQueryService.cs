using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;
using System.Text.Json.Nodes;

namespace Bovis.Service.Queries
{
    public class PcsQueryService : IPcsQueryService
    {
        #region base
        private readonly IPcsBusiness _pcsBusiness;

        private readonly IMapper _map;

        public PcsQueryService(IMapper _map, IPcsBusiness _pcsBusiness)
        {
            this._map = _map;
            this._pcsBusiness = _pcsBusiness;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<Response<List<Proyecto>>> GetProyectos(bool? OrdenAlfabetico)
        {
            var response = await _pcsBusiness.GetProyectos(OrdenAlfabetico);
            return new Response<List<Proyecto>> { Data = _map.Map<List<Proyecto>>(response), Success = true };
        }

        public async Task<Response<Proyecto>> GetProyecto(int numProyecto)
        {
            var response = await _pcsBusiness.GetProyecto(numProyecto);
            return new Response<Proyecto> { Data = _map.Map<Proyecto>(response), Success = true };
        }

        public async Task<Response<List<InfoCliente>>> GetClientes()
        {
            var response = await _pcsBusiness.GetClientes();
            return new Response<List<InfoCliente>> { Data = _map.Map<List<InfoCliente>>(response), Success = true };
        }
        public async Task<Response<List<InfoEmpresa>>> GetEmpresas()
        {
            var response = await _pcsBusiness.GetEmpresas();
            return new Response<List<InfoEmpresa>> { Data = _map.Map<List<InfoEmpresa>>(response), Success = true };
        }

        #region Proyectos
        public async Task<Response<(bool Success, string Message)>> AddProyecto(JsonObject registro)
        {
            var response = await _pcsBusiness.AddProyecto(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<List<Proyecto_Detalle>>> GetProyectos(int IdProyecto)
        {
            var response = await _pcsBusiness.GetProyectos(IdProyecto);
            return new Response<List<Proyecto_Detalle>> { Data = _map.Map<List<Proyecto_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<(bool Success, string Message)>> UpdateProyecto(JsonObject registro)
        {
            var response = await _pcsBusiness.UpdateProyecto(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool Success, string Message)>> DeleteProyecto(int IdProyecto)
        {
            var response = await _pcsBusiness.DeleteProyecto(IdProyecto);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Proyectos

        #region Etapas
        public async Task<Response<(bool Success, string Message)>> AddEtapa(JsonObject registro)
        {
            var response = await _pcsBusiness.AddEtapa(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<List<PCS_Etapa_Detalle>>> GetEtapas(int IdProyecto)
        {
            var response = await _pcsBusiness.GetEtapas(IdProyecto);
            return new Response<List<PCS_Etapa_Detalle>> { Data = _map.Map<List<PCS_Etapa_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<(bool Success, string Message)>> UpdateEtapa(JsonObject registro)
        {
            var response = await _pcsBusiness.UpdateEtapa(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool Success, string Message)>> DeleteEtapa(int IdEtapa)
        {
            var response = await _pcsBusiness.DeleteEtapa(IdEtapa);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Etapas

        #region Empleados
        public async Task<Response<(bool Success, string Message)>> AddEmpleado(JsonObject registro)
        {
            var response = await _pcsBusiness.AddEmpleado(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<List<PCS_Empleado_Detalle>>> GetEmpleados(int IdProyecto)
        {
            var response = await _pcsBusiness.GetEmpleados(IdProyecto);
            return new Response<List<PCS_Empleado_Detalle>> { Data = _map.Map<List<PCS_Empleado_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<(bool Success, string Message)>> UpdateEmpleado(JsonObject registro)
        {
            var response = await _pcsBusiness.UpdateEmpleado(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool Success, string Message)>> DeleteEmpleado(int IdEmpleado)
        {
            var response = await _pcsBusiness.DeleteEmpleado(IdEmpleado);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Empleados
    }
}

