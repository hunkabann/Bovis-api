using Bovis.Common;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface IRequerimientoQueryService : IDisposable
    {
        Task<Response<List<Requerimiento>>> GetRequerimientos(bool? Activo);
        Task<Response<bool>> AddRegistro(TB_Requerimiento registro);
        Task<Response<List<Habilidad>>> GetHabilidades(int idRequerimiento);
        Task<Response<List<Experiencia>>> GetExperiencias(int idRequerimiento);
    }
}
