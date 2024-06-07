using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Responses;
using System.Text.Json.Nodes;

namespace Bovis.Service.Queries.Interface
{
    public interface IDashboardQueryService : IDisposable
    {
        Task<Response<List<ProyectosDocumentos>>> GetProyectosDocumentos();
    }
}


