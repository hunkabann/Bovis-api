using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;

namespace Bovis.Business
{
    public class CostoBusiness : ICostoBusiness
    {
        #region base
        private readonly ICostoData _costoData;
        public CostoBusiness(ICostoData _costoData)
        {
            this._costoData = _costoData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public Task<(bool Success, string Message)> AddCosto(JsonObject registro) => _costoData.AddCosto(registro);
        public Task<List<Costo_Detalle>> GetCostos(int IdCosto) => _costoData.GetCostos(IdCosto);

    }
}
