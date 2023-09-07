using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Business.Interface
{
	public interface IFacturaBusiness : IDisposable
	{
		Task<BaseCFDI?> ExtraerDatos(string xml);
        Task<List<FacturaRevision>> AddFacturas(AgregarFactura request);
        Task<List<FacturaRevision>> AddNotasCredito(AgregarNotaCredito request);
        Task<List<FacturaRevision>> AddPagos(AgregarPagos request);
        Task<(bool Success, string Message)> CancelFactura(InsertMovApi MovAPI, CancelarFactura factura);
        Task<Factura_Proyecto> GetInfoProyecto(int numProyecto);
        //Task<(TB_ProyectoFactura factura, List<TB_Proyecto_Factura_Nota_Credito>? nc, List<TB_Proyecto_Factura_Cobranza>? crp)> Search();
        Task<List<FacturaDetalles>> Search(int? idProyecto, int? idCliente, int? idEmpresa, DateTime? fechaIni, DateTime? fechaFin, string? noFactura);
        Task<(bool Success, string Message)> CancelNota(JsonObject registro);
        Task<(bool Success, string Message)> CancelCobranza(JsonObject registro);

    }
}
