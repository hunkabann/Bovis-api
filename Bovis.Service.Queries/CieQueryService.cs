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
    public class CieQueryService : ICieQueryService
    {
        #region base
        private readonly ICieBusiness _cieBusiness;

        private readonly IMapper _map;

        public CieQueryService(IMapper _map, ICieBusiness _cieBusiness)
        {
            this._map = _map;
            this._cieBusiness = _cieBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion
    }
}

