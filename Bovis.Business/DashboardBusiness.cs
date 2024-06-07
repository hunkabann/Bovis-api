using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;

namespace Bovis.Business
{
    public class DashboardBusiness : IDashboardBusiness
    {
        #region base
        private readonly IDashboardData _dashboardData;
        private readonly ITransactionData _transactionData;
        public DashboardBusiness(IDashboardData _dashboardData, ITransactionData _transactionData)
        {
            this._dashboardData = _dashboardData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Proyectos Documentos
        public Task<List<ProyectosDocumentos>> GetProyectosDocumentos() => _dashboardData.GetProyectosDocumentos();
        #endregion Proyectos Documentos
    }
}
