using Bovis.Common;
using LinqToDB.Mapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace Bovis.Service.Queries.Dto.Commands
{
    public class RegistroCommand : UpdateBaseCommand, IRequest<Response<bool>>
    { 
        [Required(ErrorMessage = "Es requerido el cuerpo de la solicitud")]
        [JsonIgnore]
        public JsonObject registro { get; set; }
    }
}
