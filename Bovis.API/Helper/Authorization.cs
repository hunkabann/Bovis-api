using Bovis.Data.Interface;
using Bovis.Service.Queries;
using Bovis.Service.Queries.Interface;

namespace Bovis.API.Helper
{
    public class Authorization
    {
        #region base
        private readonly IRolQueryService _rolQueryService;

        public Authorization(IRolQueryService _rolQueryService)
        {
            this._rolQueryService = _rolQueryService;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public Task<string> GetAuthorization(string token)
        {
            var response = _rolQueryService.GetAuthorization(token);
            return response;
        }        
    }
}
