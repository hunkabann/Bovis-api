using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Service.Queries.Dto.Request;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Text.Json.Nodes;
using System.Xml;

namespace Bovis.Business
{
    public class FacturaBusiness : IFacturaBusiness
    {
        #region base

        private readonly IFacturaData _facturaData;
        private readonly ILogger<FacturaBusiness> _logger;
        private readonly ITransactionData _transactionData;
        public FacturaBusiness(IFacturaData _facturaData, ITransactionData _transactionData)
        {
            this._facturaData = _facturaData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        #endregion


        #region Info Proyecto
        public Task<Factura_Proyecto> GetInfoProyecto(int numProyecto) => _facturaData.GetInfoProyecto(numProyecto);
        #endregion Info Proyecto


        #region Facturas
        public async Task<List<FacturaRevision>> AddFacturas(AgregarFactura request)
        {
            var LstFacturas = new List<FacturaRevision>();
            
            var tryDate = default(DateTime);
            foreach (var factura in request.LstFacturas)
            {
                var cfdi = await ExtraerDatos(factura.FacturaB64);

                if (cfdi is not null && cfdi.IsVersionValida && cfdi.TipoDeComprobante.Equals("I"))
                {
                    var tmpFactura = new FacturaRevision
                    {
                        RfcEmisor = cfdi.RfcEmisor,
                        RfcReceptor = cfdi.RfcReceptor,
                        FechaEmision = cfdi.Fecha,
                        Total = cfdi.Total,
                        Conceptos = string.Join("|", cfdi.Conceptos),
                        TipoFactura = cfdi?.Conceptos?.FirstOrDefault()?.ToUpper()?.Contains("COBROS POR PAGOS A CTA DE TERCEROS") == true ? "TRADES" : "PROPIOS",
                        NoFactura = $"{cfdi.Serie ?? string.Empty}{cfdi.Folio ?? string.Empty}",
                        FacturaNombre = factura.FacturaNombre,
                        Almacenada = true
                    };

                    string rfcReceptor = request.RfcReceptor.Where(x => x == cfdi.RfcReceptor).FirstOrDefault();

                    //if (cfdi.RfcReceptor.Equals(request.RfcReceptor) && cfdi.RfcEmisor.Equals(request.RfcEmisor))
                    if ((!string.IsNullOrEmpty(rfcReceptor) && cfdi.RfcReceptor.Equals(rfcReceptor)) && cfdi.RfcEmisor.Equals(request.RfcEmisor))
                    {
                        var existeF = await _facturaData.SearchFactura(cfdi.UUID);

                        if (existeF != null)
                        {
                            tmpFactura.Almacenada = false;
                            tmpFactura.Error = $@"La factura {factura.FacturaNombre} ya existe en la BD";
                        }
                        else
                        {
                            tryDate = default;
                            if (DateTime.TryParse(cfdi.Fecha, out tryDate))
                            {
                                decimal total = cfdi.Total is not null ? Convert.ToDecimal(cfdi.Total) : 0;
                                decimal tipoCambio = cfdi.TipoCambio is not null ? Convert.ToDecimal(cfdi.TipoCambio) : 0;

                                if (cfdi.Moneda != "MXN")
                                {
                                    total = total * tipoCambio;
                                }

                                var responseFactura = await _facturaData.AddFactura(new TB_ProyectoFactura
                                {
                                    NumProyecto = request.NumProyecto,
                                    Anio = Convert.ToInt16(tryDate.Year),
                                    Concepto = string.Join("|", cfdi.Conceptos),
                                    FechaEmision = tryDate,
                                    IdMoneda = cfdi.Moneda,
                                    Importe = Convert.ToDecimal(cfdi.SubTotal ?? "-1"),
                                    MotivoCancelacion = null,
                                    FechaPago = null,
                                    FechaCancelacion = null,
                                    NoFactura = $"{cfdi.Serie ?? string.Empty}{cfdi.Folio ?? string.Empty}",
                                    Iva = cfdi.TotalImpuestosTrasladados is not null ? Convert.ToDecimal(cfdi.TotalImpuestosTrasladados) : 0,
                                    IvaRet = cfdi.TotalImpuestosRetenidos is not null ? Convert.ToDecimal(cfdi.TotalImpuestosRetenidos) : 0,
                                    Total = total,
                                    Mes = Convert.ToByte(tryDate.Month),
                                    Uuid = cfdi.UUID,
                                    TipoCambio = tipoCambio,
                                    XmlB64 = cfdi.XmlB64,
                                    IdTipoFactura = cfdi?.Conceptos?.FirstOrDefault()?.ToUpper()?.Contains("COBROS POR PAGOS A CTA DE TERCEROS") == true ? "TRADES" : "PROPIOS"
                                });
                                tmpFactura.Almacenada = responseFactura.existe;
                                tmpFactura.Error = responseFactura.mensaje;
                            }
                        }
                    }
                    else
                    {
                        tmpFactura.Almacenada = false;
                        tmpFactura.Error = $@"La factura {factura.FacturaNombre} no coincide con los RFCs esperados";
                    }

                    LstFacturas.Add(tmpFactura);
                }
                else
                {
                    LstFacturas.Add(new FacturaRevision
                    {
                        FacturaNombre = factura.FacturaNombre,
                        Almacenada = false,
                        Error = $@"La factura {factura.FacturaNombre} no se pudo procesar"
                    });
                }
            }

            return LstFacturas;
        }

        public async Task<(bool Success, string Message)> CancelFactura(InsertMovApi MovAPI, CancelarFactura factura)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _facturaData.CancelFactura(new TB_ProyectoFactura { Id = factura.Id, FechaCancelacion = factura.FechaCancelacion, MotivoCancelacion = factura.MotivoCancelacion });
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo cancelar la factura"; return resp; }
            else await _transactionData.AddMovApi(new Mov_Api { Nombre = MovAPI.Nombre, Roles = MovAPI.Roles, Usuario = MovAPI.Usuario, FechaAlta = DateTime.Now, IdRel = MovAPI.Rel, ValorNuevo = JsonConvert.SerializeObject(factura) });
            return resp;
        }

