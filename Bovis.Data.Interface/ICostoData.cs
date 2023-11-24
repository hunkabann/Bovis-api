using Bovis.Common;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface ICostoData : IDisposable
    {
        Task<Response<decimal>> AddCosto(TB_Costo_Por_Empleado registro);
        Task<List<TB_Costo_Por_Empleado>> GetCostos(bool hist);
        Task<TB_Costo_Por_Empleado> GetCosto(int IdCosto);
        Task<Response<List<TB_Costo_Por_Empleado>>> GetCostosEmpleado(int NumEmpleadoRrHh, bool hist);
        Task<Response<List<TB_Costo_Por_Empleado>>> GetCostoEmpleado(int NumEmpleadoRrHh, int anno, int mes, bool hist);
        Task<Response<decimal>> GetCostoLaborable(int NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max);
        Task<Response<List<TB_Costo_Por_Empleado>>> GetCostosBetweenDates(int NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max, bool hist);
        Task<Response<TB_Costo_Por_Empleado>> UpdateCostos(int costoId, TB_Costo_Por_Empleado registro);
        Task<Response<bool>> DeleteCosto(int costoId);


    }
}