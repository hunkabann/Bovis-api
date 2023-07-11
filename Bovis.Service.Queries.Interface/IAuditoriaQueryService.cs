using Bovis.Common.Model.NoTable;
using Bovis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface IAuditoriaQueryService
    {
        Task<Response<List<Documentos_Auditoria_Cumplimiento_Detalle>>> GetDocumentosAuditoriaCumplimiento();
    }
}

