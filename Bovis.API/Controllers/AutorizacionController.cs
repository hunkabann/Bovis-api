using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Text.Json.Nodes;
using Bovis.API.Helper;
using Newtonsoft.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Bovis.Common.Model.NoTable;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Permissions;

namespace Bovis.API.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class AutorizacionController : ControllerBase
    {
        #region base
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<AutorizacionController> _logger;
        private readonly IAutorizacionQueryService _autorizacionQueryService;
        private readonly IMediator _mediator;

        public AutorizacionController(ILogger<AutorizacionController> logger, IAutorizacionQueryService _autorizacionQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._autorizacionQueryService = _autorizacionQueryService;
            this._mediator = _mediator;
        }
        #endregion base


        #region Usuarios
        [HttpGet, Route("Usuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            var query = await _autorizacionQueryService.GetUsuarios();
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPost, Route("Usuarios")]
        public async Task<IActionResult> AddUsuario([FromBody] JsonObject registro)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            var query = await _autorizacionQueryService.AddUsuario(registro);
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete, Route("Usuarios/{idUsuario}")]
        public async Task<IActionResult> DeleteUsuario(int idUsuario)
        {
            var query = await _autorizacionQueryService.DeleteUsuario(idUsuario);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpGet, Route("Usuario/{idUsuario}/Perfiles")]
        public async Task<IActionResult> GetUsuarioPerfiles(int idUsuario)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            var query = await _autorizacionQueryService.GetUsuarioPerfiles(idUsuario);
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut, Route("Usuario/Perfiles")]
        public async Task<IActionResult> UpdateUsuarioPerfiles([FromBody] JsonObject registro)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", nombre);
            registroJsonObject.Add("Usuario", email);
            registroJsonObject.Add("Roles", string.Empty);
            registroJsonObject.Add("TransactionId", TransactionId);
            registroJsonObject.Add("Rel", 2055);

            var query = await _autorizacionQueryService.UpdateUsuarioPerfiles(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }        
        #endregion Usuarios

        #region Módulos
        [HttpGet, Route("Modulos")]
        public async Task<IActionResult> GetModulos()
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            var query = await _autorizacionQueryService.GetModulos();
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpGet, Route("Modulo/{idModulo}/Perfiles")]
        public async Task<IActionResult> GetModuloPerfiles(int idModulo)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            var query = await _autorizacionQueryService.GetModuloPerfiles(idModulo);
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Módulos

        #region Perfiles
        [HttpGet, Route("Perfiles")]
        public async Task<IActionResult> GetPerfiles()
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            var query = await _autorizacionQueryService.GetPerfiles();
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPost, Route("Perfiles")]
        public async Task<IActionResult> AddPerfil([FromBody] JsonObject registro)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            var query = await _autorizacionQueryService.AddPerfil(registro);
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpDelete, Route("Perfiles/{idPerfil}")]
        public async Task<IActionResult> DeletePerfil(int idPerfil)
        {
            var query = await _autorizacionQueryService.DeletePerfil(idPerfil);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpGet, Route("Perfil/{idPerfil}/Permisos")]
        public async Task<IActionResult> GetPerfilPermisos(int idPerfil)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            var query = await _autorizacionQueryService.GetPerfilPermisos(idPerfil);
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpGet, Route("Perfil/{idPerfil}/Modulos")]
        public async Task<IActionResult> GetPerfilModulos(int idPerfil)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            var query = await _autorizacionQueryService.GetPerfilModulos(idPerfil);
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut, Route("Perfil/Modulos")]
        public async Task<IActionResult> UpdatePerfilModulos([FromBody] JsonObject registro)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", nombre);
            registroJsonObject.Add("Usuario", email);
            registroJsonObject.Add("Roles", string.Empty);
            registroJsonObject.Add("TransactionId", TransactionId);
            registroJsonObject.Add("Rel", 2056);

            var query = await _autorizacionQueryService.UpdatePerfilModulos(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }

        [HttpPut, Route("Perfil/Permisos")]
        public async Task<IActionResult> UpdatePerfilPermisos([FromBody] JsonObject registro)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];
            JsonObject registroJsonObject = new JsonObject();
            registroJsonObject.Add("Registro", registro);
            registroJsonObject.Add("Nombre", nombre);
            registroJsonObject.Add("Usuario", email);
            registroJsonObject.Add("Roles", string.Empty);
            registroJsonObject.Add("TransactionId", TransactionId);
            registroJsonObject.Add("Rel", 2053);

            var query = await _autorizacionQueryService.UpdatePerfilPermisos(registroJsonObject);
            if (query.Message == string.Empty) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Perfiles

        #region Permisos
        [HttpGet, Route("Permisos")]
        public async Task<IActionResult> GetPermisos()
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            var query = await _autorizacionQueryService.GetPermisos();
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }
        #endregion Permisos
    }
}
