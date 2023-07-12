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
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        Task<Response<List<Documentos_Auditoria_Cumplimiento_Detalle>>> GetAuditoriasCumplimiento();
        Task<Response<(bool existe, string mensaje)>> AddDocumentosAuditoriaCumplimiento(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> UpdateAuditoriaCumplimientoProyecto(JsonObject registro);
        #endregion Auditoria de Calidad (Cumplimiento)
    }
}

