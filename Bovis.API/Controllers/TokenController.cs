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
using Bovis.Common.Model;
using Microsoft.Win32;

namespace Bovis.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        #region base
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<TokenController> _logger;
        private readonly ITokenQueryService _tokenQueryService;
        private readonly IMediator _mediator;

        public TokenController(ILogger<TokenController> logger, ITokenQueryService _tokenQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._tokenQueryService = _tokenQueryService;
            this._mediator = _mediator;
        }
        #endregion base

        [HttpPost]
        public async Task<IActionResult> Token([FromBody] JsonObject request)
        {
            string email = request["email"].ToString();
            string get_username = request["username"].ToString();
            string get_password = request["password"].ToString();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string username = configuration["AppSettings:username"];
            string password = configuration["AppSettings:password"];
            string claveSecreta = configuration["AppSettings:secretKey"];

            if (get_username == username && get_password == password)
            {
                var key = Encoding.ASCII.GetBytes(claveSecreta);

                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, username));

                var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

                var token = new SecurityTokenDescriptor
                {
                    Subject = claims,                    
                    Expires = DateTimeOffset.UtcNow.AddHours(8).UtcDateTime,
                    SigningCredentials = creds
                };

                var token_handler = new JwtSecurityTokenHandler();
                var token_config = token_handler.CreateToken(token);
                string str_token = token_handler.WriteToken(token_config);

                var query = await _tokenQueryService.AddToken(email, str_token);

                if (query.Message != string.Empty)
                {
                    return BadRequest(query.Message);
                }
                else
                {
                    var response = new
                    {
                        result = true,
                        expiration = token.Expires,
                        token = str_token,
                    };

                    return Ok(response);
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}


//using (var db = new ConnectionDB(dbConfig))
//{
//    var isr_record = await (from isr in db.tB_Cat_Tabla_ISRs
//    where isr.Anio == registro.NuAnno
//                            && isr.Mes == registro.NuMes
//    && (isr.LimiteInferior <= registro.SueldoBruto && isr.LimiteSuperior >= registro.SueldoBruto)
//    select isr).FirstOrDefaultAsync();

//    if (isr_record != null)
//    {
//        registro.Ispt = ((registro.SueldoBruto - isr_record.LimiteInferior) * isr_record.PorcentajeAplicable) + isr_record.CuotaFija;
//    }
//}
