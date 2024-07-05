using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bovis.Common;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.Tables;

namespace Bovis.Data.Interface
{
    public interface IBeneficiosData :IDisposable
    {
        Task<Response<object>> AddBeneficio(TB_EmpleadoBeneficio registro);
        Task<Response<List<TB_EmpleadoBeneficio>>> GetBeneficios(string NumEmpleado);
        Task<Response<List<TB_EmpleadoBeneficio>>> GetBeneficio(int idBeneficio, string NumEmpleado, int Mes, int Anno);
        Task<Response<int>> UpdateBeneficio(TB_EmpleadoBeneficio registro, int idBeneficio, string numEmpleado);
        Task<Response<int>> UpdateBeneficioProyecto(TB_EmpleadoProyectoBeneficio registro, int idBeneficio, string numEmpleado);
        Task<Response<object>> AddBeneficioProyecto(TB_EmpleadoProyectoBeneficio registro);


        

    }
}
