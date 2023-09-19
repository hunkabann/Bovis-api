using Bovis.Common.Model.NoTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Data.Interface
{
    public interface IRolData : IDisposable
    {
        Task<(bool Success, string Message)> AddToken(string email, string str_token);
        Task<string> GetAuthorization(string email);
        Task<Rol_Detalle> GetRoles(string email);
    }
}
