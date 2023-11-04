using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Tools;
using System.Text;
using System.Text.Json.Nodes;
using System.Xml;

namespace Bovis.Data
{
    public class FacturaData : RepositoryLinq2DB<ConnectionDB>, IFacturaData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public FacturaData()
        {
            this.ConfigurationDB = dbConfig;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<Factura_Proyecto> GetInfoProyecto(int numProyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {

                var res = from a in db.tB_Proyectos
                          from b in db.tB_Clientes
                          from c in db.tB_Empresas
                          where a.IdCliente == b.IdCliente &&
                            a.IdEmpresa == c.IdEmpresa &&
                            a.NumProyecto == numProyecto
                          select new Factura_Proyecto
                          {
                              NumProyecto = a.NumProyecto,
                              Nombre = a.Proyecto,
                              RfcBaseEmisor = c.Rfc,
                              RfcBaseReceptor = b.Rfc
                          };

                return await res.FirstOrDefaultAsync();
            }
        }

        public async Task<TB_ProyectoFactura> SearchFactura(string uuid)
        {
            using (var db = new ConnectionDB(dbConfig)) return await (from a in db.tB_ProyectoFacturas
                                                                      where a.Uuid == uuid
                                                                      select a).FirstOrDefaultAsync();
        }

        public async Task<TB_Proyecto_Factura_Nota_Credito> SearchNotaCredito(string uuid)
        {
            using (var db = new ConnectionDB(dbConfig)) return await (from a in db.tB_ProyectoFacturasNotaCredito
                                                                      where a.UuidNotaCredito == uuid
                                                                      select a).FirstOrDefaultAsync();
        }

        public async Task<TB_Proyecto_Factura_Cobranza> SearchPagos(string uuid)
        {
            using (var db = new ConnectionDB(dbConfig)) return await (from a in db.tB_ProyectoFacturasCobranza
                                                                      where a.UuidCobranza == uuid
                                                                      select a).FirstOrDefaultAsync();
        }

        public Task<List<TB_Proyecto>> GetProyecto() => GetAllFromEntityAsync<TB_Proyecto>();

        public async Task<(bool existe, string mensaje)> AddFactura(TB_ProyectoFactura factura)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                var inseert = await db.tB_ProyectoFacturas
                    .Value(x => x.Uuid, factura.Uuid)
                    .Value(x => x.NumProyecto, factura.NumProyecto)
                    .Value(x => x.IdTipoFactura, factura.IdTipoFactura)
                    .Value(x => x.IdMoneda, factura.IdMoneda)
                    .Value(x => x.Importe, factura.Importe)
                    .Value(x => x.Iva, factura.Iva)
                    .Value(x => x.IvaRet, factura.IvaRet)
                    .Value(x => x.Total, factura.Total)
                    .Value(x => x.Concepto, factura.Concepto)
                    .Value(x => x.Mes, factura.Mes)
                    .Value(x => x.Anio, factura.Anio)
                    .Value(x => x.FechaEmision, factura.FechaEmision)
                    .Value(x => x.NoFactura, factura.NoFactura)
                    .Value(x => x.TipoCambio, factura.TipoCambio)
                    .Value(x => x.XmlB64, factura.XmlB64)
                    .InsertAsync() > 0;

                resp.Success = inseert;
                resp.Message = inseert == default ? "Ocurrio un error al agregar la factura." : string.Empty;
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> AddNotaCredito(TB_Proyecto_Factura_Nota_Credito notaCredito)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                var inseert = await db.tB_ProyectoFacturasNotaCredito
                    .Value(x => x.IdFactura, notaCredito.IdFactura)
                    .Value(x => x.UuidNotaCredito, notaCredito.UuidNotaCredito)
                    .Value(x => x.IdMoneda, notaCredito.IdMoneda)
                    .Value(x => x.IdTipoRelacion, notaCredito.IdTipoRelacion)
                    .Value(x => x.NotaCredito, notaCredito.NotaCredito)
                    .Value(x => x.Importe, notaCredito.Importe)
                    .Value(x => x.Iva, notaCredito.Iva)
                    .Value(x => x.Total, notaCredito.Total)
                    .Value(x => x.Concepto, notaCredito.Concepto)
                    .Value(x => x.Mes, notaCredito.Mes)
                    .Value(x => x.Anio, notaCredito.Anio)
                    .Value(x => x.TipoCambio, notaCredito.TipoCambio)
                    .Value(x => x.FechaNotaCredito, notaCredito.FechaNotaCredito)
                    .Value(x => x.Xml, notaCredito.Xml)
                    .InsertAsync() > 0;

                resp.Success = inseert;
                resp.Message = inseert == default ? "Ocurrio un error al agregar la nota de credito." : string.Empty;
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> AddNotaCreditoSinFactura(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int num_proyecto = Convert.ToInt32(registro["NumProyecto"].ToString());
            var cfdi = await ExtraerDatos(registro["FacturaB64"].ToString());

            var tryDate = default(DateTime);
            DateTime.TryParse(cfdi.Fecha, out tryDate);

            using (var db = new ConnectionDB(dbConfig))
            {
                var insert = await db.tB_ProyectoFacturasNotaCredito
                    .Value(x => x.NumProyecto, num_proyecto)
                    .Value(x => x.UuidNotaCredito, cfdi.UUID)
                    .Value(x => x.IdMoneda, cfdi.Moneda)
                    .Value(x => x.IdTipoRelacion, cfdi.TipoRelacion)
                    .Value(x => x.NotaCredito, $"{cfdi.Serie ?? string.Empty}{cfdi.Folio ?? string.Empty}")
                    .Value(x => x.Importe, Convert.ToDecimal(cfdi.SubTotal ?? "-1"))
                    .Value(x => x.Iva, cfdi.TotalImpuestosTrasladados is not null ? Convert.ToDecimal(cfdi.TotalImpuestosTrasladados) : 0)
                    .Value(x => x.Total, cfdi.Total is not null ? Convert.ToDecimal(cfdi.Total) : 0)
                    .Value(x => x.Concepto, string.Join("|", cfdi.Conceptos))
                    .Value(x => x.Mes, Convert.ToByte(tryDate.Month))
                    .Value(x => x.Anio, Convert.ToInt16(tryDate.Year))
                    .Value(x => x.TipoCambio, cfdi.TipoCambio is not null ? Convert.ToDecimal(cfdi.TipoCambio) : null)
                    .Value(x => x.FechaNotaCredito, tryDate)
                    .Value(x => x.Xml, cfdi.XmlB64)
                    .InsertAsync() > 0;

                resp.Success = insert;
                resp.Message = insert == default ? "Ocurrio un error al agregar la nota de credito." : string.Empty;
            }

            return resp;
        }

