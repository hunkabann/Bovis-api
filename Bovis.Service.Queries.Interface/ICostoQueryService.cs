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
        Task<Response<List<Costo_Detalle>>> GetCostos(bool? hist, string? idEmpleado, int? idPuesto, int? idProyecto, int? idEmpresa, int? idUnidadNegocio, DateTime? FechaIni, DateTime? FechaFin); 
        Task<Response<Costo_Detalle>> GetCosto(int IdCosto, string fecha);
        Task<Response<List<Costo_Detalle>>> GetCostosEmpleado(string NumEmpleadoRrHh, string fecha, bool hist);
        Task<Response<List<Costo_Detalle>>> GetCostosEmpleadoSoloCosto(string NumEmpleadoRrHh, bool hist);//LEO Fix CostosEmpleado Seleccionar Empleado
        Task<Response<List<Costo_Detalle>>> GetCostoEmpleado(string NumEmpleadoRrHh, int anno, int mes, string fecha, bool hist);
        Task<Response<decimal>> GetCostoLaborable(string NumEmpleadoRrHh,int anno_min,int mes_min, int anno_max, int mes_max, string fecha);
        Task<Response<List<Costo_Detalle>>> GetCostosBetweenDates(string NumEmpleadoRrHh,int anno_min,int mes_min,int anno_max,int mes_max, string fecha, bool hist);
        Task<Response<TB_CostoPorEmpleado>> UpdateCostos(int costoId, CostoPorEmpleadoDTO registro);
        Task<Response<TB_CostoPorEmpleado>> UpdateCostoEmpleado(CostoPorEmpleadoDTO registro);
        Task<Response<bool>> DeleteCosto(int costoId);

        //LEO TBD
        Task<Response<List<Costo_Detalle>>> GetCostosEmpleadoPuesto(string NumEmpleadoRrHh, string NumPuesto, string fecha, bool hist);
    
    }
}

