using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;

namespace Bovis.Business
{
    public class AuditoriaBusiness : IAuditoriaBusiness
    {
        #region base
        private readonly IAuditoriaData _auditoriaData;
        public AuditoriaBusiness(IAuditoriaData _auditoriaData)
        {
            this._auditoriaData = _auditoriaData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

    }
}
