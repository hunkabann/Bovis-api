using Bovis.Common;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface ICostoBusiness : IDisposable
    {
        Task<Response<decimal>> AddCosto(CostoPorEmpleadoDTO source);
        Task<List<TB_Costo_Por_Empleado>> GetCostos(bool hist);
        Task<TB_Costo_Por_Empleado> GetCosto(int IdCosto);
        Task<Response<List<TB_Costo_Por_Empleado>>> GetCostosEmpleado(int NumEmpleadoRrHh, bool hist);
        Task<Response<List<TB_Costo_Por_Empleado>>> GetCostoEmpleado(int NumEmpleadoRrHh, int año, int mes, bool hist);
        Task<Response<decimal>> GetCostoLaborable(int NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max);
        Task<Response<List<TB_Costo_Por_Empleado>>> GetCostosBetweenDates(int NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max, bool hist);
        Task<Response<TB_Costo_Por_Empleado>> UpdateCostos(int costoId, CostoPorEmpleadoDTO registro);
        Task<Response<bool>> DeleteCosto(int costoId);


    }
}
