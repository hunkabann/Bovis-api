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
        Task<List<Costo_Detalle>> GetCostos(bool? hist, string? idEmpleado, int? idPuesto, int? idProyecto, int? idEmpresa, int? idUnidadNegocio, string? FechaIni, string? FechaFin);
        Task<Costo_Detalle> GetCosto(int IdCosto);
        Task<Response<List<Costo_Detalle>>> GetCostosEmpleado(string NumEmpleadoRrHh, bool hist);
        Task<Response<List<Costo_Detalle>>> GetCostoEmpleado(string NumEmpleadoRrHh, int anno, int mes, bool hist);
        Task<Response<decimal>> GetCostoLaborable(string NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max);
        Task<Response<List<Costo_Detalle>>> GetCostosBetweenDates(string NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max, bool hist);
        //ATC
        //Task<Response<TB_CostoPorEmpleado>> UpdateCostos(CostoPorEmpleadoDTO source, int costoId, TB_CostoPorEmpleado registro); 
        Task<Response<TB_CostoPorEmpleado>> UpdateCostos(CostoPorEmpleadoDTO source,int costoId, TB_CostoPorEmpleado registro);
        Task<Response<TB_CostoPorEmpleado>> UpdateCostoEmpleado(TB_CostoPorEmpleado registro);
        Task<Response<bool>> DeleteCosto(int costoId);

        Task<TB_Empleado> GetEmpleado(string numEmpleadoRrHh);
        Task<TB_Proyecto> GetProyecto(int numProyecto);
    }                                                         
}