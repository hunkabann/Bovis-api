using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;
namespace Bovis.Data.Interface
{
    public interface IContratoData : IDisposable
    {
        #region Templates
        Task<List<TB_Contrato_Template>> GetTemplates(bool? Activo);
        Task<TB_Contrato_Template> GetTemplate(int IdTemplate);
        Task<(bool existe, string mensaje)> AddTemplate(JsonObject registro);
        Task<(bool existe, string mensaje)> UpdateTemplate(JsonObject registro);
        #endregion Templates

        #region Contratos Empleado
        Task<List<TB_Contrato_Empleado>> GetContratosEmpleado(int IdEmpleado);
        Task<TB_Contrato_Empleado> GetContratoEmpleado(int IdContratoEmpleado);
        Task<(bool existe, string mensaje)> AddContratoEmpleado(JsonObject registro);
        Task<(bool existe, string mensaje)> UpdateContratoEmpleado(JsonObject registro);
        #endregion Contratos Empleado
    }
}
