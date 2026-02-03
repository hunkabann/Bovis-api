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
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class TimesheetQueryService : ITimesheetQueryService
    {
        #region base
        private readonly ITimesheetBusiness _timesheetBusiness;
        private readonly IMapper _map;

        public TimesheetQueryService(IMapper _map, ITimesheetBusiness _timesheetBusiness)
        {
            this._map = _map;
            this._timesheetBusiness = _timesheetBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<Response<Detalle_Dias_Timesheet>> GetDiasHabiles(int mes, int anio, bool sabados)
        {
            var response = await _timesheetBusiness.GetDiasHabiles(mes, anio, sabados);
            return new Response<Detalle_Dias_Timesheet> { Data = _map.Map<Detalle_Dias_Timesheet>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<List<Detalle_Dias_Timesheet>>> GetDiasTimesheet(int mes)
        {
            var response = await _timesheetBusiness.GetDiasTimesheet(mes);
            return new Response<List<Detalle_Dias_Timesheet>> { Data = _map.Map<List<Detalle_Dias_Timesheet>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<(bool Success, string Message)>> UpdateDiasFeriadosTimeSheet(JsonObject registro)
        {
            var response = await _timesheetBusiness.UpdateDiasFeriadosTimeSheet(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }


        public async Task<Response<(bool Success, string Message)>> AddRegistro(JsonObject registro)
        {
            var response = await _timesheetBusiness.AddRegistro(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<List<TimeSheet_Detalle>>> GetTimeSheets(bool? Activo)
        {
            var response = await _timesheetBusiness.GetTimeSheets(Activo);
            return new Response<List<TimeSheet_Detalle>> { Data = _map.Map<List<TimeSheet_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }
        public async Task<Response<List<TimeSheet_Detalle>>> GetTimeSheetsByFiltro(string email, string idEmpleado, int idProyecto, int idUnidadNegocio, int idEmpresa, int mes, int anio)
        {
            var response = await _timesheetBusiness.GetTimeSheetsByFiltro(email, idEmpleado, idProyecto, idUnidadNegocio, idEmpresa, mes, anio);
            return new Response<List<TimeSheet_Detalle>> { Data = _map.Map<List<TimeSheet_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }

        //Reporte EmpleadosXProyecto I
        public async Task<Response<TimeSheetEmpProyectoResponse>> GetTimeSheetsEmpleadosProyecto(int idProyecto)
        {
            var response = await _timesheetBusiness.GetTimeSheetsEmpleadosProyecto(idProyecto);
            return new Response<TimeSheetEmpProyectoResponse> { Data = response, Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }
        //Reporte EmpleadosXProyectF

        public async Task<Response<List<TimeSheet_Detalle>>> GetTimeSheetsByFecha(int mes, int anio)
        {
            var response = await _timesheetBusiness.GetTimeSheetsByFecha(mes, anio);
            return new Response<List<TimeSheet_Detalle>> { Data = _map.Map<List<TimeSheet_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }
        public async Task<Response<TimeSheet_Detalle>> GetTimeSheet(int idTimeSheet)
        {
            var response = await _timesheetBusiness.GetTimeSheet(idTimeSheet);
            return new Response<TimeSheet_Detalle> { Data = _map.Map<TimeSheet_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<(bool Success, string Message)>> UpdateRegistro(JsonObject registro)
        {
            var response = await _timesheetBusiness.UpdateRegistro(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool Success, string Message)>> DeleteTimeSheet(int idTimeSheet)
        {
            var response = await _timesheetBusiness.DeleteTimeSheet(idTimeSheet);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<List<Empleado_Detalle>>> GetEmpleadosByResponsable(string EmailResponsable)
        {
            var response = await _timesheetBusiness.GetEmpleadosByResponsable(EmailResponsable);
            return new Response<List<Empleado_Detalle>> { Data = _map.Map<List<Empleado_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }        
        public async Task<Response<List<TB_Proyecto>>> GetProyectosByResponsable(string EmailResponsable)
        {
            var response = await _timesheetBusiness.GetProyectosByResponsable(EmailResponsable);
            return new Response<List<TB_Proyecto>> { Data = _map.Map<List<TB_Proyecto>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }
        public async Task<Response<List<TB_Proyecto>>> GetNotProyectosByEmpleado(string IdEmpleado)
        {
            var response = await _timesheetBusiness.GetNotProyectosByEmpleado(IdEmpleado);
            return new Response<List<TB_Proyecto>> { Data = _map.Map<List<TB_Proyecto>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }
        //atc 11-11-2024
        public async Task<Response<List<TB_Proyecto>>> GetNotProyectosByEmpleadoNoClose(string IdEmpleado)
        {
            var response = await _timesheetBusiness.GetNotProyectosByEmpleadoNoClose(IdEmpleado);
            return new Response<List<TB_Proyecto>> { Data = _map.Map<List<TB_Proyecto>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }

        public async Task<Response<(bool Success, string Message)>> AddProyectoEmpleado(JsonObject registro)
        {
            var response = await _timesheetBusiness.AddProyectoEmpleado(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool Success, string Message)>> DeleteProyectoEmpleado(JsonObject registro)
        {
            var response = await _timesheetBusiness.DeleteProyectoEmpleado(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool Success, string Message)>> UpdateDiasDedicacion(JsonObject registro)
        {
            var response = await _timesheetBusiness.UpdateDiasDedicacion(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }


        #region Usuarios
        public async Task<Response<List<UsuarioTimesheet_Detalle>>> GetUsuariosTimeSheet(string fecha)
        {
            var response = await _timesheetBusiness.GetUsuariosTimeSheet(fecha);
            return new Response<List<UsuarioTimesheet_Detalle>> { Data = _map.Map<List<UsuarioTimesheet_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }

        public async Task<Response<(bool Success, string Message)>> AddUsuarioTimesheet(JsonObject registro)
        {
            var response = await _timesheetBusiness.AddUsuarioTimesheet(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool Success, string Message)>> UpdateUsuarioTimesheet(JsonObject registro)
        {
            var response = await _timesheetBusiness.UpdateUsuarioTimesheet(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool Success, string Message)>> DeleteUsuarioTimesheet(JsonObject registro)
        {
            var response = await _timesheetBusiness.DeleteUsuarioTimesheet(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Usuarios
    }
}

