using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Data.Interface
{
    public interface ITokenData : IDisposable
    {
        Task<(bool Success, string Message)> AddToken(string email, string str_token);
    }
}
