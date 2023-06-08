using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface ITimesheetData : IDisposable
    {
        Task<Dias_Timesheet_Detalle> GetDiasHabiles(int mes, int anio, bool sabados);
        Task<(bool existe, string mensaje)> AgregarRegistro(JsonObject registro);
    }
}