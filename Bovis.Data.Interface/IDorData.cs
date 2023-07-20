using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;

namespace Bovis.Data.Interface;

public interface IDorData : IDisposable
{
    Task<DOR_Empleados?> GetDorEjecutivoCorreo(string email);
    Task<Dor_Subordinados?> GetDorEmpleadoCorreo(string email);
    Task<List<Dor_Subordinados>> GetDorListaSubordinados(string name);
    Task<List<Dor_ObjetivosGenerales>> GetDorObjetivosGenerales(string nivel, string unidadNegocio, int mes, string seccion);
    //Task<List<DOR_ObjetivosDesepeno>> GetDorObjetivosDesepeno(int anio, int proyecto, string concepto, int? empleado);
    Task<List<Dor_ObjetivosEmpleado>> GetDorObjetivosDesepeno(int anio, int proyecto, int empleado, int nivel, int? acepto, int mes);
    Task<(bool existe, string mensaje)> AddObjetivo(DOR_ObjetivosDesepeno objetivo);
    Task<(bool existe, string mensaje)> UpdObjetivo(DOR_ObjetivosDesepeno objetivo);
    Task<List<Dor_ObjetivosGenerales>> GetDorGpmProyecto(int proyecto);
    Task<List<Dor_ObjetivosGenerales>> GetDorMetasProyecto(int proyecto, int nivel, int mes, string seccion);
}


