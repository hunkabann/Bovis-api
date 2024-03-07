using Azure;
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


        #region Info Proyecto
        public async Task<Factura_Proyecto> GetInfoProyecto(int numProyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from p in db.tB_Proyectos
                                 join e in db.tB_Empresas on p.IdEmpresa equals e.IdEmpresa into eJoin
                                 from eItem in eJoin.DefaultIfEmpty()
                                 join cp in db.tB_ClienteProyectos on p.NumProyecto equals cp.NumProyecto into cpJoin
                                 from cpItem in cpJoin.DefaultIfEmpty()
                                 join c in db.tB_Clientes on cpItem.IdCliente equals c.IdCliente into cJoin
                                 from cItem in cJoin.DefaultIfEmpty()
                                 where p.NumProyecto == numProyecto
                                 select new Factura_Proyecto
                                 {
                                     NumProyecto = p.NumProyecto,
                                     Nombre = p.Proyecto,
                                     RfcBaseEmisor = eItem.Rfc ?? string.Empty
                                 }).FirstOrDefaultAsync();
                
                res.RfcBaseReceptor = new List<string>();

                res.RfcBaseReceptor.AddRange(await (from cp in db.tB_ClienteProyectos
                                  join c in db.tB_Clientes on cp.IdCliente equals c.IdCliente into cJoin
                                  from cItem in cJoin.DefaultIfEmpty()
                                  where cp.NumProyecto == numProyecto
                                  select cItem.Rfc).ToListAsync());

                return res;
            }
        }

        public Task<List<TB_Proyecto>> GetProyecto() => GetAllFromEntityAsync<TB_Proyecto>();
        #endregion Info Proyecto


        #region Facturas
        public async Task<TB_ProyectoFactura> SearchFactura(string uuid)
        {
            using (var db = new ConnectionDB(dbConfig)) return await (from a in db.tB_ProyectoFacturas
                                                                      where a.Uuid == uuid
                                                                      select a).FirstOrDefaultAsync();
        }

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

        public async Task<(bool Success, string Message)> CancelFactura(TB_ProyectoFactura factura)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            List<TB_ProyectoFacturaNotaCredito> notas_factura = null;
            List<TB_ProyectoFacturaCobranza> cobranzas_factura = null;
            DateTime? fecha_cancelacion = factura.FechaCancelacion;
            string? motivo_cancelacion = factura.MotivoCancelacion;

            using (var db = new ConnectionDB(dbConfig))
            {
                factura = await (from fact in db.tB_ProyectoFacturas
                                 where fact.Id == factura.Id
                                 select fact).FirstOrDefaultAsync();


                if (factura.FechaEmision.Month == fecha_cancelacion.Value.Month && factura.FechaEmision.Year == fecha_cancelacion.Value.Year)
                {
                    var objetivoDB = await (db.tB_ProyectoFacturas.Where(x => x.Id == factura.Id)
                        .UpdateAsync(x => new TB_ProyectoFactura
                        {
                            //Total = 0,
                            FechaCancelacion = fecha_cancelacion,
                            MotivoCancelacion = motivo_cancelacion
                        })) > 0;

                    resp.Success = objetivoDB;
                    resp.Message = objetivoDB == default ? "Ocurrio un error al cancelar registro de factura." : string.Empty;

                    notas_factura = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.FechaCancelacion == null
                                           && notas.IdFactura == factura.Id
                                           select notas).ToListAsync();

                    foreach (var nota in notas_factura)
                    {
                        var resp_nota = await (db.tB_ProyectoFacturasNotaCredito.Where(x => x.UuidNotaCredito == nota.UuidNotaCredito)
                            .UpdateAsync(x => new TB_ProyectoFacturaNotaCredito
                            {
                                //Total = 0,
                                FechaCancelacion = fecha_cancelacion,
                                MotivoCancelacion = motivo_cancelacion
                            })) > 0;

                        resp.Success = resp_nota;
                        resp.Message = resp_nota == default ? "Ocurrio un error al cancelar registro de nota de factura." : string.Empty;
                    }

                    cobranzas_factura = await (from cobranza in db.tB_ProyectoFacturasCobranza
                                               where cobranza.FechaCancelacion == null
                                               && cobranza.IdFactura == factura.Id
                                               select cobranza).ToListAsync();

                    foreach (var cobranza in cobranzas_factura)
                    {
                        var resp_cobranza = await (db.tB_ProyectoFacturasCobranza.Where(x => x.UuidCobranza == cobranza.UuidCobranza)
                                .UpdateAsync(x => new TB_ProyectoFacturaCobranza
                                {
                                    FechaCancelacion = fecha_cancelacion,
                                    MotivoCancelacion = motivo_cancelacion
                                })) > 0;

                        resp.Success = resp_cobranza;
                        resp.Message = resp_cobranza == default ? "Ocurrio un error al cancelar registro de cobranza de factura." : string.Empty;
                    }
                }
                else
                {
                    var insert_cancel_invoice = await db.tB_ProyectoFacturas
                        .Value(x => x.NumProyecto, factura.NumProyecto)
                        .Value(x => x.IdTipoFactura, factura.IdTipoFactura)
                        .Value(x => x.IdMoneda, factura.IdMoneda)
                        .Value(x => x.Uuid, factura.Uuid)
                        .Value(x => x.Importe, factura.Importe)
                        .Value(x => x.Iva, factura.Iva)
                        .Value(x => x.IvaRet, factura.IvaRet)
                        .Value(x => x.Total, factura.Total)
                        .Value(x => x.Concepto, factura.Concepto)
                        .Value(x => x.Mes, factura.FechaEmision.Month)
                        .Value(x => x.Anio, factura.FechaEmision.Year)
                        .Value(x => x.FechaEmision, factura.FechaEmision)
                        .Value(x => x.NoFactura, factura.NoFactura)
                        .Value(x => x.TipoCambio, factura.TipoCambio)
                        .Value(x => x.XmlB64, factura.XmlB64)
                        .Value(x => x.FechaCancelacion, fecha_cancelacion)
                        .Value(x => x.MotivoCancelacion, motivo_cancelacion)
                        .InsertAsync() > 0;

                    resp.Success = insert_cancel_invoice;
                    resp.Message = insert_cancel_invoice == default ? "Ocurrio un error al agregar la cancelación de la factura." : string.Empty;
                }

                notas_factura = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                       where notas.FechaCancelacion == null
                                       && notas.IdFactura == factura.Id
                                       select notas).ToListAsync();

                foreach (var nota in notas_factura)
                {


                    var insert_cancel_nota = await db.tB_ProyectoFacturasNotaCredito
                        .Value(x => x.IdFactura, nota.IdFactura)
                        .Value(x => x.NumProyecto, nota.NumProyecto)
                        .Value(x => x.UuidNotaCredito, nota.UuidNotaCredito)
                        .Value(x => x.IdMoneda, nota.IdMoneda)
                        .Value(x => x.IdTipoRelacion, nota.IdTipoRelacion)
                        .Value(x => x.NotaCredito, nota.NotaCredito)
                        .Value(x => x.Importe, nota.Importe)
                        .Value(x => x.Iva, nota.Iva)
                        .Value(x => x.Total, nota.Total)
                        .Value(x => x.Concepto, nota.Concepto)
                        .Value(x => x.Mes, nota.FechaNotaCredito.Month)
                        .Value(x => x.Anio, nota.FechaNotaCredito.Year)
                        .Value(x => x.TipoCambio, nota.TipoCambio)
                        .Value(x => x.FechaNotaCredito, nota.FechaNotaCredito)
                        .Value(x => x.Xml, nota.Xml)
                        .Value(x => x.FechaCancelacion, fecha_cancelacion)
                        .Value(x => x.MotivoCancelacion, motivo_cancelacion)
                        .InsertAsync() > 0;

                    resp.Success = insert_cancel_nota;
                    resp.Message = insert_cancel_nota == default ? "Ocurrio un error al agregar la cancelación de la nota de credito." : string.Empty;
                }

                cobranzas_factura = await (from cobranza in db.tB_ProyectoFacturasCobranza
                                           where cobranza.FechaCancelacion == null
                                           && cobranza.IdFactura == factura.Id
                                           select cobranza).ToListAsync();

                foreach (var cobranza in cobranzas_factura)
                {
                    var insert_cancel_cobranza = await db.tB_ProyectoFacturasCobranza
                        .Value(x => x.IdFactura, cobranza.IdFactura)
                        .Value(x => x.UuidCobranza, cobranza.UuidCobranza)
                        .Value(x => x.IdMonedaP, cobranza.IdMonedaP)
                        .Value(x => x.ImportePagado, cobranza.ImportePagado)
                        .Value(x => x.ImpSaldoAnt, cobranza.ImpSaldoAnt)
                        .Value(x => x.ImporteSaldoInsoluto, cobranza.ImporteSaldoInsoluto)
                        .Value(x => x.IvaP, cobranza.IvaP)
                        .Value(x => x.TipoCambioP, cobranza.TipoCambioP)
                        .Value(x => x.FechaPago, cobranza.FechaPago)
                        .Value(x => x.Xml, cobranza.Xml)
                        .Value(x => x.CRP, cobranza.CRP)
                        .Value(x => x.Base, cobranza.Base)
                        .Value(X => X.FechaCancelacion, fecha_cancelacion)
                        .Value(x => x.MotivoCancelacion, motivo_cancelacion)
                        .InsertAsync() > 0;

                    resp.Success = insert_cancel_cobranza;
                    resp.Message = insert_cancel_cobranza == default ? "Ocurrio un error al agregar la cancelación del pago." : string.Empty;
                }
            }

            return resp;
        }

        public async Task<List<FacturaDetalles>> GetAllFacturas(int? idProyecto, int? idCliente, int? idEmpresa, DateTime? fechaIni, DateTime? fechaFin, string? noFactura)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                List<int> lstProyectosCliente = null;
                List<int> lstProyectosEmpresa = null;

                if (idCliente != null)
                {
                    lstProyectosCliente = await (from a in db.tB_ClienteProyectos
                                                 where a.IdCliente == idCliente
                                                 select a.NumProyecto.GetValueOrDefault()).ToListAsync();
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
                                 join cp in db.tB_ClienteProyectos on a.NumProyecto equals cp.NumProyecto into cpJoin
                                 from cpItem in cpJoin.DefaultIfEmpty()
                                 join d in db.tB_Clientes on cpItem.IdCliente equals d.IdCliente into dJoin
                                 from dItem in dJoin.DefaultIfEmpty()
                                 join e in db.tB_Empresas on cItem.IdEmpresa equals e.IdEmpresa into eJoin
                                 from eItem in eJoin.DefaultIfEmpty()
                                 where (idProyecto == null || a.NumProyecto == idProyecto)
                                 && (lstProyectosCliente == null || a.NumProyecto.In(lstProyectosCliente))
                                 && (lstProyectosEmpresa == null || a.NumProyecto.In(lstProyectosEmpresa))
                                 && (fechaIni == null || a.FechaEmision >= fechaIni)
                                 && (fechaFin == null || a.FechaEmision <= fechaFin)
                                 && (noFactura == null || a.NoFactura == noFactura)
                                 orderby a.Id descending
                                 select new FacturaDetalles
                                 {
                                     Id = a.Id,
                                     Uuid = a.Uuid,
                                     NumProyecto = a.NumProyecto,
                                     Cliente = dItem.Cliente ?? string.Empty,
                                     ClienteRFC = dItem.Rfc ?? string.Empty,
                                     IdEmpresa = (int)cItem.IdEmpresa,
                                     Empresa = eItem.Empresa,
                                     EmpresaRFC = eItem.Rfc,
                                     IdTipoFactura = a.IdTipoFactura,
                                     IdMoneda = a.IdMoneda,
                                     Importe = a.Importe,
                                     Iva = a.Iva,
                                     IvaRet = a.IvaRet,
                                     Total = a.FechaCancelacion == null ? a.Total : 0,
                                     Concepto = a.Concepto,
                                     Mes = a.Mes,
                                     Anio = a.Anio,
                                     FechaEmision = a.FechaEmision,
                                     FechaCancelacion = a.FechaCancelacion,
                                     FechaPago = a.FechaPago,
                                     NoFactura = a.NoFactura,
                                     TipoCambio = a.TipoCambio,
                                     MotivoCancelacion = a.MotivoCancelacion
                                 })
                                 .ToListAsync();

                //res = res.DistinctBy(x => x.Uuid).ToList();
                res = res.GroupBy(x => x.Uuid)
                         .Select(group => group.OrderByDescending(x => x.FechaCancelacion ?? DateTime.MinValue).First())
                         .ToList();

                foreach (var facturaDetalle in res)
                {
                    var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.IdFactura == facturaDetalle.Id
                                           //&& notas.FechaCancelacion == null
                                           select new NotaDetalle
                                           {
                                               NC_UuidNotaCredito = notas.UuidNotaCredito,
                                               NC_IdMoneda = notas.IdMoneda,
                                               NC_IdTipoRelacion = notas.IdTipoRelacion,
                                               NC_NotaCredito = notas.NotaCredito,
                                               NC_Importe = notas.Importe,
                                               NC_Iva = notas.Iva,
                                               NC_Total = notas.FechaCancelacion == null ? notas.Total : 0,
                                               NC_Concepto = notas.Concepto,
                                               NC_Mes = notas.Mes,
                                               NC_Anio = notas.Anio,
                                               NC_TipoCambio = notas.TipoCambio,
                                               NC_FechaNotaCredito = notas.FechaNotaCredito,
                                               NC_FechaCancelacion = notas.FechaCancelacion,
                                               Cliente = facturaDetalle.Cliente,
                                               IdEmpresa = facturaDetalle.IdEmpresa,
                                               Empresa = facturaDetalle.Empresa,
                                               EmpresaRFC = facturaDetalle.EmpresaRFC
                                           }).ToListAsync();

                    //res_notas = res_notas.DistinctBy(x => x.NC_UuidNotaCredito).ToList();
                    res_notas = res_notas.GroupBy(x => x.NC_UuidNotaCredito)
                                         .Select(group => group.OrderByDescending(x => x.NC_FechaCancelacion ?? DateTime.MinValue).First())
                                         .ToList();
                    facturaDetalle.Notas = new List<NotaDetalle>();
                    facturaDetalle.Notas.AddRange(res_notas);

                    var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                               where cobr.IdFactura == facturaDetalle.Id
                                               //&& cobr.FechaCancelacion == null
                                               select new CobranzaDetalle
                                               {
                                                   C_UuidCobranza = cobr.UuidCobranza,
                                                   C_IdMonedaP = cobr.IdMonedaP,
                                                   C_ImportePagado = cobr.ImportePagado,
                                                   C_ImpSaldoAnt = cobr.ImpSaldoAnt,
                                                   C_ImporteSaldoInsoluto = cobr.ImporteSaldoInsoluto,
                                                   C_IvaP = cobr.IvaP,
                                                   C_TipoCambioP = cobr.TipoCambioP,
                                                   C_FechaPago = cobr.FechaPago,
                                                   C_FechaCancelacion = cobr.FechaCancelacion,
                                                   CRP = cobr.CRP,
                                                   Base = cobr.Base,
                                                   Cliente = facturaDetalle.Cliente,
                                                   IdEmpresa = facturaDetalle.IdEmpresa,
                                                   Empresa = facturaDetalle.Empresa,
                                                   EmpresaRFC = facturaDetalle.EmpresaRFC
                                               }).ToListAsync();

                    //res_cobranzas = res_cobranzas.DistinctBy(x => x.C_UuidCobranza).ToList();
                    res_cobranzas = res_cobranzas.GroupBy(x => x.C_FechaCancelacion)
                                                 .Select(group => group.OrderByDescending(x => x.C_FechaCancelacion ?? DateTime.MinValue).First())
                                                 .ToList();
                    facturaDetalle.Cobranzas = new List<CobranzaDetalle>();
                    facturaDetalle.Cobranzas.AddRange(res_cobranzas);

                    facturaDetalle.TotalNotasCredito = res_notas.Count();
                    facturaDetalle.TotalCobranzas = res_cobranzas.Count();
                }

                return res;
            }
        }

        #endregion Facturas


        #region Notas
        public async Task<TB_ProyectoFacturaNotaCredito> SearchNotaCredito(string uuid)
        {
            using (var db = new ConnectionDB(dbConfig)) return await (from a in db.tB_ProyectoFacturasNotaCredito
                                                                      where a.UuidNotaCredito == uuid
                                                                      select a).FirstOrDefaultAsync();
        }

        public async Task<(bool Success, string Message)> AddNotaCredito(TB_ProyectoFacturaNotaCredito notaCredito)
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

        public async Task<List<NotaCredito_Detalle>> GetNotaCreditoSinFactura(int NumProyecto, int Mes, int Anio)
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
                                           where (notas.NumProyecto != null && notas.IdFactura == null)
                                           && (NumProyecto == 0 || notas.NumProyecto == NumProyecto)
                                           && (Mes == 0 || notas.Mes == Mes)
                                           && (Anio == 0 || notas.Anio == Anio)
                                           orderby notas.FechaNotaCredito descending
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

        public async Task<(bool Success, string Message)> AddNotaCreditoSinFacturaToFactura(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_factura = Convert.ToInt32(registro["id_factura"].ToString());
            string uuid_nota_credito = registro["uuid_nota_credito"].ToString();

            using (var db = new ConnectionDB(dbConfig))
            {
                var update_add_nota = await (db.tB_ProyectoFacturasNotaCredito.Where(x => x.UuidNotaCredito == uuid_nota_credito)
                    .UpdateAsync(x => new TB_ProyectoFacturaNotaCredito
                    {
                        IdFactura = id_factura
                    })) > 0;

                resp.Success = update_add_nota;
                resp.Message = update_add_nota == default ? "Ocurrio un error al relacionar la nota de credito a la factura." : string.Empty;
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
                var nota_factura = await (from nota in db.tB_ProyectoFacturasNotaCredito
                                          where nota.FechaCancelacion == null
                                          && nota.UuidNotaCredito == uuid_nota
                                          select nota).FirstOrDefaultAsync();

                if (nota_factura.FechaNotaCredito.Month == DateTime.Now.Month && nota_factura.FechaNotaCredito.Year == DateTime.Now.Year)
                {
                    var res_update_nota = await db.tB_ProyectoFacturasNotaCredito.Where(x => x.UuidNotaCredito == uuid_nota)
                                    .UpdateAsync(x => new TB_ProyectoFacturaNotaCredito
                                    {
                                        //Total = 0,
                                        FechaCancelacion = fecha_cancelacion,
                                        MotivoCancelacion = motivo_cancelacion
                                    }) > 0;

                    resp.Success = res_update_nota;
                    resp.Message = res_update_nota == default ? "Ocurrio un error al cancelar la nota." : string.Empty;
                }
                else
                {
                    var insert_cancel_nota = await db.tB_ProyectoFacturasNotaCredito
                    .Value(x => x.IdFactura, nota_factura.IdFactura)
                    .Value(x => x.NumProyecto, nota_factura.NumProyecto)
                    .Value(x => x.UuidNotaCredito, nota_factura.UuidNotaCredito)
                    .Value(x => x.IdMoneda, nota_factura.IdMoneda)
                    .Value(x => x.IdTipoRelacion, nota_factura.IdTipoRelacion)
                    .Value(x => x.NotaCredito, nota_factura.NotaCredito)
                    .Value(x => x.Importe, nota_factura.Importe)
                    .Value(x => x.Iva, nota_factura.Iva)
                    .Value(x => x.Total, nota_factura.Total)
                    .Value(x => x.Concepto, nota_factura.Concepto)
                    .Value(x => x.Mes, nota_factura.FechaNotaCredito.Month)
                    .Value(x => x.Anio, nota_factura.FechaNotaCredito.Year)
                    .Value(x => x.FechaNotaCredito, nota_factura.FechaNotaCredito)
                    .Value(x => x.TipoCambio, nota_factura.TipoCambio)
                    .Value(x => x.Xml, nota_factura.Xml)
                    .Value(x => x.FechaCancelacion, fecha_cancelacion)
                    .Value(x => x.MotivoCancelacion, motivo_cancelacion)
                    .InsertAsync() > 0;

                    resp.Success = insert_cancel_nota;
                    resp.Message = insert_cancel_nota == default ? "Ocurrio un error al agregar la cancelación de la nota de credito." : string.Empty;

                }
            }

            return resp;
        }
        #endregion Notas


        #region Pagos
        public async Task<TB_ProyectoFacturaCobranza> SearchPagos(string uuid)
        {
            using (var db = new ConnectionDB(dbConfig)) return await (from a in db.tB_ProyectoFacturasCobranza
                                                                      where a.UuidCobranza == uuid
                                                                      select a).FirstOrDefaultAsync();
        }

        public async Task<(bool Success, string Message)> AddPagos(TB_ProyectoFacturaCobranza pagos)
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
                    .Value(x => x.CRP, pagos.CRP)
                    .Value(x => x.Base, pagos.Base)
                    .InsertAsync() > 0;

                resp.Success = inseert;
                resp.Message = inseert == default ? "Ocurrio un error al agregar el pago." : string.Empty;
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
                var cobranza_factura = await (from cobranza in db.tB_ProyectoFacturasCobranza
                                              where cobranza.FechaCancelacion == null
                                              && cobranza.UuidCobranza == uuid_cobranza
                                              select cobranza).FirstOrDefaultAsync();

                if (cobranza_factura.FechaPago.Month == DateTime.Now.Month && cobranza_factura.FechaPago.Year == DateTime.Now.Year)
                {
                    var res_update_nota = await db.tB_ProyectoFacturasCobranza.Where(x => x.UuidCobranza == uuid_cobranza)
                                    .UpdateAsync(x => new TB_ProyectoFacturaCobranza
                                    {
                                        FechaCancelacion = fecha_cancelacion,
                                        MotivoCancelacion = motivo_cancelacion
                                    }) > 0;

                    resp.Success = res_update_nota;
                    resp.Message = res_update_nota == default ? "Ocurrio un error al cancelar la cobranza." : string.Empty;
                }
                else
                {
                    var insert_cancel_cobranza = await db.tB_ProyectoFacturasCobranza
                    .Value(x => x.IdFactura, cobranza_factura.IdFactura)
                    .Value(x => x.UuidCobranza, cobranza_factura.UuidCobranza)
                    .Value(x => x.IdMonedaP, cobranza_factura.IdMonedaP)
                    .Value(x => x.ImportePagado, cobranza_factura.ImportePagado)
                    .Value(x => x.ImpSaldoAnt, cobranza_factura.ImpSaldoAnt)
                    .Value(x => x.ImporteSaldoInsoluto, cobranza_factura.ImporteSaldoInsoluto)
                    .Value(x => x.IvaP, cobranza_factura.IvaP)
                    .Value(x => x.TipoCambioP, cobranza_factura.TipoCambioP)
                    .Value(x => x.FechaPago, cobranza_factura.FechaPago)
                    .Value(x => x.Xml, cobranza_factura.Xml)
                    .Value(x => x.CRP, cobranza_factura.CRP)
                    .Value(x => x.Base, cobranza_factura.Base)
                    .Value(X => X.FechaCancelacion, fecha_cancelacion)
                    .Value(x => x.MotivoCancelacion, motivo_cancelacion)
                    .InsertAsync() > 0;

                    resp.Success = insert_cancel_cobranza;
                    resp.Message = insert_cancel_cobranza == default ? "Ocurrio un error al agregar la cancelación del pago." : string.Empty;
                }

            }

            return resp;
        }

        #endregion Pagos








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
                                     MotivoCancelacion = a.MotivoCancelacion
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
                                                   C_FechaPago = cobr.FechaPago,
                                                   CRP = cobr.CRP,
                                                   Base = cobr.Base
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
                                                   C_FechaPago = cobr.FechaPago,
                                                   CRP = cobr.CRP,
                                                   Base = cobr.Base
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
                                                   C_FechaPago = cobr.FechaPago,
                                                   CRP = cobr.CRP,
                                                   Base = cobr.Base
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
                                     MotivoCancelacion = a.MotivoCancelacion
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
                                                   C_FechaPago = cobr.FechaPago,
                                                   CRP = cobr.CRP,
                                                   Base = cobr.Base
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
                                                   C_FechaPago = cobr.FechaPago,
                                                   CRP = cobr.CRP,
                                                   Base = cobr.Base
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
                                                   C_FechaPago = cobr.FechaPago,
                                                   CRP = cobr.CRP,
                                                   Base = cobr.Base
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
                                                   C_FechaPago = cobr.FechaPago,
                                                   CRP = cobr.CRP,
                                                   Base = cobr.Base
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
