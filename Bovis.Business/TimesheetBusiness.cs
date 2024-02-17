using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;
using Microsoft.Win32;

namespace Bovis.Business
{
    public class TimesheetBusiness : ITimesheetBusiness
    {
        #region base
        private readonly ITimesheetData _timesheetData;
        private readonly ITransactionData _transactionData;
        public TimesheetBusiness(ITimesheetData _timesheetData, ITransactionData _transactionData)
        {
            this._timesheetData = _timesheetData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public Task<Detalle_Dias_Timesheet> GetDiasHabiles(int mes, int anio, bool sabados) => _timesheetData.GetDiasHabiles(mes, anio, sabados);

        public Task<List<Detalle_Dias_Timesheet>> GetDiasTimesheet(int mes) => _timesheetData.GetDiasTimesheet(mes);

        public async Task<(bool Success, string Message)> UpdateDiasFeriadosTimeSheet(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _timesheetData.UpdateDiasFeriadosTimeSheet((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public async Task<(bool Success, string Message)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _timesheetData.AddRegistro(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }        

        public Task<List<TimeSheet_Detalle>> GetTimeSheets(bool? Activo) => _timesheetData.GetTimeSheets(Activo);

        public Task<List<TimeSheet_Detalle>> GetTimeSheetsByFiltro(string email, string idEmpleado, int idProyecto, int idUnidadNegocio, int idEmpresa, int mes, int anio) => _timesheetData.GetTimeSheetsByFiltro(email, idEmpleado, idProyecto, idUnidadNegocio, idEmpresa, mes, anio);

        public Task<List<TimeSheet_Detalle>> GetTimeSheetsByFecha(int mes, int anio) => _timesheetData.GetTimeSheetsByFecha(mes, anio);

        public Task<TimeSheet_Detalle> GetTimeSheet(int idTimeSheet) => _timesheetData.GetTimeSheet(idTimeSheet);

        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _timesheetData.UpdateRegistro((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public Task<(bool Success, string Message)> DeleteTimeSheet(int idTimeSheet) => _timesheetData.DeleteTimeSheet(idTimeSheet);

        public Task<List<Empleado_Detalle>> GetEmpleadosByResponsable(string EmailResponsable) => _timesheetData.GetEmpleadosByResponsable(EmailResponsable);
        public Task<List<TB_Proyecto>> GetProyectosByResponsable(string EmailResponsable) => _timesheetData.GetProyectosByResponsable(EmailResponsable);
        public Task<List<TB_Proyecto>> GetNotProyectosByEmpleado(string IdEmpleado) => _timesheetData.GetNotProyectosByEmpleado(IdEmpleado);
        public async Task<(bool Success, string Message)> AddProyectoEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _timesheetData.AddProyectoEmpleado(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        public Task<(bool Success, string Message)> DeleteProyectoEmpleado(JsonObject registro) => _timesheetData.DeleteProyectoEmpleado(registro);
        public async Task<(bool Success, string Message)> UpdateDiasDedicacion(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _timesheetData.UpdateDiasDedicacion((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
    }
}