using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using Microsoft.Win32;
using Bovis.Service.Queries.Dto.Responses;
using System.Text.Json.Nodes;

namespace Bovis.Business
{
    public class RequerimientoBusiness : IRequerimientoBusiness
    {
        #region base
        private readonly IRequerimientoData _RequerimientoData;
        private readonly ITransactionData _transactionData;
        public RequerimientoBusiness(IRequerimientoData _RequerimientoData, ITransactionData _transactionData)
        {
            this._RequerimientoData = _RequerimientoData;
            this._transactionData = _transactionData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Habilidades
        public Task<List<TB_RequerimientoHabilidad>> GetHabilidades(int idRequerimiento) => _RequerimientoData.GetHabilidades(idRequerimiento);
        #endregion Habilidades

        #region Experiencias
        public Task<List<TB_RequerimientoExperiencia>> GetExperiencias(int idRequerimiento) => _RequerimientoData.GetExperiencias(idRequerimiento);
        #endregion Experiencias

        #region Registros
        public Task<List<Requerimiento_Detalle>> GetRequerimientos(bool? Asignados, int? idDirector, int? idProyecto, int? idPuesto) => _RequerimientoData.GetRequerimientos(Asignados, idDirector, idProyecto, idPuesto);

        public Task<Requerimiento_Detalle> GetRequerimiento(int idRequerimiento) => _RequerimientoData.GetRequerimiento(idRequerimiento);

        public async Task<(bool Success, string Message)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _RequerimientoData.AddRegistro(registro);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo agregar el registro del Requerimiento a la base de datos"; return resp; }
            else resp = respData;
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            var respData = await _RequerimientoData.UpdateRegistro((JsonObject)registro["Registro"]);
            if (!respData.Success) { resp.Success = false; resp.Message = "No se pudo actualizar el registro en la base de datos"; return resp; }
            else
            {
                resp = respData;
                _transactionData.AddMovApi(new Mov_Api { Nombre = registro["Nombre"].ToString(), Roles = registro["Roles"].ToString(), Usuario = registro["Usuario"].ToString(), FechaAlta = DateTime.Now, IdRel = Convert.ToInt32(registro["Rel"].ToString()), ValorNuevo = registro["Registro"].ToString() });
            }
            return resp;
        }

        public Task<(bool Success, string Message)> DeleteRequerimiento(int idRequerimiento) => _RequerimientoData.DeleteRequerimiento(idRequerimiento);
        #endregion Registros

        #region Director Ejecutivo
        public Task<List<Empleado_Detalle>> GetDirectoresEjecutivos() => _RequerimientoData.GetDirectoresEjecutivos();
        #endregion Director Ejecutivo

        #region Proyectos
        public Task<List<TB_Proyecto>> GetProyectosByDirectorEjecutivo(int IdDirectorEjecutivo) => _RequerimientoData.GetProyectosByDirectorEjecutivo(IdDirectorEjecutivo);
        #endregion Proyectos
    }
}
