using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface ITimesheetData : IDisposable
    {
        Task<Detalle_Dias_Timesheet> GetDiasHabiles(int mes, int anio, bool sabados);
        Task<(bool Success, string Message)> AddRegistro(JsonObject registro);
        Task<List<TimeSheet_Detalle>> GetTimeSheets(bool? activo);
        Task<List<TimeSheet_Detalle>> GetTimeSheetsByFiltro(int idEmpleado, int idProyecto, int idUnidadNegocio, int mes);
        Task<List<TimeSheet_Detalle>> GetTimeSheetsByFecha(int mes, int anio);
        Task<TimeSheet_Detalle> GetTimeSheet(int idTimeSheet);
        Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro);
        Task<(bool Success, string Message)> DeleteTimeSheet(int idTimeSheet);
        Task<List<Empleado_Detalle>> GetEmpleadosByResponsable(string EmailResponsable);
        Task<List<TB_Proyecto>> GetProyectosByResponsable(string EmailResponsable);
        Task<List<TB_Proyecto>> GetNotProyectosByEmpleado(int IdEmpleado);
        Task<(bool Success, string Message)> AddProyectoEmpleado(JsonObject registro);
    }
}