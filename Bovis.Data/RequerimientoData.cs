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
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace Bovis.Data
{
    public class RequerimientoData : RepositoryLinq2DB<ConnectionDB>, IRequerimientoData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public RequerimientoData()
        {
            this.ConfigurationDB = dbConfig;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Habilidades
        public async Task<List<TB_Requerimiento_Habilidad>> GetHabilidades(int idRequerimiento)
        {
            if (idRequerimiento > 0)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from req in db.tB_Requerimiento_Habilidades
                                                                          where req.IdRequerimiento == idRequerimiento
                                                                          select req).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Requerimiento_Habilidad>();
        }
        #endregion #Habilidades

        #region Expereincias
        public async Task<List<TB_Requerimiento_Experiencia>> GetExperiencias(int idRequerimiento)
        {
            if (idRequerimiento > 0)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from req in db.tB_Requerimiento_Experiencias
                                                                          where req.IdRequerimiento == idRequerimiento
                                                                          select req).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Requerimiento_Experiencia>();
        }
        #endregion Experiencias

        #region Registros
        public async Task<List<Requerimiento_Detalle>> GetRequerimientos(bool? Asignados)
        {
            List<Requerimiento_Detalle> requerimientos = new List<Requerimiento_Detalle>();
            Requerimiento_Detalle requerimiento = new Requerimiento_Detalle();
            if (Asignados.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig))
                {
                    var res_requerimientos = await (from req in db.tB_Requerimientos
                                                    join cat in db.tB_Cat_Categorias on req.IdCategoria equals cat.IdCategoria
                                                    join pue in db.tB_Cat_Puestos on req.IdPuesto equals pue.IdPuesto
                                                    join niv in db.tB_Cat_NivelEstudios on req.IdNivelEstudios equals niv.IdNivelEstudios
                                                    join prof in db.tB_Cat_Profesiones on req.IdProfesion equals prof.IdProfesion
                                                    join jor in db.tB_Cat_Jornadas on req.IdJornada equals jor.IdJornada
                                                    where (Asignados == false) ? req.NumEmpleadoRrHh == null : req.NumEmpleadoRrHh != null
                                                    && req.Activo == true
                                                    orderby req.IdRequerimiento descending
                                                    select new Requerimiento_Detalle
                                                    {
                                                        nukidrequerimiento = req.IdRequerimiento,
                                                        nukidcategoria = req.IdCategoria,
                                                        chcategoria = cat.Categoria,
                                                        nukidpuesto = req.IdPuesto,
                                                        chpuesto = pue.Puesto,
                                                        nukidnivel_estudios = req.IdNivelEstudios,
                                                        chnivel_estudios = niv.NivelEstudios,
                                                        nukidprofesion = req.IdProfesion,
                                                        chprofesion = prof.Profesion,
                                                        nukidjornada = req.IdJornada,
                                                        chjornada = jor.Jornada,
                                                        nusueldo_min = req.SueldoMin,
                                                        nusueldo_max = req.SueldoMax,
                                                        boactivo = req.Activo
                                                    }).ToListAsync();

                    foreach (var req in res_requerimientos)
                    {
                        var res_experiencias = await (from exp in db.tB_Requerimiento_Experiencias
                                                      where exp.IdRequerimiento == req.nukidrequerimiento
                                                      && exp.Activo == true
                                                      select exp).ToListAsync();

                        var res_habilidades = await (from hab in db.tB_Requerimiento_Habilidades
                                                     where hab.IdRequerimiento == req.nukidrequerimiento
                                                     && hab.Activo == true
                                                     select hab).ToListAsync();

                        requerimiento = new Requerimiento_Detalle();
                        requerimiento.nukidrequerimiento = req.nukidrequerimiento;
                        requerimiento.nukidcategoria = req.nukidcategoria;
                        requerimiento.chcategoria = req.chcategoria;
                        requerimiento.nukidpuesto = req.nukidpuesto;
                        requerimiento.chpuesto = req.chpuesto;
                        requerimiento.nukidnivel_estudios = req.nukidnivel_estudios;
                        requerimiento.chnivel_estudios = req.chnivel_estudios;
                        requerimiento.nukidprofesion = req.nukidprofesion;
                        requerimiento.chprofesion = req.chprofesion;
                        requerimiento.nukidjornada = req.nukidjornada;
                        requerimiento.chjornada = req.chjornada;
                        requerimiento.nusueldo_min = req.nusueldo_min;
                        requerimiento.nusueldo_max = req.nusueldo_max;
                        requerimiento.experiencias = res_experiencias;
                        requerimiento.habilidades = res_habilidades;
                        requerimiento.boactivo = req.boactivo;

                        requerimientos.Add(requerimiento);
                    }
                }

                return requerimientos;
            }
            else return await GetAllFromEntityAsync<Requerimiento_Detalle>();
        }

        public async Task<Requerimiento_Detalle> GetRequerimiento(int idRequerimiento)
        {
            Requerimiento_Detalle requerimiento = new Requerimiento_Detalle();

            using (var db = new ConnectionDB(dbConfig))
            {
#pragma warning disable CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL
                requerimiento = await (from req in db.tB_Requerimientos
                                       join cat in db.tB_Cat_Categorias on req.IdCategoria equals cat.IdCategoria into catGroup
                                       from cat in catGroup.DefaultIfEmpty()
                                       join pue in db.tB_Cat_Puestos on req.IdPuesto equals pue.IdPuesto into pueGroup
                                       from pue in pueGroup.DefaultIfEmpty()
                                       join niv in db.tB_Cat_NivelEstudios on req.IdNivelEstudios equals niv.IdNivelEstudios into nivGroup
                                       from niv in nivGroup.DefaultIfEmpty()
                                       join prof in db.tB_Cat_Profesiones on req.IdProfesion equals prof.IdProfesion into profGroup
                                       from prof in profGroup.DefaultIfEmpty()
                                       join jor in db.tB_Cat_Jornadas on req.IdJornada equals jor.IdJornada into jorGroup
                                       from jor in jorGroup.DefaultIfEmpty()
                                       where req.IdRequerimiento == idRequerimiento                                       
                                       select new Requerimiento_Detalle
                                       {
                                           nukidrequerimiento = req.IdRequerimiento,
                                           nukidcategoria = req.IdCategoria,
                                           chcategoria = cat != null ? cat.Categoria : null,
                                           nukidpuesto = req.IdPuesto,
                                           chpuesto = pue != null ? pue.Puesto : null,
                                           nukidnivel_estudios = req.IdNivelEstudios,
                                           chnivel_estudios = niv != null ? niv.NivelEstudios : null,
                                           nukidprofesion = req.IdProfesion,
                                           chprofesion = prof != null ? prof.Profesion : null,
                                           nukidjornada = req.IdJornada,
                                           chjornada = jor != null ? jor.Jornada : null,
                                           nusueldo_min = req.SueldoMin,
                                           nusueldo_max = req.SueldoMax
                                       }).FirstOrDefaultAsync();
#pragma warning restore CS8600 // Se va a convertir un literal nulo o un posible valor nulo en un tipo que no acepta valores NULL

                requerimiento.experiencias = await (from exp in db.tB_Requerimiento_Experiencias
                                                    where exp.IdRequerimiento == idRequerimiento
                                                    && exp.Activo == true
                                                    select exp).ToListAsync();

                requerimiento.habilidades = await (from hab in db.tB_Requerimiento_Habilidades
                                                   where hab.IdRequerimiento == idRequerimiento
                                                   && hab.Activo == true
                                                   select hab).ToListAsync();

            }
            return requerimiento;
        }

        public async Task<(bool existe, string mensaje)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_categoria = Convert.ToInt32(registro["categoria"].ToString());
            int id_puesto = Convert.ToInt32(registro["puesto"].ToString());
            int id_nivel_estudios = Convert.ToInt32(registro["nivelEstudios"].ToString());
            int id_profesion = Convert.ToInt32(registro["profesion"].ToString());
            int id_jornada = Convert.ToInt32(registro["jornada"].ToString());
            int sueldo_min = Convert.ToInt32(registro["sueldoMin"].ToString());
            int sueldo_max = Convert.ToInt32(registro["sueldoMax"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                int last_inserted_id = 0;

                var insert_requerimiento = await db.tB_Requerimientos
                    .Value(x => x.IdCategoria, id_categoria)
                    .Value(x => x.IdPuesto, id_puesto)
                    .Value(x => x.IdNivelEstudios, id_nivel_estudios)
                    .Value(x => x.IdProfesion, id_profesion)
                    .Value(x => x.IdJornada, id_jornada)
                    .Value(x => x.SueldoMin, sueldo_min)
                    .Value(x => x.SueldoMax, sueldo_max)
                    .Value(x => x.Activo, true)
                    .InsertAsync() > 0;
                
                resp.Success = insert_requerimiento;
                resp.Message = insert_requerimiento == default ? "Ocurrio un error al agregar registro del requerimiento." : string.Empty;

                if (insert_requerimiento != null)
                {
                    var lastInsertedRecord = db.tB_Requerimientos.OrderByDescending(x => x.IdRequerimiento).FirstOrDefault();
                    last_inserted_id = lastInsertedRecord.IdRequerimiento;
                }

                foreach (var habilidad in registro["habilidades"].AsArray())
                {
                    int id_habilidad = Convert.ToInt32(habilidad.ToString());

                    var insert_habilidad = await db.tB_Requerimiento_Habilidades
                        .Value(x => x.IdRequerimiento, last_inserted_id)
                        .Value(x => x.IdHabilidad, id_habilidad)
                        .Value(x => x.Activo, true)
                        .InsertAsync() > 0;

                    resp.Success = insert_habilidad;
                    resp.Message = insert_habilidad == default ? "Ocurrio un error al agregar registro de la habilidad." : string.Empty;
                }

                foreach (var experiencia in registro["experiencias"].AsArray())
                {
                    int id_experiencia = Convert.ToInt32(experiencia.ToString());

                    var insert_experiencia = await db.tB_Requerimiento_Experiencias
                        .Value(x => x.IdRequerimiento, last_inserted_id)
                        .Value(x => x.IdExperiencia, id_experiencia)
                        .Value(x => x.Activo, true)
                        .InsertAsync() > 0;

                    resp.Success = insert_experiencia;
                    resp.Message = insert_experiencia == default ? "Ocurrio un error al agregar registro de la experiencia." : string.Empty;
                }
            }
            return resp;
        }

        public async Task<(bool existe, string mensaje)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_requerimiento = Convert.ToInt32(registro["id_requerimiento"].ToString());
            int id_categoria = Convert.ToInt32(registro["categoria"].ToString());
            int id_puesto = Convert.ToInt32(registro["puesto"].ToString());
            int id_nivel_estudios = Convert.ToInt32(registro["nivelEstudios"].ToString());
            int id_profesion = Convert.ToInt32(registro["profesion"].ToString());
            int id_jornada = Convert.ToInt32(registro["jornada"].ToString());
            int sueldo_min = Convert.ToInt32(registro["sueldoMin"].ToString());
            int sueldo_max = Convert.ToInt32(registro["sueldoMax"].ToString());
            int index = 0;

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_requerimiento = await db.tB_Requerimientos.Where(x => x.IdRequerimiento == id_requerimiento)
                    .UpdateAsync(x => new TB_Requerimiento
                    {
                        IdCategoria = id_categoria,
                        IdPuesto = id_puesto,
                        IdNivelEstudios = id_nivel_estudios,
                        IdProfesion = id_profesion,
                        IdJornada = id_jornada,
                        SueldoMin = sueldo_min,
                        SueldoMax = sueldo_max
                    }) > 0;

                resp.Success = res_update_requerimiento;
                resp.Message = res_update_requerimiento == default ? "Ocurrio un error al actualizar registro." : string.Empty;

                var res_requerimiento_habilidades = await (from req_hab in db.tB_Requerimiento_Habilidades
                                                     where req_hab.IdRequerimiento == id_requerimiento
                                                     select req_hab).ToListAsync();

                int[] ids_habilidades_db = new int[res_requerimiento_habilidades.Count()];
                index = 0;
                foreach (var r in res_requerimiento_habilidades)
                {
                    ids_habilidades_db[index] = r.IdHabilidad;
                    index++;
                }
                int[] ids_habilidades_request = new int[registro["habilidades"].AsArray().Count()];
                index = 0;
                foreach (var r in registro["habilidades"].AsArray())
                {
                    ids_habilidades_request[index] = Convert.ToInt32(r.ToString());
                    index++;
                }
                HashSet<int> ids_habilidades = new HashSet<int>(ids_habilidades_db.Concat(ids_habilidades_request));

                foreach (int id in ids_habilidades)
                {                    
                    if (ids_habilidades_db.Contains(id))
                    {
                        if (ids_habilidades_request.Contains(id))
                        {
                            // Se actualiza
                            var res_update_requerimiento_habilidad = await db.tB_Requerimiento_Habilidades.Where(x => x.IdHabilidad == id && x.IdRequerimiento == id_requerimiento)
                                .UpdateAsync(x => new TB_Requerimiento_Habilidad
                                {
                                    IdHabilidad = id,
                                    Activo = true
                                }) > 0;

                            resp.Success = res_update_requerimiento_habilidad;
                            resp.Message = res_update_requerimiento_habilidad == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                        }
                        else
                        {
                            // Se elimina
                            var res_delete_requerimiento_habilidad = await (db.tB_Requerimiento_Habilidades
                               .Where(x => x.IdHabilidad == id && x.IdRequerimiento == id_requerimiento)
                               .Set(x => x.Activo, false))
                               .UpdateAsync() >= 0;

                            resp.Success = res_delete_requerimiento_habilidad;
                            resp.Message = res_delete_requerimiento_habilidad == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                        }
                    }
                    else
                    {
                        // Se agrega
                        var res_insert_requerimiento_habilidad = await db.tB_Requerimiento_Habilidades
                        .Value(x => x.IdRequerimiento, id_requerimiento)
                        .Value(x => x.IdHabilidad, id)
                        .Value(x => x.Activo, true)
                        .InsertAsync() > 0;

                        resp.Success = res_insert_requerimiento_habilidad;
                        resp.Message = res_insert_requerimiento_habilidad == default ? "Ocurrio un error al agregar registro." : string.Empty;
                    }
                    Console.WriteLine();
                }


                var res_requerimiento_experiencias = await (from req_exp in db.tB_Requerimiento_Experiencias
                                                           where req_exp.IdRequerimiento == id_requerimiento
                                                           select req_exp)
                                                           .ToListAsync();

                int[] ids_experiencias_db = new int[res_requerimiento_experiencias.Count()];
                index = 0;
                foreach (var r in res_requerimiento_experiencias)
                {
                    ids_experiencias_db[index] = r.IdExperiencia;
                    index++;
                }
                int[] ids_experiencias_request = new int[registro["experiencias"].AsArray().Count()];
                index = 0;
                foreach (var r in registro["experiencias"].AsArray())
                {
                    ids_experiencias_request[index] = Convert.ToInt32(r.ToString());
                    index++;
                }
                HashSet<int> ids_experiencias = new HashSet<int>(ids_experiencias_db.Concat(ids_experiencias_request));

                foreach (int id in ids_experiencias)
                {
                    if (ids_experiencias_db.Contains(id))
                    {
                        if (ids_experiencias_request.Contains(id))
                        {
                            // Se actualiza
                            var res_update_requerimiento_experiencia = await db.tB_Requerimiento_Experiencias.Where(x => x.IdExperiencia == id && x.IdRequerimiento == id_requerimiento)
                                .UpdateAsync(x => new TB_Requerimiento_Experiencia
                                {
                                    IdExperiencia = id,
                                    Activo = true
                                }) > 0;

                            resp.Success = res_update_requerimiento_experiencia;
                            resp.Message = res_update_requerimiento_experiencia == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                        }
                        else
                        {
                            // Se elimina
                            var res_delete_requerimiento_experiencia = await (db.tB_Requerimiento_Experiencias
                               .Where(x => x.IdExperiencia == id && x.IdRequerimiento == id_requerimiento)
                               .Set(x => x.Activo, false))
                               .UpdateAsync() >= 0;

                            resp.Success = res_delete_requerimiento_experiencia;
                            resp.Message = res_delete_requerimiento_experiencia == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                        }
                    }
                    else
                    {
                        // Se agrega
                        var res_insert_requerimiento_experiencia = await db.tB_Requerimiento_Experiencias
                        .Value(x => x.IdRequerimiento, id_requerimiento)
                        .Value(x => x.IdExperiencia, id)
                        .Value(x => x.Activo, true)
                        .InsertAsync() > 0;

                        resp.Success = res_insert_requerimiento_experiencia;
                        resp.Message = res_insert_requerimiento_experiencia == default ? "Ocurrio un error al agregar registro." : string.Empty;
                    }
                    Console.WriteLine();
                }
            }

            return resp;
        }

        public async Task<(bool existe, string mensaje)> DeleteRequerimiento(int idRequerimiento)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_timesheet = await db.tB_Requerimientos.Where(x => x.IdRequerimiento == idRequerimiento)
                                .UpdateAsync(x => new TB_Requerimiento
                                {
                                    Activo = false
                                }) > 0;

                resp.Success = res_update_timesheet;
                resp.Message = res_update_timesheet == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        #endregion Registros

        #region Director Ejecutivo
        public async Task<List<Empleado_Detalle>> GetDirectoresEjecutivos()
        {
            using (var db = new ConnectionDB(dbConfig)) return await (from emp in db.tB_Empleados
                                                                      join per in db.tB_Personas on emp.IdPersona equals per.IdPersona into perJoin
                                                                      from perItem in perJoin.DefaultIfEmpty()
                                                                      join tipo_emp in db.tB_Cat_TipoEmpleados on emp.IdTipoEmpleado equals tipo_emp.IdTipoEmpleado into tipo_empJoin
                                                                      from tipo_empItem in tipo_empJoin.DefaultIfEmpty()
                                                                      join cat in db.tB_Cat_Categorias on emp.IdCategoria equals cat.IdCategoria into catJoin
                                                                      from catItem in catJoin.DefaultIfEmpty()
                                                                      join contrato in db.tB_Cat_TipoContratos on emp.IdTipoContrato equals contrato.IdTipoContrato into contratoJoin
                                                                      from contratoItem in contratoJoin.DefaultIfEmpty()
                                                                      join empresa in db.tB_Empresas on emp.IdEmpresa equals empresa.IdEmpresa into empresaJoin
                                                                      from empresaItem in empresaJoin.DefaultIfEmpty()
                                                                      join ciudad in db.tB_Ciudads on emp.IdCiudad equals ciudad.IdCiudad into ciudadJoin
                                                                      from ciudadItem in ciudadJoin.DefaultIfEmpty()
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
                                                                      join puesto in db.tB_Cat_Puestos on emp.CvePuesto equals puesto.IdPuesto into puestoJoin
                                                                      from puestoItem in puestoJoin.DefaultIfEmpty()
                                                                      where puestoItem.Puesto == "Director Ejecutivo"
                                                                      && emp.Activo == true
                                                                      orderby emp.NumEmpleadoRrHh descending
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
                                                                          chtipo_contrato = contratoItem != null ? contratoItem.Contrato : string.Empty,
                                                                          chcve_puesto = emp.CvePuesto,
                                                                          nukidempresa = emp.IdEmpresa,
                                                                          chempresa = empresaItem != null ? empresaItem.Empresa : string.Empty,
                                                                          nukidciudad = emp.IdCiudad,
                                                                          chciudad = ciudadItem != null ? ciudadItem.Ciudad : string.Empty,
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
                                                                          bodescuento_pension = emp.DescuentoPension,
                                                                          nuporcentaje_pension = emp.PorcentajePension,
                                                                          nufondo_fijo = emp.FondoFijo,
                                                                          nucredito_infonavit = emp.CreditoInfonavit,
                                                                          chtipo_descuento = emp.TipoDescuento,
                                                                          nuvalor_descuento = emp.ValorDescuento,
                                                                          nuno_empleado_noi = emp.NoEmpleadoNoi,
                                                                          chrol = emp.Rol
                                                                      }).ToListAsync();

        }
        #endregion Director Ejecutivo

        #region Proyectos
        public async Task<List<TB_EmpleadoProyecto>> GetProyectosByDirectorEjecutivo(int IdDirectorEjecutivo)
        {
            List<TB_EmpleadoProyecto> list = null;
            using (var db = new ConnectionDB(dbConfig))
            {
                list = await (from proyectos in db.tB_EmpleadoProyectos
                              where proyectos.NumEmpleadoRrHh == IdDirectorEjecutivo
                              select proyectos).ToListAsync();
            }

            return list;
        }
        #endregion Proyectos
    }
}