        public async Task<List<FacturaDetalles>> Search(int? idProyecto, int? idCliente, int? idEmpresa, DateTime? fechaIni, DateTime? fechaFin, string? noFactura)
        {
            var resp = new List<FacturaDetalles>();
            resp = await _facturaData.GetAllFacturas(idProyecto, idCliente, idEmpresa, fechaIni, fechaFin, noFactura);
            return resp;
        }

        #endregion Facturas


        #region Notas
        public async Task<List<FacturaRevision>> AddNotasCredito(AgregarNotaCredito request)
        {
            var LstFacturas = new List<FacturaRevision>();

            var tryDate = default(DateTime);
            foreach (var notaCredito in request.LstFacturas)
            {
                var cfdi = await ExtraerDatos(notaCredito.FacturaB64);
                
                if (cfdi is not null && cfdi.IsVersionValida && cfdi.TipoDeComprobante.Equals("E"))
                {
                    if (cfdi.CfdiRelacionados.Count == 0)
                    {
                        LstFacturas.Add(new FacturaRevision
                        {
                            FacturaNombre = notaCredito.FacturaNombre,
                            Almacenada = false,
                            Error = $@"No existe un CFDI relacionado para esta nota de crédito."
                        });
                    }
                    else
                    {
                        foreach (var uuid in cfdi.CfdiRelacionados)
                        {
                            var factura = await _facturaData.SearchFactura(uuid);

                            var tmpFactura = new FacturaRevision
                            {
                                RfcEmisor = cfdi.RfcEmisor,
                                RfcReceptor = cfdi.RfcReceptor,
                                FechaEmision = cfdi.Fecha,
                                Total = cfdi.Total,
                                Conceptos = string.Join("|", cfdi.Conceptos),
                                //TipoFactura = cfdi?.Conceptos?.FirstOrDefault()?.ToUpper()?.Contains("COBROS POR PAGOS A CTA DE TERCEROS") == true ? "TRADES" : "PROPIOS",
                                NoFactura = $"{cfdi.Serie ?? string.Empty}{cfdi.Folio ?? string.Empty}",
                                FacturaNombre = notaCredito.FacturaNombre,
                                Almacenada = true
                            };

                            tryDate = default;
                            if (factura != null)
                            {
                                var existeNC = await _facturaData.SearchNotaCredito(cfdi.UUID);

                                if (existeNC != null)
                                {
                                    tmpFactura.Almacenada = false;
                                    tmpFactura.Error = $@"La nota de credito {uuid} ya existe en la BD";
                                }
                                else
                                {
                                    if ((DateTime.TryParse(cfdi.Fecha, out tryDate)) && (factura.Id > 0))
                                    {
                                        decimal total = cfdi.Total is not null ? Convert.ToDecimal(cfdi.Total) : 0;
                                        decimal tipoCambio = cfdi.TipoCambio is not null ? Convert.ToDecimal(cfdi.TipoCambio) : 0;

                                        if (cfdi.Moneda != "MXN")
                                        {
                                            total = total * tipoCambio;
                                        }

                                        var responseFactura = await _facturaData.AddNotaCredito(new TB_ProyectoFacturaNotaCredito
                                        {
                                            IdFactura = factura.Id,
                                            UuidNotaCredito = cfdi.UUID,
                                            Anio = Convert.ToInt16(tryDate.Year),
                                            Concepto = string.Join("|", cfdi.Conceptos),
                                            FechaNotaCredito = tryDate,
                                            IdMoneda = cfdi.Moneda,
                                            Importe = Convert.ToDecimal(cfdi.SubTotal ?? "-1"),
                                            NotaCredito = $"{cfdi.Serie ?? string.Empty}{cfdi.Folio ?? string.Empty}",
                                            Iva = cfdi.TotalImpuestosTrasladados is not null ? Convert.ToDecimal(cfdi.TotalImpuestosTrasladados) : 0,
                                            Total = total,
                                            Mes = Convert.ToByte(tryDate.Month),
                                            TipoCambio = tipoCambio,
                                            Xml = cfdi.XmlB64
                                        });

                                        tmpFactura.Almacenada = responseFactura.Success;
                                        tmpFactura.Error = responseFactura.Message;
                                    }
                                }
                            }
                            else
                            {
                                tmpFactura.Almacenada = false;
                                tmpFactura.Error = $@"La factura {uuid} no ha sido cargada";
                            }
                            LstFacturas.Add(tmpFactura);
                        }
                    }
                }
                else
                {
                    var tmpError = string.Empty;

                    if (cfdi is not null && !cfdi.TipoDeComprobante.Equals("E"))
                        tmpError = $@"El tipo de comprobante es {cfdi.TipoDeComprobante}";

                    LstFacturas.Add(new FacturaRevision
                    {
                        FacturaNombre = notaCredito.FacturaNombre,
                        Almacenada = false,
                        Error = $@"La factura {notaCredito.FacturaNombre} no se pudo procesar - {tmpError}"
                    });
                }
            }

            return LstFacturas;
        }

