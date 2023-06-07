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
                using (var db = new ConnectionDB(dbConfig)) return await (from emp in db.tB_Empleados
                                                                          where emp.Activo == activo
                                                                          select emp).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Empleado>();
        }

        public async Task<Empleado_Detalle> GetEmpleado(int idEmpleado)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = from emp in db.tB_Empleados
                          join per in db.tB_Personas on emp.IdPersona equals per.IdPersona
                          where emp.NumEmpleadoRrHh == idEmpleado
                          select new Empleado_Detalle
                          {
                              nunum_empleado_rr_hh = emp.NumEmpleadoRrHh,
                              nukidpersona = emp.IdPersona,
                              nombre_persona = per.Nombre + " " + per.ApPaterno + " " + per.ApPaterno,
                              nukidtipo_empleado = emp.IdTipoEmpleado,
                              nukidcategoria = emp.IdCategoria,
                              nukidtipo_contrato = emp.IdTipoContrato,
                              chcve_puesto = emp.CvePuesto,
                              nukidempresa = emp.IdEmpresa,
                              nukidciudad = emp.IdCiudad,
                              nukidnivel_estudios = emp.IdNivelEstudios,
                              nukidforma_pago = emp.IdFormaPago,
                              nukidjornada = emp.IdJornada,
                              nukiddepartamento = emp.IdDepartamento,
                              nukidclasificacion = emp.IdClasificacion,
                              nukidjefe_directo = emp.IdJefeDirecto,
                              nukidunidad_negocio = emp.IdUnidadNegocio,
                              nukidtipo_contrato_sat = emp.IdTipoContrato_sat,
                              nunum_empleado = emp.NumEmpleado,
                              dtfecha_ingreso = emp.FechaIngreso,
                              dtfecha_salida = emp.FechaSalida,
                              dtfecha_ultimo_reingreso = emp.FechaUltimoReingreso,
                              chnss = emp.Nss,
                              chemail_bovis = emp.EmailBovis,
                              chexperiencias = emp.Experiencias,
                              chhabilidades = emp.Habilidades,
                              churl_repositorio = emp.UrlRepositorio,
                              nusalario = emp.Salario,
                              chprofesion = emp.Profesion,
                              nuantiguedad = emp.Antiguedad,
                              chturno = emp.Turno,
                              nuunidad_medica = emp.UnidadMedica,
                              chregistro_patronal = emp.RegistroPatronal,
                              chcotizacion = emp.Cotizacion,
                              nuduracion = emp.Duracion,
                              boactivo = emp.Activo,
                              bodescuento_pension = emp.DescuentoPension,
                              nuporcentaje_pension = emp.PorcentajePension,
                              nufondo_fijo = emp.FondoFijo,
                              nucredito_infonavit = emp.CreditoInfonavit,
                              chtipo_descuento = emp.TipoDescuento,
                              nuvalor_descuento = emp.ValorDescuento,
                              nuno_empleado_noi = emp.NoEmpleadoNoi,
                              chrol = emp.Rol
                          };

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

        #region Proyectos
        public async Task<List<Proyecto_Detalle>> GetProyectos(int idEmpleado)
        {
            if (idEmpleado > 0)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from emp_proj in db.tB_EmpleadoProyectos
                                                                          join proj in db.tB_Proyectos on emp_proj.NumProyecto equals proj.NumProyecto
                                                                          where emp_proj.NumEmpleadoRrHh == idEmpleado
                                                                          select new Proyecto_Detalle
                                                                          {
                                                                              nunum_proyecto = proj.NumProyecto,
                                                                              chproyecto = proj.Proyecto,
                                                                              chalcance = proj.Alcance,
                                                                              chcp = proj.Cp,
                                                                              chciudad = proj.Ciudad,
                                                                              nukidestatus = proj.IdEstatus,
                                                                              nukidsector = proj.IdSector,
                                                                              nukidtipo_proyecto = proj.IdTipoProyecto,
                                                                              nukidresponsable_preconstruccion = proj.IdResponsablePreconstruccion,
                                                                              nukidresponsable_construiccion = proj.IdResponsableConstruccion,
                                                                              nukidresponsable_ehs = proj.IdResponsableEhs,
                                                                              nukidresponsable_supervisor = proj.IdResponsableSupervisor,
                                                                              nukidcliente = proj.IdCliente,
                                                                              nukidempresa = proj.IdEmpresa,
                                                                              nukidpais = proj.IdPais,
                                                                              nukiddirector_ejecutivo = proj.IdDirectorEjecutivo,
                                                                              nucosto_promedio_m2 = proj.CostoPromedioM2,
                                                                              dtfecha_ini = proj.FechaIni,
                                                                              dtfecha_fin = proj.FechaFin,
                                                                              nunum_empleado_rr_hh = emp_proj.NumEmpleadoRrHh,
                                                                              nuporcantaje_participacion = emp_proj.PorcentajeParticipacion,
                                                                              chalias_puesto = emp_proj.AliasPuesto,
                                                                              chgrupo_proyecto = emp_proj.GrupoProyecto
                                                                          }).ToListAsync();
            }
            else return await GetAllFromEntityAsync<Proyecto_Detalle>();
        }
        #endregion Proyectos
    }
}
