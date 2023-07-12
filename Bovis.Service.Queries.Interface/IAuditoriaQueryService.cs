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
        Task<Response<(bool existe, string mensaje)>> AddAuditoriasContractual(JsonObject registro);
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        Task<Response<List<Documentos_Auditoria_Cumplimiento_Detalle>>> GetAuditoriasCumplimiento();
        Task<Response<List<Documentos_Auditoria_Cumplimiento_Proyecto_Detalle>>> GetAuditoriasCumplimientoByProyecto(int IdProyecto);
        Task<Response<(bool existe, string mensaje)>> AddAuditoriasCumplimiento(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> UpdateAuditoriaCumplimientoProyecto(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> UpdateAuditoriaCumplimientoDocumento(JsonObject registro);
        #endregion Auditoria de Calidad (Cumplimiento)
    }
}

