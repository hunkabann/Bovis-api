using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;

namespace Bovis.Business
{
    public class CieBusiness : ICieBusiness
    {
        #region base
        private readonly ICieData _cieData;
        public CieBusiness(ICieData _cieData)
        {
            this._cieData = _cieData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

    }
}
