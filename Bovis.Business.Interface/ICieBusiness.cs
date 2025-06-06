﻿using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface ICieBusiness : IDisposable
    {
        #region Empresas
        Task<List<TB_Empresa>> GetEmpresas(bool? activo);
        #endregion Empresas

        #region Cuenta Data
        Task<List<CuentaContable_Detalle>> GetCuentaData(JsonObject cuentas);
        Task<List<CtaContableRespuesta_Detalle>> AddCuentas(JsonObject registros);
        #endregion Cuenta Data

        #region Proyecto
        Task<List<ProyectoData_Detalle>> GetProyectoData(JsonObject proyectos);
        #endregion Proyecto


        #region Catálogos
        Task<List<string>> GetNombresCuenta();
        Task<List<string>> GetConceptos();
        Task<List<int>> GetNumsProyecto();
        Task<List<string>> GetResponsables();
        Task<List<string>> GetClasificacionesPY();
        Task<List<string>> GetTiposPY();
        #endregion Catálogos

        #region Registros
        Task<Cie_Detalle> GetRegistro(int? numProyecto);
        Task<Cie_Registros> GetRegistros(JsonObject registro);
        Task<(bool Success, string Message)> AddRegistros(JsonObject registros);
        Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro);
        Task<(bool Success, string Message)> DeleteRegistro(int idRegistro);
        Task<(bool Success, string Message)> DeleteArchivo(JsonObject registro);
        #endregion Registros
    }

}
