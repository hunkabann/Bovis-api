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
    public interface ICieBusiness : IDisposable
    {
        #region Empresas
        Task<List<TB_Empresa>> GetEmpresas(bool? activo);
        #endregion Empresas

        #region Registros
        Task<TB_Cie_Data> GetRegistro(int? numProyecto);
        Task<List<TB_Cie_Data>> GetRegistros(bool? activo, int offset, int limit);
        Task<(bool Success, string Message)> AddRegistros(JsonObject registros);
        Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro);
        Task<(bool Success, string Message)> DeleteRegistro(int idRegistro);
        #endregion Registros
    }

}
