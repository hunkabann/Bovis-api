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



        #region Clientes
        public async Task<Response<List<InfoCliente>>> GetClientes()
        {
            var response = await _pcsBusiness.GetClientes();
            return new Response<List<InfoCliente>> { Data = _map.Map<List<InfoCliente>>(response), Success = true };
        }
        #endregion Clientes



        #region Empresas
        public async Task<Response<List<InfoEmpresa>>> GetEmpresas()
        {
            var response = await _pcsBusiness.GetEmpresas();
            return new Response<List<InfoEmpresa>> { Data = _map.Map<List<InfoEmpresa>>(response), Success = true };
        }
        #endregion Empresas




        #region Proyectos
        public async Task<Response<List<Proyecto>>> GetProyectos(bool? OrdenAlfabetico)
        {
            var response = await _pcsBusiness.GetProyectos(OrdenAlfabetico);
            return new Response<List<Proyecto>> { Data = _map.Map<List<Proyecto>>(response), Success = true };
        }
        //atc 09-11-2024
        public async Task<Response<List<Proyecto>>> GetProyectosNoClose(bool? OrdenAlfabetico)
        {
            var response = await _pcsBusiness.GetProyectosNoClose(OrdenAlfabetico);
            return new Response<List<Proyecto>> { Data = _map.Map<List<Proyecto>>(response), Success = true };
        }
        public async Task<Response<Proyecto>> GetProyecto(int numProyecto)
        {
            var response = await _pcsBusiness.GetProyecto(numProyecto);
            return new Response<Proyecto> { Data = _map.Map<Proyecto>(response), Success = true };
        }
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
        public async Task<Response<List<Tipo_Proyecto>>> GetTipoProyectos()
        {
            var response = await _pcsBusiness.GetTipoProyectos();
            return new Response<List<Tipo_Proyecto>> { Data = _map.Map<List<Tipo_Proyecto>>(response), Success = true };
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
        public async Task<Response<(bool Success, string Message)>> UpdateProyectoFechaAuditoria(JsonObject registro)
        {
            var response = await _pcsBusiness.UpdateProyectoFechaAuditoria(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Proyectos




        #region Etapas
        public async Task<Response<PCS_Etapa_Detalle>> AddEtapa(JsonObject registro)
        {
            var response = await _pcsBusiness.AddEtapa(registro);
            return new Response<PCS_Etapa_Detalle> { Data = _map.Map<PCS_Etapa_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<PCS_GanttData>> GetPEtapas(int IdProyecto)
        {
            var response = await _pcsBusiness.GetPEtapas(IdProyecto);
            return new Response<PCS_GanttData> { Data = _map.Map<PCS_GanttData>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<PCS_Proyecto_Detalle>> GetEtapas(int IdProyecto)
        {
            var response = await _pcsBusiness.GetEtapas(IdProyecto);
            return new Response<PCS_Proyecto_Detalle> { Data = _map.Map<PCS_Proyecto_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
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
        public async Task<Response<List<PCS_Empleado_Detalle>>> GetEmpleados(int IdFase)
        {
            var response = await _pcsBusiness.GetEmpleados(IdFase);
            return new Response<List<PCS_Empleado_Detalle>> { Data = _map.Map<List<PCS_Empleado_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<(bool Success, string Message)>> UpdateEmpleado(JsonObject registro)
        {
            var response = await _pcsBusiness.UpdateEmpleado(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool Success, string Message)>> DeleteEmpleado(int IdFase, string NumEmpleado)
        {
            var response = await _pcsBusiness.DeleteEmpleado(IdFase, NumEmpleado);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Empleados




        #region Gastos / Ingresos
        public async Task<Response<List<Seccion_Detalle>>> GetGastosIngresosSecciones(int IdProyecto, string Tipo)
        {
            var response = await _pcsBusiness.GetGastosIngresosSecciones(IdProyecto, Tipo);
            return new Response<List<Seccion_Detalle>> { Data = _map.Map<List<Seccion_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<GastosIngresos_Detalle>> GetGastosIngresos(int IdProyecto, string Tipo, string Seccion)
        {
            var response = await _pcsBusiness.GetGastosIngresos(IdProyecto, Tipo, Seccion);
            return new Response<GastosIngresos_Detalle> { Data = _map.Map<GastosIngresos_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<GastosIngresos_Detalle>> GetTotalesIngresos(int IdProyecto)
        {
            var response = await _pcsBusiness.GetTotalesIngresos(IdProyecto);
            return new Response<GastosIngresos_Detalle> { Data = _map.Map<GastosIngresos_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<(bool Success, string Message)>> UpdateGastosIngresos(JsonObject registro)
        {
            var response = await _pcsBusiness.UpdateGastosIngresos(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<GastosIngresos_Detalle>> GetTotalFacturacion(int IdProyecto)
        {
            var response = await _pcsBusiness.GetTotalFacturacion(IdProyecto);
            return new Response<GastosIngresos_Detalle> { Data = _map.Map<GastosIngresos_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        #endregion Gastos / Ingresos




        #region Control
        public async Task<Response<Control_Detalle>> GetControl(int IdProyecto)
        {
            var response = await _pcsBusiness.GetControl(IdProyecto);
            return new Response<Control_Detalle> { Data = _map.Map<Control_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<Control_Data>> GetSeccionControl(int IdProyecto, string Seccion)
        {
            var response = await _pcsBusiness.GetSeccionControl(IdProyecto, Seccion);
            return new Response<Control_Data> { Data = _map.Map<Control_Data>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        #endregion Control
    }
}