        public async Task<(bool Success, string Message)> AddNotaCreditoSinFactura(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            var cfdi = await ExtraerDatos(registro["FacturaB64"].ToString());
            var existeNC = await _facturaData.SearchNotaCredito(cfdi.UUID);

            if(cfdi.TipoDeComprobante != "E")
            {
                resp.Success = true;
                resp.Message = $@"El archivo seleccionado con el UUID {cfdi.UUID}, no es una nota de crédito.";
                return resp;
            }
            if(cfdi.CfdiRelacionados != null && cfdi.CfdiRelacionados?.Count > 0)
            {
                resp.Success = true;
                resp.Message = $@"Se han encontrado documentos relacionados a esta nota de crédito.";
                return resp;
            }
            if (existeNC != null)
            {
                resp.Success = true;
                resp.Message = $@"La nota de credito {cfdi.UUID} ya existe en la BD";
                return resp;
            }
            else
            {
                var respData = await _facturaData.AddNotaCreditoSinFactura(registro);
                if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro a la base de datos"; return resp; }
                else resp = respData;
            }

            return resp;
        }

        public Task<List<NotaCredito_Detalle>> GetNotaCreditoSinFactura(int NumProyecto, int Mes, int Anio) => _facturaData.GetNotaCreditoSinFactura(NumProyecto, Mes, Anio);

