using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using Microsoft.Win32;
using Bovis.Service.Queries.Dto.Responses;
using System.Text.Json.Nodes;
using Bovis.Common;

namespace Bovis.Business
{
    public class AutorizacionBusiness : IAutorizacionBusiness
    {
        #region base
        private readonly IAutorizacionData _AutorizacionData;
        private readonly ITransactionData _transactionData;
        public AutorizacionBusiness(IAutorizacionData _AutorizacionData, ITransactionData _transactionData)
        {
            this._AutorizacionData = _AutorizacionData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base        


        #region Usuarios
        public Task<List<Usuario_Detalle>> GetUsuarios() => _AutorizacionData.GetUsuarios();
        public async Task<(bool Success, string Message)> AddUsuario(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _AutorizacionData.AddUsuario(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        public Task<Usuario_Perfiles_Detalle> GetUsuarioPerfiles(int idUsuario) => _AutorizacionData.GetUsuarioPerfiles(idUsuario);
        #endregion Usuarios

        #region Módulos
        public Task<List<Modulo_Detalle>> GetModulos() => _AutorizacionData.GetModulos();
        public Task<Modulo_Perfiles_Detalle> GetModuloPerfiles(int idModulo) => _AutorizacionData.GetModuloPerfiles(idModulo);
        #endregion Módulos

        #region Perfiles
        public Task<List<Perfil_Detalle>> GetPerfiles() => _AutorizacionData.GetPerfiles();
        public Task<Perfil_Permisos_Detalle> GetPerfilPermisos(int idPerfil) => _AutorizacionData.GetPerfilPermisos(idPerfil);
        #endregion Perfiles

        #region Permisos
        public Task<List<Permiso_Detalle>> GetPermisos() => _AutorizacionData.GetPermisos();
        #endregion Permisos
    }
}
