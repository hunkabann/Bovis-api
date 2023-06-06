using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using Microsoft.Win32;

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
        #endregion base

        #region Empresas
        public Task<List<TB_Empresa>> GetEmpresas(bool? Activo) => _cieData.GetEmpresas(Activo);
        #endregion Empresas

        #region Registros
        public Task<CieRegistro> GetInfoRegistro(int? idRegistro) => _cieData.GetInfoRegistro(idRegistro);
        public Task<List<TB_Cie>> GetRegistros(byte? Estatus) => _cieData.GetRegistros(Estatus);

        public async Task<(bool Success, string Message)> AddRegistro(TB_Cie registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _cieData.AddRegistro(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo agregar el registro Cie a la base de datos"; return resp; }
            return resp;
        }
        public async Task<(bool Success, string Message)> AddRegistros(List<TB_Cie> registros)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _cieData.AddRegistros(registros);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudieron agregar los registros Cie a la base de datos"; return resp; }
            return resp;
        }
        #endregion Registros
    }
}
