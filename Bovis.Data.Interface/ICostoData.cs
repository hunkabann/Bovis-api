using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface ICostoData : IDisposable
    {
        Task<(bool Success, string Message)> AddCosto(JsonObject registro);
        Task<List<Costo_Detalle>> GetCostos(int IdCosto);
    }
}