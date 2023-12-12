using Bovis.Common;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface IBeneficiosBusiness
    {
        Task<Response<object>> AddBeneficio(TB_EmpleadoBeneficio regsitro);
        Task<Response<List<TB_EmpleadoBeneficio>>> GetBeneficios(int NumEmpleado);
        Task<Response<List<TB_EmpleadoBeneficio>>> GetBeneficio(int idBeneficio, int NumEmpleado, int Mes, int Anno);
        Task<Response<int>> UpdateBeneficio(TB_EmpleadoBeneficio registro, int idBeneficio, int numEmpleado);
    }
}
