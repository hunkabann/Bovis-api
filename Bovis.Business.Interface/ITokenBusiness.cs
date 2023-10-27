using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface ITokenBusiness : IDisposable
    {
        Task<(bool Success, string Message)> AddToken(string email, string str_token);
    }
}
