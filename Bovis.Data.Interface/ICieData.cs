using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface ICieData : IDisposable
    {
        #region Empresas
        Task<List<TB_Empresa>> GetEmpresas(bool? activo);
        #endregion Empresas

        #region Registros
        Task<TB_Cie_Data> GetRegistro(int? idRegistro);
        Task<List<TB_Cie_Data>> GetRegistros(bool? activo, int offset, int limit);
        Task<(bool existe, string mensaje)> AddRegistros(JsonObject registros);
        Task<(bool existe, string mensaje)> UpdateRegistro(JsonObject registro);
        Task<(bool existe, string mensaje)> DeleteRegistro(int idRegistro);

        #endregion Registros
    }
}