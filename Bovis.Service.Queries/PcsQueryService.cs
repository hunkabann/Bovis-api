using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;

namespace Bovis.Service.Queries
{
    public class PcsQueryService : IPcsQueryService
    {
        #region base
        private readonly IPcsBusiness _pcsBusiness;

        private readonly IMapper _map;

        public PcsQueryService(IMapper _map, IPcsBusiness _pcsBusiness)
        {
            this._map = _map;
            this._pcsBusiness = _pcsBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

        public async Task<Response<List<Proyecto>>> GetProyectos()
        {
            var response = await _pcsBusiness.GetProyectos();
            return new Response<List<Proyecto>> { Data = _map.Map<List<Proyecto>>(response), Success = true };
        }

        public async Task<Response<Proyecto>> GetProyecto(int numProyecto)
        {
            var response = await _pcsBusiness.GetProyecto(numProyecto);
            return new Response<Proyecto> { Data = _map.Map<Proyecto>(response), Success = true };
        }

        public async Task<Response<List<InfoCliente>>> GetClientes()
        {
            var response = await _pcsBusiness.GetClientes();
            return new Response<List<InfoCliente>> { Data = _map.Map<List<InfoCliente>>(response), Success = true };
        }
        public async Task<Response<List<InfoEmpresa>>> GetEmpresas()
        {
            var response = await _pcsBusiness.GetEmpresas();
            return new Response<List<InfoEmpresa>> { Data = _map.Map<List<InfoEmpresa>>(response), Success = true };
        }
    }
}

