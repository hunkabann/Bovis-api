using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface IContratoQueryService
    {
        #region Templates
        Task<Response<List<TB_Contrato_Template>>> GetTemplates(string Estatus);
        Task<Response<TB_Contrato_Template>> GetTemplate(int IdTemplate);
        Task<Response<(bool Success, string Message)>> AddTemplate(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdateTemplate(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdateTemplateEstatus(JsonObject registro);
        #endregion Templates

        #region Contratos Empleado
        Task<Response<List<TB_Contrato_Empleado>>> GetContratosEmpleado(int IdEmpleado);
        Task<Response<TB_Contrato_Empleado>> GetContratoEmpleado(int IdContratoEmpleado);
        Task<Response<(bool Success, string Message)>> AddContratoEmpleado(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdateContratoEmpleado(JsonObject registro);
        #endregion Contratos Empleado
    }
}
