using Bovis.Common.Model.NoTable;
using Bovis.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.DTO;

namespace Bovis.Service.Queries.Interface
{
    public interface ICostoQueryService
    {
        Task<Response<decimal>> AddCosto(CostoPorEmpleadoDTO source);
        Task<Response<List<Costo_Detalle>>> GetCostos(bool? hist); 
        Task<Response<TB_CostoPorEmpleado>> GetCosto(int IdCosto);
        Task<Response<List<TB_CostoPorEmpleado>>> GetCostosEmpleado(string NumEmpleadoRrHh, bool hist);
        Task<Response<List<TB_CostoPorEmpleado>>> GetCostoEmpleado(string NumEmpleadoRrHh, int anno, int mes, bool hist);
        Task<Response<decimal>> GetCostoLaborable(string NumEmpleadoRrHh,int anno_min,int mes_min, int anno_max, int mes_max);
        Task<Response<List<TB_CostoPorEmpleado>>> GetCostosBetweenDates(string NumEmpleadoRrHh,int anno_min,int mes_min,int anno_max,int mes_max, bool hist);
        Task<Response<TB_CostoPorEmpleado>> UpdateCostos(int costoId, CostoPorEmpleadoDTO registro);
        Task<Response<bool>> DeleteCosto(int costoId);
        

    }
}