        public async Task<List<NotaCredito_Detalle>> GetNotaCreditoSinFactura()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var notas_credito = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           join relaciones in db.tB_Cat_TipoRelacions on notas.IdTipoRelacion equals relaciones.IdTipoRelacion into relacionesJoin
                                           from relacionesItem in relacionesJoin.DefaultIfEmpty()
                                           join monedas in db.tB_Cat_Monedas on notas.IdMoneda equals monedas.IdMoneda into monedasJoin
                                           from monedasItem in monedasJoin.DefaultIfEmpty()
                                           join proyectos in db.tB_Proyectos on notas.NumProyecto equals proyectos.NumProyecto into proyectosJoin
                                           from proyectosItem in proyectosJoin.DefaultIfEmpty()
                                           where notas.NumProyecto != null
                                           select new NotaCredito_Detalle
                                           {
                                               nunum_proyecto = notas.NumProyecto,
                                               chproyecto = proyectosItem.Proyecto ?? string.Empty,
                                               chuuid_nota_credito = notas.UuidNotaCredito,
                                               nukidmoneda = notas.IdMoneda,
                                               chmoneda = monedasItem.Moneda ?? string.Empty,
                                               nukidtipo_relacion = notas.IdTipoRelacion,
                                               chtipo_relacion = relacionesItem.TipoRelacion ?? string.Empty,
                                               chnota_credito = notas.NotaCredito,
                                               nuimporte = notas.Importe,
                                               nuiva = notas.Iva,
                                               nutotal = notas.Total,
                                               chconcepto = notas.Concepto,
                                               numes = notas.Mes,
                                               nuanio = notas.Anio,
                                               nutipo_cambio = notas.TipoCambio,
                                               dtfecha_nota_credito = notas.FechaNotaCredito,
                                               chxml = notas.Xml,
                                               dtfecha_cancelacion = notas.FechaCancelacion,
                                               chmotivocancela = notas.MotivoCancelacion
                                           }).ToListAsync();
            
