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
        Task<TB_Proyecto_Factura_Nota_Credito> SearchNotaCredito(string uuid);
        Task<TB_Proyecto_Factura_Cobranza> SearchPagos(string uuid);
        Task<(bool existe, string mensaje)> AddNotaCredito(TB_Proyecto_Factura_Nota_Credito notaCredito);
        Task<(bool existe, string mensaje)> AddPagos(TB_Proyecto_Factura_Cobranza pagos);
        Task<(bool existe, string mensaje)> CancelFactura(TB_ProyectoFactura factura);
        Task<List<FacturaDetalles>> GetAllFacturas();
        Task<List<FacturaDetalles>> GetFacturasProyecto(int? idProyecto);
        Task<List<FacturaDetalles>> GetFacturasProyectoFecha(int? idProyecto,DateTime? fechaIni, DateTime? fechaFin);
        Task<List<FacturaDetalles>> GetFacturasEmpresa(int? idEmpresa);
        Task<List<FacturaDetalles>> GetFacturasEmpresaFecha(int? idEmpresa, DateTime? fechaIni, DateTime? fechaFin);
        Task<List<FacturaDetalles>> GetFacturasCliente(int? idCliente);
        Task<List<FacturaDetalles>> GetFacturasClienteFecha(int? idCliente, DateTime? fechaIni, DateTime? fechaFin);
        Task<(bool existe, string mensaje)> CancelNota(JsonObject registro);
        Task<(bool existe, string mensaje)> CancelCobranza(JsonObject registro);
    }
}
