using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface IRequerimientoBusiness : IDisposable
    {
        Task<List<TB_Requerimiento_Habilidad>> GetHabilidades(int idRequerimiento);
        Task<List<TB_Requerimiento_Experiencia>> GetExperiencias(int idRequerimiento);
        Task<List<TB_Requerimiento>> GetRequerimientos(bool? activo);
        Task<(bool Success, string Message)> AddRegistro(TB_Requerimiento registro);
    }
}
