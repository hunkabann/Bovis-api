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
        Task<Response<List<Documentos_Auditoria_Detalle>>> GetAuditorias(string TipoAuditoria);
        Task<Response<List<Documentos_Auditoria_Proyecto_Detalle>>> GetAuditoriasByProyecto(int IdProyecto, string TipoAuditoria);
        Task<Response<(bool Success, string Message)>> AddAuditorias(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdateAuditoriaProyecto(JsonObject registro);
        Task<Response<(bool Success, string Message)>> AddAuditoriaDocumento(JsonObject registro);
        Task<Response<List<TB_Auditoria_Cumplimiento_Documento>>> GetDocumentosAuditoria(int IdAuditoria, int offset, int limit);
        Task<Response<TB_Auditoria_Cumplimiento_Documento>> GetDocumentoAuditoria(int IdDocumento);
        Task<Response<(bool Success, string Message)>> AddAuditoriaDocumentoValidacion(JsonObject registro);
        #endregion Auditoria de Calidad (Cumplimiento)
    }
}

