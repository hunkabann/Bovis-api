using Bovis.Common;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface IRequerimientoQueryService : IDisposable
    {
        Task<Response<List<Habilidad>>> GetHabilidades(int idRequerimiento);
        Task<Response<List<Experiencia>>> GetExperiencias(int idRequerimiento);
        Task<Response<List<Requerimiento>>> GetRequerimientos(bool? Activo);
        Task<Response<Requerimiento>> GetRequerimiento(int idRequerimiento);
        Task<Response<(bool existe, string mensaje)>> AgregarRegistro(JsonObject registro);
        Task<Response<(bool existe, string mensaje)>> UpdateRegistro(JsonObject registro);
    }
}
