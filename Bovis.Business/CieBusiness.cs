using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;

namespace Bovis.Business
{
    public class CieBusiness : ICieBusiness
    {
        #region base
        private readonly ICieData _cieData;
        public CieBusiness(ICieData _cieData)
        {
            this._cieData = _cieData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

        #region Empresas
        public Task<List<TB_Empresa>> GetEmpresas(bool? Activo) => _cieData.GetEmpresas(Activo);
        #endregion Empresas

        #region Registros
        public Task<CieRegistro> GetInfoRegistro(int? idRegistro) => _cieData.GetInfoRegistro(idRegistro);
        public Task<List<TB_Cie>> GetRegistros(byte? Estatus) => _cieData.GetRegistros(Estatus);
        #endregion Registros
    }
}
