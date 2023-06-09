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
        Task<List<TB_Cie_Data>> GetRegistros(byte? estatus);
        Task<(bool existe, string mensaje)> AgregarRegistros(JsonObject registros);
        #endregion Registros
    }
}