using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Tools;
using System.Text.Json.Nodes;

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
		#endregion

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

        public async Task<(bool existe, string mensaje)> AddNotaCredito(TB_Proyecto_Factura_Nota_Credito notaCredito)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                var inseert = await db.tB_ProyectoFacturasNotaCredito
                .Value(x => x.IdFactura, notaCredito.IdFactura)
                .Value(x => x.UuidNotaCredito, notaCredito.UuidNotaCredito)                
                .Value(x => x.IdMoneda, notaCredito.IdMoneda)
                .Value(x => x.IdTipoRelacion, notaCredito.IdMoneda)
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

        public async Task<(bool existe, string mensaje)> AddPagos(TB_Proyecto_Factura_Cobranza pagos)
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
        public async Task<(bool existe, string mensaje)> CancelFactura(TB_ProyectoFactura factura)
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

                foreach(var nota in notas_factura)
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

        public async Task<(bool existe, string mensaje)> CancelNota(JsonObject registro)
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

        public async Task<(bool existe, string mensaje)> CancelCobranza(JsonObject registro)
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
        public async Task<List<FacturaDetalles>> GetAllFacturas()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from a in db.tB_ProyectoFacturas
                                 join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                                 from ab in factNC.DefaultIfEmpty()
                                 join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                                 from ac in factC.DefaultIfEmpty()
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
                                     NC_UuidNotaCredito = ab.UuidNotaCredito,
                                     NC_IdMoneda = ab.IdMoneda,
                                     NC_IdTipoRelacion = ab.IdTipoRelacion,
                                     NC_NotaCredito = ab.NotaCredito,
                                     NC_Importe = ab.Importe,
                                     NC_Iva = ab.Iva,
                                     NC_Total = ab.Total,
                                     NC_Concepto = ab.Concepto,
                                     NC_Mes = ab.Mes,
                                     NC_Anio = ab.Anio,
                                     NC_TipoCambio = ab.TipoCambio,
                                     NC_FechaNotaCredito = ab.FechaNotaCredito,
                                     C_UuidCobranza = ac.UuidCobranza,
                                     C_IdMonedaP = ac.IdMonedaP,
                                     C_ImportePagado = ac.ImportePagado,
                                     C_ImpSaldoAnt = ac.ImpSaldoAnt,
                                     C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                                     C_IvaP = ac.IvaP,
                                     C_TipoCambioP = ac.TipoCambioP,
                                     C_FechaPago = ac.FechaPago
                                 }).ToListAsync();

                foreach (var facturaDetalle in res)
                {
                    var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                           where notas.IdFactura == facturaDetalle.Id
                                           && notas.FechaCancelacion == null
                                           select notas).ToListAsync();

                    facturaDetalle.NotasCredito = res_notas.Count();

                    var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                               where cobr.IdFactura == facturaDetalle.Id
                                               && cobr.FechaCancelacion == null
                                               select cobr).ToListAsync();

                    facturaDetalle.Cobranzas = res_cobranzas.Count();
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
                var res = from a in db.tB_ProyectoFacturas
                          join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                          from ab in factNC.DefaultIfEmpty()
                          join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                          from ac in factC.DefaultIfEmpty()
                          where a.NumProyecto == idProyecto
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
                              NC_UuidNotaCredito = ab.UuidNotaCredito,
                              NC_IdMoneda = ab.IdMoneda,
                              NC_IdTipoRelacion = ab.IdTipoRelacion,
                              NC_NotaCredito = ab.NotaCredito,
                              NC_Importe = ab.Importe,
                              NC_Iva = ab.Iva,
                              NC_Total = ab.Total,
                              NC_Concepto = ab.Concepto,
                              NC_Mes = ab.Mes,
                              NC_Anio = ab.Anio,
                              NC_TipoCambio = ab.TipoCambio,
                              NC_FechaNotaCredito = ab.FechaNotaCredito,
                              C_UuidCobranza = ac.UuidCobranza,
                              C_IdMonedaP = ac.IdMonedaP,
                              C_ImportePagado = ac.ImportePagado,
                              C_ImpSaldoAnt = ac.ImpSaldoAnt,
                              C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                              C_IvaP = ac.IvaP,
                              C_TipoCambioP = ac.TipoCambioP,
                              C_FechaPago = ac.FechaPago
                          };

                return await res.ToListAsync();

            }
        }
        public async Task<List<FacturaDetalles>> GetFacturasProyectoFecha(int? idProyecto, DateTime? fechaIni, DateTime? fechaFin)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = from a in db.tB_ProyectoFacturas
                          join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                          from ab in factNC.DefaultIfEmpty()
                          join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                          from ac in factC.DefaultIfEmpty()
                          where a.NumProyecto == idProyecto && a.FechaEmision >= fechaIni && a.FechaEmision <= fechaFin
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
                              NC_UuidNotaCredito = ab.UuidNotaCredito,
                              NC_IdMoneda = ab.IdMoneda,
                              NC_IdTipoRelacion = ab.IdTipoRelacion,
                              NC_NotaCredito = ab.NotaCredito,
                              NC_Importe = ab.Importe,
                              NC_Iva = ab.Iva,
                              NC_Total = ab.Total,
                              NC_Concepto = ab.Concepto,
                              NC_Mes = ab.Mes,
                              NC_Anio = ab.Anio,
                              NC_TipoCambio = ab.TipoCambio,
                              NC_FechaNotaCredito = ab.FechaNotaCredito,
                              C_UuidCobranza = ac.UuidCobranza,
                              C_IdMonedaP = ac.IdMonedaP,
                              C_ImportePagado = ac.ImportePagado,
                              C_ImpSaldoAnt = ac.ImpSaldoAnt,
                              C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                              C_IvaP = ac.IvaP,
                              C_TipoCambioP = ac.TipoCambioP,
                              C_FechaPago = ac.FechaPago
                          };

                return await res.ToListAsync();

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

                var res = from a in db.tB_ProyectoFacturas
                          join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                          from ab in factNC.DefaultIfEmpty()
                          join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                          from ac in factC.DefaultIfEmpty()
                          where a.NumProyecto.In(lstProyectos)
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
                              NC_UuidNotaCredito = ab.UuidNotaCredito,
                              NC_IdMoneda = ab.IdMoneda,
                              NC_IdTipoRelacion = ab.IdTipoRelacion,
                              NC_NotaCredito = ab.NotaCredito,
                              NC_Importe = ab.Importe,
                              NC_Iva = ab.Iva,
                              NC_Total = ab.Total,
                              NC_Concepto = ab.Concepto,
                              NC_Mes = ab.Mes,
                              NC_Anio = ab.Anio,
                              NC_TipoCambio = ab.TipoCambio,
                              NC_FechaNotaCredito = ab.FechaNotaCredito,
                              C_UuidCobranza = ac.UuidCobranza,
                              C_IdMonedaP = ac.IdMonedaP,
                              C_ImportePagado = ac.ImportePagado,
                              C_ImpSaldoAnt = ac.ImpSaldoAnt,
                              C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                              C_IvaP = ac.IvaP,
                              C_TipoCambioP = ac.TipoCambioP,
                              C_FechaPago = ac.FechaPago
                          };

                return await res.ToListAsync();

            }
        }

        public async Task<List<FacturaDetalles>> GetFacturasEmpresaFecha(int? idEmpresa, DateTime? fechaIni, DateTime? fechaFin)
        {
            using (var db = new ConnectionDB(dbConfig))
            {


                var lstProyectos = (from a in db.tB_Proyectos
                                    where a.IdEmpresa == idEmpresa
                                    select a.NumProyecto).ToArray();

                var res = from a in db.tB_ProyectoFacturas
                          join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                          from ab in factNC.DefaultIfEmpty()
                          join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                          from ac in factC.DefaultIfEmpty()
                          where a.NumProyecto.In(lstProyectos)
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
                              NC_UuidNotaCredito = ab.UuidNotaCredito,
                              NC_IdMoneda = ab.IdMoneda,
                              NC_IdTipoRelacion = ab.IdTipoRelacion,
                              NC_NotaCredito = ab.NotaCredito,
                              NC_Importe = ab.Importe,
                              NC_Iva = ab.Iva,
                              NC_Total = ab.Total,
                              NC_Concepto = ab.Concepto,
                              NC_Mes = ab.Mes,
                              NC_Anio = ab.Anio,
                              NC_TipoCambio = ab.TipoCambio,
                              NC_FechaNotaCredito = ab.FechaNotaCredito,
                              C_UuidCobranza = ac.UuidCobranza,
                              C_IdMonedaP = ac.IdMonedaP,
                              C_ImportePagado = ac.ImportePagado,
                              C_ImpSaldoAnt = ac.ImpSaldoAnt,
                              C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                              C_IvaP = ac.IvaP,
                              C_TipoCambioP = ac.TipoCambioP,
                              C_FechaPago = ac.FechaPago
                          };

                return await res.ToListAsync();

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

                var res = from a in db.tB_ProyectoFacturas
                          join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                          from ab in factNC.DefaultIfEmpty()
                          join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                          from ac in factC.DefaultIfEmpty()
                          where a.NumProyecto.In(lstProyectos) 
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
                              NC_UuidNotaCredito = ab.UuidNotaCredito,
                              NC_IdMoneda = ab.IdMoneda,
                              NC_IdTipoRelacion = ab.IdTipoRelacion,
                              NC_NotaCredito = ab.NotaCredito,
                              NC_Importe = ab.Importe,
                              NC_Iva = ab.Iva,
                              NC_Total = ab.Total,
                              NC_Concepto = ab.Concepto,
                              NC_Mes = ab.Mes,
                              NC_Anio = ab.Anio,
                              NC_TipoCambio = ab.TipoCambio,
                              NC_FechaNotaCredito = ab.FechaNotaCredito,
                              C_UuidCobranza = ac.UuidCobranza,
                              C_IdMonedaP = ac.IdMonedaP,
                              C_ImportePagado = ac.ImportePagado,
                              C_ImpSaldoAnt = ac.ImpSaldoAnt,
                              C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                              C_IvaP = ac.IvaP,
                              C_TipoCambioP = ac.TipoCambioP,
                              C_FechaPago = ac.FechaPago
                          };

                return await res.ToListAsync();

            }
        }
        public async Task<List<FacturaDetalles>> GetFacturasClienteFecha(int? idCliente, DateTime? fechaIni, DateTime? fechaFin)
        {
            using (var db = new ConnectionDB(dbConfig))
            {


                var lstProyectos = (from a in db.tB_Proyectos
                                    where a.IdEmpresa == idCliente
                                    select a.NumProyecto).ToArray();

                var res = from a in db.tB_ProyectoFacturas
                          join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                          from ab in factNC.DefaultIfEmpty()
                          join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                          from ac in factC.DefaultIfEmpty()
                          where a.NumProyecto.In(lstProyectos) && a.FechaEmision >= fechaIni && a.FechaEmision <= fechaFin
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
                              NC_UuidNotaCredito = ab.UuidNotaCredito,
                              NC_IdMoneda = ab.IdMoneda,
                              NC_IdTipoRelacion = ab.IdTipoRelacion,
                              NC_NotaCredito = ab.NotaCredito,
                              NC_Importe = ab.Importe,
                              NC_Iva = ab.Iva,
                              NC_Total = ab.Total,
                              NC_Concepto = ab.Concepto,
                              NC_Mes = ab.Mes,
                              NC_Anio = ab.Anio,
                              NC_TipoCambio = ab.TipoCambio,
                              NC_FechaNotaCredito = ab.FechaNotaCredito,
                              C_UuidCobranza = ac.UuidCobranza,
                              C_IdMonedaP = ac.IdMonedaP,
                              C_ImportePagado = ac.ImportePagado,
                              C_ImpSaldoAnt = ac.ImpSaldoAnt,
                              C_ImporteSaldoInsoluto = ac.ImporteSaldoInsoluto,
                              C_IvaP = ac.IvaP,
                              C_TipoCambioP = ac.TipoCambioP,
                              C_FechaPago = ac.FechaPago
                          };

                return await res.ToListAsync();

            }
        }

        #endregion        

        public Task<List<TB_Proyecto>> GetProyecto() => GetAllFromEntityAsync<TB_Proyecto>();

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect();
		}
	}
}
