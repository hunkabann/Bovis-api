using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IDashboardData : IDisposable
    {
        #region Proyectos Documentos
        Task<List<ProyectosDocumentos>> GetProyectosDocumentos();
        #endregion Proyectos Documentos
    }
}
