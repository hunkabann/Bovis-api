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

namespace Bovis.API.Controllers
{
    [ApiController, Route("api/[controller]"), RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class RolController : ControllerBase
    {
        #region base
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<RolController> _logger;
        private readonly IRolQueryService _rolQueryService;
        private readonly IMediator _mediator;

        public RolController(ILogger<RolController> logger, IRolQueryService _rolQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._rolQueryService = _rolQueryService;
            this._mediator = _mediator;
        }
        #endregion base

        [HttpPost("Token")]
        public async Task<IActionResult> Token([FromBody] JsonObject request)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string username = configuration["AppSettings:username"];
            string password = configuration["AppSettings:password"];
            string claveSecreta = configuration["AppSettings:secretKey"];
            string issuer = configuration["AppSettings:issuer"];
            string audience = configuration["AppSettings:audience"];

            string email = request["email"].ToString();
            string get_username = request["username"].ToString();
            string get_password = request["password"].ToString();

            if (get_username == username && get_password == password)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, username) };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveSecreta));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.Now.AddYears(999),
                    signingCredentials: creds
                );
                string str_token = new JwtSecurityTokenHandler().WriteToken(token);

                var query = await _rolQueryService.AddToken(email, str_token);

                if (query.Message != string.Empty)
                {
                    return BadRequest(query.Message);
                }
                else
                {
                    var response = new
                    {
                        token = str_token,
                        expiration = token.ValidTo,
                    };

                    return Ok(response);
                }
            }
            else
            {
                return Ok(new { error = "Credenciales inválidas.", message = "Usuario y/o contraseña incorrectas." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            JsonObject registro = new JsonObject();
            IHeaderDictionary headers = HttpContext.Request.Headers;
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string token = headers["token"];
            string email = headers["email"];

            if (configuration["AppSettings:environment"] == "prod")
            {
                string bd_token = await _rolQueryService.GetAuthorization(email);
                if (bd_token != token)
                    return BadRequest(new { error = "Credenciales inválidas.", message = "Usuario y/o contraseña incorrectas." });
            }

            var query = await _rolQueryService.GetRoles(email);
            if (query.Message.IsNullOrEmpty()) return Ok(query);
            else return BadRequest(query.Message);
        }
    }
}
