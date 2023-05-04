using Bovis.Common;
using Bovis.Common.Model.NoTable;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Bovis.Service.Queries.Dto.Commands
{
	public class AddFacturasCommand : IRequest<Response<List<FacturaRevision>>>
    {
		[Required(ErrorMessage = "El campo NumProyecto es requerido")]
		public int NumProyecto { get; set; }
        [Required(ErrorMessage = "El campo RfcEmisor es requerido")]
        public string RfcEmisor { get; set; }
        [Required(ErrorMessage = "El campo RfcReceptor es requerido")]
        public string RfcReceptor { get; set; }

        [Required(ErrorMessage = "Es requerida la factura")]
		public List<Factura> LstFacturas { get; set; }
	}

    public class AddNotaCreditoCommand : IRequest<Response<List<FacturaRevision>>>
    {
        [Required(ErrorMessage = "Es requerida la factura")]
        public List<Factura> LstFacturas { get; set; }
    }

    public class AddPagosCommand : IRequest<Response<List<FacturaRevision>>>
    {
        [Required(ErrorMessage = "Es requerida la factura")]
        public List<Factura> LstFacturas { get; set; }
    }

    public class Factura
	{
		public string NombreFactura { get; set; }
		public string FacturaB64 { get; set; }
	}

    public class CancelFacturaCommand : UpdateBaseCommand, IRequest<Response<bool>>
    {
        [Required, Range(1, int.MaxValue, ErrorMessage = "El id debe ser mayor a 0.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo MotivoCancelacion es requerido")]
        public string MotivoCancelacion { get; set; }
    }
}
