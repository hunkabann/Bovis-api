using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;

namespace Bovis.Business
{
    public class AuditoriaBusiness : IAuditoriaBusiness
    {
        #region base
        private readonly IAuditoriaData _auditoriaData;
        private readonly ITransactionData _transactionData;
        public AuditoriaBusiness(IAuditoriaData _auditoriaData, ITransactionData _transactionData)
        {
            this._auditoriaData = _auditoriaData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Auditoria Legal
        public Task<List<TB_Cat_Auditoria_Contractual>> GetAuditoriasContractual() => _auditoriaData.GetAuditoriasContractual();

        public async Task<(bool Success, string Message)> AddAuditoriasContractual(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _auditoriaData.AddAuditoriasContractual(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        public Task<List<Documentos_Auditoria_Cumplimiento_Detalle>> GetAuditoriasCumplimiento() => _auditoriaData.GetAuditoriasCumplimiento();
        
        public Task<List<Documentos_Auditoria_Cumplimiento_Proyecto_Detalle>> GetAuditoriasCumplimientoByProyecto(int IdProyecto) => _auditoriaData.GetAuditoriasCumplimientoByProyecto(IdProyecto);

        public async Task<(bool Success, string Message)> AddAuditoriasCumplimiento(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _auditoriaData.AddAuditoriasCumplimiento(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateAuditoriaCumplimientoProyecto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _auditoriaData.UpdateAuditoriaCumplimientoProyecto((JsonObject)registro["Registro"]);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
        #endregion Auditoria de Calidad (Cumplimiento)
    }
}
