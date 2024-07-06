using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Business
{
    public class BeneficiosBusiness : IBeneficiosBusiness
    {
        private readonly IBeneficiosData _beneficiosData;

        public BeneficiosBusiness(IBeneficiosData beneficiosData)
        {
            _beneficiosData = beneficiosData;
        }

        public async Task<Response<object>> AddBeneficio(TB_EmpleadoBeneficio registro)
        {
            var response = await _beneficiosData.AddBeneficio(registro);
            return response;

        }
        public async Task<Response<List<TB_EmpleadoBeneficio>>> GetBeneficios(string NumEmpleado)
        {
            var response = await _beneficiosData.GetBeneficios(NumEmpleado); 
            return response;
        }
        public async Task<Response<List<TB_EmpleadoBeneficio>>> GetBeneficio(int idBeneficio, string NumEmpleado, int Mes, int Anno)
        {
            var response = await _beneficiosData.GetBeneficio(idBeneficio, NumEmpleado, Mes, Anno);
            return response;
        }

       

        public async Task<Response<int>> UpdateBeneficio(TB_EmpleadoBeneficio registro, int idBeneficio, string numEmpleado)
        {
            var response = await _beneficiosData.UpdateBeneficio(registro, idBeneficio, numEmpleado);
            return response;
        }

        public async Task<Response<int>> UpdateBeneficioProyecto(TB_EmpleadoProyectoBeneficio registro, int idBeneficio, string numEmpleado)
        {
            var response = await _beneficiosData.UpdateBeneficioProyecto(registro, idBeneficio, numEmpleado);
            return response;
        }

        public async Task<Response<object>> AddBeneficioProyecto(TB_EmpleadoProyectoBeneficio registro)
        {
            var response = await _beneficiosData.AddBeneficioProyecto(registro);
            return response;

        }
    }
}