        public Task<(bool Success, string Message)> AddNotaCreditoSinFacturaToFactura(JsonObject registro) => _facturaData.AddNotaCreditoSinFacturaToFactura(registro);

        public async Task<(bool Success, string Message)> CancelNota(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _facturaData.CancelNota(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo cancelar el registro de la nota en la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        #endregion Notas


        #region Pagos
        public async Task<List<FacturaRevision>> AddPagos(AgregarPagos request)
        {
            var LstFacturas = new List<FacturaRevision>();

            var tryDate = default(DateTime);
            foreach (var pagos in request.LstFacturas)
            {
                var cfdi = await ExtraerDatos(pagos.FacturaB64);

                
                if (cfdi is not null && cfdi.IsVersionValida && cfdi.TipoDeComprobante.Equals("P"))
                {
                    var existePago = await _facturaData.SearchPagos(cfdi.UUID);

                    if (existePago != null)
                    {
                        //tmpFactura.Almacenada = false;
                        //tmpFactura.Error = $@"El pago {cfdi.UUID} ya existe en la BD.";
                    }
                    else
                    {
                        foreach (CfdiPagos tmpPagos in cfdi.Pagos)
                        {
                            decimal total = cfdi.Total is not null ? Convert.ToDecimal(cfdi.Total) : 0;
                            decimal tipoCambio = tmpPagos.TipoCambioP is not null ? Convert.ToDecimal(tmpPagos.TipoCambioP) : 0;

                            if (cfdi.Moneda != "MXN")
                            {
                                total = total * tipoCambio;
                            }

                            foreach (var docto in tmpPagos.DoctosRelacionados)
                            {
                                decimal importe_pagado = Convert.ToDecimal(docto.ImportePagado ?? "-1");
                                decimal importe_saldo_anterior = Convert.ToDecimal(docto.ImporteSaldoAnt ?? "-1");
                                decimal importe_saldo_insoluto = Convert.ToDecimal(docto.ImporteSaldoInsoluto ?? "-1");

                                if (cfdi.Moneda != "MXN")
                                {
                                    importe_pagado = importe_pagado * tipoCambio;
                                    importe_saldo_anterior = importe_saldo_anterior * tipoCambio;
                                    importe_saldo_insoluto = importe_saldo_insoluto * tipoCambio;
                                }

                                var factura = await _facturaData.SearchFactura(docto.Uuid);
                                var tmpFactura = new FacturaRevision
                                {
                                    RfcEmisor = cfdi.RfcEmisor,
                                    RfcReceptor = cfdi.RfcReceptor,
                                    FechaEmision = cfdi.Fecha,
                                    Total = total.ToString(),
                                    Conceptos = string.Join("|", cfdi.Conceptos),
                                    //TipoFactura = cfdi?.Conceptos?.FirstOrDefault()?.ToUpper()?.Contains("COBROS POR PAGOS A CTA DE TERCEROS") == true ? "TRADES" : "PROPIOS",
                                    NoFactura = $"{cfdi.Serie ?? string.Empty}{cfdi.Folio ?? string.Empty}",
                                    FacturaNombre = pagos.FacturaNombre,
                                    Almacenada = true
                                };

                                tryDate = default;
                                if (factura != null)
                                {
                                    if ((DateTime.TryParse(tmpPagos.FechaPago, out tryDate)) && (factura.Id > 0))
                                    {
                                        var responseFactura = await _facturaData.AddPagos(new TB_ProyectoFacturaCobranza
                                        {
                                            IdFactura = factura.Id,
                                            UuidCobranza = cfdi.UUID,
                                            IdMonedaP = docto.MonedaDR,
                                            ImportePagado = importe_pagado,
                                            ImpSaldoAnt = importe_saldo_anterior,
                                            ImporteSaldoInsoluto = importe_saldo_insoluto,
                                            IvaP = Convert.ToDecimal(docto.ImporteDR ?? "-1"),
                                            TipoCambioP = tipoCambio,
                                            FechaPago = tryDate,
                                            Xml = cfdi.XmlB64,
                                            CRP = cfdi.Serie + cfdi.Folio,
                                            //Base = tmpPagos.TrasladoP != null && !string.IsNullOrEmpty(tmpPagos.TrasladoP.BaseP) ? Convert.ToDecimal(tmpPagos.TrasladoP.BaseP) : 0
                                            Base = Convert.ToDecimal(docto.BaseDR  ?? "-1")
                                        });
                                        tmpFactura.Almacenada = responseFactura.Success;
                                        tmpFactura.Error = responseFactura.Message;
                                    }

                                }
                                else
                                {
                                    tmpFactura.Almacenada = false;
                                    tmpFactura.Error = $@"La factura {docto.Uuid} no ha sido cargada";
                                }

                                LstFacturas.Add(tmpFactura);
                            }
                        }
                    }                    
                }
                else
                {
                    LstFacturas.Add(new FacturaRevision
                    {
                        FacturaNombre = pagos.FacturaNombre,
                        Almacenada = false,
                        Error = $@"La factura {pagos.FacturaNombre} no se pudo procesar"
                    });
                }
            }
            return LstFacturas;
        }
        public async Task<(bool Success, string Message)> CancelCobranza(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _facturaData.CancelCobranza(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo cancelar el registro de la cobranza en la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        #endregion Pagos
                


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
                        string strXPathImpuestoDR = string.Empty;
                        string strXPathImpuestoP = string.Empty;

                        if (datosCFDI.Version.Equals("3.3"))
                            nodePagos = doc.SelectNodes("//cfdi:Comprobante//cfdi:Complemento//pago10:Pagos//pago10:Pago", nsm);
                        else
                        {
                            nodePagos = doc.SelectNodes("//cfdi:Comprobante//cfdi:Complemento//pago20:Pagos//pago20:Pago", nsm);
                            strXPathImpuestoDR = "pago20:ImpuestosDR//pago20:TrasladosDR//pago20:TrasladoDR";
                            strXPathImpuestoP = "pago20:TrasladosP//pago20:TrasladoP";
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
                                    if (!string.IsNullOrEmpty(strXPathImpuestoDR))
                                    {
                                        tDoctoRel.ImporteDR = childNode.SelectSingleNode(strXPathImpuestoDR, nsm).Attributes["ImporteDR"] != null ? childNode.SelectSingleNode(strXPathImpuestoDR, nsm).Attributes["ImporteDR"].Value : null;
                                        tDoctoRel.BaseDR = childNode.SelectSingleNode(strXPathImpuestoDR, nsm).Attributes["BaseDR"] != null ? childNode.SelectSingleNode(strXPathImpuestoDR, nsm).Attributes["BaseDR"].Value : null;
                                    }

                                    tPagos.DoctosRelacionados.Add(tDoctoRel);
                                }
                                else
                                {
                                    tPagos.TrasladoP = new CfdiTrasladoP();
                                    if (!string.IsNullOrEmpty(strXPathImpuestoP))
                                        tPagos.TrasladoP.BaseP = childNode.SelectSingleNode(strXPathImpuestoP, nsm).Attributes["BaseP"] != null ? childNode.SelectSingleNode(strXPathImpuestoP, nsm).Attributes["BaseP"].Value : null;                                    
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
