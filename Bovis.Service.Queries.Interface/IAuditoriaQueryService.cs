using Bovis.Common.Model.NoTable;
using Bovis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Nodes;

namespace Bovis.Service.Queries.Interface
{
    public interface IAuditoriaQueryService
    {
        #region Auditoria Legal
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        Task<Response<List<Documentos_Auditoria_Cumplimiento_Detalle>>> GetDocumentosAuditoriaCumplimiento();
        Task<Response<(bool existe, string mensaje)>> AddDocumentosAuditoriaCumplimiento(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> UpdateAuditoriaCumplimientoProyecto(JsonObject registro);
        #endregion Auditoria de Calidad (Cumplimiento)
    }
}

