using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using Microsoft.Win32;
using Bovis.Service.Queries.Dto.Responses;
using System.Text.Json.Nodes;

namespace Bovis.Business
{
    public class RolBusiness : IRolBusiness
    {
        #region base
        private readonly IRolData _RolData;
        private readonly ITransactionData _transactionData;
        public RolBusiness(IRolData _RolData, ITransactionData _transactionData)
        {
            this._RolData = _RolData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public Task<Rol_Detalle> GetRoles(string email) => _RolData.GetRoles(email);
    }
}
