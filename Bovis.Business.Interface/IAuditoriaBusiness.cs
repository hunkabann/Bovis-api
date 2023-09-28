using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface IAuditoriaBusiness : IDisposable
    {
        #region Auditoria Legal
        Task<List<TB_Cat_Auditoria_Contractual>> GetAuditoriasContractual();
        Task<(bool Success, string Message)> AddAuditoriasContractual(JsonObject registro);
        #endregion Auditoria Legal



        #region Auditoria de Calidad (Cumplimiento)
        Task<List<Documentos_Auditoria_Detalle>> GetAuditorias(string TipoAuditoria);
        Task<List<Documentos_Auditoria_Proyecto_Detalle>> GetAuditoriasByProyecto(int IdProyecto, string TipoAuditoria);
        Task<(bool Success, string Message)> AddAuditorias(JsonObject registro);
        Task<(bool Success, string Message)> UpdateAuditoriaProyecto(JsonObject registro);
        Task<(bool Success, string Message)> AddAuditoriaDocumento(JsonObject registro);
        Task<List<TB_Auditoria_Cumplimiento_Documento>> GetDocumentosAuditoria(int IdAuditoria, int offset, int limit);
        Task<TB_Auditoria_Cumplimiento_Documento> GetDocumentoAuditoria(int IdDocumento);
        Task<(bool Success, string Message)> AddAuditoriaDocumentoValidacion(JsonObject registro);
        #endregion Auditoria de Calidad (Cumplimiento)
    }

}
