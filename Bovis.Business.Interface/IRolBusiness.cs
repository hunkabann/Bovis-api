using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface IRolBusiness : IDisposable
    {
        Task<Rol_Detalle> GetRoles(string email);
    }
}
