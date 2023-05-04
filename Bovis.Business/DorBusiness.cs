using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;

namespace Bovis.Business
{
    public class DorBusiness : IDorBusiness
    {
        #region base
        private readonly IDorData _dorData;
        public DorBusiness(IDorData _dorData)
        {
            this._dorData = _dorData;
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
        public Task<List<Dor_ObjetivosGenerales>> GetDorObjetivosGenerales(string nivel, string unidadNegocio) => _dorData.GetDorObjetivosGenerales(nivel, unidadNegocio);
        //public Task<List<DOR_ObjetivosDesepeno>> GetDorObjetivosDesepeno(int anio, int proyecto, string concepto, int? empleado) => _dorData.GetDorObjetivosDesepeno(anio, proyecto, concepto, empleado);
        public Task<List<Dor_ObjetivosEmpleado>> GetDorObjetivosDesepeno(int anio, int proyecto, int empleado, int nivel, int? acepto) => _dorData.GetDorObjetivosDesepeno(anio, proyecto, empleado,nivel, acepto);
        public Task<(bool Success, string Message)> AddDorObjetivo(DOR_ObjetivosDesepeno objetivo) => _dorData.AddObjetivo(objetivo);
        public Task<(bool Success, string Message)> UpdDorObjetivo(DOR_ObjetivosDesepeno objetivo) => _dorData.UpdObjetivo(objetivo);

        public Task<List<Dor_ObjetivosGenerales>> GetDorGpmProyecto(int proyecto) => _dorData.GetDorGpmProyecto(proyecto);
        public Task<List<Dor_ObjetivosGenerales>> GetDorMetasProyecto(int proyecto, int nivel) => _dorData.GetDorMetasProyecto(proyecto, nivel);
    }
}
