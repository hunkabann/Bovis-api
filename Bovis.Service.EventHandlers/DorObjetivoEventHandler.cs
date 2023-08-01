using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Commands;
using MediatR;

namespace Bovis.Service.EventHandlers
{
    public class DorObjetivoEventHandler : IRequestHandler<UpdDorObjetivoCommand, Response<bool>>
    {
        private readonly IDorBusiness _business;
        private readonly IMapper _mapper;

        public DorObjetivoEventHandler(IDorBusiness _business, IMapper _mapper)
        {
            this._business = _business;
            this._mapper = _mapper;
        }

        public async Task<Response<bool>> Handle(UpdDorObjetivoCommand objetivo, CancellationToken cancellationToken)
        {
            DateTime fechaLocal = DateTime.Now;

            if (!string.IsNullOrEmpty(objetivo.Acepto.ToString()))
                switch (objetivo.Acepto)
                {
                    case 1:
                        objetivo.FechaCarga = fechaLocal;
                        break;
                    case 2:
                        objetivo.FechaAceptado = fechaLocal;
                        break;
                    case 3:
                        objetivo.FechaRechazo = fechaLocal;
                        break;
                }

            var resp = new Response<bool>();
            (bool Success, string Message) tmpResp = await _business.UpdDorObjetivo(new DOR_ObjetivosDesepeno
            {
                IdEmpOb = objetivo.IdEmpOb,
                UnidadDeNegocio = objetivo.UnidadDeNegocio,
                Concepto = objetivo.Concepto,
                Descripcion = objetivo.Descripcion,
                Meta = objetivo.Meta,
                Real = objetivo.Real,
                Ponderado = objetivo.Ponderado,
                Calificacion = objetivo.Calificacion,
                Nivel = objetivo.Nivel,               
                Anio = objetivo.Anio,
                Proyecto = objetivo.Proyecto,
                Empleado = objetivo.Empleado,
                Acepto = objetivo.Acepto,
                MotivoR = objetivo.MotivoR,
                FechaCarga = objetivo.FechaCarga,
                FechaAceptado = objetivo.FechaAceptado,
                FechaRechazo = objetivo.FechaRechazo
            });
            if (!tmpResp.Success) resp.AddError(tmpResp.Message);
            else resp.Data = tmpResp.Success;
            return resp;
        }
    }
}
