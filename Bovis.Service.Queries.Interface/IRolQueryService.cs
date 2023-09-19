using Bovis.Common;
using Bovis.Common.Model.NoTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface IRolQueryService : IDisposable
    {
        Task<Response<Rol_Detalle>> GetRoles(string email);
    }
}
