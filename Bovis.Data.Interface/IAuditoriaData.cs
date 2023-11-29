using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IAuditoriaData : IDisposable
    {
        Task<List<Documentos_Auditoria_Detalle>> GetAuditorias(string TipoAuditoria);
        Task<List<Documentos_Auditoria_Proyecto_Detalle>> GetAuditoriasByProyecto(int IdProyecto, string TipoAuditoria);
        Task<(bool Success, string Message)> AddAuditorias(JsonObject registro);
        Task<(bool Success, string Message)> UpdateAuditoriaProyecto(JsonObject registro);
        Task<(bool Success, string Message)> AddAuditoriaDocumento(JsonObject registro);
        Task<List<TB_AuditoriaDocumento>> GetDocumentosAuditoria(int IdAuditoria, int offset, int limit);
        Task<TB_AuditoriaDocumento> GetDocumentoAuditoria(int IdDocumento);
        Task<(bool Success, string Message)> AddAuditoriaDocumentoValidacion(JsonObject registro);
    }
}