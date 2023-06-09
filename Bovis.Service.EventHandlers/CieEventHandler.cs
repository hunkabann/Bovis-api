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
public class AddCieEventHandler : IRequestHandler<AddCieCommand, Response<bool>>
{
    private readonly ICieBusiness _business;
    private readonly IMapper _mapper;

    public AddCieEventHandler(ICieBusiness _business, IMapper _mapper)
    {
        this._business = _business;
        this._mapper = _mapper;
    }

    public Task<Response<bool>> Handle(AddCieCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

