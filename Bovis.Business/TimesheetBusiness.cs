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

        public async Task<(bool Success, string Message)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _timesheetData.AddRegistro(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        public Task<List<TimeSheet_Detalle>> GetTimeSheets(bool? Activo) => _timesheetData.GetTimeSheets(Activo);

        public Task<List<TimeSheet_Detalle>> GetTimeSheetsByEmpleado(int idEmpleado) => _timesheetData.GetTimeSheetsByEmpleado(idEmpleado);

        public Task<List<TimeSheet_Detalle>> GetTimeSheetsByFecha(int mes, int anio) => _timesheetData.GetTimeSheetsByFecha(mes, anio);

        public Task<TimeSheet_Detalle> GetTimeSheet(int idTimeSheet) => _timesheetData.GetTimeSheet(idTimeSheet);

        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _timesheetData.UpdateRegistro(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        public Task<(bool Success, string Message)> DeleteTimeSheet(int idTimeSheet) => _timesheetData.DeleteTimeSheet(idTimeSheet);
    }
}
