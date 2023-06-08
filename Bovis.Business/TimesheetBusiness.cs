using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;

namespace Bovis.Business
{
    public class TimesheetBusiness : ITimesheetBusiness
    {
        #region base
        private readonly ITimesheetData _timesheetData;
        public TimesheetBusiness(ITimesheetData _timesheetData)
        {
            this._timesheetData = _timesheetData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public Task<Dias_Timesheet_Detalle> GetDiasHabiles(int mes, int anio, bool sabados) => _timesheetData.GetDiasHabiles(mes, anio, sabados);

        public async Task<(bool Success, string Message)> AgregarRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _timesheetData.AgregarRegistro(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            return resp;
        }
    }
}
