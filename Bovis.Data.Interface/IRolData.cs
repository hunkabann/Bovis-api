using Bovis.Common.Model.NoTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Data.Interface
{
    public interface IRolData : IDisposable
    {
        Task<Rol_Detalle> GetRoles(string email);
    }
}
