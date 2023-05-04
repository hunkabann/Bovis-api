using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;

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
        #endregion

    }
}
