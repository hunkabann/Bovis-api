using AutoMapper;
using Bovis.Business.Interface;
using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries
{
    public class AuditoriaQueryService : IAuditoriaQueryService
    {
        #region base
        private readonly IAuditoriaBusiness _auditoriaBusiness;

        private readonly IMapper _map;

        public AuditoriaQueryService(IMapper _map, IAuditoriaBusiness _auditoriaBusiness)
        {
            this._map = _map;
            this._auditoriaBusiness = _auditoriaBusiness;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion


        public async Task<Response<List<Proyecto>>> GetProyectos(string email_loged_user, string TipoAuditoria)
        {
            var response = await _auditoriaBusiness.GetProyectos(email_loged_user, TipoAuditoria);
            return new Response<List<Proyecto>> { Data = _map.Map<List<Proyecto>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<List<Documentos_Auditoria_Detalle>>> GetAuditorias(string TipoAuditoria)
        {
            var response = await _auditoriaBusiness.GetAuditorias(TipoAuditoria);
            return new Response<List<Documentos_Auditoria_Detalle>> { Data = _map.Map<List<Documentos_Auditoria_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<List<Documentos_Auditoria_Proyecto_Detalle>>> GetAuditoriasByProyecto(int IdProyecto, string TipoAuditoria, string FechaInicio, string FechaFin)
        {
            var response = await _auditoriaBusiness.GetAuditoriasByProyecto(IdProyecto, TipoAuditoria, FechaInicio, FechaFin);
            return new Response<List<Documentos_Auditoria_Proyecto_Detalle>> { Data = _map.Map<List<Documentos_Auditoria_Proyecto_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        
        public async Task<Response<List<Periodos_Auditoria_Detalle>>> GetPeriodosAuditoriaByProyecto(int IdProyecto, string TipoAuditoria)
        {
            var response = await _auditoriaBusiness.GetPeriodosAuditoriaByProyecto(IdProyecto, TipoAuditoria);
            return new Response<List<Periodos_Auditoria_Detalle>> { Data = _map.Map<List<Periodos_Auditoria_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<(bool Success, string Message)>> ClosePeriodoAuditoriaByProyecto(JsonObject registro)
        {
            var response = await _auditoriaBusiness.ClosePeriodoAuditoriaByProyecto(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        
        public async Task<Response<(bool Success, string Message)>> OpenPeriodoAuditoriaByProyecto(JsonObject registro)
        {
            var response = await _auditoriaBusiness.OpenPeriodoAuditoriaByProyecto(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<List<TB_Cat_AuditoriaTipoComentario>>> GetTipoComentarios()
        {
            var response = await _auditoriaBusiness.GetTipoComentarios();
            return new Response<List<TB_Cat_AuditoriaTipoComentario>> { Data = _map.Map<List<TB_Cat_AuditoriaTipoComentario>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }
        
        public async Task<Response<List<Comentario_Detalle>>> GetComentarios(int numProyecto)
        {
            var response = await _auditoriaBusiness.GetComentarios(numProyecto);
            return new Response<List<Comentario_Detalle>> { Data = _map.Map<List<Comentario_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<(bool Success, string Message)>> AddAuditorias(JsonObject registro)
        {
            var response = await _auditoriaBusiness.AddAuditorias(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
        
        public async Task<Response<(bool Success, string Message)>> AddComentarios(JsonObject registro, string usuario_logueado)
        {
            var response = await _auditoriaBusiness.AddComentarios(registro, usuario_logueado);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool Success, string Message)>> UpdateAuditoriaProyecto(JsonObject registro)
        {
            var response = await _auditoriaBusiness.UpdateAuditoriaProyecto(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool Success, string Message)>> AddAuditoriaDocumento(JsonObject registro)
        {
            var response = await _auditoriaBusiness.AddAuditoriaDocumento(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<List<TB_AuditoriaDocumento>>> GetDocumentosAuditoria(int IdAuditoria, int offset, int limit)
        {
            var response = await _auditoriaBusiness.GetDocumentosAuditoria(IdAuditoria, offset, limit);
            return new Response<List<TB_AuditoriaDocumento>> { Data = _map.Map<List<TB_AuditoriaDocumento>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<TB_AuditoriaDocumento>> GetDocumentoAuditoria(int IdDocumento)
        {
            var response = await _auditoriaBusiness.GetDocumentoAuditoria(IdDocumento);
            return new Response<TB_AuditoriaDocumento> { Data = _map.Map<TB_AuditoriaDocumento>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<(bool Success, string Message)>> AddAuditoriaDocumentoValidacion(JsonObject registro)
        {
            var response = await _auditoriaBusiness.AddAuditoriaDocumentoValidacion(registro);
            return new Response<(bool Success, string Message)> { Data = _map.Map<(bool Success, string Message)>(response), Success = response.Success, Message = response.Message };
        }
    }
}