                return notas_credito;
            }
        }


        public async Task<(bool Success, string Message)> AddPagos(TB_Proyecto_Factura_Cobranza pagos)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                var inseert = await db.tB_ProyectoFacturasCobranza
                    .Value(x => x.IdFactura, pagos.IdFactura)
                    .Value(x => x.UuidCobranza, pagos.UuidCobranza)
                    .Value(x => x.IdMonedaP, pagos.IdMonedaP)
                    .Value(x => x.ImportePagado, pagos.ImportePagado)
                    .Value(x => x.ImpSaldoAnt, pagos.ImpSaldoAnt)
                    .Value(x => x.ImporteSaldoInsoluto, pagos.ImporteSaldoInsoluto)
                    .Value(x => x.IvaP, pagos.IvaP)
                    .Value(x => x.TipoCambioP, pagos.TipoCambioP)
                    .Value(x => x.FechaPago, pagos.FechaPago)
                    .Value(x => x.Xml, pagos.Xml)
                    .InsertAsync() > 0;

                resp.Success = inseert;
                resp.Message = inseert == default ? "Ocurrio un error al agregar el pago." : string.Empty;
            }

            return resp;
        }

        //public Task<bool> CancelFactura( ) => UpdateEntityAsync<TB_ProyectoFactura>(factura);

        public async Task<(bool Success, string Message)> CancelFactura(TB_ProyectoFactura factura)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (var db = new ConnectionDB(dbConfig))
            {
                var objetivoDB = await (db.tB_ProyectoFacturas.Where(x => x.Id == factura.Id)
                    .UpdateAsync(x => new TB_ProyectoFactura
                    {
                        FechaCancelacion = factura.FechaCancelacion,
                        MotivoCancelacion = factura.MotivoCancelacion
                    })) > 0;

                resp.Success = objetivoDB;
                resp.Message = objetivoDB == default ? "Ocurrio un error al cancelar registro de factura." : string.Empty;

                var notas_factura = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.FechaCancelacion == null
                                           && notas.IdFactura == factura.Id
                                           select notas).ToListAsync();

                foreach (var nota in notas_factura)
                {
                    var resp_nota = await (db.tB_ProyectoFacturasNotaCredito.Where(x => x.UuidNotaCredito == nota.UuidNotaCredito)
                            .UpdateAsync(x => new TB_Proyecto_Factura_Nota_Credito
                            {
                                FechaCancelacion = factura.FechaCancelacion,
                                MotivoCancelacion = factura.MotivoCancelacion
                            })) > 0;

                    resp.Success = resp_nota;
                    resp.Message = resp_nota == default ? "Ocurrio un error al cancelar registro de nota de factura." : string.Empty;
                }

                var cobranzas_factura = await (from cobranza in db.tB_ProyectoFacturasCobranza
                                               where cobranza.FechaCancelacion == null
                                               && cobranza.IdFactura == factura.Id
                                               select cobranza).ToListAsync();

                foreach (var cobranza in cobranzas_factura)
                {
                    var resp_cobranza = await (db.tB_ProyectoFacturasCobranza.Where(x => x.UuidCobranza == cobranza.UuidCobranza)
                            .UpdateAsync(x => new TB_Proyecto_Factura_Cobranza
                            {
                                FechaCancelacion = factura.FechaCancelacion,
                                MotivoCancelacion = factura.MotivoCancelacion
                            })) > 0;

                    resp.Success = resp_cobranza;
                    resp.Message = resp_cobranza == default ? "Ocurrio un error al cancelar registro de cobranza de factura." : string.Empty;
                }
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> CancelNota(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            string uuid_nota = registro["uuid"].ToString();
            DateTime fecha_cancelacion = Convert.ToDateTime(registro["fecha_cancelacion"].ToString());
            string motivo_cancelacion = registro["motivo_cancelacion"].ToString();

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_nota = await db.tB_ProyectoFacturasNotaCredito.Where(x => x.UuidNotaCredito == uuid_nota)
                                .UpdateAsync(x => new TB_Proyecto_Factura_Nota_Credito
                                {
                                    FechaCancelacion = fecha_cancelacion,
                                    MotivoCancelacion = motivo_cancelacion
                                }) > 0;

                resp.Success = res_update_nota;
                resp.Message = res_update_nota == default ? "Ocurrio un error al cancelar la nota." : string.Empty;
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> CancelCobranza(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            string uuid_cobranza = registro["uuid"].ToString();
            DateTime fecha_cancelacion = Convert.ToDateTime(registro["fecha_cancelacion"].ToString());
            string motivo_cancelacion = registro["motivo_cancelacion"].ToString();

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_nota = await db.tB_ProyectoFacturasCobranza.Where(x => x.UuidCobranza == uuid_cobranza)
                                .UpdateAsync(x => new TB_Proyecto_Factura_Cobranza
                                {
                                    FechaCancelacion = fecha_cancelacion,
                                    MotivoCancelacion = motivo_cancelacion
                                }) > 0;

                resp.Success = res_update_nota;
                resp.Message = res_update_nota == default ? "Ocurrio un error al cancelar la cobranza." : string.Empty;
            }

            return resp;
        }

        #region consultaFacturas
        public async Task<List<FacturaDetalles>> GetAllFacturas(int? idProyecto, int? idCliente, int? idEmpresa, DateTime? fechaIni, DateTime? fechaFin, string? noFactura)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                List<int> lstProyectosCliente = null;
                List<int> lstProyectosEmpresa = null;

                if (idCliente != null)
                {
                    lstProyectosCliente = await (from a in db.tB_Proyectos
                                        where a.IdEmpresa == idCliente
                                        select a.NumProyecto).ToListAsync();
                }
                if (idEmpresa != null)
                {
                    lstProyectosEmpresa = await (from a in db.tB_Proyectos
                                                 where a.IdEmpresa == idEmpresa
                                                 select a.NumProyecto).ToListAsync();
                }

                var res = await (from a in db.tB_ProyectoFacturas
                                 join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                                 from ab in factNC.DefaultIfEmpty()
                                 join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                                 from ac in factC.DefaultIfEmpty()
                                 join c in db.tB_Proyectos on a.NumProyecto equals c.NumProyecto into cJoin
                                 from cItem in cJoin.DefaultIfEmpty()
                                 join d in db.tB_Clientes on cItem.IdCliente equals d.IdCliente into dJoin
                                 from dItem in dJoin.DefaultIfEmpty()
                                 where (idProyecto == null || a.NumProyecto == idProyecto)
                                 && (lstProyectosCliente == null || a.NumProyecto.In(lstProyectosCliente))
                                 && (lstProyectosEmpresa == null || a.NumProyecto.In(lstProyectosEmpresa))
                                 && (fechaIni == null || a.FechaEmision >= fechaIni)
                                 && (fechaFin == null || a.FechaEmision <= fechaFin)
                                 && (noFactura == null || a.NoFactura == noFactura)
                                 orderby dItem.Cliente ascending
                                 select new FacturaDetalles
                                 {
                                     Id = a.Id,
                                     Uuid = a.Uuid,
                                     NumProyecto = a.NumProyecto,
                                     Cliente = dItem.Cliente ?? string.Empty,
                                     ClienteRFC = dItem.Rfc ?? string.Empty,
                                     IdTipoFactura = a.IdTipoFactura,
                                     IdMoneda = a.IdMoneda,
                                     Importe = a.Importe,
                                     Iva = a.Iva,
                                     IvaRet = a.IvaRet,
                                     Total = a.Total,
                                     Concepto = a.Concepto,
                                     Mes = a.Mes,
                                     Anio = a.Anio,
                                     FechaEmision = a.FechaEmision,
                                     FechaCancelacion = a.FechaCancelacion,
                                     FechaPago = a.FechaPago,
                                     NoFactura = a.NoFactura,
                                     TipoCambio = a.TipoCambio,
                                     MotivoCancelacion = a.MotivoCancelacion
                                     //NC_UuidNotaCredito = ab.UuidNotaCredito,
                                     //NC_IdMoneda = ab.IdMoneda,
                                     //NC_IdTipoRelacion = ab.IdTipoRelacion,
                                     //NC_NotaCredito = ab.NotaCredito,
                                     //NC_Importe = ab.Importe,
                                     //NC_Iva = ab.Iva,
                                     //NC_Total = ab.Total,
                                     //NC_Concepto = ab.Concepto,
                                     //NC_Mes = ab.Mes,
                                     //NC_Anio = ab.Anio,
                                     //NC_TipoCambio = ab.TipoCambio,
                                     //NC_FechaNotaCredito = ab.FechaNotaCredito,
                                     //C_UuidCobranza = ac.UuidCobranza,
                                     //C_IdMonedaP = ac.IdMonedaP,
                                     //C_ImportePagado = ac.ImportePagado,
                                     //C_ImpSaldoAnt = ac.ImpSaldoAnt,
                                     //C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                                     //C_IvaP = ac.IvaP,
                                     //C_TipoCambioP = ac.TipoCambioP,
                                     //C_FechaPago = ac.FechaPago
                                 }).ToListAsync();

                foreach (var facturaDetalle in res)
                {
                    var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.IdFactura == facturaDetalle.Id
                                           && notas.FechaCancelacion == null
                                           select new NotaDetalle
                                           {
                                               NC_UuidNotaCredito = notas.UuidNotaCredito,
                                               NC_IdMoneda = notas.IdMoneda,
                                               NC_IdTipoRelacion = notas.IdTipoRelacion,
                                               NC_NotaCredito = notas.NotaCredito,
                                               NC_Importe = notas.Importe,
                                               NC_Iva = notas.Iva,
                                               NC_Total = notas.Total,
                                               NC_Concepto = notas.Concepto,
                                               NC_Mes = notas.Mes,
                                               NC_Anio = notas.Anio,
                                               NC_TipoCambio = notas.TipoCambio,
                                               NC_FechaNotaCredito = notas.FechaNotaCredito
                                           }).ToListAsync();

                    facturaDetalle.Notas = new List<NotaDetalle>();
                    foreach (var nota in res_notas)
                    {
                        facturaDetalle.Notas.Add(nota);
                    }

                    var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                               where cobr.IdFactura == facturaDetalle.Id
                                               && cobr.FechaCancelacion == null
                                               select new CobranzaDetalle
                                               {
                                                   C_UuidCobranza = cobr.UuidCobranza,
                                                   C_IdMonedaP = cobr.IdMonedaP,
                                                   C_ImportePagado = cobr.ImportePagado,
                                                   C_ImpSaldoAnt = cobr.ImpSaldoAnt,
                                                   C_ImporteSaldoInsoluto = cobr.ImporteSaldoInsoluto,
                                                   C_IvaP = cobr.IvaP,
                                                   C_TipoCambioP = cobr.TipoCambioP,
                                                   C_FechaPago = cobr.FechaPago
                                               }).ToListAsync();

                    facturaDetalle.Cobranzas = new List<CobranzaDetalle>();
                    foreach (var cobranza in res_cobranzas)
                    {
                        facturaDetalle.Cobranzas.Add(cobranza);
                    }

                    facturaDetalle.TotalNotasCredito = res_notas.Count();
                    facturaDetalle.TotalCobranzas = res_cobranzas.Count();
                }

                return res;
            }
        }
        #endregion

        #region facturasProyecto
        public async Task<List<FacturaDetalles>> GetFacturasProyecto(int? idProyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from a in db.tB_ProyectoFacturas
                                 join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                                 from ab in factNC.DefaultIfEmpty()
                                 join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                                 from ac in factC.DefaultIfEmpty()
                                 where a.NumProyecto == idProyecto
                                 orderby a.Id descending
                                 select new FacturaDetalles
                                 {
                                     Id = a.Id,
                                     Uuid = a.Uuid,
                                     NumProyecto = a.NumProyecto,
                                     IdTipoFactura = a.IdTipoFactura,
                                     IdMoneda = a.IdMoneda,
                                     Importe = a.Importe,
                                     Iva = a.Iva,
                                     IvaRet = a.IvaRet,
                                     Total = a.Total,
                                     Concepto = a.Concepto,
                                     Mes = a.Mes,
                                     Anio = a.Anio,
                                     FechaEmision = a.FechaEmision,
                                     FechaCancelacion = a.FechaCancelacion,
                                     FechaPago = a.FechaPago,
                                     NoFactura = a.NoFactura,
                                     TipoCambio = a.TipoCambio,
                                     MotivoCancelacion = a.MotivoCancelacion,
                                     //NC_UuidNotaCredito = ab.UuidNotaCredito,
                                     //NC_IdMoneda = ab.IdMoneda,
                                     //NC_IdTipoRelacion = ab.IdTipoRelacion,
                                     //NC_NotaCredito = ab.NotaCredito,
                                     //NC_Importe = ab.Importe,
                                     //NC_Iva = ab.Iva,
                                     //NC_Total = ab.Total,
                                     //NC_Concepto = ab.Concepto,
                                     //NC_Mes = ab.Mes,
                                     //NC_Anio = ab.Anio,
                                     //NC_TipoCambio = ab.TipoCambio,
                                     //NC_FechaNotaCredito = ab.FechaNotaCredito,
                                     //C_UuidCobranza = ac.UuidCobranza,
                                     //C_IdMonedaP = ac.IdMonedaP,
                                     //C_ImportePagado = ac.ImportePagado,
                                     //C_ImpSaldoAnt = ac.ImpSaldoAnt,
                                     //C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                                     //C_IvaP = ac.IvaP,
                                     //C_TipoCambioP = ac.TipoCambioP,
                                     //C_FechaPago = ac.FechaPago
                                 }).ToListAsync();

                foreach (var facturaDetalle in res)
                {
                    var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.IdFactura == facturaDetalle.Id
                                           && notas.FechaCancelacion == null
                                           select new NotaDetalle
                                           {
                                               NC_UuidNotaCredito = notas.UuidNotaCredito,
                                               NC_IdMoneda = notas.IdMoneda,
                                               NC_IdTipoRelacion = notas.IdTipoRelacion,
                                               NC_NotaCredito = notas.NotaCredito,
                                               NC_Importe = notas.Importe,
                                               NC_Iva = notas.Iva,
                                               NC_Total = notas.Total,
                                               NC_Concepto = notas.Concepto,
                                               NC_Mes = notas.Mes,
                                               NC_Anio = notas.Anio,
                                               NC_TipoCambio = notas.TipoCambio,
                                               NC_FechaNotaCredito = notas.FechaNotaCredito
                                           }).ToListAsync();

                    facturaDetalle.Notas = new List<NotaDetalle>();
                    foreach (var nota in res_notas)
                    {
                        facturaDetalle.Notas.Add(nota);
                    }

                    var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                               where cobr.IdFactura == facturaDetalle.Id
                                               && cobr.FechaCancelacion == null
                                               select new CobranzaDetalle
                                               {
                                                   C_UuidCobranza = cobr.UuidCobranza,
                                                   C_IdMonedaP = cobr.IdMonedaP,
                                                   C_ImportePagado = cobr.ImportePagado,
                                                   C_ImpSaldoAnt = cobr.ImpSaldoAnt,
                                                   C_ImporteSaldoInsoluto = cobr.ImporteSaldoInsoluto,
                                                   C_IvaP = cobr.IvaP,
                                                   C_TipoCambioP = cobr.TipoCambioP,
                                                   C_FechaPago = cobr.FechaPago
                                               }).ToListAsync();

                    facturaDetalle.Cobranzas = new List<CobranzaDetalle>();
                    foreach (var cobranza in res_cobranzas)
                    {
                        facturaDetalle.Cobranzas.Add(cobranza);
                    }

                    facturaDetalle.TotalNotasCredito = res_notas.Count();
                    facturaDetalle.TotalCobranzas = res_cobranzas.Count();
                }

                return res;

            }
        }
        public async Task<List<FacturaDetalles>> GetFacturasProyectoFecha(int? idProyecto, DateTime? fechaIni, DateTime? fechaFin)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from a in db.tB_ProyectoFacturas
                                 join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                                 from ab in factNC.DefaultIfEmpty()
                                 join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                                 from ac in factC.DefaultIfEmpty()
                                 where a.NumProyecto == idProyecto && a.FechaEmision >= fechaIni && a.FechaEmision <= fechaFin
                                 orderby a.Id descending
                                 select new FacturaDetalles
                                 {
                                     Id = a.Id,
                                     Uuid = a.Uuid,
                                     NumProyecto = a.NumProyecto,
                                     IdTipoFactura = a.IdTipoFactura,
                                     IdMoneda = a.IdMoneda,
                                     Importe = a.Importe,
                                     Iva = a.Iva,
                                     IvaRet = a.IvaRet,
                                     Total = a.Total,
                                     Concepto = a.Concepto,
                                     Mes = a.Mes,
                                     Anio = a.Anio,
                                     FechaEmision = a.FechaEmision,
                                     FechaCancelacion = a.FechaCancelacion,
                                     FechaPago = a.FechaPago,
                                     NoFactura = a.NoFactura,
                                     TipoCambio = a.TipoCambio,
                                     MotivoCancelacion = a.MotivoCancelacion
                                     //NC_UuidNotaCredito = ab.UuidNotaCredito,
                                     //NC_IdMoneda = ab.IdMoneda,
                                     //NC_IdTipoRelacion = ab.IdTipoRelacion,
                                     //NC_NotaCredito = ab.NotaCredito,
                                     //NC_Importe = ab.Importe,
                                     //NC_Iva = ab.Iva,
                                     //NC_Total = ab.Total,
                                     //NC_Concepto = ab.Concepto,
                                     //NC_Mes = ab.Mes,
                                     //NC_Anio = ab.Anio,
                                     //NC_TipoCambio = ab.TipoCambio,
                                     //NC_FechaNotaCredito = ab.FechaNotaCredito,
                                     //C_UuidCobranza = ac.UuidCobranza,
                                     //C_IdMonedaP = ac.IdMonedaP,
                                     //C_ImportePagado = ac.ImportePagado,
                                     //C_ImpSaldoAnt = ac.ImpSaldoAnt,
                                     //C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                                     //C_IvaP = ac.IvaP,
                                     //C_TipoCambioP = ac.TipoCambioP,
                                     //C_FechaPago = ac.FechaPago
                                 }).ToListAsync();

                foreach (var facturaDetalle in res)
                {
                    var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.IdFactura == facturaDetalle.Id
                                           && notas.FechaCancelacion == null
                                           select new NotaDetalle
                                           {
                                               NC_UuidNotaCredito = notas.UuidNotaCredito,
                                               NC_IdMoneda = notas.IdMoneda,
                                               NC_IdTipoRelacion = notas.IdTipoRelacion,
                                               NC_NotaCredito = notas.NotaCredito,
                                               NC_Importe = notas.Importe,
                                               NC_Iva = notas.Iva,
                                               NC_Total = notas.Total,
                                               NC_Concepto = notas.Concepto,
                                               NC_Mes = notas.Mes,
                                               NC_Anio = notas.Anio,
                                               NC_TipoCambio = notas.TipoCambio,
                                               NC_FechaNotaCredito = notas.FechaNotaCredito
                                           }).ToListAsync();

                    facturaDetalle.Notas = new List<NotaDetalle>();
                    foreach (var nota in res_notas)
                    {
                        facturaDetalle.Notas.Add(nota);
                    }

                    var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                               where cobr.IdFactura == facturaDetalle.Id
                                               && cobr.FechaCancelacion == null
                                               select new CobranzaDetalle
                                               {
                                                   C_UuidCobranza = cobr.UuidCobranza,
                                                   C_IdMonedaP = cobr.IdMonedaP,
                                                   C_ImportePagado = cobr.ImportePagado,
                                                   C_ImpSaldoAnt = cobr.ImpSaldoAnt,
                                                   C_ImporteSaldoInsoluto = cobr.ImporteSaldoInsoluto,
                                                   C_IvaP = cobr.IvaP,
                                                   C_TipoCambioP = cobr.TipoCambioP,
                                                   C_FechaPago = cobr.FechaPago
                                               }).ToListAsync();

                    facturaDetalle.Cobranzas = new List<CobranzaDetalle>();
                    foreach (var cobranza in res_cobranzas)
                    {
                        facturaDetalle.Cobranzas.Add(cobranza);
                    }

                    facturaDetalle.TotalNotasCredito = res_notas.Count();
                    facturaDetalle.TotalCobranzas = res_cobranzas.Count();
                }

                return res;

            }
        }
        #endregion

        #region facturasEmpresa
        public async Task<List<FacturaDetalles>> GetFacturasEmpresa(int? idEmpresa)
        {
            using (var db = new ConnectionDB(dbConfig))
            {


                var lstProyectos = (from a in db.tB_Proyectos
                                    where a.IdEmpresa == idEmpresa
                                    select a.NumProyecto).ToArray();

                var res = await (from a in db.tB_ProyectoFacturas
                                 join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                                 from ab in factNC.DefaultIfEmpty()
                                 join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                                 from ac in factC.DefaultIfEmpty()
                                 where a.NumProyecto.In(lstProyectos)
                                 orderby a.Id descending
                                 select new FacturaDetalles
                                 {
                                     Id = a.Id,
                                     Uuid = a.Uuid,
                                     NumProyecto = a.NumProyecto,
                                     IdTipoFactura = a.IdTipoFactura,
                                     IdMoneda = a.IdMoneda,
                                     Importe = a.Importe,
                                     Iva = a.Iva,
                                     IvaRet = a.IvaRet,
                                     Total = a.Total,
                                     Concepto = a.Concepto,
                                     Mes = a.Mes,
                                     Anio = a.Anio,
                                     FechaEmision = a.FechaEmision,
                                     FechaCancelacion = a.FechaCancelacion,
                                     FechaPago = a.FechaPago,
                                     NoFactura = a.NoFactura,
                                     TipoCambio = a.TipoCambio,
                                     MotivoCancelacion = a.MotivoCancelacion
                                     //NC_UuidNotaCredito = ab.UuidNotaCredito,
                                     //NC_IdMoneda = ab.IdMoneda,
                                     //NC_IdTipoRelacion = ab.IdTipoRelacion,
                                     //NC_NotaCredito = ab.NotaCredito,
                                     //NC_Importe = ab.Importe,
                                     //NC_Iva = ab.Iva,
                                     //NC_Total = ab.Total,
                                     //NC_Concepto = ab.Concepto,
                                     //NC_Mes = ab.Mes,
                                     //NC_Anio = ab.Anio,
                                     //NC_TipoCambio = ab.TipoCambio,
                                     //NC_FechaNotaCredito = ab.FechaNotaCredito,
                                     //C_UuidCobranza = ac.UuidCobranza,
                                     //C_IdMonedaP = ac.IdMonedaP,
                                     //C_ImportePagado = ac.ImportePagado,
                                     //C_ImpSaldoAnt = ac.ImpSaldoAnt,
                                     //C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                                     //C_IvaP = ac.IvaP,
                                     //C_TipoCambioP = ac.TipoCambioP,
                                     //C_FechaPago = ac.FechaPago
                                 }).ToListAsync();

                foreach (var facturaDetalle in res)
                {
                    var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.IdFactura == facturaDetalle.Id
                                           && notas.FechaCancelacion == null
                                           select new NotaDetalle
                                           {
                                               NC_UuidNotaCredito = notas.UuidNotaCredito,
                                               NC_IdMoneda = notas.IdMoneda,
                                               NC_IdTipoRelacion = notas.IdTipoRelacion,
                                               NC_NotaCredito = notas.NotaCredito,
                                               NC_Importe = notas.Importe,
                                               NC_Iva = notas.Iva,
                                               NC_Total = notas.Total,
                                               NC_Concepto = notas.Concepto,
                                               NC_Mes = notas.Mes,
                                               NC_Anio = notas.Anio,
                                               NC_TipoCambio = notas.TipoCambio,
                                               NC_FechaNotaCredito = notas.FechaNotaCredito
                                           }).ToListAsync();

                    facturaDetalle.Notas = new List<NotaDetalle>();
                    foreach (var nota in res_notas)
                    {
                        facturaDetalle.Notas.Add(nota);
                    }

                    var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                               where cobr.IdFactura == facturaDetalle.Id
                                               && cobr.FechaCancelacion == null
                                               select new CobranzaDetalle
                                               {
                                                   C_UuidCobranza = cobr.UuidCobranza,
                                                   C_IdMonedaP = cobr.IdMonedaP,
                                                   C_ImportePagado = cobr.ImportePagado,
                                                   C_ImpSaldoAnt = cobr.ImpSaldoAnt,
                                                   C_ImporteSaldoInsoluto = cobr.ImporteSaldoInsoluto,
                                                   C_IvaP = cobr.IvaP,
                                                   C_TipoCambioP = cobr.TipoCambioP,
                                                   C_FechaPago = cobr.FechaPago
                                               }).ToListAsync();

                    facturaDetalle.Cobranzas = new List<CobranzaDetalle>();
                    foreach (var cobranza in res_cobranzas)
                    {
                        facturaDetalle.Cobranzas.Add(cobranza);
                    }

                    facturaDetalle.TotalNotasCredito = res_notas.Count();
                    facturaDetalle.TotalCobranzas = res_cobranzas.Count();
                }

                return res;

            }
        }

        public async Task<List<FacturaDetalles>> GetFacturasEmpresaFecha(int? idEmpresa, DateTime? fechaIni, DateTime? fechaFin)
        {
            using (var db = new ConnectionDB(dbConfig))
            {


                var lstProyectos = (from a in db.tB_Proyectos
                                    where a.IdEmpresa == idEmpresa
                                    select a.NumProyecto).ToArray();

                var res = await (from a in db.tB_ProyectoFacturas
                                 join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                                 from ab in factNC.DefaultIfEmpty()
                                 join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                                 from ac in factC.DefaultIfEmpty()
                                 where a.NumProyecto.In(lstProyectos)
                                 orderby a.Id descending
                                 select new FacturaDetalles
                                 {
                                     Id = a.Id,
                                     Uuid = a.Uuid,
                                     NumProyecto = a.NumProyecto,
                                     IdTipoFactura = a.IdTipoFactura,
                                     IdMoneda = a.IdMoneda,
                                     Importe = a.Importe,
                                     Iva = a.Iva,
                                     IvaRet = a.IvaRet,
                                     Total = a.Total,
                                     Concepto = a.Concepto,
                                     Mes = a.Mes,
                                     Anio = a.Anio,
                                     FechaEmision = a.FechaEmision,
                                     FechaCancelacion = a.FechaCancelacion,
                                     FechaPago = a.FechaPago,
                                     NoFactura = a.NoFactura,
                                     TipoCambio = a.TipoCambio,
                                     MotivoCancelacion = a.MotivoCancelacion,
                                     //NC_UuidNotaCredito = ab.UuidNotaCredito,
                                     //NC_IdMoneda = ab.IdMoneda,
                                     //NC_IdTipoRelacion = ab.IdTipoRelacion,
                                     //NC_NotaCredito = ab.NotaCredito,
                                     //NC_Importe = ab.Importe,
                                     //NC_Iva = ab.Iva,
                                     //NC_Total = ab.Total,
                                     //NC_Concepto = ab.Concepto,
                                     //NC_Mes = ab.Mes,
                                     //NC_Anio = ab.Anio,
                                     //NC_TipoCambio = ab.TipoCambio,
                                     //NC_FechaNotaCredito = ab.FechaNotaCredito,
                                     //C_UuidCobranza = ac.UuidCobranza,
                                     //C_IdMonedaP = ac.IdMonedaP,
                                     //C_ImportePagado = ac.ImportePagado,
                                     //C_ImpSaldoAnt = ac.ImpSaldoAnt,
                                     //C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                                     //C_IvaP = ac.IvaP,
                                     //C_TipoCambioP = ac.TipoCambioP,
                                     //C_FechaPago = ac.FechaPago
                                 }).ToListAsync();

                foreach (var facturaDetalle in res)
                {
                    var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.IdFactura == facturaDetalle.Id
                                           && notas.FechaCancelacion == null
                                           select new NotaDetalle
                                           {
                                               NC_UuidNotaCredito = notas.UuidNotaCredito,
                                               NC_IdMoneda = notas.IdMoneda,
                                               NC_IdTipoRelacion = notas.IdTipoRelacion,
                                               NC_NotaCredito = notas.NotaCredito,
                                               NC_Importe = notas.Importe,
                                               NC_Iva = notas.Iva,
                                               NC_Total = notas.Total,
                                               NC_Concepto = notas.Concepto,
                                               NC_Mes = notas.Mes,
                                               NC_Anio = notas.Anio,
                                               NC_TipoCambio = notas.TipoCambio,
                                               NC_FechaNotaCredito = notas.FechaNotaCredito
                                           }).ToListAsync();

                    facturaDetalle.Notas = new List<NotaDetalle>();
                    foreach (var nota in res_notas)
                    {
                        facturaDetalle.Notas.Add(nota);
                    }

                    var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                               where cobr.IdFactura == facturaDetalle.Id
                                               && cobr.FechaCancelacion == null
                                               select new CobranzaDetalle
                                               {
                                                   C_UuidCobranza = cobr.UuidCobranza,
                                                   C_IdMonedaP = cobr.IdMonedaP,
                                                   C_ImportePagado = cobr.ImportePagado,
                                                   C_ImpSaldoAnt = cobr.ImpSaldoAnt,
                                                   C_ImporteSaldoInsoluto = cobr.ImporteSaldoInsoluto,
                                                   C_IvaP = cobr.IvaP,
                                                   C_TipoCambioP = cobr.TipoCambioP,
                                                   C_FechaPago = cobr.FechaPago
                                               }).ToListAsync();

                    facturaDetalle.Cobranzas = new List<CobranzaDetalle>();
                    foreach (var cobranza in res_cobranzas)
                    {
                        facturaDetalle.Cobranzas.Add(cobranza);
                    }

                    facturaDetalle.TotalNotasCredito = res_notas.Count();
                    facturaDetalle.TotalCobranzas = res_cobranzas.Count();
                }

                return res;

            }
        }
        #endregion

        #region facturasCliente
        public async Task<List<FacturaDetalles>> GetFacturasCliente(int? idCliente)
        {
            using (var db = new ConnectionDB(dbConfig))
            {


                var lstProyectos = (from a in db.tB_Proyectos
                                    where a.IdEmpresa == idCliente
                                    select a.NumProyecto).ToArray();

                var res = await (from a in db.tB_ProyectoFacturas
                                 join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                                 from ab in factNC.DefaultIfEmpty()
                                 join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                                 from ac in factC.DefaultIfEmpty()
                                 where a.NumProyecto.In(lstProyectos)
                                 orderby a.Id descending
                                 select new FacturaDetalles
                                 {
                                     Id = a.Id,
                                     Uuid = a.Uuid,
                                     NumProyecto = a.NumProyecto,
                                     IdTipoFactura = a.IdTipoFactura,
                                     IdMoneda = a.IdMoneda,
                                     Importe = a.Importe,
                                     Iva = a.Iva,
                                     IvaRet = a.IvaRet,
                                     Total = a.Total,
                                     Concepto = a.Concepto,
                                     Mes = a.Mes,
                                     Anio = a.Anio,
                                     FechaEmision = a.FechaEmision,
                                     FechaCancelacion = a.FechaCancelacion,
                                     FechaPago = a.FechaPago,
                                     NoFactura = a.NoFactura,
                                     TipoCambio = a.TipoCambio,
                                     MotivoCancelacion = a.MotivoCancelacion
                                     //NC_UuidNotaCredito = ab.UuidNotaCredito,
                                     //NC_IdMoneda = ab.IdMoneda,
                                     //NC_IdTipoRelacion = ab.IdTipoRelacion,
                                     //NC_NotaCredito = ab.NotaCredito,
                                     //NC_Importe = ab.Importe,
                                     //NC_Iva = ab.Iva,
                                     //NC_Total = ab.Total,
                                     //NC_Concepto = ab.Concepto,
                                     //NC_Mes = ab.Mes,
                                     //NC_Anio = ab.Anio,
                                     //NC_TipoCambio = ab.TipoCambio,
                                     //NC_FechaNotaCredito = ab.FechaNotaCredito,
                                     //C_UuidCobranza = ac.UuidCobranza,
                                     //C_IdMonedaP = ac.IdMonedaP,
                                     //C_ImportePagado = ac.ImportePagado,
                                     //C_ImpSaldoAnt = ac.ImpSaldoAnt,
                                     //C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                                     //C_IvaP = ac.IvaP,
                                     //C_TipoCambioP = ac.TipoCambioP,
                                     //C_FechaPago = ac.FechaPago
                                 }).ToListAsync();

                foreach (var facturaDetalle in res)
                {
                    var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.IdFactura == facturaDetalle.Id
                                           && notas.FechaCancelacion == null
                                           select new NotaDetalle
                                           {
                                               NC_UuidNotaCredito = notas.UuidNotaCredito,
                                               NC_IdMoneda = notas.IdMoneda,
                                               NC_IdTipoRelacion = notas.IdTipoRelacion,
                                               NC_NotaCredito = notas.NotaCredito,
                                               NC_Importe = notas.Importe,
                                               NC_Iva = notas.Iva,
                                               NC_Total = notas.Total,
                                               NC_Concepto = notas.Concepto,
                                               NC_Mes = notas.Mes,
                                               NC_Anio = notas.Anio,
                                               NC_TipoCambio = notas.TipoCambio,
                                               NC_FechaNotaCredito = notas.FechaNotaCredito
                                           }).ToListAsync();

                    facturaDetalle.Notas = new List<NotaDetalle>();
                    foreach (var nota in res_notas)
                    {
                        facturaDetalle.Notas.Add(nota);
                    }

                    var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                               where cobr.IdFactura == facturaDetalle.Id
                                               && cobr.FechaCancelacion == null
                                               select new CobranzaDetalle
                                               {
                                                   C_UuidCobranza = cobr.UuidCobranza,
                                                   C_IdMonedaP = cobr.IdMonedaP,
                                                   C_ImportePagado = cobr.ImportePagado,
                                                   C_ImpSaldoAnt = cobr.ImpSaldoAnt,
                                                   C_ImporteSaldoInsoluto = cobr.ImporteSaldoInsoluto,
                                                   C_IvaP = cobr.IvaP,
                                                   C_TipoCambioP = cobr.TipoCambioP,
                                                   C_FechaPago = cobr.FechaPago
                                               }).ToListAsync();

                    facturaDetalle.Cobranzas = new List<CobranzaDetalle>();
                    foreach (var cobranza in res_cobranzas)
                    {
                        facturaDetalle.Cobranzas.Add(cobranza);
                    }

                    facturaDetalle.TotalNotasCredito = res_notas.Count();
                    facturaDetalle.TotalCobranzas = res_cobranzas.Count();
                }

                return res;

            }
        }
        public async Task<List<FacturaDetalles>> GetFacturasClienteFecha(int? idCliente, DateTime? fechaIni, DateTime? fechaFin)
        {
            using (var db = new ConnectionDB(dbConfig))
            {


                var lstProyectos = (from a in db.tB_Proyectos
                                    where a.IdEmpresa == idCliente
                                    select a.NumProyecto).ToArray();

                var res = await (from a in db.tB_ProyectoFacturas
                                 join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                                 from ab in factNC.DefaultIfEmpty()
                                 join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                                 from ac in factC.DefaultIfEmpty()
                                 where a.NumProyecto.In(lstProyectos) && a.FechaEmision >= fechaIni && a.FechaEmision <= fechaFin
                                 orderby a.Id descending
                                 select new FacturaDetalles
                                 {
                                     Id = a.Id,
                                     Uuid = a.Uuid,
                                     NumProyecto = a.NumProyecto,
                                     IdTipoFactura = a.IdTipoFactura,
                                     IdMoneda = a.IdMoneda,
                                     Importe = a.Importe,
                                     Iva = a.Iva,
                                     IvaRet = a.IvaRet,
                                     Total = a.Total,
                                     Concepto = a.Concepto,
                                     Mes = a.Mes,
                                     Anio = a.Anio,
                                     FechaEmision = a.FechaEmision,
                                     FechaCancelacion = a.FechaCancelacion,
                                     FechaPago = a.FechaPago,
                                     NoFactura = a.NoFactura,
                                     TipoCambio = a.TipoCambio,
                                     MotivoCancelacion = a.MotivoCancelacion
                                     //NC_UuidNotaCredito = ab.UuidNotaCredito,
                                     //NC_IdMoneda = ab.IdMoneda,
                                     //NC_IdTipoRelacion = ab.IdTipoRelacion,
                                     //NC_NotaCredito = ab.NotaCredito,
                                     //NC_Importe = ab.Importe,
                                     //NC_Iva = ab.Iva,
                                     //NC_Total = ab.Total,
                                     //NC_Concepto = ab.Concepto,
                                     //NC_Mes = ab.Mes,
                                     //NC_Anio = ab.Anio,
                                     //NC_TipoCambio = ab.TipoCambio,
                                     //NC_FechaNotaCredito = ab.FechaNotaCredito,
                                     //C_UuidCobranza = ac.UuidCobranza,
                                     //C_IdMonedaP = ac.IdMonedaP,
                                     //C_ImportePagado = ac.ImportePagado,
                                     //C_ImpSaldoAnt = ac.ImpSaldoAnt,
                                     //C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                                     //C_IvaP = ac.IvaP,
                                     //C_TipoCambioP = ac.TipoCambioP,
                                     //C_FechaPago = ac.FechaPago
                                 }).ToListAsync();

                foreach (var facturaDetalle in res)
                {
                    var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.IdFactura == facturaDetalle.Id
                                           && notas.FechaCancelacion == null
                                           select new NotaDetalle
                                           {
                                               NC_UuidNotaCredito = notas.UuidNotaCredito,
                                               NC_IdMoneda = notas.IdMoneda,
                                               NC_IdTipoRelacion = notas.IdTipoRelacion,
                                               NC_NotaCredito = notas.NotaCredito,
                                               NC_Importe = notas.Importe,
                                               NC_Iva = notas.Iva,
                                               NC_Total = notas.Total,
                                               NC_Concepto = notas.Concepto,
                                               NC_Mes = notas.Mes,
                                               NC_Anio = notas.Anio,
                                               NC_TipoCambio = notas.TipoCambio,
                                               NC_FechaNotaCredito = notas.FechaNotaCredito
                                           }).ToListAsync();

                    facturaDetalle.Notas = new List<NotaDetalle>();
                    foreach (var nota in res_notas)
                    {
                        facturaDetalle.Notas.Add(nota);
                    }

                    var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                               where cobr.IdFactura == facturaDetalle.Id
                                               && cobr.FechaCancelacion == null
                                               select new CobranzaDetalle
                                               {
                                                   C_UuidCobranza = cobr.UuidCobranza,
                                                   C_IdMonedaP = cobr.IdMonedaP,
                                                   C_ImportePagado = cobr.ImportePagado,
                                                   C_ImpSaldoAnt = cobr.ImpSaldoAnt,
                                                   C_ImporteSaldoInsoluto = cobr.ImporteSaldoInsoluto,
                                                   C_IvaP = cobr.IvaP,
                                                   C_TipoCambioP = cobr.TipoCambioP,
                                                   C_FechaPago = cobr.FechaPago
                                               }).ToListAsync();

                    facturaDetalle.Cobranzas = new List<CobranzaDetalle>();
                    foreach (var cobranza in res_cobranzas)
                    {
                        facturaDetalle.Cobranzas.Add(cobranza);
                    }

                    facturaDetalle.TotalNotasCredito = res_notas.Count();
                    facturaDetalle.TotalCobranzas = res_cobranzas.Count();
                }

                return res;

            }
        }

        #endregion

        #region facturas por número
        public async Task<List<FacturaDetalles>> GetFacturaNumero(string? noFactura)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from a in db.tB_ProyectoFacturas
                                 join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                                 from ab in factNC.DefaultIfEmpty()
                                 join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                                 from ac in factC.DefaultIfEmpty()
                                 where a.NoFactura == noFactura
                                 orderby a.Id descending
                                 select new FacturaDetalles
                                 {
                                     Id = a.Id,
                                     Uuid = a.Uuid,
                                     NumProyecto = a.NumProyecto,
                                     IdTipoFactura = a.IdTipoFactura,
                                     IdMoneda = a.IdMoneda,
                                     Importe = a.Importe,
                                     Iva = a.Iva,
                                     IvaRet = a.IvaRet,
                                     Total = a.Total,
                                     Concepto = a.Concepto,
                                     Mes = a.Mes,
                                     Anio = a.Anio,
                                     FechaEmision = a.FechaEmision,
                                     FechaCancelacion = a.FechaCancelacion,
                                     FechaPago = a.FechaPago,
                                     NoFactura = a.NoFactura,
                                     TipoCambio = a.TipoCambio,
                                     MotivoCancelacion = a.MotivoCancelacion
                                     //NC_UuidNotaCredito = ab.UuidNotaCredito,
                                     //NC_IdMoneda = ab.IdMoneda,
                                     //NC_IdTipoRelacion = ab.IdTipoRelacion,
                                     //NC_NotaCredito = ab.NotaCredito,
                                     //NC_Importe = ab.Importe,
                                     //NC_Iva = ab.Iva,
                                     //NC_Total = ab.Total,
                                     //NC_Concepto = ab.Concepto,
                                     //NC_Mes = ab.Mes,
                                     //NC_Anio = ab.Anio,
                                     //NC_TipoCambio = ab.TipoCambio,
                                     //NC_FechaNotaCredito = ab.FechaNotaCredito,
                                     //C_UuidCobranza = ac.UuidCobranza,
                                     //C_IdMonedaP = ac.IdMonedaP,
                                     //C_ImportePagado = ac.ImportePagado,
                                     //C_ImpSaldoAnt = ac.ImpSaldoAnt,
                                     //C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                                     //C_IvaP = ac.IvaP,
                                     //C_TipoCambioP = ac.TipoCambioP,
                                     //C_FechaPago = ac.FechaPago
                                 }).ToListAsync();

                foreach (var facturaDetalle in res)
                {
                    var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.IdFactura == facturaDetalle.Id
                                           && notas.FechaCancelacion == null
                                           select new NotaDetalle
                                           {
                                               NC_UuidNotaCredito = notas.UuidNotaCredito,
                                               NC_IdMoneda = notas.IdMoneda,
                                               NC_IdTipoRelacion = notas.IdTipoRelacion,
                                               NC_NotaCredito = notas.NotaCredito,
                                               NC_Importe = notas.Importe,
                                               NC_Iva = notas.Iva,
                                               NC_Total = notas.Total,
                                               NC_Concepto = notas.Concepto,
                                               NC_Mes = notas.Mes,
                                               NC_Anio = notas.Anio,
                                               NC_TipoCambio = notas.TipoCambio,
                                               NC_FechaNotaCredito = notas.FechaNotaCredito
                                           }).ToListAsync();

                    facturaDetalle.Notas = new List<NotaDetalle>();
                    foreach (var nota in res_notas)
                    {
                        facturaDetalle.Notas.Add(nota);
                    }

                    var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                               where cobr.IdFactura == facturaDetalle.Id
                                               && cobr.FechaCancelacion == null
                                               select new CobranzaDetalle
                                               {
                                                   C_UuidCobranza = cobr.UuidCobranza,
                                                   C_IdMonedaP = cobr.IdMonedaP,
                                                   C_ImportePagado = cobr.ImportePagado,
                                                   C_ImpSaldoAnt = cobr.ImpSaldoAnt,
                                                   C_ImporteSaldoInsoluto = cobr.ImporteSaldoInsoluto,
                                                   C_IvaP = cobr.IvaP,
                                                   C_TipoCambioP = cobr.TipoCambioP,
                                                   C_FechaPago = cobr.FechaPago
                                               }).ToListAsync();

                    facturaDetalle.Cobranzas = new List<CobranzaDetalle>();
                    foreach (var cobranza in res_cobranzas)
                    {
                        facturaDetalle.Cobranzas.Add(cobranza);
                    }

                    facturaDetalle.TotalNotasCredito = res_notas.Count();
                    facturaDetalle.TotalCobranzas = res_cobranzas.Count();
                }

                return res;
            }
        }
        #endregion facturas por número        

        #region Extraer Datos Cfdi
        public async Task<BaseCFDI?> ExtraerDatos(string base64String)
        {
            var datosCFDI = default(BaseCFDI);

            try
            {
                var base64EncodedBytes = Convert.FromBase64String(base64String);
                var strXml = Encoding.UTF8.GetString(base64EncodedBytes);

                var doc = new XmlDocument();
                doc.LoadXml(strXml);
                var nsm = new XmlNamespaceManager(doc.NameTable);
                nsm.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/4");
                nsm.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");
                nsm.AddNamespace("pago20", "http://www.sat.gob.mx/Pagos20");
                var nodeComprobante = doc.SelectSingleNode("//cfdi:Comprobante", nsm);
                if (nodeComprobante is null)
                {
                    nsm = new XmlNamespaceManager(doc.NameTable);
                    nsm.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3");
                    nsm.AddNamespace("tfd", "http://www.sat.gob.mx/TimbreFiscalDigital");
                    nsm.AddNamespace("pago10", "http://www.sat.gob.mx/Pagos");
                    nodeComprobante = doc.SelectSingleNode("//cfdi:Comprobante", nsm);
                }

                if (new List<string> { "3.3", "4.0" }.Contains(nodeComprobante?.Attributes["Version"]?.Value ?? string.Empty))
                {
                    datosCFDI = new BaseCFDI
                    {
                        Version = nodeComprobante.Attributes["Version"].Value,
                        Serie = nodeComprobante.Attributes["Serie"].Value,
                        Folio = nodeComprobante.Attributes["Folio"].Value,
                        Fecha = nodeComprobante.Attributes["Fecha"].Value,
                        Moneda = nodeComprobante.Attributes["Moneda"].Value,
                        TipoCambio = nodeComprobante.Attributes["TipoCambio"] is not null ? nodeComprobante.Attributes["TipoCambio"].Value : null,
                        SubTotal = nodeComprobante.Attributes["SubTotal"].Value,
                        Total = nodeComprobante.Attributes["Total"].Value,
                        TipoDeComprobante = nodeComprobante.Attributes["TipoDeComprobante"].Value,
                        RfcEmisor = doc.SelectSingleNode("//cfdi:Comprobante/cfdi:Emisor/@Rfc", nsm).InnerText,
                        RfcReceptor = doc.SelectSingleNode("//cfdi:Comprobante/cfdi:Receptor/@Rfc", nsm).InnerText,
                        TotalImpuestosTrasladados = doc.SelectSingleNode("//cfdi:Comprobante/cfdi:Impuestos/@TotalImpuestosTrasladados", nsm) is not null ? doc.SelectSingleNode("//cfdi:Comprobante/cfdi:Impuestos/@TotalImpuestosTrasladados", nsm).InnerText : null,
                        TotalImpuestosRetenidos = doc.SelectSingleNode("//cfdi:Comprobante/cfdi:Impuestos/@TotalImpuestosRetenidos", nsm) is not null ? doc.SelectSingleNode("//cfdi:Comprobante/cfdi:Impuestos/@TotalImpuestosRetenidos", nsm).InnerText : null,
                        UUID = doc.SelectSingleNode("//cfdi:Comprobante//cfdi:Complemento//tfd:TimbreFiscalDigital", nsm)?.Attributes["UUID"].Value,
                        IsVersionValida = true,
                        XmlB64 = base64String
                    };
                    datosCFDI.Conceptos.AddRange(from XmlNode nc in doc.SelectNodes("//cfdi:Comprobante//cfdi:Conceptos//cfdi:Concepto", nsm) select nc.Attributes["Descripcion"].Value);
                    if (datosCFDI.TipoDeComprobante.Equals("E"))
                    {
                        datosCFDI.TipoRelacion = doc.SelectSingleNode("//cfdi:Comprobante//cfdi:CfdiRelacionados", nsm)?.Attributes["TipoRelacion"]?.Value;
                        datosCFDI.CfdiRelacionados.AddRange(from XmlNode nc in doc.SelectNodes("//cfdi:Comprobante//cfdi:CfdiRelacionados//cfdi:CfdiRelacionado", nsm) select nc.Attributes["UUID"].Value);
                    }
                    if (datosCFDI.TipoDeComprobante.Equals("P"))
                    {
                        XmlNodeList nodePagos = null;
                        string strXPathImpuesto = string.Empty;

                        if (datosCFDI.Version.Equals("3.3"))
                            nodePagos = doc.SelectNodes("//cfdi:Comprobante//cfdi:Complemento//pago10:Pagos//pago10:Pago", nsm);
                        else
                        {
                            nodePagos = doc.SelectNodes("//cfdi:Comprobante//cfdi:Complemento//pago20:Pagos//pago20:Pago", nsm);
                            strXPathImpuesto = "pago20:ImpuestosDR//pago20:TrasladosDR//pago20:TrasladoDR";
                        }

                        datosCFDI.Pagos = new List<CfdiPagos>();
                        foreach (XmlNode np in nodePagos)
                        {
                            CfdiPagos tPagos = new CfdiPagos();
                            tPagos.FechaPago = np.Attributes["FechaPago"].Value;
                            tPagos.TipoCambioP = np.Attributes["TipoCambioP"] != null ? np.Attributes["TipoCambioP"].Value : null;
                            tPagos.DoctosRelacionados = new List<CfdiPagoDocto>();

                            foreach (XmlNode childNode in np.ChildNodes)
                            {
                                if (childNode.Name != "pago20:ImpuestosP")
                                {
                                    CfdiPagoDocto tDoctoRel = new CfdiPagoDocto();
                                    tDoctoRel.Uuid = childNode.Attributes["IdDocumento"].Value;
                                    tDoctoRel.Serie = childNode.Attributes["Serie"] != null ? childNode.Attributes["Serie"].Value : null;
                                    tDoctoRel.Folio = childNode.Attributes["Folio"] != null ? childNode.Attributes["Folio"].Value : null;
                                    tDoctoRel.ImporteSaldoInsoluto = childNode.Attributes["ImpSaldoInsoluto"] != null ? childNode.Attributes["ImpSaldoInsoluto"].Value : null;
                                    tDoctoRel.ImportePagado = childNode.Attributes["ImpPagado"] != null ? childNode.Attributes["ImpPagado"].Value : null;
                                    tDoctoRel.ImporteSaldoAnt = childNode.Attributes["ImpSaldoAnt"] != null ? childNode.Attributes["ImpSaldoAnt"].Value : null;
                                    tDoctoRel.MonedaDR = childNode.Attributes["MonedaDR"] != null ? childNode.Attributes["MonedaDR"].Value : null;
                                    if (!string.IsNullOrEmpty(strXPathImpuesto))
                                        tDoctoRel.ImporteDR = childNode.SelectSingleNode(strXPathImpuesto, nsm).Attributes["ImporteDR"] != null ?
                                        childNode.SelectSingleNode(strXPathImpuesto, nsm).Attributes["ImporteDR"].Value : null;

                                    tPagos.DoctosRelacionados.Add(tDoctoRel);
                                }

                            }

                            datosCFDI.Pagos.Add(tPagos);


                        }
                    }
                }
                else datosCFDI.IsVersionValida = default;
            }
            catch (Exception ex)
            {
                // _logger.LogError($"La factura no es una versión valida: {base64String}, error {ex.Message} || {ex.InnerException}");
                datosCFDI = null;
            }
            return datosCFDI;
        }        
        #endregion
    }
}
