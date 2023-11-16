using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.Tables;

namespace Bovis.Common.Mapper
{
    //Este perfil de mapeo se utiliza en el módulo de "Costo por Empleado" para evitar que campos numerales (int y decimal) que vienen en null desde la Interfaz de usuario, interfieran con los cálculos que deben realizarse en el módulo "CostoBusinessUpdate"
    //
    public class NotNullMappingProfile : Profile
    {
        public NotNullMappingProfile()
        {
            CreateMap<CostoPorEmpleadoDTO, TB_Costo_Por_Empleado>()
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember, destMember, contest) => srcMember != null);

                });

            
        }
            
    }
}
