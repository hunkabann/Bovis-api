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
    public interface ICostoBusiness : IDisposable
    {
        Task<(bool Success, string Message)> AddCosto(JsonObject registro);
        Task<List<Costo_Detalle>> GetCostos(int IdCosto);
    }

}
