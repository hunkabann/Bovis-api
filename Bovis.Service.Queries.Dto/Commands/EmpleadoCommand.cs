using Bovis.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Dto.Commands
{
    public class AddEmpleadoCommand : IRequest<Response<bool>>
    {
        [Required(ErrorMessage = "El campo NumEmpleado es requerido")]
        public int nunum_empleado { get; set; }
    }
}
