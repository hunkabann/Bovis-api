using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Responses;
using System.Text.Json.Nodes;

namespace Bovis.Service.Queries.Interface
{
    public interface IDorQueryService : IDisposable
    {
        Task<Response<DorEmpleadoCorreo>> GetDorEjecutivoCorreo(string email);
        Task<Response<DorSubordinado>> GetDorEmpleadoCorreo(string email);
        Task<Response<List<DorSubordinado>>> GetDorListaSubordinados(string name);
        Task<Response<List<Dor_ObjetivosGenerales>>> GetDorObjetivosGenerales(int nivel, string unidadNegocio, int mes, string seccion);
        //Task<Response<List<DorObjetivoDesepeno>>> GetDorObjetivoDesepeno(int anio, int proyecto, string concepto, int? empleado);
        Task<Response<List<DorObjetivoDesepeno>>> GetDorObjetivoDesepeno(int anio, int proyecto, int empleado, int nivel, int? acepto, int mes);
        Task<Response<List<DorObjetivoGeneral>>> GetDorGpmProyecto(int proyecto);
        Task<Response<List<Dor_ObjetivosGenerales>>> GetDorMetasProyecto(int proyecto, int nivel, int mes, string seccion);
        Task<Response<(bool existe, string mensaje)>> UpdateReal(JsonObject registro);
    }
}
