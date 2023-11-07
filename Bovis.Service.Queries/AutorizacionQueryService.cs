using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class AutorizacionQueryService : IAutorizacionQueryService
    {

        #region base
        private readonly IAutorizacionBusiness _autorizacionBusiness;
        private readonly IMapper _map;

        public AutorizacionQueryService(IMapper _map, IAutorizacionBusiness _autorizacionBusiness)
        {
            this._map = _map;
            this._autorizacionBusiness = _autorizacionBusiness;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base


        #region Usuarios
        public async Task<Response<List<Usuario_Detalle>>> GetUsuarios()
        {
            var response = await _autorizacionBusiness.GetUsuarios();
            return new Response<List<Usuario_Detalle>> { Data = _map.Map<List<Usuario_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<(bool Success, string Message)>> AddUsuario(JsonObject registro)
        {
            var response = await _autorizacionBusiness.AddUsuario(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<Usuario_Perfiles_Detalle>> GetUsuarioPerfiles(int idUsuario)
        {
            var response = await _autorizacionBusiness.GetUsuarioPerfiles(idUsuario);
            return new Response<Usuario_Perfiles_Detalle> { Data = _map.Map<Usuario_Perfiles_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        #endregion Usuarios

        #region Módulos
        public async Task<Response<List<Modulo_Detalle>>> GetModulos()
        {
            var response = await _autorizacionBusiness.GetModulos();
            return new Response<List<Modulo_Detalle>> { Data = _map.Map<List<Modulo_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<Modulo_Perfiles_Detalle>> GetModuloPerfiles(int idModulo)
        {
            var response = await _autorizacionBusiness.GetModuloPerfiles(idModulo);
            return new Response<Modulo_Perfiles_Detalle> { Data = _map.Map<Modulo_Perfiles_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        #endregion Módulos

        #region Perfiles
        public async Task<Response<List<Perfil_Detalle>>> GetPerfiles()
        {
            var response = await _autorizacionBusiness.GetPerfiles();
            return new Response<List<Perfil_Detalle>> { Data = _map.Map<List<Perfil_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<Perfil_Permisos_Detalle>> GetPerfilPermisos(int idPerfil)
        {
            var response = await _autorizacionBusiness.GetPerfilPermisos(idPerfil);
            return new Response<Perfil_Permisos_Detalle> { Data = _map.Map<Perfil_Permisos_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        #endregion Perfiles

        #region Permisos
        public async Task<Response<List<Permiso_Detalle>>> GetPermisos()
        {
            var response = await _autorizacionBusiness.GetPermisos();
            return new Response<List<Permiso_Detalle>> { Data = _map.Map<List<Permiso_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        #endregion Permisos
    }
}
