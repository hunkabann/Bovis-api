﻿using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class TokenQueryService : ITokenQueryService
    {
        #region base
        private readonly ITokenBusiness _tokenBusiness;
        private readonly IMapper _map;

        public TokenQueryService(IMapper _map, ITokenBusiness _tokenBusiness)
        {
            this._map = _map;
            this._tokenBusiness = _tokenBusiness;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<Response<(bool Success, string Message)>> AddToken(string email, string str_token)
        {
            var response = await _tokenBusiness.AddToken(email, str_token);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
    }
}
