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
    public interface IContratoBusiness : IDisposable
    {
        #region Templates
        Task<List<TB_Contrato_Template>> GetTemplates(bool? activo);
        Task<TB_Contrato_Template> GetTemplate(int IdTemplate);
        Task<(bool Success, string Message)> AddTemplate(JsonObject registro);
        Task<(bool Success, string Message)> UpdateTemplate(JsonObject registro);
        #endregion Templates

        #region Contratos Empleado
        Task<List<TB_Contrato_Empleado>> GetContratosEmpleado(int IdEmpleado);
        Task<TB_Contrato_Empleado> GetContratoEmpleado(int IdContratoEmpleado);
        Task<(bool Success, string Message)> AddContratoEmpleado(JsonObject registro);
        Task<(bool Success, string Message)> UpdateContratoEmpleado(JsonObject registro);
        #endregion Contratos Empleado
    }
}
