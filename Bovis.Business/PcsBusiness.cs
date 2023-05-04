using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;

namespace Bovis.Business
{
    public class PcsBusiness : IPcsBusiness
    {
        #region base
        private readonly IPcsData _pcsData;
        public PcsBusiness(IPcsData _pcsData)
        {
            this._pcsData = _pcsData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

        public Task<List<TB_Proyecto>> GetProyectos() => _pcsData.GetProyectos();
        public Task<TB_Proyecto> GetProyecto(int numProyecto) => _pcsData.GetProyecto(numProyecto);

        public Task<List<TB_Empresa>> GetEmpresas() => _pcsData.GetEmpresas();
        public Task<List<TB_Cliente>> GetClientes() => _pcsData.GetClientes();
    }
}
