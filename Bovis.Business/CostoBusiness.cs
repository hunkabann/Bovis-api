﻿using Bovis.Business.Interface;
using Bovis.Common.Model.Tables;
using Bovis.Common.Model.NoTable;
using Bovis.Data.Interface;
using System.Text.Json.Nodes;
using Bovis.Common.Model.DTO;
using System.Security.Cryptography.X509Certificates;
using Bovis.Common;

namespace Bovis.Business
{
    public class CostoBusiness : ICostoBusiness
    {
        #region base
        private readonly ICostoData _costoData; 
        public CostoBusiness(ICostoData _costoData)
        {
            this._costoData = _costoData;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region AddCosto
        public async Task<Response<decimal>> AddCosto(CostoPorEmpleadoDTO source)
        {
            TB_CostoPorEmpleado destination = new();

            var empleado = await _costoData.GetEmpleado(source.NumEmpleadoRrHh);
            source.IdPersona = empleado.IdPersona;
            source.FechaIngreso = empleado.FechaIngreso;
            source.IdPuesto = empleado.CvePuesto;
            source.NumProyecto = empleado.NumProyectoPrincipal;
            source.IdUnidadNegocio = empleado.IdUnidadNegocio;
            source.IdEmpresa = empleado.IdEmpresa;
            source.IdEmpleadoJefe = empleado.IdJefeDirecto;
            source.IdCategoria = empleado.IdCategoria;

            var proyecto = await _costoData.GetProyecto((int)source.NumProyecto);
            source.ImpuestoNomina = proyecto.ImpuestoNomina;

            destination = CostoBusinessUpdate.ValueFields(source);

            var response = await _costoData.AddCosto(destination);

            // var Categoria = await _costoData.Getcategoria((int)source.IdCategoria);
            // source.ChCategoria = Categoria.Categoria;

            return response;


        }
        #endregion

        #region GetCostos
        public async Task<List<Costo_Detalle>> GetCostos(bool? hist, string? idEmpleado, int? idPuesto, int? idProyecto, int? idEmpresa, int? idUnidadNegocio, DateTime? FechaIni, DateTime? FechaFin) => await _costoData.GetCostos(hist, idEmpleado, idPuesto, idProyecto, idEmpresa, idUnidadNegocio, FechaIni, FechaFin);
        #endregion

        #region GetCosto
        public Task<Costo_Detalle> GetCosto(int IdCosto) => _costoData.GetCosto(IdCosto);
        #endregion

        #region GetCostosEmpleado
        public Task<Response<List<Costo_Detalle>>> GetCostosEmpleado(string NumEmpleadoRrHh, bool hist)
        {
            return _costoData.GetCostosEmpleado(NumEmpleadoRrHh, hist);
        }
        #endregion

        #region GetCostoEmpleado
        public Task<Response<List<Costo_Detalle>>> GetCostoEmpleado(string NumEmpleado, int anno, int mes, bool hist = false)
        {
            return _costoData.GetCostoEmpleado(NumEmpleado, anno, mes, hist);
        }
        #endregion

        #region GetCostosBetweenDates
        public Task<Response<List<Costo_Detalle>>> GetCostosBetweenDates(string NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max, bool hist)
        {
            return _costoData.GetCostosBetweenDates(NumEmpleadoRrHh, anno_min, mes_min, anno_max, mes_max, hist);

        }
        #endregion

        #region GetCostoLaborable
        public Task<Response<decimal>> GetCostoLaborable(string NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max)
        {
            return _costoData.GetCostoLaborable(NumEmpleadoRrHh, anno_min, mes_min, anno_max, mes_max);
        }
        #endregion

        #region UpdeateCostos

        public async Task<Response<TB_CostoPorEmpleado>> UpdateCostos(int costoId, CostoPorEmpleadoDTO source)
        {
            TB_CostoPorEmpleado destination = new();
            destination = CostoBusinessUpdate.ValueFields(source);
            //ATC
           // var response = await _costoData.UpdateCostos(source, costoId, destination);
            var response = await _costoData.UpdateCostos(source,costoId, destination);
            return response;
        }

        public async Task<Response<TB_CostoPorEmpleado>> UpdateCostoEmpleado(CostoPorEmpleadoDTO source)
        {
            TB_CostoPorEmpleado destination = new();
            destination = CostoBusinessUpdate.ValueFields(source);
            var response = await _costoData.UpdateCostoEmpleado(destination);
            return response;
        }
        #endregion

        #region DeleteCosto
        public Task<Response<bool>> DeleteCosto(int costoId)
        {
            var response = _costoData.DeleteCosto(costoId);
            return response;
        }
        #endregion 

    }
}
