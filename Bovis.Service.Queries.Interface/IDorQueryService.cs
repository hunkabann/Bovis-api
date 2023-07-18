using Bovis.Common;
using Bovis.Service.Queries.Dto.Responses;

namespace Bovis.Service.Queries.Interface
{
    public interface IDorQueryService : IDisposable
    {
        Task<Response<DorEmpleadoCorreo>> GetDorEjecutivoCorreo(string email);
        Task<Response<DorSubordinado>> GetDorEmpleadoCorreo(string email);
        Task<Response<List<DorSubordinado>>> GetDorListaSubordinados(string name);
        Task<Response<List<DorObjetivoGeneral>>> GetDorObjetivosGenerales(string nivel, string unidadNegocio);
        //Task<Response<List<DorObjetivoDesepeno>>> GetDorObjetivoDesepeno(int anio, int proyecto, string concepto, int? empleado);
        Task<Response<List<DorObjetivoDesepeno>>> GetDorObjetivoDesepeno(int anio, int proyecto, int empleado, int nivel, int? acepto, int mes);
        Task<Response<List<DorObjetivoGeneral>>> GetDorGpmProyecto(int proyecto);
        Task<Response<List<DorObjetivoGeneral>>> GetDorMetasProyecto(int proyecto, int nivel);
    }
}
