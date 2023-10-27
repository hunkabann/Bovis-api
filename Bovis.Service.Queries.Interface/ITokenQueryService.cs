using Bovis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface ITokenQueryService : IDisposable
    {
        Task<Response<(bool Success, string Message)>> AddToken(string email, string str_token);
    }
}
