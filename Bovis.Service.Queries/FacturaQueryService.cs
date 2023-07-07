using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;
using System.Text.Json.Nodes;

namespace Bovis.Service.Queries
{
	public class FacturaQueryService : IFacturaQueryService
	{
		private readonly IFacturaBusiness _facturaBusiness;

		private readonly IMapper _map;

		public FacturaQueryService(IMapper _map, IFacturaBusiness _facturaBusiness)
		{
			this._map = _map;
			this._facturaBusiness = _facturaBusiness;
		}

		public async Task<Response<InfoFactura>> ExtraerInfoFactura(string xml)
		{
			var response = await _facturaBusiness.ExtraerDatos(xml);
			return new Response<InfoFactura> { Data = _map.Map<InfoFactura>(response), Success = response is not null ? true : default, Message = response is null ? "Mo se pudo extraer información del CFDI." : default };
		}

		public async Task<Response<FacturaProyecto>> GetInfoProyecto(int numProyecto)
		{
			//Factura_Proyecto response = null;
			//try
			//{
			var response = await _facturaBusiness.GetInfoProyecto(numProyecto);

			//}
			//catch (Exception ex)
			//{

			//}
			return new Response<FacturaProyecto> { Data = _map.Map<FacturaProyecto>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontro información del proyecto." : default };
		}

		public async Task<Response<List<FacturaDetalles>>> Search(ConsultarFactura request)
		{
			var response = await _facturaBusiness.Search(request.IdProyecto, request.IdCliente, request.IdEmpresa, request.FechaIni, request.FechaFin);
			return new Response<List<FacturaDetalles>> { Data = _map.Map<List<FacturaDetalles>>(response), Success = response is not null ? true : default, Message = response is null ? "Mo se encontró información con los parametros especificados." : default };
		}

        public async Task<Response<(bool existe, string mensaje)>> CancelNota(JsonObject registro)
        {
            var response = await _facturaBusiness.CancelNota(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool existe, string mensaje)>> CancelCobranza(JsonObject registro)
        {
            var response = await _facturaBusiness.CancelCobranza(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }

        public void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect();
		}
	}
}
