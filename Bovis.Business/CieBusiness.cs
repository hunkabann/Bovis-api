using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using Microsoft.Win32;
using System.Text.Json.Nodes;

namespace Bovis.Business
{
    public class CieBusiness : ICieBusiness
    {
        #region base
        private readonly ICieData _cieData;
        private readonly ITransactionData _transactionData;
        public CieBusiness(ICieData _cieData, ITransactionData _transactionData)
        {
            this._cieData = _cieData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Empresas
        public Task<List<TB_Empresa>> GetEmpresas(bool? Activo) => _cieData.GetEmpresas(Activo);
        #endregion Empresas

        #region Cuenta Data
        public Task<List<CuentaContable_Detalle>> GetCuentaData(JsonObject cuentas) => _cieData.GetCuentaData(cuentas);
        public async Task<(bool Success, string Message)> AddCuentas(JsonObject registros)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _cieData.AddCuentas(registros);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudieron agregar los registros Cie a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        #endregion Cuenta Data

        #region Proyecto
        public Task<List<ProyectoData_Detalle>> GetProyectoData(JsonObject proyectos) => _cieData.GetProyectoData(proyectos);
        #endregion Proyecto

        #region Catálogos
        public Task<List<string>> GetNombresCuenta() => _cieData.GetNombresCuenta();
        public Task<List<string>> GetConceptos() => _cieData.GetConceptos();
        public Task<List<int>> GetNumsProyecto() => _cieData.GetNumsProyecto();
        public Task<List<string>> GetResponsables() => _cieData.GetResponsables();
        public Task<List<string>> GetClasificacionesPY() => _cieData.GetClasificacionesPY();
        #endregion Catálogos

        #region Registros
        public Task<Cie_Detalle> GetRegistro(int? idRegistro) => _cieData.GetRegistro(idRegistro);
        public Task<Cie_Registros> GetRegistros(JsonObject registro) => _cieData.GetRegistros(registro);
        public async Task<(bool Success, string Message)> AddRegistros(JsonObject registros)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _cieData.AddRegistros(registros);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudieron agregar los registros Cie a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _cieData.UpdateRegistro((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
        public async Task<(bool Success, string Message)> DeleteRegistro(int idRegistro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _cieData.DeleteRegistro(idRegistro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }
        #endregion Registros
    }
}
