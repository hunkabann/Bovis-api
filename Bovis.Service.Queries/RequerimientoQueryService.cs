using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class RequerimientoQueryService : IRequerimientoQueryService
    {
        #region base
        private readonly IRequerimientoBusiness _requerimientoBussines;
        private readonly IMapper _map;

        public RequerimientoQueryService(IMapper _map, IRequerimientoBusiness _requerimientoBussines)
        {
            this._map = _map;
            this._requerimientoBussines = _requerimientoBussines;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

        #region Registros
        public async Task<Response<List<Requerimiento>>> GetRequerimientos(bool? Activo)
        {
            var response = await _requerimientoBussines.GetRequerimientos(Activo);
            return new Response<List<Requerimiento>> { Data = _map.Map<List<Requerimiento>>(response), Success = true };
        }
        public async Task<Response<bool>> AddRegistro(TB_Requerimiento registro)
        {
            var response = await _requerimientoBussines.AddRegistro(registro);
            return new Response<bool> { Data = _map.Map<bool>(response), Success = true };
        }
        #endregion Registros
    }
}
