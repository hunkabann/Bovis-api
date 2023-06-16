using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using Microsoft.Win32;
using Bovis.Service.Queries.Dto.Responses;
using System.Text.Json.Nodes;

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

        #region Habilidades
        public Task<List<TB_Requerimiento_Habilidad>> GetHabilidades(int idRequerimiento) => _RequerimientoData.GetHabilidades(idRequerimiento);
        #endregion Habilidades

        #region Experiencias
        public Task<List<TB_Requerimiento_Experiencia>> GetExperiencias(int idRequerimiento) => _RequerimientoData.GetExperiencias(idRequerimiento);
        #endregion Experiencias

        #region Registros
        public Task<List<Requerimiento_Detalle>> GetRequerimientos(bool? Activo) => _RequerimientoData.GetRequerimientos(Activo);

        public Task<Requerimiento_Detalle> GetRequerimiento(int idRequerimiento) => _RequerimientoData.GetRequerimiento(idRequerimiento);

        public async Task<(bool Success, string Message)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _RequerimientoData.AddRegistro(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo agregar el registro del Requerimiento a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _RequerimientoData.UpdateRegistro(registro);
            if (!respData.existe) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        public Task<(bool Success, string Message)> DeleteRequerimiento(int idRequerimiento) => _RequerimientoData.DeleteRequerimiento(idRequerimiento);
        #endregion Registros
    }
}
