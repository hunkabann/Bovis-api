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
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        Task<List<Documentos_Auditoria_Cumplimiento_Detalle>> GetAuditoriasCumplimiento();
        Task<(bool Success, string Message)> AddDocumentosAuditoriaCumplimiento(JsonObject registro);
        Task<(bool Success, string Message)> UpdateAuditoriaCumplimientoProyecto(JsonObject registro);
        #endregion Auditoria de Calidad (Cumplimiento)
    }

}
