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
        Task<List<Documentos_Auditoria_Cumplimiento_Detalle>> GetAuditoriasCumplimiento();
        Task<List<Documentos_Auditoria_Cumplimiento_Proyecto_Detalle>> GetAuditoriasCumplimientoByProyecto(int IdProyecto);
        Task<(bool Success, string Message)> AddAuditoriasCumplimiento(JsonObject registro);
        Task<(bool Success, string Message)> UpdateAuditoriaCumplimientoProyecto(JsonObject registro);
        Task<(bool Success, string Message)> AddAuditoriaCumplimientoDocumento(JsonObject registro);
        Task<List<TB_Auditoria_Cumplimiento_Documento>> GetDocumentosAuditoriaCumplimiento(int IdAuditoriaCumplimiento, int offset, int limit);
        Task<TB_Auditoria_Cumplimiento_Documento> GetDocumentoAuditoriaCumplimiento(int IdDocumento);
        Task<(bool Success, string Message)> AddAuditoriaCumplimientoDocumentoValidacion(JsonObject registro);
        #endregion Auditoria de Calidad (Cumplimiento)
    }

}
