using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bovis.Service.Queries.Dto.Request
{
	public class DorEmpCorreoRequest
	{
		[Required(ErrorMessage = "Se requiere el campo de correo electrónico")]
		public string email { get; set; }
	}

	public class DorEmpNombreRequest
	{
		[Required(ErrorMessage = "Se requiere el campo de nombre")]
		public string nombre { get; set; }
	}
}
