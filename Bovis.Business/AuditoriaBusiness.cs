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


        public Task<List<TB_Proyecto>> GetProyectos(string email_loged_user, string TipoAuditoria) => _auditoriaData.GetProyectos(email_loged_user, TipoAuditoria);
        public Task<List<Documentos_Auditoria_Detalle>> GetAuditorias(string TipoAuditoria) => _auditoriaData.GetAuditorias(TipoAuditoria);
        
        public Task<List<Documentos_Auditoria_Proyecto_Detalle>> GetAuditoriasByProyecto(int IdProyecto, string TipoAuditoria, string FechaInicio, string FechaFin) => _auditoriaData.GetAuditoriasByProyecto(IdProyecto, TipoAuditoria, FechaInicio, FechaFin);
        public Task<List<Periodos_Auditoria_Detalle>> GetPeriodosAuditoriaByProyecto(int IdProyecto, string TipoAuditoria) => _auditoriaData.GetPeriodosAuditoriaByProyecto(IdProyecto, TipoAuditoria);
        
        public Task<List<TB_Cat_AuditoriaTipoComentario>> GetTipoComentarios() => _auditoriaData.GetTipoComentarios();

        public Task<List<Comentario_Detalle>> GetComentarios(int numProyecto) => _auditoriaData.GetComentarios(numProyecto);

        public async Task<(bool Success, string Message)> AddAuditorias(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _auditoriaData.AddAuditorias(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        
        public async Task<(bool Success, string Message)> AddComentarios(JsonObject registro, string usuario_logueado)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _auditoriaData.AddComentarios(registro, usuario_logueado);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateAuditoriaProyecto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _auditoriaData.UpdateAuditoriaProyecto((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public async Task<(bool Success, string Message)> AddAuditoriaDocumento(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _auditoriaData.AddAuditoriaDocumento(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        public Task<List<TB_AuditoriaDocumento>> GetDocumentosAuditoria(int IdAuditoria, int offset, int limit) => _auditoriaData.GetDocumentosAuditoria(IdAuditoria, offset, limit);

        public Task<TB_AuditoriaDocumento> GetDocumentoAuditoria(int IdDocumento) => _auditoriaData.GetDocumentoAuditoria(IdDocumento);

        public async Task<(bool Success, string Message)> AddAuditoriaDocumentoValidacion(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _auditoriaData.AddAuditoriaDocumentoValidacion(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
    }
}
