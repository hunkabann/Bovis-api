using Bovis.Common.Model.NoTable;
using Bovis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface ICostoQueryService
    {
        Task<Response<(bool Success, string Message)>> AddCosto(JsonObject registro);
        Task<Response<List<Costo_Detalle>>> GetCostos(int IdCosto);
    }
}

