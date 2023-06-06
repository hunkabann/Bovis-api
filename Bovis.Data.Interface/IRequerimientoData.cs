using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;

namespace Bovis.Data.Interface
{
    public interface IRequerimientoData : IDisposable
    {
        Task<List<TB_Requerimiento_Habilidad>> GetHabilidades(int idRequerimiento);
        Task<List<TB_Requerimiento_Experiencia>> GetExperiencias(int idRequerimiento);
        Task<List<TB_Requerimiento>> GetRequerimientos(bool? activo);
        Task<(bool existe, string mensaje)> AddRegistro(TB_Requerimiento registro);
    }
}
