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
        #endregion base

        #region Empresas
        public async Task<Response<List<EmpresaRegistro>>> GetEmpresas(bool? Activo)
        {
            var response = await _cieBusiness.GetEmpresas(Activo);
            return new Response<List<EmpresaRegistro>> { Data = _map.Map<List<EmpresaRegistro>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }
        #endregion Empresas

        #region Cuenta Data
        public async Task<Response<List<CuentaContable_Detalle>>> GetCuentaData(JsonObject cuentas)
        {
            var response = await _cieBusiness.GetCuentaData(cuentas);
            return new Response<List<CuentaContable_Detalle>> { Data = _map.Map<List<CuentaContable_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        #endregion Cuenta Data

        #region Proyecto
        public async Task<Response<List<ProyectoData_Detalle>>> GetProyectoData(JsonObject proyectos)
        {
            var response = await _cieBusiness.GetProyectoData(proyectos);
            return new Response<List<ProyectoData_Detalle>> { Data = _map.Map<List<ProyectoData_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        #endregion Proyecto

        #region Registros
        public async Task<Response<Cie_Detalle>> GetRegistro(int? idRegistro)
        {
            var response = await _cieBusiness.GetRegistro(idRegistro);
            return new Response<Cie_Detalle> { Data = _map.Map<Cie_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<Cie_Registros>> GetRegistros(bool? activo, string nombre_cuenta, string fecha, string concepto, string empresa, int num_proyecto, string responsable, int offset, int limit)
        {
            var response = await _cieBusiness.GetRegistros(activo, nombre_cuenta, fecha, concepto, empresa, num_proyecto, responsable, offset, limit);
            return new Response<Cie_Registros> { Data = _map.Map<Cie_Registros>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }

        public async Task<Response<(bool Success, string Message)>> AddRegistros(JsonObject registros)
        {
            var response = await _cieBusiness.AddRegistros(registros);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool Success, string Message)>> UpdateRegistro(JsonObject registro)
        {
            var response = await _cieBusiness.UpdateRegistro(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool Success, string Message)>> DeleteRegistro(int idRegistro)
        {
            var response = await _cieBusiness.DeleteRegistro(idRegistro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Registros
    }
}

