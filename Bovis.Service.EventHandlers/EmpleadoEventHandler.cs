using Bovis.Common.Model.NoTable;
using Bovis.Common;
using Bovis.Service.Queries.Dto.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;


namespace Bovis.Service.EventHandlers;
public class AddEmpleadoEventHandler : IRequestHandler<AddEmpleadoCommand, Response<bool>>
{
    private readonly IEmpleadoBusiness _business;
    private readonly IMapper _mapper;

    public AddEmpleadoEventHandler(IEmpleadoBusiness _business, IMapper _mapper)
    {
        this._business = _business;
        this._mapper = _mapper;
    }

    public async Task<Response<bool>> Handle(AddEmpleadoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}