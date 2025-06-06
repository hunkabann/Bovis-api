﻿using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Service.Queries.Dto.Both;
using Bovis.Service.Queries.Dto.Request;
using Bovis.Service.Queries.Dto.Responses;
using System.Text.Json.Nodes;

namespace Bovis.Service.Queries.Interface
{
    public interface IFacturaQueryService : IDisposable
    {
        Task<Response<InfoFactura>> ExtraerInfoFactura(string B64Xml);
        Task<Response<(bool Success, string Message)>> AddNotaCreditoSinFactura(JsonObject registro);
        Task<Response<List<NotaCredito_Detalle>>> GetNotaCreditoSinFactura(int NumProyecto, int Mes, int Anio);
        Task<Response<(bool Success, string Message)>> AddNotaCreditoSinFacturaToFactura(JsonObject registro);
        Task<Response<FacturaProyecto>> GetInfoProyecto(int numProyecto);
        Task<Response<List<FacturaDetalles>>> Search(ConsultarFactura request);
        Task<Response<(bool Success, string Message)>> CancelNota(JsonObject registro);
        Task<Response<(bool Success, string Message)>> CancelCobranza(JsonObject registro);
    }
}