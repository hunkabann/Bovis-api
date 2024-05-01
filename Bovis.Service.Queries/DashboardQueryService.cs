using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class DashboardQueryService : IDashboardQueryService
    {
        #region base
        private readonly IDashboardBusiness _dashboardBusiness;

        private readonly IMapper _map;

        public DashboardQueryService(IMapper _map, IDashboardBusiness _dashboardBusiness)
        {
            this._map = _map;
            this._dashboardBusiness = _dashboardBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Proyectos Documentos
        public async Task<Response<List<ProyectosDocumentos>>> GetProyectosDocumentos()
        {
            var response = await _dashboardBusiness.GetProyectosDocumentos();
            return new Response<List<ProyectosDocumentos>> { Data = _map.Map<List<ProyectosDocumentos>>(response), Success = true };
        }
        #endregion Proyectos Documentos
    }
}
