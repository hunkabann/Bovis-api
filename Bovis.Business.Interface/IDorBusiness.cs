using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Common;

namespace Bovis.Business.Interface
{
    public interface IDorBusiness : IDisposable
    {
        Task<DOR_Empleados?> GetDorEjecutivoCorreo(string email);
        Task<Dor_Subordinados?> GetDorEmpleadoCorreo(string email);
        Task<List<Dor_Subordinados>> GetDorListaSubordinados(string name);
        Task<List<Dor_ObjetivosGenerales>> GetDorObjetivosGenerales(string nivel, string unidadNegocio, int mes);

        //Task<List<DOR_ObjetivosDesepeno>> GetDorObjetivosDesepeno(int anio, int proyecto, string concepto, int? empleado);
        Task<List<Dor_ObjetivosEmpleado>> GetDorObjetivosDesepeno(int anio, int proyecto, int empleado,int nivel, int? acepto, int mes);
        Task<(bool Success, string Message)> AddDorObjetivo(DOR_ObjetivosDesepeno objetivo);
        Task<(bool Success, string Message)> UpdDorObjetivo(DOR_ObjetivosDesepeno objetivo);
        Task<List<Dor_ObjetivosGenerales>> GetDorGpmProyecto(int proyecto);
        Task<List<Dor_ObjetivosGenerales>> GetDorMetasProyecto(int proyecto, int nivel, int mes);
    }
}
