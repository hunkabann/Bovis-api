using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System.Text.Json.Nodes;

namespace Bovis.Data.Interface
{
    public interface ICieData : IDisposable
    {
        #region Empresas
        Task<List<TB_Empresa>> GetEmpresas(bool? activo);
        #endregion Empresas

        #region Cuenta Data
        Task<List<CuentaContable_Detalle>> GetCuentaData(JsonObject cuentas);
        #endregion Cuenta Data

        #region Proyecto
        Task<List<ProyectoData_Detalle>> GetProyectoData(JsonObject proyectos);
        #endregion Proyecto

        #region Registros
        Task<TB_Cie_Data> GetRegistro(int? idRegistro);
        Task<List<TB_Cie_Data>> GetRegistros(bool? activo, int offset, int limit);
        Task<(bool existe, string mensaje)> AddRegistros(JsonObject registros);
        Task<(bool existe, string mensaje)> UpdateRegistro(JsonObject registro);
        Task<(bool existe, string mensaje)> DeleteRegistro(int idRegistro);

        #endregion Registros
    }
}