using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Common;
using System.Text.Json.Nodes;

namespace Bovis.Business.Interface
{
    public interface IDashboardBusiness : IDisposable
    {
        #region Proyectos Documentos
        Task<List<ProyectosDocumentos>> GetProyectosDocumentos();
        #endregion Proyectos Documentos
    }
}
