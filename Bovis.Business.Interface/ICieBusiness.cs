using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface ICieBusiness : IDisposable
    {
        #region Empresas
        Task<List<TB_Empresa>> GetEmpresas(bool? activo);
        #endregion Empresas

        #region Registros
        Task<CieRegistro> GetInfoRegistro(int? numProyecto);
        Task<List<TB_Cie>> GetRegistros(byte? estatus);
        Task<(bool Success, string Message)> AddRegistro(TB_Cie registro);
        Task<(bool Success, string Message)> AddRegistros(List<TB_Cie> registros);
        #endregion Registros
    }

}
