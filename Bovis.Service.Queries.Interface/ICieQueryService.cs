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
    public interface ICieQueryService : IDisposable
    {
        Task<Response<List<Empresa>>> GetEmpresas(bool? Activo);
        Task<Response<Cie>> GetInfoRegistro(int? idRegistro);
        Task<Response<List<Cie>>> GetRegistros(byte? Activo);
        Task<Response<bool>> AddRegistros(List<TB_Cie> registros);
    }
}

