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
using System.Net;
using System.Net.Mail;

namespace Bovis.API.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        #region base
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<EmailController> _logger;
        private readonly IRolQueryService _rolQueryService;
        private readonly IMediator _mediator;

        public EmailController(ILogger<EmailController> logger, IRolQueryService _rolQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._rolQueryService = _rolQueryService;
            this._mediator = _mediator;
        }
        #endregion base

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] JsonObject request)
        {
            IHeaderDictionary headers = HttpContext.Request.Headers;
            string nombre = headers["nombre"];
            string email = headers["email"];

            string subject = request["subject"].ToString();
            string body = request["body"].ToString();
            JsonArray emailsTo = request["emailsTo"].AsArray();

            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                string smtpServer = configuration["EmailSettings:smtpServer"];
                int smtpPort = Convert.ToInt32(configuration["EmailSettings:smtpPort"]);
                string username = configuration["EmailSettings:username"];
                string password = configuration["EmailSettings:password"];
                bool enableSSL = Convert.ToBoolean(configuration["EmailSettings:enableSSL"]);
                string senderEmailAddress = configuration["EmailSettings:senderEmailAddress"];


                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
                {
                    EnableSsl = true
                };
                smtpClient.Credentials = new NetworkCredential(username, password);
                smtpClient.EnableSsl = enableSSL;
                smtpClient.Timeout = 5000;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                MailMessage mailMessage = new MailMessage();
                mailMessage.Priority = MailPriority.Normal;
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.UTF8;
                mailMessage.SubjectEncoding = Encoding.UTF8;
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.From = new MailAddress(senderEmailAddress);

                foreach (string emailTo in emailsTo)
                {
                    mailMessage.To.Add(new MailAddress(emailTo));
                }

                await smtpClient.SendMailAsync(mailMessage);

                return Ok("Email enviado satisfactoriamente.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
