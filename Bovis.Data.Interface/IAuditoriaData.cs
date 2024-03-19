using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IAuditoriaData : IDisposable
    {
        Task<List<TB_Proyecto>> GetProyectos(string email_loged_user, string TipoAuditoria);
        Task<List<Documentos_Auditoria_Detalle>> GetAuditorias(string TipoAuditoria);
        Task<List<Documentos_Auditoria_Proyecto_Detalle>> GetAuditoriasByProyecto(int IdProyecto, string TipoAuditoria, string FechaInicio, string FechaFin);
        Task<List<Periodos_Auditoria_Detalle>> GetPeriodosAuditoriaByProyecto(int IdProyecto, string TipoAuditoria);
        Task<List<TB_Cat_AuditoriaTipoComentario>> GetTipoComentarios();
        Task<List<Comentario_Detalle>> GetComentarios(int numProyecto);
        Task<(bool Success, string Message)> AddAuditorias(JsonObject registro);
        Task<(bool Success, string Message)> AddComentarios(JsonObject registro, string usuario_logueado);
        Task<(bool Success, string Message)> UpdateAuditoriaProyecto(JsonObject registro);
        Task<(bool Success, string Message)> AddAuditoriaDocumento(JsonObject registro);
        Task<List<TB_AuditoriaDocumento>> GetDocumentosAuditoria(int IdAuditoria, int offset, int limit);
        Task<TB_AuditoriaDocumento> GetDocumentoAuditoria(int IdDocumento);
        Task<(bool Success, string Message)> AddAuditoriaDocumentoValidacion(JsonObject registro);
    }
}