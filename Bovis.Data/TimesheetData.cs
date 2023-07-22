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
using static LinqToDB.SqlQuery.SqlPredicate;

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

        public async Task<List<TimeSheet_Detalle>> GetTimeSheetsByFiltro(int idEmpleado, int idProyecto, int mes)
        {
            // Si idEmpleado == 0, no filtrar por empleado
            // Si idProyecto == 0, no filtrar por proyecto
            // Si mes == 0, devolver los del mes anterior

            List<TimeSheet_Detalle> timesheets_summary = new List<TimeSheet_Detalle>();
            TimeSheet_Detalle timesheetDetalle = new TimeSheet_Detalle();
            int currentYear = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;
            int targetMonth = mes == 0 ? currentMonth == 1 ? 12 : currentMonth - 1 : mes;
            int targetYear = currentMonth == 1 && mes == 0 ? currentYear - 1 : currentYear;

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_timesheets = await (from ts in db.tB_Timesheets
                                            join emp1 in db.tB_Empleados on ts.IdResponsable equals emp1.NumEmpleadoRrHh
                                            join per1 in db.tB_Personas on emp1.IdPersona equals per1.IdPersona
                                            join emp2 in db.tB_Empleados on ts.IdEmpleado equals emp2.NumEmpleadoRrHh
                                            join per2 in db.tB_Personas on emp2.IdPersona equals per2.IdPersona
                                            join empr in db.tB_Empresas on emp2.IdEmpresa equals empr.IdEmpresa
                                            join proy in db.tB_Timesheet_Proyectos on ts.IdTimesheet equals proy.IdTimesheet into proyJoin
                                            from proyItem in proyJoin.DefaultIfEmpty()
                                            where ts.Activo == true
                                            && (idEmpleado == 0 || ts.IdEmpleado == idEmpleado)
                                            && (idProyecto == 0 || proyItem.IdProyecto == idProyecto)
                                            && ((currentMonth == 1 && ts.Mes == targetMonth && ts.Anio == targetYear) || (currentMonth > 1 && ts.Mes == targetMonth && ts.Anio == currentYear))
                                            orderby ts.IdTimesheet descending
                                            group new TimeSheet_Detalle
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
                                            } by ts.IdTimesheet into g
                                            select new TimeSheet_Detalle
                                            {
                                                id = g.First().id,
                                                id_empleado = g.First().id_empleado,
                                                empleado = g.First().empleado,
                                                mes = g.First().mes,
                                                anio = g.First().anio,
                                                id_responsable = g.First().id_responsable,
                                                responsable = g.First().responsable,
                                                sabados = g.First().sabados,
                                                dias_trabajo = g.First().dias_trabajo,
                                                coi_empresa = g.First().coi_empresa,
                                                noi_empresa = g.First().noi_empresa,
                                                noi_empleado = g.First().noi_empleado,
                                                num_empleado = g.First().num_empleado
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

                empleados = await (from empleadoProyecto in db.tB_EmpleadoProyectos
                                   join usuarioTimesheet in db.tB_Usuario_Timesheets on empleadoProyecto.NumProyecto equals usuarioTimesheet.NumProyecto
                                   join empleado in db.tB_Empleados on usuarioTimesheet.NumEmpleadoRrHh equals empleado.NumEmpleadoRrHh
                                   where empleado.EmailBovis == EmailResponsable
                                   group new Empleado_Detalle
                                   {
                                       nunum_empleado_rr_hh = empleadoProyecto.NumEmpleadoRrHh
                                   } by empleadoProyecto.NumEmpleadoRrHh into g
                                   select new Empleado_Detalle
                                   {
                                       nunum_empleado_rr_hh = g.Key
                                   }).ToListAsync();


                foreach (var empleado in empleados)
                {
                    var id_persona = await (from emp in db.tB_Empleados
                                            where emp.NumEmpleadoRrHh == empleado.nunum_empleado_rr_hh
                                            select emp.IdPersona).FirstOrDefaultAsync();

                    var persona = await (from p in db.tB_Personas
                                         where p.IdPersona == id_persona
                                         select p).FirstOrDefaultAsync();

                    empleado.nombre_persona = persona.Nombre + " " + persona.ApPaterno + " " + persona.ApMaterno;
                }



                return empleados;
            }
        }
    }
}
