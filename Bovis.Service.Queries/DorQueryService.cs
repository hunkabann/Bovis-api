using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;

namespace Bovis.Service.Queries
{
    public class DorQueryService : IDorQueryService
    {
        private readonly IDorBusiness _dorBusiness;

        private readonly IMapper _map;

        public DorQueryService(IMapper _map, IDorBusiness _dorBusiness)
        {
            this._map = _map;
            this._dorBusiness = _dorBusiness;
        }

        public async Task<Response<DorEmpleadoCorreo>> GetDorEjecutivoCorreo(string email)
        {
            var response = await _dorBusiness.GetDorEjecutivoCorreo(email);
            return new Response<DorEmpleadoCorreo> { Data = _map.Map<DorEmpleadoCorreo>(response), Success = response is not null ? true : default, Message = response is null ? "Mo se encontró información para el usuario solicitado." : default };
        }

        public async Task<Response<DorSubordinado>> GetDorEmpleadoCorreo(string email)
        {
            var response = await _dorBusiness.GetDorEmpleadoCorreo(email);
            return new Response<DorSubordinado> { Data = _map.Map<DorSubordinado>(response), Success = response is not null ? true : default, Message = response is null ? "Mo se encontró información para el usuario solicitado." : default };
        }

        public async Task<Response<List<DorSubordinado>>> GetDorListaSubordinados(string name)
        {
            var response = await _dorBusiness.GetDorListaSubordinados(name);
            return new Response<List<DorSubordinado>> { Data = _map.Map<List<DorSubordinado>>(response), Success = response is not null ? true : default, Message = response is null ? "Mo se encontró información para el usuario solicitado." : default };
        }
        public async Task<Response<List<DorObjetivoGeneral>>> GetDorObjetivosGenerales(string nivel, string unidadNegocio)
        {
            var response = await _dorBusiness.GetDorObjetivosGenerales(nivel, unidadNegocio);
            return new Response<List<DorObjetivoGeneral>> { Data = _map.Map<List<DorObjetivoGeneral>>(response), Success = response is not null ? true : default, Message = response is null ? "Mo se encontró información para el nivel solicitado." : default };
        }
        public async Task<Response<List<DorObjetivoGeneral>>> GetDorGpmProyecto(int proyecto)
        {
            var response = await _dorBusiness.GetDorGpmProyecto(proyecto);
            return new Response<List<DorObjetivoGeneral>> { Data = _map.Map<List<DorObjetivoGeneral>>(response), Success = response is not null ? true : default, Message = response is null ? "Mo se encontró información para el proyecto solicitado." : default };
        }
        public async Task<Response<List<DorObjetivoGeneral>>> GetDorMetasProyecto(int proyecto,int nivel)
        {
            var response = await _dorBusiness.GetDorMetasProyecto(proyecto,nivel);
            return new Response<List<DorObjetivoGeneral>> { Data = _map.Map<List<DorObjetivoGeneral>>(response), Success = response is not null ? true : default, Message = response is null ? "Mo se encontró información para el proyecto solicitado." : default };
        }

        public async Task<Response<List<DorObjetivoDesepeno>>> GetDorObjetivoDesepeno(int anio, int proyecto, int empleado, int nivel, int? acepto, int mes)
        {
            var response = await _dorBusiness.GetDorObjetivosDesepeno(anio, proyecto, empleado, nivel, acepto, mes);
            return new Response<List<DorObjetivoDesepeno>> { Data = _map.Map<List<DorObjetivoDesepeno>>(response), Success = response is not null ? true : default, Message = response is null ? "Mo se encontró información para el usuario solicitado." : default };
        }

        //      public async Task<Response<List<DorObjetivoDesepeno>>> GetDorObjetivoDesepeno(int anio, int proyecto, string concepto, int? empleado)
        //{
        //	var response = await _dorBusiness.GetDorObjetivosDesepeno(anio, proyecto, concepto, empleado);
        //	return new Response<List<DorObjetivoDesepeno>> { Data = _map.Map<List<DorObjetivoDesepeno>>(response), Success = response is not null ? true : default, Message = response is null ? "Mo se encontró información para el usuario solicitado." : default };
        //}

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
    }
}
