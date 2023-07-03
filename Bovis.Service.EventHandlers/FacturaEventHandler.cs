using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common;
using Bovis.Service.Queries.Dto.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bovis.Common.Model.NoTable;

namespace Bovis.Service.EventHandlers
{
    public class AddFacturaEventHandler : IRequestHandler<AddFacturasCommand, Response<List<FacturaRevision>>>
    {
        private readonly IFacturaBusiness _business;
        private readonly IMapper _mapper;

        public AddFacturaEventHandler(IFacturaBusiness _business, IMapper _mapper)
        {
            this._business = _business;
            this._mapper = _mapper;
        }

        public async Task<Response<List<FacturaRevision>>> Handle(AddFacturasCommand request, CancellationToken cancellationToken)
        {
            var resp = new Response<List<FacturaRevision>>();
            var facturas = new AgregarFactura { NumProyecto = request.NumProyecto, RfcEmisor = request.RfcEmisor, RfcReceptor=request.RfcReceptor };
            request.LstFacturas?.ForEach(x=>  { facturas.LstFacturas.Add(new Common.Model.NoTable.Factura { FacturaB64 = x.FacturaB64, FacturaNombre = x.NombreFactura }); });
            //			(bool Success, string Message) tmpResp = await _business.AddFacturas(facturas);
            List<FacturaRevision> lstFacturas = await _business.AddFacturas(facturas);
            resp.Data = lstFacturas;
            //if (!tmpResp.Success) resp.AddError(tmpResp.Message);
                //resp.Data = lista
            //else 
            return resp;
        }
    }

    public class AddNotaCreditoEventHandler : IRequestHandler<AddNotaCreditoCommand, Response<List<FacturaRevision>>>
    {
        private readonly IFacturaBusiness _business;
        private readonly IMapper _mapper;

        public AddNotaCreditoEventHandler(IFacturaBusiness _business, IMapper _mapper)
        {
            this._business = _business;
            this._mapper = _mapper;
        }

        public async Task<Response<List<FacturaRevision>>> Handle(AddNotaCreditoCommand request, CancellationToken cancellationToken)
        {
            var resp = new Response<List<FacturaRevision>>();
            var facturas = new AgregarNotaCredito();
            request.LstFacturas?.ForEach(x => { facturas.LstFacturas.Add(new Common.Model.NoTable.Factura { FacturaB64 = x.FacturaB64, FacturaNombre = x.NombreFactura }); });
            List<FacturaRevision> lstFacturas = await _business.AddNotasCredito(facturas);
            resp.Data = lstFacturas;

            return resp;
        }
    }

    public class AddPagosEventHandler : IRequestHandler<AddPagosCommand, Response<List<FacturaRevision>>>
    {
        private readonly IFacturaBusiness _business;
        private readonly IMapper _mapper;

        public AddPagosEventHandler(IFacturaBusiness _business, IMapper _mapper)
        {
            this._business = _business;
            this._mapper = _mapper;
        }

        public async Task<Response<List<FacturaRevision>>> Handle(AddPagosCommand request, CancellationToken cancellationToken)
        {
            var resp = new Response<List<FacturaRevision>>();
            var facturas = new AgregarPagos();
            request.LstFacturas?.ForEach(x => { facturas.LstFacturas.Add(new Common.Model.NoTable.Factura { FacturaB64 = x.FacturaB64, FacturaNombre = x.NombreFactura }); });
            List<FacturaRevision> lstFacturas = await _business.AddPagos(facturas);
            resp.Data = lstFacturas;

            return resp;
        }
    }

    public class CancelFacturaEventHandler : IRequestHandler<CancelFacturaCommand, Response<bool>>
    {
        private readonly IFacturaBusiness _business;
        private readonly IMapper _mapper;

        public CancelFacturaEventHandler(IFacturaBusiness _business, IMapper _mapper)
        {
            this._business = _business;
            this._mapper = _mapper;
        }

        public async Task<Response<bool>> Handle(CancelFacturaCommand request, CancellationToken cancellationToken)
        {
            var resp = new Response<bool>();
            (bool Success, string Message) tmpResp = await _business.CancelFactura(new InsertMovApi { Rel = request.Rel, Nombre = request.Nombre, Roles = request.Roles, TransactionId = request.TransactionId, Usuario = request.Usuario }, new CancelarFactura { FechaCancelacion = request.FechaCancelacion, Id = request.Id, MotivoCancelacion = request.MotivoCancelacion });
            if (!tmpResp.Success) resp.AddError(tmpResp.Message);
            else resp.Data = tmpResp.Success;
            return resp;
        }
    }
}