using Bovis.Common.Model.Tables;

namespace Bovis.Business.Interface
{
    public interface IPcsBusiness : IDisposable
    {
        Task<List<TB_Proyecto>> GetProyectos();
        Task<TB_Proyecto> GetProyecto(int numProyecto);

        Task<List<TB_Empresa>> GetEmpresas();
        Task<List<TB_Cliente>> GetClientes();
    }

}
