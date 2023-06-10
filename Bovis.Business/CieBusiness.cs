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
        public Task<List<TB_Cie_Data>> GetRegistros(byte? Estatus) => _cieData.GetRegistros(Estatus);
        public async Task<(bool Success, string Message)> AgregarRegistros(JsonObject registros)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _cieData.AgregarRegistros(registros);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudieron agregar los registros Cie a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        #endregion Registros
    }
}
