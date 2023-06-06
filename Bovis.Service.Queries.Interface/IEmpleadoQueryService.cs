using Bovis.Common;
using Bovis.Service.Queries.Dto.Responses;


namespace Bovis.Service.Queries.Interface
{
    public interface IEmpleadoQueryService
    {
        Task<Response<List<Empleado>>> GetEmpleados(bool? Activo);
        Task<Response<Empleado>> GetEmpleado(int idEmpleado);
    }
}

