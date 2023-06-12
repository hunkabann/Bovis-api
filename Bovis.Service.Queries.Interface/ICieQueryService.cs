using Bovis.Common;
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
    public interface ICieQueryService : IDisposable
    {
        Task<Response<List<Empresa>>> GetEmpresas(bool? Activo);
        Task<Response<TB_Cie_Data>> GetRegistro(int? idRegistro);
        Task<Response<List<TB_Cie_Data>>> GetRegistros(bool? Activo);
        Task<Response<(bool existe, string mensaje)>> AgregarRegistros(JsonObject registros);
    }
}

