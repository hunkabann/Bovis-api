using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface ITimesheetQueryService
    {
        Task<Response<Detalle_Dias_Timesheet>> GetDiasHabiles(int mes, int anio, bool sabados);
        Task<Response<List<Detalle_Dias_Timesheet>>> GetDiasTimesheet(int mes);
        Task<Response<(bool Success, string Message)>> UpdateDiasFeriadosTimeSheet(JsonObject registro);
        Task<Response<(bool Success, string Message)>> AddRegistro(JsonObject registro);
        Task<Response<List<TimeSheet_Detalle>>> GetTimeSheets(bool? Activo);
        Task<Response<List<TimeSheet_Detalle>>> GetTimeSheetsByFiltro(string email, string idEmpleado, int idProyecto, int idUnidadNegocio, int idEmpresa, int mes);
        Task<Response<List<TimeSheet_Detalle>>> GetTimeSheetsByFecha(int mes, int anio);
        Task<Response<TimeSheet_Detalle>> GetTimeSheet(int idTimeSheet);
        Task<Response<(bool Success, string Message)>> UpdateRegistro(JsonObject registro);
        Task<Response<(bool Success, string Message)>> DeleteTimeSheet(int idTimeSheet);
        Task<Response<List<Empleado_Detalle>>> GetEmpleadosByResponsable(string EmailResponsable);
        Task<Response<List<TB_Proyecto>>> GetProyectosByResponsable(string EmailResponsable);
        Task<Response<List<TB_Proyecto>>> GetNotProyectosByEmpleado(string IdEmpleado);
        Task<Response<(bool Success, string Message)>> AddProyectoEmpleado(JsonObject registro);
        Task<Response<(bool Success, string Message)>> DeleteProyectoEmpleado(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdateDiasDedicacion(JsonObject registro);
    }
}

