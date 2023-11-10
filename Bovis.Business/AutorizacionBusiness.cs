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

        public Task<(bool Success, string Message)> DeleteUsuario(int idUsuario) => _AutorizacionData.DeleteUsuario(idUsuario);

        public Task<Usuario_Perfiles_Detalle> GetUsuarioPerfiles(int idUsuario) => _AutorizacionData.GetUsuarioPerfiles(idUsuario);

        public async Task<(bool Success, string Message)> UpdateUsuarioPerfiles(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _AutorizacionData.UpdateUsuarioPerfiles((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
        #endregion Usuarios


        #region Empleados
        public Task<List<Empleado_BasicData>> GetEmpleadosNoUsuarios() => _AutorizacionData.GetEmpleadosNoUsuarios();
        #endregion Empleados


        #region Módulos
        public Task<List<Modulo_Detalle>> GetModulos() => _AutorizacionData.GetModulos();

        public Task<Modulo_Perfiles_Detalle> GetModuloPerfiles(int idModulo) => _AutorizacionData.GetModuloPerfiles(idModulo);
        #endregion Módulos


        #region Perfiles
        public Task<List<Perfil_Detalle>> GetPerfiles() => _AutorizacionData.GetPerfiles();
        public Task<Perfil_Detalle> AddPerfil(JsonObject registro) => _AutorizacionData.AddPerfil(registro);
        public Task<(bool Success, string Message)> DeletePerfil(int idPerfil) => _AutorizacionData.DeletePerfil(idPerfil);

        public Task<Perfil_Permisos_Detalle> GetPerfilPermisos(int idPerfil) => _AutorizacionData.GetPerfilPermisos(idPerfil);

        public Task<Perfil_Modulos_Detalle> GetPerfilModulos(int idPerfil) => _AutorizacionData.GetPerfilModulos(idPerfil);

        public async Task<(bool Success, string Message)> UpdatePerfilModulos(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _AutorizacionData.UpdatePerfilModulos((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdatePerfilPermisos(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _AutorizacionData.UpdatePerfilPermisos((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
        #endregion Perfiles


        #region Permisos
        public Task<List<Permiso_Detalle>> GetPermisos() => _AutorizacionData.GetPermisos();
        #endregion Permisos
    }
}
