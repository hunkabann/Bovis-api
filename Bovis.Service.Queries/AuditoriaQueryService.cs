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

        #region Auditoria Legal
        public async Task<Response<List<TB_Cat_Auditoria_Contractual>>> GetAuditoriasContractual()
        {
            var response = await _auditoriaBusiness.GetAuditoriasContractual();
            return new Response<List<TB_Cat_Auditoria_Contractual>> { Data = _map.Map<List<TB_Cat_Auditoria_Contractual>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<(bool existe, string mensaje)>> AddAuditoriasContractual(JsonObject registro)
        {
            var response = await _auditoriaBusiness.AddAuditoriasContractual(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        public async Task<Response<List<Documentos_Auditoria_Cumplimiento_Detalle>>> GetAuditoriasCumplimiento()
        {
            var response = await _auditoriaBusiness.GetAuditoriasCumplimiento();
            return new Response<List<Documentos_Auditoria_Cumplimiento_Detalle>> { Data = _map.Map<List<Documentos_Auditoria_Cumplimiento_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<List<Documentos_Auditoria_Cumplimiento_Proyecto_Detalle>>> GetAuditoriasCumplimientoByProyecto(int IdProyecto)
        {
            var response = await _auditoriaBusiness.GetAuditoriasCumplimientoByProyecto(IdProyecto);
            return new Response<List<Documentos_Auditoria_Cumplimiento_Proyecto_Detalle>> { Data = _map.Map<List<Documentos_Auditoria_Cumplimiento_Proyecto_Detalle>>(response), Success = response is not null ? true : default, Message = response is null ? "No se encontró registro." : default };
        }

        public async Task<Response<(bool existe, string mensaje)>> AddAuditoriasCumplimiento(JsonObject registro)
        {
            var response = await _auditoriaBusiness.AddAuditoriasCumplimiento(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool existe, string mensaje)>> UpdateAuditoriaCumplimientoProyecto(JsonObject registro)
        {
            var response = await _auditoriaBusiness.UpdateAuditoriaCumplimientoProyecto(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }

        public async Task<Response<(bool existe, string mensaje)>> UpdateAuditoriaCumplimientoDocumento(JsonObject registro)
        {
            var response = await _auditoriaBusiness.UpdateAuditoriaCumplimientoDocumento(registro);
            return new Response<(bool existe, string mensaje)> { Data = _map.Map<(bool existe, string mensaje)>(response), Success = response.Success, Message = response.Message };
        }
        #endregion Auditoria de Calidad (Cumplimiento)
    }
}

