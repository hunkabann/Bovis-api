using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface IRequerimientoBusiness : IDisposable
    {
        Task<List<TB_Requerimiento_Habilidad>> GetHabilidades(int idRequerimiento);
        Task<List<TB_Requerimiento_Experiencia>> GetExperiencias(int idRequerimiento);
        Task<List<TB_Requerimiento>> GetRequerimientos(bool? activo);
        Task<TB_Requerimiento> GetRequerimiento(int idRequerimiento);
        Task<(bool Success, string Message)> AgregarRegistro(JsonObject registro);
        Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro);
    }
}
