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
    public class EmpleadoQueryService : IEmpleadoQueryService
    {
        #region base
        private readonly IEmpleadoBusiness _empleadoBusiness;

        private readonly IMapper _map;

        public EmpleadoQueryService(IMapper _map, IEmpleadoBusiness _empleadoBusiness)
        {
            this._map = _map;
            this._empleadoBusiness = _empleadoBusiness;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Empleados
        public async Task<Response<List<Empleado_Detalle>>> GetEmpleados(bool? Activo)
        {
            var response = await _empleadoBusiness.GetEmpleados(Activo);
            return new Response<List<Empleado_Detalle>> { Data = _map.Map<List<Empleado_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }

        public async Task<Response<List<Empleado_Detalle>>> GetEmpleadosAll(bool? Activo)
        {
            var response = await _empleadoBusiness.GetEmpleadosAll(Activo);
            return new Response<List<Empleado_Detalle>> { Data = _map.Map<List<Empleado_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }

        //ATC 03-12-2024
        public async Task<Response<List<Empleado_Detalle>>> GetEmpleadosAllFiltro(bool? Activo, bool? idEstado, int? idPuesto, int? idProyecto, int? idEmpresa, int? idUnidadNegocio)
        {
            var response = await _empleadoBusiness.GetEmpleadosAllFiltro(Activo, idEstado, idPuesto, idProyecto, idEmpresa, idUnidadNegocio);
            return new Response<List<Empleado_Detalle>> { Data = _map.Map<List<Empleado_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }

        public async Task<Response<Empleado_Detalle>> GetEmpleado(string idEmpleado)
        {
            var response = await _empleadoBusiness.GetEmpleado(idEmpleado);
            return new Response<Empleado_Detalle> { Data = _map.Map<Empleado_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<List<Empleado_Detalle>>> GetEmpleadosByIDPuesto(string idPuesto)
        {
            var response = await _empleadoBusiness.GetEmpleadosByIDPuesto(idPuesto);
            return new Response<List<Empleado_Detalle>> { Data = _map.Map<List<Empleado_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }

        public async Task<Response<Empleado_BasicData>> GetEmpleadoByEmail(string email)
        {
            var response = await _empleadoBusiness.GetEmpleadoByEmail(email);
            return new Response<Empleado_BasicData> { Data = _map.Map<Empleado_BasicData>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        
        public async Task<Response<List<Empleado_BasicData>>> GetEmpleadoDetalle()
        {
            var response = await _empleadoBusiness.GetEmpleadoDetalle();
            return new Response<List<Empleado_BasicData>> { Data = _map.Map<List<Empleado_BasicData>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<(bool Success, string Message)>> AddRegistro(JsonObject registro)
        {
            var response = await _empleadoBusiness.AddRegistro(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool Success, string Message)>> UpdateRegistro(JsonObject registro)
        {
            var response = await _empleadoBusiness.UpdateRegistro(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool Success, string Message)>> UpdateEstatus(JsonObject registro)
        {
            var response = await _empleadoBusiness.UpdateEstatus(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Empleados

        #region Proyectos
        public async Task<Response<List<Proyecto_Detalle>>> GetProyectos(string idEmpleado)
        {
            var response = await _empleadoBusiness.GetProyectos(idEmpleado);
            return new Response<List<Proyecto_Detalle>> { Data = _map.Map<List<Proyecto_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }
        #endregion Proyectos

        #region Ciudades
        public async Task<Response<List<TB_Ciudad>>> GetCiudades(bool? Activo, int? IdEstado)
        {
            var response = await _empleadoBusiness.GetCiudades(Activo, IdEstado);
            return new Response<List<TB_Ciudad>> { Data = _map.Map<List<TB_Ciudad>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }
        #endregion Ciudades
    }
}

