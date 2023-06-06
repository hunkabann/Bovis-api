using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface IEmpleadoBusiness : IDisposable
    {
        #region Empleados
        Task<List<TB_Empleado>> GetEmpleados(bool? activo);
        Task<(bool Success, string Message)> AddRegistro(TB_Empleado registro);
        #endregion Empleados
    }

}
