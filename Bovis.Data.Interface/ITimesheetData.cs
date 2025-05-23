﻿using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Data.Interface
{
    public interface ITimesheetData : IDisposable
    {
        Task<Detalle_Dias_Timesheet> GetDiasHabiles(int mes, int anio, bool sabados);
        Task<List<Detalle_Dias_Timesheet>> GetDiasTimesheet(int mes);
        Task<(bool Success, string Message)> UpdateDiasFeriadosTimeSheet(JsonObject registro);
        Task<(bool Success, string Message)> AddRegistro(JsonObject registro);
        Task<List<TimeSheet_Detalle>> GetTimeSheets(bool? activo);
        Task<List<TimeSheet_Detalle>> GetTimeSheetsByFiltro(string email_loged_user, string idEmpleado, int idProyecto, int idUnidadNegocio, int idEmpresa, int mes, int anio);
        Task<List<TimeSheet_Detalle>> GetTimeSheetsByFecha(int mes, int anio);
        Task<TimeSheet_Detalle> GetTimeSheet(int idTimeSheet);
        Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro);
        Task<(bool Success, string Message)> DeleteTimeSheet(int idTimeSheet);
        Task<List<Empleado_Detalle>> GetEmpleadosByResponsable(string EmailResponsable);
        Task<List<TB_Proyecto>> GetProyectosByResponsable(string EmailResponsable);
        Task<List<TB_Proyecto>> GetNotProyectosByEmpleado(string IdEmpleado);
        //atc 11-11-2024
        Task<List<TB_Proyecto>> GetNotProyectosByEmpleadoNoClose(string IdEmpleado);
        Task<(bool Success, string Message)> AddProyectoEmpleado(JsonObject registro);
        Task<(bool Success, string Message)> DeleteProyectoEmpleado(JsonObject registro);
        Task<(bool Success, string Message)> UpdateDiasDedicacion(JsonObject registro);


        #region Usuarios
        Task<(bool Success, string Message)> AddUsuarioTimesheet(JsonObject registro);
        Task<List<UsuarioTimesheet_Detalle>> GetUsuariosTimeSheet();
        Task<(bool Success, string Message)> UpdateUsuarioTimesheet(JsonObject registro);
        Task<(bool Success, string Message)> DeleteUsuarioTimesheet(JsonObject registro);
        #endregion Usuarios
    }
}