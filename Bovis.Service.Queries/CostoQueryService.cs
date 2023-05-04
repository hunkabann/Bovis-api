using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class CostoQueryService : ICostoQueryService
    {
        #region base
        private readonly ICostoBusiness _costoBusiness;

        private readonly IMapper _map;

        public CostoQueryService(IMapper _map, ICostoBusiness _costoBusiness)
        {
            this._map = _map;
            this._costoBusiness = _costoBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion
    }
}

