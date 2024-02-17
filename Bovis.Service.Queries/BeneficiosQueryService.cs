using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class BeneficiosQueryService : IBeneficiosQueryService
    {
        private readonly IBeneficiosBusiness _beneficiosBusiness;

        public BeneficiosQueryService(IBeneficiosBusiness beneficiosBusiness)
        {
            _beneficiosBusiness = beneficiosBusiness;
            
        }
        public async Task<Response<object>> AddBeneficio(TB_EmpleadoBeneficio registro)
        {
            var response = await _beneficiosBusiness.AddBeneficio(registro);
            return response;
        }
        public async Task<Response<List<TB_EmpleadoBeneficio>>> GetBeneficios(string NumEmpleado)
        {
            var response = await _beneficiosBusiness.GetBeneficios(NumEmpleado);
            return response;
        }
        public async Task<Response<List<TB_EmpleadoBeneficio>>> GetBeneficio(int idBeneficio, string NumEmpleado, int Mes, int Anno)
        {
            var response = await _beneficiosBusiness.GetBeneficio(idBeneficio, NumEmpleado, Mes, Anno);
            return response;
        }

        public async Task<Response<int>> UpdateBeneficio(TB_EmpleadoBeneficio registro, int idBeneficio, string numEmpleado)
        {
            var response = await _beneficiosBusiness.UpdateBeneficio(registro, idBeneficio, numEmpleado);
            return response;
        }

        


    }
}
