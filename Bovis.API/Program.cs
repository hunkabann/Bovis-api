using Bovis.API.Helper;
using Bovis.Business;
using Bovis.Business.Interface;
using Bovis.Common.Mapper;
using Bovis.Data;
using Bovis.Data.Connection;
using Bovis.Data.Interface;
using Bovis.Service.Queries;
using Bovis.Service.Queries.Interface;
using LinqToDB.Data;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System.Reflection;
using System.Text;

Log.Logger = new LoggerConfiguration()
	.Enrich.FromLogContext()
	.WriteTo.Console()
	.WriteTo.File(new JsonFormatter(),
		"important-logs.json",
		restrictedToMinimumLevel: LogEventLevel.Error)
	.WriteTo.File(@"D:\home\LogFiles\http\RawLogs\LogBoviApi-.log", rollingInterval: RollingInterval.Day)
	.MinimumLevel.Debug()
	.CreateLogger();

DataConnection.DefaultSettings = new DBSettings();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddMediatR(Assembly.Load("Bovis.Service.EventHandlers"));
builder.Services.AddControllers(options =>
{
	options.Filters.Add(typeof(GlobalExceptionFilter));
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bovis.Api", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(MappingsProfile));
builder.Services.AddCors(options => options.AddPolicy("policyAPI",
				builder => builder.WithOrigins("*")
								  .AllowAnyHeader()
								  .AllowAnyMethod()));

builder.Services.AddScoped<IAuditoriaQueryService, AuditoriaQueryService>();
builder.Services.AddScoped<IAuditoriaBusiness, AuditoriaBusiness>();
builder.Services.AddScoped<IAuditoriaData, AuditoriaData>();

builder.Services.AddScoped<IAutorizacionQueryService, AutorizacionQueryService>();
builder.Services.AddScoped<IAutorizacionBusiness, AutorizacionBusiness>();
builder.Services.AddScoped<IAutorizacionData, AutorizacionData>();

builder.Services.AddScoped<IBeneficiosQueryService, BeneficiosQueryService>();
builder.Services.AddScoped<IBeneficiosBusiness, BeneficiosBusiness>();
builder.Services.AddScoped<IBeneficiosData, BeneficiosData>(); 

builder.Services.AddScoped<ICatalogoQueryService, CatalogoQueryService>();
builder.Services.AddScoped<ICatalogoBusiness, CatalogoBusiness>();
builder.Services.AddScoped<ICatalogoData, CatalogoData>();
builder.Services.AddScoped<ITransactionData, TransactionData>();

builder.Services.AddScoped<ICieQueryService, CieQueryService>();
builder.Services.AddScoped<ICieBusiness, CieBusiness>();
builder.Services.AddScoped<ICieData, CieData>();

builder.Services.AddScoped<IContratoQueryService, ContratoQueryService>();
builder.Services.AddScoped<IContratoBusiness, ContratoBusiness>();
builder.Services.AddScoped<IContratoData, ContratoData>();

builder.Services.AddScoped<ICostoQueryService, CostoQueryService>();
builder.Services.AddScoped<ICostoBusiness, CostoBusiness>();
builder.Services.AddScoped<ICostoData, CostoData>();

builder.Services.AddScoped<IDorQueryService, DorQueryService>();
builder.Services.AddScoped<IDorBusiness, DorBusiness>();
builder.Services.AddScoped<IDorData, DorData>();

builder.Services.AddScoped<IEmpleadoQueryService, EmpleadoQueryService>();
builder.Services.AddScoped<IEmpleadoBusiness, EmpleadoBusiness>();
builder.Services.AddScoped<IEmpleadoData, EmpleadoData>();

builder.Services.AddScoped<IFacturaQueryService, FacturaQueryService>();
builder.Services.AddScoped<IFacturaBusiness, FacturaBusiness>();
builder.Services.AddScoped<IFacturaData, FacturaData>();

builder.Services.AddScoped<IPcsQueryService, PcsQueryService>();
builder.Services.AddScoped<IPcsBusiness, PcsBusiness>();
builder.Services.AddScoped<IPcsData, PcsData>();

builder.Services.AddScoped<IPersonaQueryService, PersonaQueryService>();
builder.Services.AddScoped<IPersonaBusiness, PersonaBusiness>();
builder.Services.AddScoped<IPersonaData, PersonaData>();

builder.Services.AddScoped<IReporteQueryService, ReporteQueryService>();
builder.Services.AddScoped<IReporteBusiness, ReporteBusiness>();
builder.Services.AddScoped<IReporteData, ReporteData>();

builder.Services.AddScoped<IRequerimientoQueryService, RequerimientoQueryService>();
builder.Services.AddScoped<IRequerimientoBusiness, RequerimientoBusiness>();
builder.Services.AddScoped<IRequerimientoData, RequerimientoData>();

builder.Services.AddScoped<IRolQueryService, RolQueryService>();
builder.Services.AddScoped<IRolBusiness, RolBusiness>();
builder.Services.AddScoped<IRolData, RolData>();

builder.Services.AddScoped<ITimesheetQueryService, TimesheetQueryService>();
builder.Services.AddScoped<ITimesheetBusiness, TimesheetBusiness>();
builder.Services.AddScoped<ITimesheetData, TimesheetData>();

builder.Services.AddScoped<ITokenQueryService, TokenQueryService>();
builder.Services.AddScoped<ITokenBusiness, TokenBusiness>();
builder.Services.AddScoped<ITokenData, TokenData>();

var configuration =  builder.Configuration
	.SetBasePath(Directory.GetCurrentDirectory())
	.AddJsonFile("appsettings.json")
	.Build();

string claveSecreta = configuration["AppSettings:secretKey"];
var key = Encoding.ASCII.GetBytes(claveSecreta);

builder.Services.AddAuthentication(config =>
{
	config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(config =>
{
	config.RequireHttpsMetadata = false;
	config.SaveToken = true;
	config.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = false,
		ValidateAudience = false,
		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero,
	};
});

//builder.Services.AddMicrosoftIdentityWebApiAuthentication(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bovis.Api v1"));
}

app.UseCors("policyAPI");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
