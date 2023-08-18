using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;
namespace Bovis.Data.Interface
{
    public interface IContratoData : IDisposable
    {
        #region Templates
        Task<List<TB_Contrato_Template>> GetTemplates(string Estatus);
        Task<TB_Contrato_Template> GetTemplate(int IdTemplate);
        Task<(bool Success, string Message)> AddTemplate(JsonObject registro);
        Task<(bool Success, string Message)> UpdateTemplate(JsonObject registro);
        Task<(bool Success, string Message)> UpdateTemplateEstatus(JsonObject registro);
        #endregion Templates

        #region Contratos Empleado
        Task<List<TB_Contrato_Empleado>> GetContratosEmpleado(int IdEmpleado);
        Task<TB_Contrato_Empleado> GetContratoEmpleado(int IdContratoEmpleado);
        Task<(bool Success, string Message)> AddContratoEmpleado(JsonObject registro);
        Task<(bool Success, string Message)> UpdateContratoEmpleado(JsonObject registro);
        #endregion Contratos Empleado
    }
}
