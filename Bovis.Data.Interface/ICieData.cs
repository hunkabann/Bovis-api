using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;

namespace Bovis.Data.Interface
{
    public interface ICieData : IDisposable
    {
        #region Empresas
        Task<List<TB_Empresa>> GetEmpresas(bool? activo);
        #endregion Empresas

        #region Registros
        Task<CieRegistro> GetInfoRegistro(int? idRegistro);
        Task<List<TB_Cie>> GetRegistros(byte? estatus);
        #endregion Registros
    }
}