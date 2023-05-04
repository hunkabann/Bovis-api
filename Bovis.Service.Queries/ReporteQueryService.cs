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
    public class ReporteQueryService : IReporteQueryService
    {
        #region base
        private readonly IReporteBusiness _reporteBusiness;

        private readonly IMapper _map;

        public ReporteQueryService(IMapper _map, IReporteBusiness _reporteBusiness)
        {
            this._map = _map;
            this._reporteBusiness = _reporteBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion
    }
}

