using Bovis.Common;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface ICostoData : IDisposable
    {
        Task<Response<decimal>> AddCosto(TB_CostoPorEmpleado registro);
        Task<List<CostoEmpleado_Detalle>> GetCostos(bool? hist);
        Task<TB_CostoPorEmpleado> GetCosto(int IdCosto);
        Task<Response<List<TB_CostoPorEmpleado>>> GetCostosEmpleado(int NumEmpleadoRrHh, bool hist);
        Task<Response<List<TB_CostoPorEmpleado>>> GetCostoEmpleado(int NumEmpleadoRrHh, int anno, int mes, bool hist);
        Task<Response<decimal>> GetCostoLaborable(int NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max);
        Task<Response<List<TB_CostoPorEmpleado>>> GetCostosBetweenDates(int NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max, bool hist);
        Task<Response<TB_CostoPorEmpleado>> UpdateCostos(int costoId, TB_CostoPorEmpleado registro);
        Task<Response<bool>> DeleteCosto(int costoId);


    }
}