using Bovis.Common;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Responses;
using System.Threading.Tasks;


namespace Bovis.Service.Queries.Interface
{
    public interface IPcsQueryService
    {

        Task<Response<List<Proyecto>>> GetProyectos();

        Task<Response<Proyecto>> GetProyecto(int numProyecto);

        Task<Response<List<InfoCliente>>> GetClientes();
        Task<Response<List<InfoEmpresa>>> GetEmpresas();
    }
}

