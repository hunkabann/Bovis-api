using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;
using Bovis.Common;
using System.Net.NetworkInformation;

namespace Bovis.Business
{
    public class ContratoBusiness : IContratoBusiness
    {
        #region base
        private readonly IContratoData _contratoData;
        private readonly ITransactionData _transactionData;
        public ContratoBusiness(IContratoData _contratoData, ITransactionData _transactionData)
        {
            this._contratoData = _contratoData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Templates
        public Task<List<TB_Contrato_Template>> GetTemplates(string Estatus) => _contratoData.GetTemplates(Estatus);
        public Task<TB_Contrato_Template> GetTemplate(int IdTemplate) => _contratoData.GetTemplate(IdTemplate);

        public async Task<(bool Success, string Message)> AddTemplate(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _contratoData.AddTemplate(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateTemplate(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _contratoData.UpdateTemplate((JsonObject)registro["Registro"]);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateTemplateEstatus(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _contratoData.UpdateTemplateEstatus((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro del Empleado"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
        #endregion Templates

        #region Contratos Empleado
        public Task<List<TB_Contrato_Empleado>> GetContratosEmpleado(int IdEmpleado) => _contratoData.GetContratosEmpleado(IdEmpleado);
        public Task<TB_Contrato_Empleado> GetContratoEmpleado(int IdContratoEmpleado) => _contratoData.GetContratoEmpleado(IdContratoEmpleado);

        public async Task<(bool Success, string Message)> AddContratoEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _contratoData.AddContratoEmpleado(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateContratoEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _contratoData.UpdateContratoEmpleado((JsonObject)registro["Registro"]);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
        #endregion Contratos Empleado
    }
}
