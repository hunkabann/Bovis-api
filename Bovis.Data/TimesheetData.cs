using Azure.Core;
using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Data
{
    public class TimesheetData : RepositoryLinq2DB<ConnectionDB>, ITimesheetData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public TimesheetData()
        {
            this.ConfigurationDB = dbConfig;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<Detalle_Dias_Timesheet> GetDiasHabiles(int mes, int anio, bool sabados)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = from timeS in db.tB_Dias_Timesheets
                          where timeS.Mes == mes
                          && timeS.Anio == anio
                          select new Detalle_Dias_Timesheet
                          {
                              id = timeS.Id,
                              mes = timeS.Mes,
                              dias = timeS.Dias,
                              feriados = timeS.Feriados,
                              sabados = timeS.Sabados,
                              anio = timeS.Anio,
                              dias_habiles = (sabados == false) ? timeS.Dias - timeS.Feriados : timeS.Dias - timeS.Feriados + timeS.Sabados
                          };

                return await res.FirstOrDefaultAsync();

            }
        }

        public async Task<(bool existe, string mensaje)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_empleado = Convert.ToInt32(registro["empleado"]["code"].ToString());
            string nombre_empleado = registro["empleado"]["name"].ToString();
            string fecha = registro["fecha"].ToString();
            int mes = Convert.ToInt32(registro["mes"].ToString());
            int anio = Convert.ToInt32(registro["anio"].ToString());
            bool sabados = Convert.ToBoolean(registro["sabados"].ToString());
            int id_responsable = Convert.ToInt32(registro["id_responsable"].ToString());
            int dias_trabajo = Convert.ToInt32(registro["dias"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from timeS in db.tB_Timesheets
                                 where timeS.IdEmpleado == id_empleado
                                 && timeS.Mes == mes
                                 && timeS.Anio == anio
                                 select timeS).FirstOrDefaultAsync();

                if (res != null)
                {
                    resp.Success = true;
                    resp.Message = String.Format("Ya existe un registro de {0}, de la fecha {1}/{2}", nombre_empleado, mes, anio);
                    return resp;
                }

                int last_inserted_id = 0;

                var insert_timesheet = await db.tB_Timesheets
                    .Value(x => x.IdEmpleado, id_empleado)
                    .Value(x => x.Mes, mes)
                    .Value(x => x.Anio, anio)
                    .Value(x => x.IdResponsable, id_responsable)
                    .Value(x => x.Sabados, sabados)
                    .Value(x => x.DiasTrabajo, dias_trabajo)
                    .Value(x => x.Activo, true)
                    .InsertAsync() > 0;

                resp.Success = insert_timesheet;
                resp.Message = insert_timesheet == default ? "Ocurrio un error al agregar registro." : string.Empty;

                if (insert_timesheet != null)
                {
                    var lastInsertedRecord = db.tB_Timesheets.OrderByDescending(x => x.IdTimesheet).FirstOrDefault();
                    last_inserted_id = lastInsertedRecord.IdTimesheet;
                }

                foreach (var proyecto in registro["proyectos"].AsArray())
                {
                    int id_proyecto = Convert.ToInt32(proyecto["id"].ToString());
                    string nombre_proyecto = proyecto["nombre"].ToString();
                    int dias = Convert.ToInt32(proyecto["dias"].ToString());
                    int dedicacion = Convert.ToInt32(proyecto["dedicacion"].ToString());
                    int costo = Convert.ToInt32(proyecto["costo"].ToString());

                    var insert_timesheet_proyecto = await db.tB_Timesheet_Proyectos
                        .Value(x => x.IdTimesheet, last_inserted_id)
                        .Value(x => x.IdProyecto, id_proyecto)
                        .Value(x => x.Descripcion, nombre_proyecto)
                        .Value(x => x.Dias, dias)
                        .Value(x => x.TDedicacion, dedicacion)
                        .Value(x => x.Costo, costo)
                        .Value(x => x.Activo, true)
                        .InsertAsync() > 0;

                    resp.Success = insert_timesheet_proyecto;
                    resp.Message = insert_timesheet_proyecto == default ? "Ocurrio un error al agregar registro." : string.Empty;
                }

                foreach (var otro in registro["otros"].AsArray())
                {
                    string id_otro = otro["id"].ToString();
                    int dias = Convert.ToInt32(otro["dias"].ToString());
                    int dedicacion = Convert.ToInt32(otro["dedicacion"].ToString());

                    var insert_timesheet_otro = await db.tB_Timesheet_Otros
                        .Value(x => x.IdTimeSheet, last_inserted_id)
                        .Value(x => x.Descripcion, id_otro)
                        .Value(x => x.Dias, dias)
                        .Value(x => x.TDedicacion, dedicacion)
                        .Value(x => x.Activo, true)
                        .InsertAsync() > 0;

                    resp.Success = insert_timesheet_otro;
                    resp.Message = insert_timesheet_otro == default ? "Ocurrio un error al agregar registro." : string.Empty;
                }
            }

            return resp;
        }

        public async Task<List<TimeSheet_Detalle>> GetTimeSheets(bool? activo)
        {
            if (activo.HasValue)
            {
                List<TimeSheet_Detalle> timesheets_summary = new List<TimeSheet_Detalle>();
                TimeSheet_Detalle timesheetDetalle = new TimeSheet_Detalle();
                using (var db = new ConnectionDB(dbConfig))
                {
                    var res_timesheets = await (from ts in db.tB_Timesheets
                                                where ts.Activo == activo
                                                orderby ts.IdTimesheet descending
                                                select ts).ToListAsync();

                    foreach (var timesheet in res_timesheets)
                    {
                        var res_timesheet_otros = await (from ts_o in db.tB_Timesheet_Otros
                                                         where ts_o.IdTimeSheet == timesheet.IdTimesheet
                                                         && ts_o.Activo == true
                                                         select ts_o).ToListAsync();

                        var res_timesheet_proyectos = await (from ts_p in db.tB_Timesheet_Proyectos
                                                             where ts_p.IdTimesheet == timesheet.IdTimesheet
                                                             && ts_p.Activo == true
                                                             select ts_p).ToListAsync();

                        timesheetDetalle = new TimeSheet_Detalle();

                        timesheetDetalle.id = timesheet.IdTimesheet;
                        timesheetDetalle.id_empleado = timesheet.IdEmpleado;
                        timesheetDetalle.mes = timesheet.Mes;
                        timesheetDetalle.anio = timesheet.Anio;
                        timesheetDetalle.id_responsable = timesheet.IdResponsable;
                        timesheetDetalle.sabados = timesheet.Sabados;
                        timesheetDetalle.dias_trabajo = timesheet.DiasTrabajo;

                        timesheetDetalle.otros = res_timesheet_otros;
                        timesheetDetalle.proyectos = res_timesheet_proyectos;

                        timesheets_summary.Add(timesheetDetalle);
                    }

                }

                return timesheets_summary;
            }
            else return await GetAllFromEntityAsync<TimeSheet_Detalle>();
        }

        public async Task<List<TimeSheet_Detalle>> GetTimeSheetsByEmpleado(int idEmpleado)
        {
            if (idEmpleado > 0)
            {
                List<TimeSheet_Detalle> timesheets_summary = new List<TimeSheet_Detalle>();
                TimeSheet_Detalle timesheetDetalle = new TimeSheet_Detalle();
                using (var db = new ConnectionDB(dbConfig))
                {
                    var res_timesheets = await (from ts in db.tB_Timesheets
                                                join emp1 in db.tB_Empleados on ts.IdResponsable equals emp1.NumEmpleadoRrHh
                                                join per1 in db.tB_Personas on emp1.IdPersona equals per1.IdPersona
                                                join emp2 in db.tB_Empleados on ts.IdEmpleado equals emp2.NumEmpleadoRrHh
                                                join per2 in db.tB_Personas on emp2.IdPersona equals per2.IdPersona
                                                join empr in db.tB_Empresas on emp2.IdEmpresa equals empr.IdEmpresa
                                                where ts.IdEmpleado == idEmpleado
                                                && ts.Activo == true
                                                orderby ts.IdTimesheet descending
                                                select new TimeSheet_Detalle
                                                {
                                                    id = ts.IdTimesheet,
                                                    id_empleado = ts.IdEmpleado,
                                                    empleado = per2.Nombre + " " + per2.ApPaterno + " " + per2.ApMaterno,
                                                    mes = ts.Mes,
                                                    anio = ts.Anio,
                                                    id_responsable = ts.IdResponsable,
                                                    responsable = per1.Nombre + " " + per1.ApPaterno + " " + per1.ApMaterno,
                                                    sabados = ts.Sabados,
                                                    dias_trabajo = ts.DiasTrabajo,
                                                    coi_empresa = empr.Coi,
                                                    noi_empresa = empr.Noi,
                                                    noi_empleado = emp2.NoEmpleadoNoi,
                                                    num_empleado = emp2.NumEmpleado
                                                }).ToListAsync();

                    foreach (var timesheet in res_timesheets)
                    {
                        timesheet.otros = await (from ts_o in db.tB_Timesheet_Otros
                                                         where ts_o.IdTimeSheet == timesheet.id
                                                         && ts_o.Activo == true
                                                         select ts_o).ToListAsync();

                        timesheet.proyectos = await (from ts_p in db.tB_Timesheet_Proyectos
                                                             where ts_p.IdTimesheet == timesheet.id
                                                             && ts_p.Activo == true
                                                             select ts_p).ToListAsync();

                        timesheets_summary.Add(timesheet);
                    }

                }

                return timesheets_summary;
            }
            else return await GetAllFromEntityAsync<TimeSheet_Detalle>();
        }

        public async Task<List<TimeSheet_Detalle>> GetTimeSheetsByFecha(int mes, int anio)
        {
            if ((mes >= 1 && mes <= 12) && anio > 0)
            {
                List<TimeSheet_Detalle> timesheets_summary = new List<TimeSheet_Detalle>();
                TimeSheet_Detalle timesheetDetalle = new TimeSheet_Detalle();
                using (var db = new ConnectionDB(dbConfig))
                {
                    var res_timesheets = await (from ts in db.tB_Timesheets
                                                join emp1 in db.tB_Empleados on ts.IdResponsable equals emp1.NumEmpleadoRrHh
                                                join per1 in db.tB_Personas on emp1.IdPersona equals per1.IdPersona
                                                join emp2 in db.tB_Empleados on ts.IdEmpleado equals emp2.NumEmpleadoRrHh
                                                join per2 in db.tB_Personas on emp2.IdPersona equals per2.IdPersona
                                                join empr in db.tB_Empresas on emp2.IdEmpresa equals empr.IdEmpresa
                                                where ts.Mes == mes
                                                && ts.Anio == anio
                                                && ts.Activo == true
                                                orderby ts.IdTimesheet descending
                                                select new TimeSheet_Detalle
                                                {
                                                    id = ts.IdTimesheet,
                                                    id_empleado = ts.IdEmpleado,
                                                    empleado = per2.Nombre + " " + per2.ApPaterno + " " + per2.ApMaterno,
                                                    mes = ts.Mes,
                                                    anio = ts.Anio,
                                                    id_responsable = ts.IdResponsable,
                                                    responsable = per1.Nombre + " " + per1.ApPaterno + " " + per1.ApMaterno,
                                                    sabados = ts.Sabados,
                                                    dias_trabajo = ts.DiasTrabajo,
                                                    coi_empresa = empr.Coi,
                                                    noi_empresa = empr.Noi,
                                                    noi_empleado = emp2.NoEmpleadoNoi,
                                                    num_empleado = emp2.NumEmpleado
                                                }).ToListAsync();

                    foreach (var timesheet in res_timesheets)
                    {
                        timesheet.otros = await (from ts_o in db.tB_Timesheet_Otros
                                                         where ts_o.IdTimeSheet == timesheet.id
                                                         && ts_o.Activo == true
                                                         select ts_o).ToListAsync();

                        timesheet.proyectos = await (from ts_p in db.tB_Timesheet_Proyectos
                                                             where ts_p.IdTimesheet == timesheet.id
                                                             && ts_p.Activo == true
                                                             select ts_p).ToListAsync();

                        timesheets_summary.Add(timesheet);
                    }

                }

                return timesheets_summary;
            }
            else return await GetAllFromEntityAsync<TimeSheet_Detalle>();
        }

        public async Task<TimeSheet_Detalle> GetTimeSheet(int idTimeSheet)
        {
            TimeSheet_Detalle res_timesheet = new TimeSheet_Detalle();
            using (var db = new ConnectionDB(dbConfig))
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                res_timesheet = await (from ts in db.tB_Timesheets
                                       join emp1 in db.tB_Empleados on ts.IdResponsable equals emp1.NumEmpleadoRrHh
                                       join per1 in db.tB_Personas on emp1.IdPersona equals per1.IdPersona
                                       join emp2 in db.tB_Empleados on ts.IdEmpleado equals emp2.NumEmpleadoRrHh
                                       join per2 in db.tB_Personas on emp2.IdPersona equals per2.IdPersona
                                       join empr in db.tB_Empresas on emp2.IdEmpresa equals empr.IdEmpresa
                                       where ts.IdTimesheet == idTimeSheet
                                       select new TimeSheet_Detalle
                                       {
                                           id = ts.IdTimesheet,
                                           id_empleado = ts.IdEmpleado,
                                           empleado = per2.Nombre + " " + per2.ApPaterno + " " + per2.ApMaterno,
                                           mes = ts.Mes,
                                           anio = ts.Anio,
                                           id_responsable = ts.IdResponsable,
                                           responsable = per1.Nombre + " " + per1.ApPaterno + " " + per1.ApMaterno,
                                           sabados = ts.Sabados,
                                           dias_trabajo = ts.DiasTrabajo,
                                           coi_empresa = empr.Coi,
                                           noi_empresa = empr.Noi,
                                           noi_empleado = emp2.NoEmpleadoNoi,
                                           num_empleado = emp2.NumEmpleado
                                       }).FirstOrDefaultAsync();
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                if (res_timesheet != null)
                {
                    res_timesheet.otros = await (from ts_o in db.tB_Timesheet_Otros
                                                 where ts_o.IdTimeSheet == idTimeSheet
                                                 && ts_o.Activo == true
                                                 select ts_o).ToListAsync();

                    res_timesheet.proyectos = await (from ts_p in db.tB_Timesheet_Proyectos
                                                     where ts_p.IdTimesheet == idTimeSheet
                                                     && ts_p.Activo == true
                                                     select ts_p).ToListAsync();
                }

            }
            return res_timesheet;
        }

        public async Task<(bool existe, string mensaje)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_time_sheet = Convert.ToInt32(registro["id_time_sheet"].ToString());
            int id_empleado = Convert.ToInt32(registro["empleado"]["code"].ToString());
            string nombre_empleado = registro["empleado"]["name"].ToString();
            string fecha = registro["fecha"].ToString();
            int mes = Convert.ToInt32(registro["mes"].ToString());
            int anio = Convert.ToInt32(registro["anio"].ToString());
            bool sabados = Convert.ToBoolean(registro["sabados"].ToString());
            int id_responsable = Convert.ToInt32(registro["id_responsable"].ToString());
            int dias_trabajo = Convert.ToInt32(registro["dias"].ToString());
            int index = 0;

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_timesheet = await db.tB_Timesheets.Where(x => x.IdTimesheet == id_time_sheet)
                    .UpdateAsync(x => new TB_Timesheet
                    {
                        IdEmpleado = id_empleado,
                        Mes = mes,
                        Anio = anio,
                        IdResponsable = id_responsable,
                        Sabados = sabados,
                        DiasTrabajo = dias_trabajo
                    }) > 0;

                resp.Success = res_update_timesheet;
                resp.Message = res_update_timesheet == default ? "Ocurrio un error al actualizar registro." : string.Empty;

                var res_timesheet_proyectos = await (from ts_p in db.tB_Timesheet_Proyectos
                                                     where ts_p.IdTimesheet == id_time_sheet
                                                     select ts_p)
                                                     .ToListAsync();

                int[] ids_proyectos_db = new int[res_timesheet_proyectos.Count()];
                index = 0;
                foreach (var r in res_timesheet_proyectos)
                {
                    ids_proyectos_db[index] = r.IdProyecto;
                    index++;
                }
                int[] ids_proyectos_request = new int[registro["proyectos"].AsArray().Count()];
                index = 0;
                foreach (var r in registro["proyectos"].AsArray())
                {
                    ids_proyectos_request[index] = Convert.ToInt32(r["id"].ToString());
                    index++;
                }
                HashSet<int> ids_proyectos = new HashSet<int>(ids_proyectos_db.Concat(ids_proyectos_request));

                foreach (int id in ids_proyectos)
                {
                    JsonObject proyecto = (JsonObject)registro["proyectos"].AsArray().FirstOrDefault(r => r["id"].ToString() == id.ToString());
                    int id_proyecto = Convert.ToInt32(proyecto["id"].ToString());
                    string nombre_proyecto = proyecto["nombre"].ToString();
                    int dias = Convert.ToInt32(proyecto["dias"].ToString());
                    int dedicacion = Convert.ToInt32(proyecto["dedicacion"].ToString());
                    int costo = Convert.ToInt32(proyecto["costo"].ToString());

                    if (ids_proyectos_db.Contains(id))
                    {
                        if (ids_proyectos_request.Contains(id))
                        {
                            // Se actualiza
                            var res_update_timesheet_proyecto = await db.tB_Timesheet_Proyectos.Where(x => x.IdProyecto == id && x.IdTimesheet == id_time_sheet)
                                .UpdateAsync(x => new TB_Timesheet_Proyecto
                                {
                                    Descripcion = nombre_proyecto,
                                    Dias = dias,
                                    TDedicacion = dedicacion,
                                    Costo = costo,
                                    Activo = true
                                }) > 0;

                            resp.Success = res_update_timesheet_proyecto;
                            resp.Message = res_update_timesheet_proyecto == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                        }
                        else
                        {
                            // Se elimina
                            var res_delete_timesheet_proyecto = await (db.tB_Timesheet_Proyectos
                               .Where(x => x.IdTimesheet_Proyecto == id)
                               .Set(x => x.Activo, false))
                               .UpdateAsync() >= 0;

                            resp.Success = res_delete_timesheet_proyecto;
                            resp.Message = res_delete_timesheet_proyecto == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                        }
                    }
                    else
                    {
                        // Se agrega
                        var insert_timesheet_proyecto = await db.tB_Timesheet_Proyectos
                            .Value(x => x.IdTimesheet, id_time_sheet)
                            .Value(x => x.IdProyecto, id_proyecto)
                            .Value(x => x.Descripcion, nombre_proyecto)
                            .Value(x => x.Dias, dias)
                            .Value(x => x.TDedicacion, dedicacion)
                            .Value(x => x.Costo, costo)
                            .Value(x => x.Activo, true)
                            .InsertAsync() > 0;

                        resp.Success = insert_timesheet_proyecto;
                        resp.Message = insert_timesheet_proyecto == default ? "Ocurrio un error al agregar registro." : string.Empty;
                    }
                    Console.WriteLine();
                }


                var res_timesheet_otros = await (from ts_o in db.tB_Timesheet_Otros
                                                 where ts_o.IdTimeSheet == id_time_sheet
                                                 select ts_o)
                                                 .ToListAsync();

                string[] ids_otros_db = new string[res_timesheet_otros.Count()];
                index = 0;
                foreach (var r in res_timesheet_otros)
                {
                    ids_otros_db[index] = r.Descripcion;
                    index++;
                }
                string[] ids_otros_request = new string[registro["otros"].AsArray().Count()];
                index = 0;
                foreach (var r in registro["otros"].AsArray())
                {
                    ids_otros_request[index] = r["id"].ToString();
                    index++;
                }
                HashSet<string> ids_otros = new HashSet<string>(ids_otros_db.Concat(ids_otros_request));

                foreach (string id in ids_otros)
                {
                    JsonObject otro = (JsonObject)registro["otros"].AsArray().FirstOrDefault(r => r["id"].ToString() == id.ToString());
                    string id_otro = otro["id"].ToString();
                    int dias = Convert.ToInt32(otro["dias"].ToString());
                    int dedicacion = Convert.ToInt32(otro["dedicacion"].ToString());

                    if (ids_otros_db.Contains(id))
                    {
                        if (ids_otros_request.Contains(id))
                        {
                            // Se actualiza
                            var res_update_timesheet_otro = await db.tB_Timesheet_Otros.Where(x => x.Descripcion == id && x.IdTimeSheet == id_time_sheet)
                                .UpdateAsync(x => new TB_Timesheet_Otro
                                {
                                    Descripcion = id_otro,
                                    Dias = dias,
                                    TDedicacion = dedicacion,
                                    Activo = true
                                }) > 0;

                            resp.Success = res_update_timesheet_otro;
                            resp.Message = res_update_timesheet_otro == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                        }
                        else
                        {
                            // Se elimina
                            var res_delete_timesheet_otro = await (db.tB_Timesheet_Otros
                               .Where(x => x.Descripcion == id)
                               .Set(x => x.Activo, false)).UpdateAsync() >= 0;

                            resp.Success = res_delete_timesheet_otro;
                            resp.Message = res_delete_timesheet_otro == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                        }
                    }
                    else
                    {
                        // Se agrega
                        var insert_timesheet_otro = await db.tB_Timesheet_Otros
                            .Value(x => x.IdTimeSheet, id_time_sheet)
                            .Value(x => x.Descripcion, id_otro)
                            .Value(x => x.Dias, dias)
                            .Value(x => x.TDedicacion, dedicacion)
                            .Value(x => x.Activo, true)
                            .InsertAsync() > 0;

                        resp.Success = insert_timesheet_otro;
                        resp.Message = insert_timesheet_otro == default ? "Ocurrio un error al agregar registro." : string.Empty;
                    }
                    Console.WriteLine();
                }
            }

            return resp;
        }

        public async Task<(bool existe, string mensaje)> DeleteTimeSheet(int idTimeSheet)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_timesheet = await db.tB_Timesheets.Where(x => x.IdTimesheet == idTimeSheet)
                                .UpdateAsync(x => new TB_Timesheet
                                {
                                    Activo = false
                                }) > 0;

                resp.Success = res_update_timesheet;
                resp.Message = res_update_timesheet == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }

        public async Task<List<Empleado_Detalle>> GetEmpleadosByResponsable(string EmailResponsable)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                List<Empleado_Detalle> empleados = new List<Empleado_Detalle>();

                var proyectos_responsable = await (from proyecto in db.tB_Proyectos
                                                   join emp_proy in db.tB_EmpleadoProyectos on proyecto.NumProyecto equals emp_proy.NumProyecto into emp_proyJoin
                                                   from emp_proyItem in emp_proyJoin.DefaultIfEmpty()
                                                   join emp in db.tB_Empleados on emp_proyItem.NumEmpleadoRrHh equals emp.NumEmpleadoRrHh into empJoin
                                                   from empItem in empJoin.DefaultIfEmpty()
                                                   where empItem.EmailBovis == EmailResponsable
                                                   select proyecto).ToListAsync();

                foreach(var proyecto in proyectos_responsable)
                {
                    var empleado = await (from emp in db.tB_Empleados
                                                  join per in db.tB_Personas on emp.IdPersona equals per.IdPersona into perJoin
                                                  from perItem in perJoin.DefaultIfEmpty()
                                                  join tipo_emp in db.tB_Cat_TipoEmpleados on emp.IdTipoEmpleado equals tipo_emp.IdTipoEmpleado into tipo_empJoin
                                                  from tipo_empItem in tipo_empJoin.DefaultIfEmpty()
                                                  join cat in db.tB_Cat_Categorias on emp.IdCategoria equals cat.IdCategoria into catJoin
                                                  from catItem in catJoin.DefaultIfEmpty()
                                                  join contrato in db.tB_Cat_TipoContratos on emp.IdTipoContrato equals contrato.IdTipoContrato into contratoJoin
                                                  from contratoItem in contratoJoin.DefaultIfEmpty()
                                                  join puesto in db.tB_Cat_Puestos on emp.CvePuesto equals puesto.IdPuesto into puestoJoin
                                                  from puestoItem in puestoJoin.DefaultIfEmpty()
                                                  join empresa in db.tB_Empresas on emp.IdEmpresa equals empresa.IdEmpresa into empresaJoin
                                                  from empresaItem in empresaJoin.DefaultIfEmpty()
                                                  join ciudad in db.tB_Ciudads on emp.IdCiudad equals ciudad.IdCiudad into ciudadJoin
                                                  from ciudadItem in ciudadJoin.DefaultIfEmpty()
                                                  join estado in db.tB_Estados on emp.IdEstado equals estado.IdEstado into estadoJoin
                                                  from estadoItem in estadoJoin.DefaultIfEmpty()
                                                  join pais in db.tB_Pais on emp.IdPais equals pais.IdPais into paisJoin
                                                  from paisItem in paisJoin.DefaultIfEmpty()
                                                  join estudios in db.tB_Cat_NivelEstudios on emp.IdNivelEstudios equals estudios.IdNivelEstudios into estudiosJoin
                                                  from estudiosItem in estudiosJoin.DefaultIfEmpty()
                                                  join pago in db.tB_Cat_FormaPagos on emp.IdFormaPago equals pago.IdFormaPago into pagoJoin
                                                  from pagoItem in pagoJoin.DefaultIfEmpty()
                                                  join jornada in db.tB_Cat_Jornadas on emp.IdJornada equals jornada.IdJornada into jornadaJoin
                                                  from jornadaItem in jornadaJoin.DefaultIfEmpty()
                                                  join depto in db.tB_Cat_Departamentos on emp.IdDepartamento equals depto.IdDepartamento into deptoJoin
                                                  from deptoItem in deptoJoin.DefaultIfEmpty()
                                                  join clasif in db.tB_Cat_Clasificacions on emp.IdClasificacion equals clasif.IdClasificacion into clasifJoin
                                                  from clasifItem in clasifJoin.DefaultIfEmpty()
                                                  join jefe in db.tB_Personas on emp.IdJefeDirecto equals jefe.IdPersona into jefeJoin
                                                  from jefeItem in jefeJoin.DefaultIfEmpty()
                                                  join unidad in db.tB_Cat_UnidadNegocios on emp.IdUnidadNegocio equals unidad.IdUnidadNegocio into unidadJoin
                                                  from unidadItem in unidadJoin.DefaultIfEmpty()
                                                  join contrato_sat in db.tB_Cat_TipoContrato_Sats on emp.IdTipoContrato_sat equals contrato_sat.IdTipoContratoSat into contrato_satJoin
                                                  from contrato_satItem in contrato_satJoin.DefaultIfEmpty()
                                                  join profesion in db.tB_Cat_Profesiones on emp.IdProfesion equals profesion.IdProfesion into profesionJoin
                                                  from profesionItem in profesionJoin.DefaultIfEmpty()
                                                  join turno in db.tB_Cat_Turnos on emp.IdTurno equals turno.IdTurno into turnoJoin
                                                  from turnoItem in turnoJoin.DefaultIfEmpty()
                                                  where emp.IdJefeDirecto == proyecto.IdResponsableSupervisor
                                                  select new Empleado_Detalle
                                                  {
                                                      nunum_empleado_rr_hh = emp.NumEmpleadoRrHh,
                                                      nukidpersona = emp.IdPersona,
                                                      nombre_persona = perItem != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : string.Empty,
                                                      nukidtipo_empleado = emp.IdTipoEmpleado,
                                                      chtipo_emplado = tipo_empItem.TipoEmpleado != null ? tipo_empItem.TipoEmpleado : string.Empty,
                                                      nukidcategoria = emp.IdCategoria,
                                                      chcategoria = catItem != null ? catItem.Categoria : string.Empty,
                                                      nukidtipo_contrato = emp.IdTipoContrato,
                                                      chtipo_contrato = contratoItem != null ? contratoItem.VeContrato : string.Empty,
                                                      chcve_puesto = emp.CvePuesto,
                                                      chpuesto = puestoItem != null ? puestoItem.Puesto : string.Empty,
                                                      nukidempresa = emp.IdEmpresa,
                                                      chempresa = empresaItem != null ? empresaItem.Empresa : string.Empty,
                                                      chcalle = emp.Calle,
                                                      nunumero_interior = emp.NumeroInterior,
                                                      nunumero_exterior = emp.NumeroExterior,
                                                      chcolonia = emp.Colonia,
                                                      chalcaldia = emp.Alcaldia,
                                                      nukidciudad = emp.IdCiudad,
                                                      chciudad = ciudadItem != null ? ciudadItem.Ciudad : string.Empty,
                                                      nukidestado = emp.IdEstado,
                                                      chestado = estadoItem != null ? estadoItem.Estado : string.Empty,
                                                      chcp = emp.CP,
                                                      nukidpais = emp.IdPais,
                                                      chpais = paisItem != null ? paisItem.Pais : string.Empty,
                                                      nukidnivel_estudios = emp.IdNivelEstudios,
                                                      chnivel_estudios = estudiosItem != null ? estudiosItem.NivelEstudios : string.Empty,
                                                      nukidforma_pago = emp.IdFormaPago,
                                                      chforma_pago = pagoItem != null ? pagoItem.TipoDocumento : string.Empty,
                                                      nukidjornada = emp.IdJornada,
                                                      chjornada = jornadaItem != null ? jornadaItem.Jornada : string.Empty,
                                                      nukiddepartamento = emp.IdDepartamento,
                                                      chdepartamento = deptoItem != null ? deptoItem.Departamento : string.Empty,
                                                      nukidclasificacion = emp.IdClasificacion,
                                                      chclasificacion = clasifItem != null ? clasifItem.Clasificacion : string.Empty,
                                                      nukidjefe_directo = emp.IdJefeDirecto,
                                                      chjefe_directo = jefeItem != null ? jefeItem.Nombre + " " + jefeItem.ApPaterno + " " + jefeItem.ApMaterno : string.Empty,
                                                      nukidunidad_negocio = emp.IdUnidadNegocio,
                                                      chunidad_negocio = unidadItem != null ? unidadItem.UnidadNegocio : string.Empty,
                                                      nukidtipo_contrato_sat = emp.IdTipoContrato_sat,
                                                      chtipo_contrato_sat = contrato_satItem != null ? contrato_satItem.ContratoSat : string.Empty,
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
                                                      nukidprofesion = emp.IdProfesion,
                                                      chprofesion = profesionItem != null ? profesionItem.Profesion : string.Empty,
                                                      nuantiguedad = emp.Antiguedad,
                                                      nukidturno = emp.IdTurno,
                                                      chturno = turnoItem != null ? turnoItem.Turno : string.Empty,
                                                      nuunidad_medica = emp.UnidadMedica,
                                                      chregistro_patronal = emp.RegistroPatronal,
                                                      chcotizacion = emp.Cotizacion,
                                                      nuduracion = emp.Duracion,
                                                      boactivo = emp.Activo,
                                                      chporcentaje_pension = emp.ChPorcentajePension,
                                                      nudescuento_pension = emp.DescuentoPension,
                                                      nufondo_fijo = emp.FondoFijo,
                                                      nucredito_infonavit = emp.CreditoInfonavit,
                                                      chtipo_descuento = emp.TipoDescuento,
                                                      nuvalor_descuento = emp.ValorDescuento,
                                                      nuno_empleado_noi = emp.NoEmpleadoNoi,
                                                      chrol = emp.Rol
                                                  }).FirstOrDefaultAsync();

                    if (empleado != null)
                    {
                        empleado.experiencias = await (from exp in db.tB_Empleado_Experiencias
                                                  join cat in db.tB_Cat_Experiencias on exp.IdExperiencia equals cat.IdExperiencia
                                                  where exp.IdEmpleado == empleado.nunum_empleado_rr_hh
                                                  && exp.Activo == true
                                                  select new Experiencia_Detalle
                                                  {
                                                      IdEmpleado = exp.IdEmpleado,
                                                      IdExperiencia = exp.IdExperiencia,
                                                      Experiencia = cat.Experiencia,
                                                      Activo = exp.Activo
                                                  }).ToListAsync();

                        foreach (var exp in empleado.experiencias)
                        {
                            empleado.chexperiencias += (empleado.chexperiencias == null) ? exp.Experiencia : ", " + exp.Experiencia;
                        }

                        empleado.habilidades = await (from hab in db.tB_Empleado_Habilidades
                                                 join cat in db.tB_Cat_Habilidades on hab.IdHabilidad equals cat.IdHabilidad
                                                 where hab.IdEmpleado == empleado.nunum_empleado_rr_hh
                                                 && hab.Activo == true
                                                 select new Habilidad_Detalle
                                                 {
                                                     IdEmpleado = hab.IdEmpleado,
                                                     IdHabilidad = hab.IdHabilidad,
                                                     Habilidad = cat.Habilidad,
                                                     Activo = hab.Activo
                                                 }).ToListAsync();

                        foreach (var hab in empleado.habilidades)
                        {
                            empleado.chhabilidades += (empleado.chhabilidades == null) ? hab.Habilidad : ", " + hab.Habilidad;
                        }
                    }

                    empleados.Add(empleado);
                }

                
                return empleados;
            }
        }
    }
}
