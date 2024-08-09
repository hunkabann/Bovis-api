using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bovis.Service.Queries.Interface;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.Tables;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Bovis.API.Controllers
{
    [Authorize]
    [ApiController, Route("api/[controller]")]
    public class BeneficiosController : ControllerBase
    {
        private readonly ILogger<BeneficiosController> _logger;
        private readonly IMapper _mapper;
        private readonly IBeneficiosQueryService _beneficiosQueryService; 
        public BeneficiosController(ILogger<BeneficiosController> logger, IMapper mapper, IBeneficiosQueryService beneficiosQueryService)
        {
            _beneficiosQueryService = beneficiosQueryService;
            _logger = logger;
            _mapper = mapper;
            
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> AddBeneficio([FromBody] EmpleadoBeneficioDTO source)
        {
            TB_EmpleadoBeneficio registro = new();
            registro = _mapper.Map<TB_EmpleadoBeneficio>(source); // Mapeo 
            var resultado = await _beneficiosQueryService.AddBeneficio(registro);
            return Ok(resultado);
        }

        [HttpGet ("{numEmpleado:int}")]
        public async Task<IActionResult> GetBeneficios([FromRoute] string numEmpleado)
        {
            var resultado = await _beneficiosQueryService.GetBeneficios(numEmpleado);
            return Ok(resultado);
        }

        [HttpGet ("{idBeneficio:int}/{numEmpleado:int}")]
        public async Task<IActionResult> GetBeneficio([FromRoute] int idBeneficio, [FromRoute] string numEmpleado, [FromQuery] int mes, [FromQuery] int anno)
        {
            var resultado = await _beneficiosQueryService.GetBeneficio(idBeneficio, numEmpleado, mes, anno);
            return Ok(resultado);
        }

        [HttpPut ("{idBeneficio:int}/{numEmpleado:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBeneficio([FromBody] EmpleadoBeneficioDTO source, [FromRoute] int idBeneficio, [FromRoute] string numEmpleado)
        {
            TB_EmpleadoBeneficio registro = new();
            registro = _mapper.Map<TB_EmpleadoBeneficio>(source); 
            var resultado = await _beneficiosQueryService.UpdateBeneficio(registro, idBeneficio, numEmpleado);
            return Ok(resultado);

        }

        [HttpPut("{idBeneficio:int}/{numEmpleado:int}/0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBeneficioProyecto([FromBody] EmpleadoProyectoBeneficioDTO source, [FromRoute] int idBeneficio, [FromRoute] string numEmpleado)
        {
            TB_EmpleadoProyectoBeneficio registro = new();
            registro = _mapper.Map<TB_EmpleadoProyectoBeneficio>(source);
            var resultado = await _beneficiosQueryService.UpdateBeneficioProyecto(registro, idBeneficio, numEmpleado);
            return Ok(resultado);

        }

        [HttpPost("0")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> AddBeneficioProyecto([FromBody] EmpleadoProyectoBeneficioDTO source)
        {
            TB_EmpleadoProyectoBeneficio registro = new();
            registro = _mapper.Map<TB_EmpleadoProyectoBeneficio>(source); // Mapeo 
            var resultado = await _beneficiosQueryService.AddBeneficioProyecto(registro);
            return Ok(resultado);
        }


    }
}
