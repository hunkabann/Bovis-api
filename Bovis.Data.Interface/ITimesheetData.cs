using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface ITimesheetData : IDisposable
    {
        Task<Detalle_Dias_Timesheet> GetDiasHabiles(int mes, int anio, bool sabados);
        Task<(bool existe, string mensaje)> AddRegistro(JsonObject registro);
        Task<List<TimeSheet_Detalle>> GetTimeSheets(bool? activo);
        Task<List<TimeSheet_Detalle>> GetTimeSheetsByFiltro(int idEmpleado, int idProyecto, int mes);
        Task<List<TimeSheet_Detalle>> GetTimeSheetsByFecha(int mes, int anio);
        Task<TimeSheet_Detalle> GetTimeSheet(int idTimeSheet);
        Task<(bool existe, string mensaje)> UpdateRegistro(JsonObject registro);
        Task<(bool existe, string mensaje)> DeleteTimeSheet(int idTimeSheet);
        Task<List<Empleado_Detalle>> GetEmpleadosByResponsable(string EmailResponsable);
    }
}