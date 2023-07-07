using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Responses;
using System.Text.Json.Nodes;

namespace Bovis.Service.Queries.Interface
{
    public interface IFacturaQueryService : IDisposable
    {
        Task<Response<InfoFactura>> ExtraerInfoFactura(string B64Xml);
        Task<Response<FacturaProyecto>> GetInfoProyecto(int numProyecto);
        Task<Response<List<FacturaDetalles>>> Search(ConsultarFactura request);
        Task<Response<(bool existe, string mensaje)>> CancelNota(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> CancelCobranza(JsonObject registro);
    }
}