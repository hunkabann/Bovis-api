using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using Microsoft.Win32;
using System.Text.Json.Nodes;

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
        public Task<TB_Cie_Data> GetRegistro(int? idRegistro) => _cieData.GetRegistro(idRegistro);
        public Task<List<TB_Cie_Data>> GetRegistros(bool? Activo) => _cieData.GetRegistros(Activo);
        public async Task<(bool Success, string Message)> AddRegistros(JsonObject registros)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _cieData.AddRegistros(registros);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudieron agregar los registros Cie a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _cieData.UpdateRegistro(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        public async Task<(bool Success, string Message)> DeleteRegistro(int idRegistro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _cieData.DeleteRegistro(idRegistro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        #endregion Registros
    }
}
