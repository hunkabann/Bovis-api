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
        [HttpGet]
        public async Task<IActionResult> GetCostos([FromQuery] bool hist = false)
        {
            var query = await _costoQueryService.GetCostos(hist);
            return Ok(query);
        }
        #endregion

        #region GetCosto
        [HttpGet("{IdCosto}")]
        public async Task<IActionResult> GetCosto(int IdCosto)
        {
            var query = await _costoQueryService.GetCosto(IdCosto);
            return Ok(query);
        }
        #endregion

        #region GetCostosEmpleado
        [HttpGet("Empleado/{NumEmpleadoRrHh:int}")]
        public async Task<IActionResult> GetCostosEmpleado(int NumEmpleadoRrHh, [FromQuery] bool hist = false)
        {
            var query = await _costoQueryService.GetCostosEmpleado(NumEmpleadoRrHh, hist);
            return Ok(query);
        }
        #endregion

        #region GetCostoEmpleado
        [HttpGet("Empleado/{NumEmpleadoRrHh:int}/{anno:int}/{mes:int}")]
        public async Task<IActionResult> GetCostoEmpleado(int NumEmpleadoRrHh, int anno, int mes, bool hist = false)
        {
            var query = await _costoQueryService.GetCostoEmpleado(NumEmpleadoRrHh, anno, mes, hist);
            return Ok(query);
        }
        #endregion

        #region GetCostosBetweenDates
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Empleado/{NumEmpleadoRrHh:int}/{anno_min:int}/{mes_min:int}/{anno_max:int}/{mes_max:int}")]
        public async Task<IActionResult> GetCostosBetweenDates([FromRoute] int NumEmpleadoRrHh, [FromRoute] int anno_min, [FromRoute] int mes_min, [FromRoute] int anno_max, [FromRoute] int mes_max, [FromQuery] bool hist=false)
        {
            var respuesta = await _costoQueryService.GetCostosBetweenDates(NumEmpleadoRrHh, anno_min, mes_min, anno_max, mes_max, hist);

            return Ok(respuesta.Data);
        }
        #endregion

        #region GetCostoLaborable
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("Empleado/tlc/{NumEmpleadoRrHh:int}/{anno_min:int}/{mes_min:int}/{anno_max:int}/{mes_max:int}")]
        public async Task<IActionResult> GetCostoLaborable([FromRoute] int NumEmpleadoRrHh, [FromRoute] int anno_min, [FromRoute] int mes_min, [FromRoute] int anno_max, [FromRoute] int mes_max)
        {
            var respuesta = await _costoQueryService.GetCostoLaborable(NumEmpleadoRrHh, anno_min, mes_min, anno_max, mes_max);

            return Ok(respuesta.Data);
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
