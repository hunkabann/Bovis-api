using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;

namespace Bovis.Business
{
    public class EmpleadoBusiness : IEmpleadoBusiness
    {
        #region base
        private readonly IEmpleadoData _empleadoData;
        public EmpleadoBusiness(IEmpleadoData _empleadoData)
        {
            this._empleadoData = _empleadoData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

    }
}
