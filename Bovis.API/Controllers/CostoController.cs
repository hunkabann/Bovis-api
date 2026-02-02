using Bovis.Service.Queries;
using Bovis.Service.Queries.Dto.Commands;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Text.Json.Nodes;
using Bovis.API.Helper;
using Newtonsoft.Json;
using System.Security.Claims;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.DTO;
using Bovis.Common;

namespace Bovis.API.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class CostoController : ControllerBase
    {
        #region base
        private string TransactionId { get { return HttpContext.TraceIdentifier; } }
        private readonly ILogger<CostoController> _logger;
        private readonly ICostoQueryService _costoQueryService;
        private readonly IMediator _mediator;

        public CostoController(ILogger<CostoController> logger, ICostoQueryService _costoQueryService, IMediator _mediator)
        {
            _logger = logger;
            this._costoQueryService = _costoQueryService;
            this._mediator = _mediator;
        }
        #endregion base

        #region AddCosto
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> AddCosto([FromBody] CostoPorEmpleadoDTO source)
        {
            var resultado = await _costoQueryService.AddCosto(source);
            return Ok(resultado.Message);
        }
        #endregion

        #region GetCostos
        [HttpGet("Costos/{hist?}/{idEmpleado?}/{idPuesto?}/{idProyecto?}/{idEmpresa?}/{idUnidadNegocio?}/{FechaIni}/{FechaFin}")]
        public async Task<IActionResult> GetCostos(bool? hist, string? idEmpleado, int? idPuesto,  int? idProyecto, int? idEmpresa, int? idUnidadNegocio, DateTime? FechaIni, DateTime? FechaFin)
        {
            //bool historico = hist ?? false;
            var query = await _costoQueryService.GetCostos(hist, idEmpleado, idPuesto, idProyecto, idEmpresa, idUnidadNegocio, FechaIni, FechaFin);
            return Ok(query);
        }
        #endregion

        #region GetCosto
        [HttpGet("{IdCosto}/{fecha}")]
        public async Task<IActionResult> GetCosto(int IdCosto, string fecha)
        {
            var query = await _costoQueryService.GetCosto(IdCosto, fecha);
            return Ok(query);
        }
        #endregion

        #region GetCostosEmpleado
        [HttpGet("Empleado/{NumEmpleadoRrHh}/{fecha}")]
        public async Task<IActionResult> GetCostosEmpleado(string NumEmpleadoRrHh, string fecha, [FromQuery] bool hist = false)
        {
            var query = await _costoQueryService.GetCostosEmpleado(NumEmpleadoRrHh, fecha, hist);
            return Ok(query);
        }

        //LEO TBD
        [HttpGet("Empleado/{NumEmpleadoRrHh}/{NumPuesto}/{fecha}")]
        public async Task<IActionResult> GetCostosEmpleadoPuesto(string NumEmpleadoRrHh, string NumPuesto, string fecha, [FromQuery] bool hist = false)
        {
            var query = await _costoQueryService.GetCostosEmpleadoPuesto(NumEmpleadoRrHh, NumPuesto, fecha, hist);
            return Ok(query);
        }

        //LEO Fix CostosEmpleado Seleccionar Empleado I
        [HttpGet("Empleado/SoloCosto/{NumEmpleadoRrHh}")]
        public async Task<IActionResult> GetCostosEmpleadoSoloCosto(string NumEmpleadoRrHh, [FromQuery] bool hist = false)
        {
            var query = await _costoQueryService.GetCostosEmpleadoSoloCosto(NumEmpleadoRrHh, hist);
            return Ok(query);
        }
        //LEO Fix CostosEmpleado Seleccionar Empleado F
        #endregion

        #region GetCostoEmpleado
        [HttpGet("Empleado/{NumEmpleadoRrHh}/{anno:int}/{mes:int}/{fecha}")]
        public async Task<IActionResult> GetCostoEmpleado(string NumEmpleadoRrHh, int anno, int mes, string fecha, [FromQuery] bool hist = false)
        {
            var query = await _costoQueryService.GetCostoEmpleado(NumEmpleadoRrHh, anno, mes, fecha, hist);
            return Ok(query);
        }
        #endregion

        #region GetCostosBetweenDates
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Empleado/{NumEmpleadoRrHh}/{anno_min:int}/{mes_min:int}/{anno_max:int}/{mes_max:int}/{fecha}")]
        public async Task<IActionResult> GetCostosBetweenDates([FromRoute] string NumEmpleadoRrHh, [FromRoute] int anno_min, [FromRoute] int mes_min, [FromRoute] int anno_max, [FromRoute] int mes_max, string fecha,[FromQuery] bool hist=false)
        {
            var respuesta = await _costoQueryService.GetCostosBetweenDates(NumEmpleadoRrHh, anno_min, mes_min, anno_max, mes_max, fecha, hist);

            return Ok(respuesta.Data);
        }
        #endregion

        #region GetCostoLaborable
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Empleado/tlc/{NumEmpleadoRrHh}/{anno_min:int}/{mes_min:int}/{anno_max:int}/{mes_max:int}/{fecha}")]
        public async Task<IActionResult> GetCostoLaborable([FromRoute] string NumEmpleadoRrHh, [FromRoute] int anno_min, [FromRoute] int mes_min, [FromRoute] int anno_max, [FromRoute] int mes_max, string fecha)
        {
            var respuesta = await _costoQueryService.GetCostoLaborable(NumEmpleadoRrHh, anno_min, mes_min, anno_max, mes_max, fecha);

            return Ok(respuesta);
        }
        #endregion

        #region UpdateCostos
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut, Route("{costoId}")]
        public async Task<IActionResult> UpdateCostos([FromRoute] int costoId, [FromBody] CostoPorEmpleadoDTO source)
        {
            var result = await _costoQueryService.UpdateCostos(costoId, source); 
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPut, Route("Empleado")]
        public async Task<IActionResult> UpdateCostoEmpleado([FromBody] CostoPorEmpleadoDTO source)
        {
            var result = await _costoQueryService.UpdateCostoEmpleado(source);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }
        #endregion

        #region DeleteCosto
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete, Route("{IdCosto:int}")]
        public async Task<IActionResult> DeleteCosto([FromRoute] int IdCosto)
        {
            var resultado = await _costoQueryService.DeleteCosto(IdCosto);
            if(resultado.Success)
                return Ok(resultado.Message);
            else
                return BadRequest(resultado.Message);
        }
        #endregion


    }
}
