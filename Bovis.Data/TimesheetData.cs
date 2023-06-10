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

        public async Task<Dias_Timesheet_Detalle> GetDiasHabiles(int mes, int anio, bool sabados)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = from timeS in db.tB_Dias_Timesheets
                          where timeS.Mes == mes
                          && timeS.Anio == anio
                          select new Dias_Timesheet_Detalle
                          {
                              id = timeS.Id,
                              mes = timeS.Mes,
                              dias = timeS.Dias,
                              feriados = timeS.Feriados,
                              sabados = timeS.Sabados,
                              anio = timeS.Anio,
                              dias_habiles = (sabados == true) ? timeS.Dias - timeS.Feriados : timeS.Dias - timeS.Feriados - timeS.Sabados
                          };

                return await res.FirstOrDefaultAsync();

            }
        }

        public async Task<(bool existe, string mensaje)> AgregarRegistro(JsonObject registro)
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
                var res = from timeS in db.tB_Timesheets
                          where timeS.IdEmpleado == id_empleado
                          && timeS.Mes == mes
                          && timeS.Anio == anio
                          select timeS;

                var timeS_record = await res.FirstOrDefaultAsync();
                
                if(timeS_record != null)
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
                        .InsertAsync() > 0;

                    resp.Success = insert_timesheet_otro;
                    resp.Message = insert_timesheet_otro == default ? "Ocurrio un error al agregar registro." : string.Empty;
                }
            }

            return resp;
        }

        public async Task<List<TB_Timesheet>> GetTimeSheets(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig))
                {
                    return await (from ts in db.tB_Timesheets
                                  select ts).ToListAsync();
                }
            }
            else return await GetAllFromEntityAsync<TB_Timesheet>();
        }

        public async Task<TimeSheet_Detalle> GetTimeSheet(int idTimeSheet)
        {
            TimeSheet_Detalle timesheetDetalle = new TimeSheet_Detalle();
            using (var db = new ConnectionDB(dbConfig))
            {
                var res_timesheet = await (from ts in db.tB_Timesheets
                                           where ts.IdTimesheet == idTimeSheet
                                           select ts).FirstOrDefaultAsync();

                var res_timesheet_otros = await (from ts_o in db.tB_Timesheet_Otros
                                                 where ts_o.IdTimeSheet == idTimeSheet
                                                 select ts_o).ToListAsync();

                var res_timesheet_proyectos = await (from ts_p in db.tB_Timesheet_Proyectos
                                                     where ts_p.IdTimesheet == idTimeSheet
                                                     select ts_p).ToListAsync();

                if (res_timesheet != null)
                {
                    timesheetDetalle.id = res_timesheet.IdTimesheet;
                    timesheetDetalle.id_empleado = res_timesheet.IdEmpleado;
                    timesheetDetalle.mes = res_timesheet.Mes;
                    timesheetDetalle.anio = res_timesheet.Anio;
                    timesheetDetalle.id_responsable = res_timesheet.IdResponsable;
                    timesheetDetalle.sabados = res_timesheet.Sabados;
                    timesheetDetalle.dias_trabajo = res_timesheet.DiasTrabajo;
                }

                timesheetDetalle.otros = res_timesheet_otros;
                timesheetDetalle.proyectos = res_timesheet_proyectos;

            }
            return timesheetDetalle;
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
                                                     select ts_p).ToListAsync();

                foreach (var proyecto in registro["proyectos"].AsArray())
                {
                    int id_proyecto = Convert.ToInt32(proyecto["id"].ToString());
                    string nombre_proyecto = proyecto["nombre"].ToString();
                    int dias = Convert.ToInt32(proyecto["dias"].ToString());
                    int dedicacion = Convert.ToInt32(proyecto["dedicacion"].ToString());
                    int costo = Convert.ToInt32(proyecto["costo"].ToString());

                    // Borrar proyecto en BD, que no viene en el nuevo arreglo.
                    bool existe_proyecto = res_timesheet_proyectos.Any(x => x.IdProyecto == id_proyecto);

                    if (existe_proyecto == false)
                    {
                        var res_delete_timesheet_proyecto = await (db.tB_Timesheet_Proyectos
                           .Where(x => x.IdProyecto == id_proyecto)
                           .Set(x => x.Activo, false)).UpdateAsync() >= 0;

                        resp.Success = res_delete_timesheet_proyecto;
                        resp.Message = res_delete_timesheet_proyecto == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                    }
                }

                var res_timesheet_otros = await (from ts_o in db.tB_Timesheet_Otros
                                                 where ts_o.IdTimeSheet == id_time_sheet
                                                 select ts_o).ToListAsync();

                foreach (var otro in registro["otros"].AsArray())
                {
                    string id_otro = otro["id"].ToString();
                    int dias = Convert.ToInt32(otro["dias"].ToString());
                    int dedicacion = Convert.ToInt32(otro["dedicacion"].ToString());

                    

                    //resp.Success = res_update_timesheet_otro;
                    //resp.Message = res_update_timesheet_otro == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                }
            }

            return resp;
        }
    }
}
