using Bovis.Common.Model.NoTable;
using Bovis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using Bovis.Common.Model.Tables;

namespace Bovis.Service.Queries.Interface
{
    public interface IAuditoriaQueryService
    {
        #region Auditoria Legal
        Task<Response<List<TB_Cat_Auditoria_Contractual>>> GetAuditoriasContractual();
        Task<Response<(bool Success, string Message)>> AddAuditoriasContractual(JsonObject registro);
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        Task<Response<List<Documentos_Auditoria_Cumplimiento_Detalle>>> GetAuditoriasCumplimiento();
        Task<Response<List<Documentos_Auditoria_Cumplimiento_Proyecto_Detalle>>> GetAuditoriasCumplimientoByProyecto(int IdProyecto);
        Task<Response<(bool Success, string Message)>> AddAuditoriasCumplimiento(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdateAuditoriaCumplimientoProyecto(JsonObject registro);
        Task<Response<(bool Success, string Message)>> AddAuditoriaCumplimientoDocumento(JsonObject registro);
        Task<Response<List<TB_Auditoria_Cumplimiento_Documento>>> GetDocumentosAuditoriaCumplimiento(int IdAuditoriaCumplimiento, int offset, int limit);
        Task<Response<TB_Auditoria_Cumplimiento_Documento>> GetDocumentoAuditoriaCumplimiento(int IdDocumento);
        Task<Response<(bool Success, string Message)>> AddAuditoriaCumplimientoDocumentoValidacion(JsonObject registro);
        #endregion Auditoria de Calidad (Cumplimiento)
    }
}

