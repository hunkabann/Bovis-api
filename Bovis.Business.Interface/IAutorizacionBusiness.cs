using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface IAutorizacionBusiness : IDisposable
    {
        #region Usuarios
        Task<List<Usuario_Detalle>> GetUsuarios();
        Task<(bool Success, string Message)> AddUsuario(JsonObject registro);
        Task<Usuario_Perfiles_Detalle> GetUsuarioPerfiles(int idUsuario);
        #endregion Usuarios

        #region Módulos
        Task<List<Modulo_Detalle>> GetModulos();
        Task<Modulo_Perfiles_Detalle> GetModuloPerfiles(int idModulo);
        #endregion Módulos

        #region Perfiles
        Task<List<Perfil_Detalle>> GetPerfiles();
        Task<Perfil_Permisos_Detalle> GetPerfilPermisos(int idPerfil);
        #endregion Perfiles

        #region Permisos
        Task<List<Permiso_Detalle>> GetPermisos();
        #endregion Permisos
    }
}
