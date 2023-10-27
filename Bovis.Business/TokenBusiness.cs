using Bovis.Business.Interface;
using Bovis.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Business
{
    public class TokenBusiness : ITokenBusiness
    {
        #region base
        private readonly ITokenData _TokenData;
        private readonly ITransactionData _transactionData;
        public TokenBusiness(ITokenData _TokenData, ITransactionData _transactionData)
        {
            this._TokenData = _TokenData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public Task<(bool Success, string Message)> AddToken(string email, string str_token) => _TokenData.AddToken(email, str_token);
    }
}
