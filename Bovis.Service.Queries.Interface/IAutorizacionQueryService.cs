using Bovis.Common;
using Bovis.Common.Model.NoTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface IAutorizacionQueryService : IDisposable
    {
        #region Usuarios
        Task<Response<List<Usuario_Detalle>>> GetUsuarios();
        Task<Response<(bool Success, string Message)>> AddUsuario(JsonObject registro);
        Task<Response<Usuario_Perfiles_Detalle>> GetUsuarioPerfiles(int idUsuario);
        Task<Response<(bool Success, string Message)>> UpdateUsuarioPerfiles(JsonObject registro);
        Task<Response<(bool Success, string Message)>> DeleteUsuario(int idUsuario);
        #endregion Usuarios

        #region Módulos
        Task<Response<List<Modulo_Detalle>>> GetModulos();
        Task<Response<Modulo_Perfiles_Detalle>> GetModuloPerfiles(int idModulo);
        #endregion Módulos

        #region Perfiles
        Task<Response<List<Perfil_Detalle>>> GetPerfiles();
        Task<Response<Perfil_Permisos_Detalle>> GetPerfilPermisos(int idPerfil);
        Task<Response<(bool Success, string Message)>> UpdatePerfilModulos(JsonObject registro);
        Task<Response<(bool Success, string Message)>> UpdatePerfilPermisos(JsonObject registro);
        #endregion Perfiles

        #region Permisos
        Task<Response<List<Permiso_Detalle>>> GetPermisos();
        #endregion Permisos
    }
}
