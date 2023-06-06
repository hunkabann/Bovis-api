using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using Microsoft.Win32;

namespace Bovis.Business
{
    public class RequerimientoBusiness : IRequerimientoBusiness
    {
        #region base
        private readonly IRequerimientoData _RequerimientoData;
        public RequerimientoBusiness(IRequerimientoData _RequerimientoData)
        {
            this._RequerimientoData = _RequerimientoData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Registros
        public Task<List<TB_Requerimiento>> GetRequerimientos(bool? Activo) => _RequerimientoData.GetRequerimientos(Activo);
        public async Task<(bool Success, string Message)> AddRegistro(TB_Requerimiento registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _RequerimientoData.AddRegistro(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo agregar el registro del Requerimiento a la base de datos"; return resp; }
            return resp;
        }
        #endregion Registros
    }
}
