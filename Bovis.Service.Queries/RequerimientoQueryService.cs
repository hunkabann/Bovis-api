using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Dto.Responses;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class RequerimientoQueryService : IRequerimientoQueryService
    {
        #region base
        private readonly IRequerimientoBusiness _requerimientoBussines;
        private readonly IMapper _map;

        public RequerimientoQueryService(IMapper _map, IRequerimientoBusiness _requerimientoBussines)
        {
            this._map = _map;
            this._requerimientoBussines = _requerimientoBussines;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Habilidades
        public async Task<Response<List<Habilidad>>> GetHabilidades(int idRequerimiento)
        {
            var response = await _requerimientoBussines.GetHabilidades(idRequerimiento);
            return new Response<List<Habilidad>> { Data = _map.Map<List<Habilidad>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }
        #endregion Habilidades

        #region Experiencias
        public async Task<Response<List<Experiencia>>> GetExperiencias(int idRequerimiento)
        {
            var response = await _requerimientoBussines.GetExperiencias(idRequerimiento);
            return new Response<List<Experiencia>> { Data = _map.Map<List<Experiencia>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };
        }
        #endregion Experiencias

        #region Registros
        public async Task<Response<List<Requerimiento_Detalle>>> GetRequerimientos(bool? Asignados, string? idDirector, int? idProyecto, int? idPuesto)
        {
            var response = await _requerimientoBussines.GetRequerimientos(Asignados, idDirector, idProyecto, idPuesto);
            return new Response<List<Requerimiento_Detalle>> { Data = _map.Map<List<Requerimiento_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }
        public async Task<Response<Requerimiento_Detalle>> GetRequerimiento(int idRequerimiento)
        {
            var response = await _requerimientoBussines.GetRequerimiento(idRequerimiento);
            return new Response<Requerimiento_Detalle> { Data = _map.Map<Requerimiento_Detalle>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        public async Task<Response<(bool Success, string Message)>> AddRegistro(JsonObject registro)
        {
            var response = await _requerimientoBussines.AddRegistro(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool Success, string Message)>> UpdateRegistro(JsonObject registro)
        {
            var response = await _requerimientoBussines.UpdateRegistro(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        public async Task<Response<(bool Success, string Message)>> DeleteRequerimiento(int idRequerimiento)
        {
            var response = await _requerimientoBussines.DeleteRequerimiento(idRequerimiento);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Registros

        #region Director Ejecutivo
        public async Task<Response<List<Empleado_Detalle>>> GetDirectoresEjecutivos()
        {
            var response = await _requerimientoBussines.GetDirectoresEjecutivos();
            return new Response<List<Empleado_Detalle>> { Data = _map.Map<List<Empleado_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }
        #endregion Director Ejecutivo

        #region Proyectos
        public async Task<Response<List<TB_Proyecto>>> GetProyectosByDirectorEjecutivo(string IdDirectorEjecutivo)
        {
            var response = await _requerimientoBussines.GetProyectosByDirectorEjecutivo(IdDirectorEjecutivo);
            return new Response<List<TB_Proyecto>> { Data = _map.Map<List<TB_Proyecto>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontraron registros." : default };

        }
        #endregion Proyectos
    }
}
