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
        Task<List<Costo_Detalle>> GetCostos(bool? hist, string? idEmpleado, int? idPuesto, int? idProyecto, int? idEmpresa, int? idUnidadNegocio);
        Task<Costo_Detalle> GetCosto(int IdCosto);
        Task<Response<List<Costo_Detalle>>> GetCostosEmpleado(string NumEmpleadoRrHh, bool hist);
        Task<Response<List<Costo_Detalle>>> GetCostoEmpleado(string NumEmpleadoRrHh, int año, int mes, bool hist);
        Task<Response<decimal>> GetCostoLaborable(string NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max);
        Task<Response<List<Costo_Detalle>>> GetCostosBetweenDates(string NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max, bool hist);
        Task<Response<TB_CostoPorEmpleado>> UpdateCostos(int costoId, CostoPorEmpleadoDTO registro);
        Task<Response<TB_CostoPorEmpleado>> UpdateCostoEmpleado(CostoPorEmpleadoDTO registro);
        Task<Response<bool>> DeleteCosto(int costoId);


    }
}
