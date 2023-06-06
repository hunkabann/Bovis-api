using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Data
{
    public class EmpleadoData : RepositoryLinq2DB<ConnectionDB>, IEmpleadoData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public EmpleadoData()
        {
            this.ConfigurationDB = dbConfig;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Empleados
        public async Task<List<TB_Empleado>> GetEmpleados(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Empleados
                                                                          where cat.Activo == activo
                                                                          select cat).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Empleado>();
        }

        public async Task<TB_Empleado> GetEmpleado(int idEmpleado)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = from cat in db.tB_Empleados
                          where cat.NumEmpleadoRrHh == idEmpleado
                          select cat;

                return await res.FirstOrDefaultAsync();

            }
        }

        public async Task<(bool existe, string mensaje)> AddRegistro(TB_Empleado registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                var insert = await db.tB_Empleados
                .Value(x => x.NumEmpleadoRrHh, registro.NumEmpleadoRrHh)
                .Value(x => x.IdPersona, registro.IdPersona)
                .Value(x => x.IdTipoEmpleado, registro.IdTipoEmpleado)
                .Value(x => x.IdCategoria, registro.IdCategoria)
                .Value(x => x.IdTipoContrato, registro.IdTipoContrato)
                .Value(x => x.CvePuesto, registro.CvePuesto)
                .Value(x => x.IdEmpresa, registro.IdEmpresa)
                .Value(x => x.IdCiudad, registro.IdCiudad)
                .Value(x => x.IdNivelEstudios, registro.IdNivelEstudios)
                .Value(x => x.IdFormaPago, registro.IdFormaPago)
                .Value(x => x.IdJornada, registro.IdJornada)
                .Value(x => x.IdDepartamento, registro.IdDepartamento)
                .Value(x => x.IdClasificacion, registro.IdClasificacion)
                .Value(x => x.IdJefeDirecto, registro.IdJefeDirecto)
                .Value(x => x.IdUnidadNegocio, registro.IdUnidadNegocio)
                .Value(x => x.IdTipoContrato_sat, registro.IdTipoContrato_sat)
                .Value(x => x.NumEmpleado, registro.NumEmpleado)
                .Value(x => x.FechaIngreso, registro.FechaIngreso)
                .Value(x => x.FechaSalida, registro.FechaSalida)
                .Value(x => x.FechaUltimoReingreso, registro.FechaUltimoReingreso)
                .Value(x => x.Nss, registro.Nss)
                .Value(x => x.EmailBovis, registro.EmailBovis)
                .Value(x => x.Experiencias, registro.Experiencias)
                .Value(x => x.Habilidades, registro.Habilidades)
                .Value(x => x.UrlRepositorio, registro.UrlRepositorio)
                .Value(x => x.Salario, registro.Salario)
                .Value(x => x.Profesion, registro.Profesion)
                .Value(x => x.Antiguedad, registro.Antiguedad)
                .Value(x => x.Turno, registro.Turno)
                .Value(x => x.UnidadMedica, registro.UnidadMedica)
                .Value(x => x.RegistroPatronal, registro.RegistroPatronal)
                .Value(x => x.Cotizacion, registro.Cotizacion)
                .Value(x => x.Duracion, registro.Duracion)
                .Value(x => x.Activo, registro.Activo)
                .Value(x => x.DescuentoPension, registro.DescuentoPension)
                .Value(x => x.PorcentajePension, registro.PorcentajePension)
                .Value(x => x.FondoFijo, registro.FondoFijo)
                .Value(x => x.CreditoInfonavit, registro.CreditoInfonavit)
                .Value(x => x.TipoDescuento, registro.TipoDescuento)
                .Value(x => x.ValorDescuento, registro.ValorDescuento)
                .Value(x => x.NoEmpleadoNoi, registro.NoEmpleadoNoi)
                .Value(x => x.Rol, registro.Rol)
                .InsertAsync() > 0;

                resp.Success = insert;
                resp.Message = insert == default ? "Ocurrio un error al agregar registro Cie." : string.Empty;
            }
            return resp;
        }
        #endregion Empleados
    }
}
