using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class EmpleadoQueryService : IEmpleadoQueryService
    {
        #region base
        private readonly IEmpleadoBusiness _empleadoBusiness;

        private readonly IMapper _map;

        public EmpleadoQueryService(IMapper _map, IEmpleadoBusiness _empleadoBusiness)
        {
            this._map = _map;
            this._empleadoBusiness = _empleadoBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

        #region Empleados
        public async Task<Response<List<Empleado>>> GetEmpleados(bool? Activo)
        {
            var response = await _empleadoBusiness.GetEmpleados(Activo);
            return new Response<List<Empleado>> { Data = _map.Map<List<Empleado>>(response), Success = true };
        }
        #endregion Empleados
    }
}

