using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface IFacturaData : IDisposable
    {
        Task<(bool existe, string mensaje)> AddFactura(TB_ProyectoFactura factura);
        Task<List<TB_Proyecto>> GetProyecto();
        Task<Factura_Proyecto> GetInfoProyecto(int numProyecto);
        Task<TB_ProyectoFactura> SearchFactura(string uuid);
        Task<TB_ProyectoFacturaNotaCredito> SearchNotaCredito(string uuid);
        Task<TB_ProyectoFacturaCobranza> SearchPagos(string uuid);
        Task<(bool Success, string Message)> AddNotaCredito(TB_ProyectoFacturaNotaCredito notaCredito);
        Task<(bool Success, string Message)> AddNotaCreditoSinFactura(JsonObject registro);
        Task<List<NotaCredito_Detalle>> GetNotaCreditoSinFactura(int NumProyecto, int Mes, int Anio);
        Task<(bool Success, string Message)> AddPagos(TB_ProyectoFacturaCobranza pagos);
        Task<(bool Success, string Message)> CancelFactura(TB_ProyectoFactura factura);
        Task<List<FacturaDetalles>> GetAllFacturas(int? idProyecto, int? idCliente, int? idEmpresa, DateTime? fechaIni, DateTime? fechaFin, string? noFactura);
        Task<List<FacturaDetalles>> GetFacturasProyecto(int? idProyecto);
        Task<List<FacturaDetalles>> GetFacturasProyectoFecha(int? idProyecto,DateTime? fechaIni, DateTime? fechaFin);
        Task<List<FacturaDetalles>> GetFacturasEmpresa(int? idEmpresa);
        Task<List<FacturaDetalles>> GetFacturasEmpresaFecha(int? idEmpresa, DateTime? fechaIni, DateTime? fechaFin);
        Task<List<FacturaDetalles>> GetFacturasCliente(int? idCliente);
        Task<List<FacturaDetalles>> GetFacturasClienteFecha(int? idCliente, DateTime? fechaIni, DateTime? fechaFin);
        Task<List<FacturaDetalles>> GetFacturaNumero(string? noFactura);
        Task<(bool Success, string Message)> CancelNota(JsonObject registro);
        Task<(bool Success, string Message)> CancelCobranza(JsonObject registro);
    }
}
