using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Business.Interface
{
    public interface IEmpleadoBusiness : IDisposable
    {
        #region Empleados
        Task<List<Empleado_Detalle>> GetEmpleados(bool? activo);
        Task<Empleado_Detalle> GetEmpleado(int idEmpleado);
        Task<Empleado_BasicData> GetEmpleadoByEmail(string email);
        Task<Empleado_BasicData> GetEmpleadoDetalle();
        Task<(bool Success, string Message)> AddRegistro(JsonObject registro);
        Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro);
        Task<(bool Success, string Message)> UpdateEstatus(JsonObject registro);
        #endregion Empleados

        #region Proyectos
        Task<List<Proyecto_Detalle>> GetProyectos(int idEmpleado);
        #endregion Proyectos

        #region Ciudades
        Task<List<TB_Ciudad>> GetCiudades(bool? Activo, int? IdEstado);
        #endregion Ciudades
    }

}
