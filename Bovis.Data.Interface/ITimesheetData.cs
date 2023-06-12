using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface ITimesheetData : IDisposable
    {
        Task<Dias_Timesheet_Detalle> GetDiasHabiles(int mes, int anio, bool sabados);
        Task<(bool existe, string mensaje)> AddRegistro(JsonObject registro);
        Task<List<TimeSheet_Detalle>> GetTimeSheets(bool? activo);
        Task<TimeSheet_Detalle> GetTimeSheet(int idTimeSheet);
        Task<(bool existe, string mensaje)> UpdateRegistro(JsonObject registro);
        Task<(bool existe, string mensaje)> DeleteTimeSheet(int idTimeSheet);
    }
}