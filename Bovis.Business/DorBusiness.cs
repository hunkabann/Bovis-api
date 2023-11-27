using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;

namespace Bovis.Business
{
    public class DorBusiness : IDorBusiness
    {
        #region base
        private readonly IDorData _dorData;
        private readonly ITransactionData _transactionData;
        public DorBusiness(IDorData _dorData, ITransactionData transactionData)
        {
            this._dorData = _dorData;
            _transactionData = transactionData; 
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

        public Task<TB_DorEmpleados?> GetDorEjecutivoCorreo(string email) => _dorData.GetDorEjecutivoCorreo(email);
        public Task<Dor_Subordinados?> GetDorEmpleadoCorreo(string email) => _dorData.GetDorEmpleadoCorreo(email);
        public Task<List<Dor_Subordinados>> GetDorListaSubordinados(string name) => _dorData.GetDorListaSubordinados(name);
        public Task<List<Dor_ObjetivosGenerales>> GetDorObjetivosGenerales(int nivel, string unidadNegocio, int mes, int anio, string seccion) => _dorData.GetDorObjetivosGenerales(nivel, unidadNegocio, mes, anio, seccion);
        //public Task<List<DOR_ObjetivosDesepeno>> GetDorObjetivosDesepeno(int anio, int proyecto, string concepto, int? empleado) => _dorData.GetDorObjetivosDesepeno(anio, proyecto, concepto, empleado);
        public Task<List<Dor_ObjetivosEmpleado>> GetDorObjetivosDesepeno(int anio, int proyecto, int empleado, int nivel, int? acepto, int mes) => _dorData.GetDorObjetivosDesepeno(anio, proyecto, empleado,nivel, acepto, mes);
        public Task<(bool Success, string Message)> AddDorObjetivo(TB_DorObjetivosDesepeno objetivo) => _dorData.AddObjetivo(objetivo);
        public Task<(bool Success, string Message)> UpdDorObjetivo(TB_DorObjetivosDesepeno objetivo) => _dorData.UpdObjetivo(objetivo);

        public Task<List<Dor_ObjetivosGenerales>> GetDorGpmProyecto(int proyecto) => _dorData.GetDorGpmProyecto(proyecto);
        public Task<List<Dor_ObjetivosGenerales>> GetDorMetasProyecto(int proyecto, int nivel, int mes, int anio, int empleado, string seccion) => _dorData.GetDorMetasProyecto(proyecto, nivel, mes, anio, empleado, seccion);

        public async Task<(bool Success, string Message)> UpdateReal(JsonObject registro)
        {
            //(bool Success, string Message) resp = (true, string.Empty);
            //var respData = await _dorData.UpdateReal((JsonObject)registro["Registro"]);
            //if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro de PEC"; return resp; }
            //else
            //{
            //    resp = respData;
            //    _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            //}
            //return resp;

            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _dorData.UpdateReal(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro de PEC"; return resp; }
            else resp = respData;
            return resp;
        }
        public async Task<(bool Success, string Message)> UpdateObjetivoPersonal(JsonObject registro)
        {
            //(bool Success, string Message) resp = (true, string.Empty);
            //var respData = await _dorData.UpdateObjetivoPersonal((JsonObject)registro["Registro"]);
            //if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro de PEC"; return resp; }
            //else
            //{
            //    resp = respData;
            //    _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            //}
            //return resp;

            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _dorData.UpdateObjetivoPersonal(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro de PEC"; return resp; }
            else resp = respData;
            return resp;
        }
        public async Task<(bool Success, string Message)> UpdateAcepto(JsonObject registro)
        {
            //(bool Success, string Message) resp = (true, string.Empty);
            //var respData = await _dorData.UpdateAcepto((JsonObject)registro["Registro"]);
            //if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro de PEC"; return resp; }
            //else
            //{
            //    resp = respData;
            //    _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            //}
            //return resp;

            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _dorData.UpdateAcepto(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro de PEC"; return resp; }
            else resp = respData;
            return resp;
        }
    }
}
