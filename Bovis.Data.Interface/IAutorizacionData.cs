using Bovis.Common.Model.NoTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Data.Interface
{
    public interface IAutorizacionData : IDisposable
    {
        #region Usuarios
        Task<List<Usuario_Detalle>> GetUsuarios();
        Task<(bool Success, string Message)> AddUsuario(JsonObject registro);
        Task<Usuario_Perfiles_Detalle> GetUsuarioPerfiles(int idUsuario);
        Task<(bool Success, string Message)> UpdateUsuarioPerfiles(JsonObject registro);
        Task<(bool Success, string Message)> DeleteUsuario(int idUsuario);
        #endregion Usuarios

        #region Módulos
        Task<List<Modulo_Detalle>> GetModulos();
        Task<Modulo_Perfiles_Detalle> GetModuloPerfiles(int idModulo);
        #endregion Módulos

        #region Perfiles
        Task<List<Perfil_Detalle>> GetPerfiles();
        Task<Perfil_Permisos_Detalle> GetPerfilPermisos(int idPerfil);
        Task<(bool Success, string Message)> UpdatePerfilModulos(JsonObject registro);
        Task<(bool Success, string Message)> UpdatePerfilPermisos(JsonObject registro);
        #endregion Perfiles

        #region Permisos
        Task<List<Permiso_Detalle>> GetPermisos();
        #endregion Permisos
    }
}
