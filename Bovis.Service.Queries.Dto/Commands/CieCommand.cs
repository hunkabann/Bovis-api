﻿using Bovis.Common.Model.NoTable;
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
    public class AddCieCommand : IRequest<Response<bool>>
    {
        [Required(ErrorMessage = "El campo NumProyecto es requerido")]
        public int nunum_proyecto { get; set; }
    }
}
