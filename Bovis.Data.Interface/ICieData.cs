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
        Task<List<CtaContableRespuesta_Detalle>> AddCuentas(JsonObject registros);
        #endregion Cuenta Data

        #region Proyecto
        Task<List<ProyectoData_Detalle>> GetProyectoData(JsonObject proyectos);
        #endregion Proyecto

        #region Catálogos
        Task<List<string>> GetNombresCuenta();
        Task<List<string>> GetConceptos();
        Task<List<int>> GetNumsProyecto();
        Task<List<string>> GetResponsables();
        Task<List<string>> GetClasificacionesPY();
        #endregion Catálogos

        #region Registros
        Task<Cie_Detalle> GetRegistro(int? idRegistro);
        Task<Cie_Registros> GetRegistros(JsonObject registro);
        Task<(bool Success, string Message)> AddRegistros(JsonObject registros);
        Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro);
        Task<(bool Success, string Message)> DeleteRegistro(int idRegistro);

        #endregion Registros
    }
}