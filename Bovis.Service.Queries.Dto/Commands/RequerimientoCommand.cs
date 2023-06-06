using Bovis.Common.Model.NoTable;
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
    public class AddRequerimientoCommand : IRequest<Response<bool>>
    {
        [Required(ErrorMessage = "El campo Categoria es requerido")]
        public int categoria { get; set; }
    }
}
