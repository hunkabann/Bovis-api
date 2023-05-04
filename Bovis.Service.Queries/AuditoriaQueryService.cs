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
    public class AuditoriaQueryService : IAuditoriaQueryService
    {
        #region base
        private readonly IAuditoriaBusiness _auditoriaBusiness;

        private readonly IMapper _map;

        public AuditoriaQueryService(IMapper _map, IAuditoriaBusiness _auditoriaBusiness)
        {
            this._map = _map;
            this._auditoriaBusiness = _auditoriaBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion
    }
}

