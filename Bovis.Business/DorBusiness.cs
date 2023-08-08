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

        public Task<DOR_Empleados?> GetDorEjecutivoCorreo(string email) => _dorData.GetDorEjecutivoCorreo(email);
        public Task<Dor_Subordinados?> GetDorEmpleadoCorreo(string email) => _dorData.GetDorEmpleadoCorreo(email);
        public Task<List<Dor_Subordinados>> GetDorListaSubordinados(string name) => _dorData.GetDorListaSubordinados(name);
        public Task<List<Dor_ObjetivosGenerales>> GetDorObjetivosGenerales(int nivel, string unidadNegocio, int mes, string seccion) => _dorData.GetDorObjetivosGenerales(nivel, unidadNegocio, mes, seccion);
        //public Task<List<DOR_ObjetivosDesepeno>> GetDorObjetivosDesepeno(int anio, int proyecto, string concepto, int? empleado) => _dorData.GetDorObjetivosDesepeno(anio, proyecto, concepto, empleado);
        public Task<List<Dor_ObjetivosEmpleado>> GetDorObjetivosDesepeno(int anio, int proyecto, int empleado, int nivel, int? acepto, int mes) => _dorData.GetDorObjetivosDesepeno(anio, proyecto, empleado,nivel, acepto, mes);
        public Task<(bool Success, string Message)> AddDorObjetivo(DOR_ObjetivosDesepeno objetivo) => _dorData.AddObjetivo(objetivo);
        public Task<(bool Success, string Message)> UpdDorObjetivo(DOR_ObjetivosDesepeno objetivo) => _dorData.UpdObjetivo(objetivo);

        public Task<List<Dor_ObjetivosGenerales>> GetDorGpmProyecto(int proyecto) => _dorData.GetDorGpmProyecto(proyecto);
        public Task<List<Dor_ObjetivosGenerales>> GetDorMetasProyecto(int proyecto, int nivel, int mes, int empleado, string seccion) => _dorData.GetDorMetasProyecto(proyecto, nivel, mes, empleado, seccion);

        public async Task<(bool Success, string Message)> UpdateReal(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _dorData.UpdateReal((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro de PEC"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }
    }
}
