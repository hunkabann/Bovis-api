using Bovis.Common;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Service.Queries.Dto.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Service.Queries.Interface
{
    public interface ICieQueryService : IDisposable
    {
        #region Empresas
        Task<Response<List<EmpresaRegistro>>> GetEmpresas(bool? Activo);
        #endregion Empresas

        #region Cuenta Data
        Task<Response<List<CuentaContable_Detalle>>> GetCuentaData(JsonObject cuentas);
        Task<Response<(bool Success, string Message)>> AddCuentas(JsonObject registros);
        #endregion Cuenta Data

        #region Proyecto
        Task<Response<List<ProyectoData_Detalle>>> GetProyectoData(JsonObject proyectos);
        #endregion Proyecto

        #region Catálogos
        Task<Response<List<string>>> GetNombresCuenta();
        Task<Response<List<string>>> GetConceptos();
        Task<Response<List<int>>> GetNumsProyecto();
        Task<Response<List<string>>> GetResponsables();
        Task<Response<List<string>>> GetClasificacionesPY();
        #endregion Catálogos

        #region Registros
        Task<Response<Cie_Detalle>> GetRegistro(int? idRegistro);
        Task<Response<Cie_Registros>> GetRegistros(bool? activo, string nombre_cuenta, int mesInicio, int anioInicio, int mesFin, int anioFin, string concepto, string empresa, int num_proyecto, string responsable, string clasificacionPY, int offset, int limit);
        Task<Response<(bool Success, string Message)>> AddRegistros(JsonObject registros);
        Task<Response<(bool Success, string Message)>> UpdateRegistro(JsonObject registros);
        Task<Response<(bool Success, string Message)>> DeleteRegistro(int idRegistro);
        #endregion Registros
    }
}

