using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;

namespace Bovis.Data.Interface
{
    public interface IAuditoriaData : IDisposable
    {
        Task<List<Documentos_Auditoria_Cumplimiento_Detalle>> GetDocumentosAuditoriaCumplimiento();
    }
}