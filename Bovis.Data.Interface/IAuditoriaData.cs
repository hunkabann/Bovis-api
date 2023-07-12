using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IAuditoriaData : IDisposable
    {
        #region Auditoria Legal
        Task<List<TB_Cat_Auditoria_Contractual>> GetAuditoriasContractual();
        Task<(bool existe, string mensaje)> AddAuditoriasContractual(JsonObject registro);
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        Task<List<Documentos_Auditoria_Cumplimiento_Detalle>> GetAuditoriasCumplimiento();
        Task<List<Documentos_Auditoria_Cumplimiento_Proyecto_Detalle>> GetAuditoriasCumplimientoByProyecto(int IdProyecto);
        Task<(bool existe, string mensaje)> AddAuditoriasCumplimiento(JsonObject registro);
        Task<(bool existe, string mensaje)> UpdateAuditoriaCumplimientoProyecto(JsonObject registro);
        Task<(bool existe, string mensaje)> AddAuditoriaCumplimientoDocumento(JsonObject registro);
        Task<List<TB_Auditoria_Cumplimiento_Documento>> GetDocumentosAuditoriaCumplimiento(int IdAuditoriaCumplimiento, int offset, int limit);
        #endregion Auditoria de Calidad (Cumplimiento)
    }
}