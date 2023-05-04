using Bovis.Common.Model.Tables;

namespace Bovis.Data.Interface
{
    public interface IPcsData : IDisposable
    {
        Task<List<TB_Proyecto>> GetProyectos();
        Task<TB_Proyecto> GetProyecto(int numProyecto);        
        Task<List<TB_Cliente>> GetClientes();
        Task<List<TB_Empresa>> GetEmpresas();
    }
}