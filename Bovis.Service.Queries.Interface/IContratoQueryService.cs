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
        Task<Response<List<TB_Contrato_Template>>> GetTemplates(bool? Activo);
        Task<Response<TB_Contrato_Template>> GetTemplate(int IdTemplate);
        Task<Response<(bool existe, string mensaje)>> AddTemplate(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> UpdateTemplate(JsonObject registro);
        #endregion Templates

        #region Contratos Empleado
        Task<Response<List<TB_Contrato_Empleado>>> GetContratosEmpleado(int IdEmpleado);
        Task<Response<TB_Contrato_Empleado>> GetContratoEmpleado(int IdContratoEmpleado);
        Task<Response<(bool existe, string mensaje)>> AddContratoEmpleado(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> UpdateContratoEmpleado(JsonObject registro);
        #endregion Contratos Empleado
    }
}
