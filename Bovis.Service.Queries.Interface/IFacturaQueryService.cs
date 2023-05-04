using Bovis.Common;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Responses;

namespace Bovis.Service.Queries.Interface
{
    public interface IFacturaQueryService : IDisposable
    {
        Task<Response<InfoFactura>> ExtraerInfoFactura(string B64Xml);
        Task<Response<FacturaProyecto>> GetInfoProyecto(int numProyecto);
        Task<Response<List<DetallesFactura>>> Search(ConsultarFactura request);
    }
}