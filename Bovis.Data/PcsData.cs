using Azure;
using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System.Text.Json.Nodes;

namespace Bovis.Data
{
    public class PcsData : RepositoryLinq2DB<ConnectionDB>, IPcsData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public PcsData()
        {
            this.ConfigurationDB = dbConfig;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base


        #region Clientes
        public async Task<List<TB_Cliente>> GetClientes()
        {
            //return await GetAllFromEntityAsync<TB_Cliente>();

            using (var db = new ConnectionDB(dbConfig))
            {
                var resp = await (from p in db.tB_Clientes
                                  orderby p.Cliente ascending
                                  select p).ToListAsync();

                return resp;
            }
        }
        #endregion Clientes

        #region Empresas
        public async Task<List<TB_Empresa>> GetEmpresas()
        {
            //return await GetAllFromEntityAsync<TB_Empresa>();

            using (var db = new ConnectionDB(dbConfig))
            {
                var resp = await (from p in db.tB_Empresas
                                  orderby p.Empresa ascending
                                  select p).ToListAsync();

                return resp;
            }
        }
        #endregion Empresas

        #region Proyectos
        public async Task<List<TB_Proyecto>> GetProyectos(bool? OrdenAlfabetico)
        {
            //return await GetAllFromEntityAsync<TB_Proyecto>();
            using (var db = new ConnectionDB(dbConfig))
            {
                var query = from p in db.tB_Proyectos select p;
                query = OrdenAlfabetico == true || OrdenAlfabetico == null ? query.OrderBy(p => p.Proyecto) : query.OrderBy(p => p.NumProyecto);

                var resp = await query.ToListAsync();

                return resp;
            }
        }

        public async Task<TB_Proyecto> GetProyecto(int numProyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var resp = await (from p in db.tB_Proyectos
                                  where p.NumProyecto == numProyecto
                                  select p).FirstOrDefaultAsync();

                return resp;
            }
        }

        public async Task<(bool Success, string Message)> AddProyecto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
            string nombre_proyecto = registro["nombre_proyecto"].ToString();
            string alcance = registro["alcance"].ToString();
            string? codigo_postal = registro["codigo_postal"] != null ? registro["codigo_postal"].ToString() : null;
            string? ciudad = registro["ciudad"] != null ? registro["ciudad"].ToString() : null;
            int? id_pais = registro["id_pais"] != null ? Convert.ToInt32(registro["id_pais"].ToString()) : null;
            int? id_estatus = registro["id_estatus"] != null ? Convert.ToInt32(registro["id_estatus"].ToString()) : null;
            int? id_sector = registro["id_sector"] != null ? Convert.ToInt32(registro["id_sector"].ToString()) : null;
            int? id_tipo_proyecto = registro["id_tipo_proyecto"] != null ? Convert.ToInt32(registro["id_tipo_proyecto"].ToString()) : null;
            int? id_responsable_preconstruccion = registro["id_responsable_preconstruccion"] != null ? Convert.ToInt32(registro["id_responsable_preconstruccion"].ToString()) : null;
            int? id_responsable_construccion = registro["id_responsable_construccion"] != null ? Convert.ToInt32(registro["id_responsable_construccion"].ToString()) : null;
            int? id_responsable_ehs = registro["id_responsable_ehs"] != null ? Convert.ToInt32(registro["id_responsable_ehs"].ToString()) : null;
            int? id_responsable_supervisor = registro["id_responsable_supervisor"] != null ? Convert.ToInt32(registro["id_responsable_supervisor"].ToString()) : null;
            int? id_cliente = registro["id_cliente"] != null ? Convert.ToInt32(registro["id_cliente"].ToString()) : null;
            int? id_empresa = registro["id_empresa"] != null ? Convert.ToInt32(registro["id_empresa"].ToString()) : null;
            int? id_director_ejecutivo = registro["id_director_ejecutivo"] != null ? Convert.ToInt32(registro["id_director_ejecutivo"].ToString()) : null;
            decimal? costo_promedio_m2 = registro["costo_promedio_m2"] != null ? Convert.ToDecimal(registro["costo_promedio_m2"].ToString()) : null;
            DateTime fecha_inicio = Convert.ToDateTime(registro["fecha_inicio"].ToString());
            DateTime? fecha_fin = registro["fecha_fin"] != null ? Convert.ToDateTime(registro["fecha_fin"].ToString()) : null;
            string nombre_contacto = registro["contacto"]["nombre"].ToString();
            string posicion_contacto = registro["contacto"]["posicion"].ToString();
            string telefono_contacto = registro["contacto"]["telefono"].ToString();
            string correo_contacto = registro["contacto"]["correo"].ToString();

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_insert_contacto = await db.tB_Contactos
                    .Value(x => x.NumProyecto, num_proyecto)
                    .Value(x => x.Nombre, nombre_contacto)
                    .Value(x => x.Posicion, posicion_contacto)
                    .Value(x => x.Telefono, telefono_contacto)
                    .Value(x => x.Correo, correo_contacto)
                    .InsertAsync() > 0;

                resp.Success = res_insert_contacto;
                resp.Message = res_insert_contacto == default ? "Ocurrio un error al insertar registro." : string.Empty;

                var res_insert_proyecto = await db.tB_Proyectos
                    .Value(x => x.NumProyecto, num_proyecto)
                    .Value(x => x.Proyecto, nombre_proyecto)
                    .Value(x => x.Alcance, alcance)
                    .Value(x => x.Cp, codigo_postal)
                    .Value(x => x.Ciudad, ciudad)
                    .Value(x => x.IdPais, id_pais)
                    .Value(x => x.IdEstatus, id_estatus)
                    .Value(x => x.IdSector, id_sector)
                    .Value(x => x.IdTipoProyecto, id_tipo_proyecto)
                    .Value(x => x.IdResponsablePreconstruccion, id_responsable_preconstruccion)
                    .Value(x => x.IdResponsableConstruccion, id_responsable_construccion)
                    .Value(x => x.IdResponsableEhs, id_responsable_ehs)
                    .Value(x => x.IdResponsableSupervisor, id_responsable_supervisor)
                    .Value(x => x.IdCliente, id_cliente)
                    .Value(x => x.IdEmpresa, id_empresa)
                    .Value(x => x.IdDirectorEjecutivo, id_director_ejecutivo)
                    .Value(x => x.CostoPromedioM2, costo_promedio_m2)
                    .Value(x => x.FechaIni, fecha_inicio)
                    .Value(x => x.FechaFin, fecha_fin)
                    .InsertAsync() > 0;

                resp.Success = res_insert_proyecto;
                resp.Message = res_insert_proyecto == default ? "Ocurrio un error al insertar registro." : string.Empty;

                // Se agregan las secciones y rubros para gastos e ingresos.
                var secciones = await (from secc in db.tB_GastoIngresoSeccions
                                       where secc.Activo == true
                                       select secc).ToListAsync();

                foreach (var seccion in secciones)
                {
                    var rubros = await (from rub in db.tB_CatRubros
                                        where rub.IdSeccion == seccion.IdSeccion
                                        select rub).ToListAsync();

                    foreach (var rubro in rubros)
                    {
                        var res_insert_rubro = await db.tB_Rubros
                                        .Value(x => x.IdSeccion, seccion.IdSeccion)
                                        .Value(x => x.IdRubro, rubro.IdRubro)
                                        .Value(x => x.NumProyecto, num_proyecto)
                                        .Value(x => x.Activo, true)
                                        .InsertAsync() > 0;

                        resp.Success = res_insert_rubro;
                        resp.Message = res_insert_rubro == default ? "Ocurrio un error al insertar registro." : string.Empty;
                    }                    
                }
            }

            return resp;
        }
        public async Task<List<Proyecto_Detalle>> GetProyectos(int IdProyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var proyectos = new List<Proyecto_Detalle>();

                proyectos = await (from proy in db.tB_Proyectos
                                   join pais in db.tB_Pais on proy.IdPais equals pais.IdPais into paisJoin
                                   from paisItem in paisJoin.DefaultIfEmpty()
                                   join estatus in db.tB_Cat_EstatusProyectos on proy.IdEstatus equals estatus.IdEstatus into estatusJoin
                                   from estatusItem in estatusJoin.DefaultIfEmpty()
                                   join sector in db.tB_Cat_Sectors on proy.IdSector equals sector.IdSector into sectorJoin
                                   from sectorItem in sectorJoin.DefaultIfEmpty()
                                   join tipo_proy in db.tB_Cat_TipoProyectos on proy.IdTipoProyecto equals tipo_proy.IdTipoProyecto into tipo_proyJoin
                                   from tipo_proyItem in tipo_proyJoin.DefaultIfEmpty()

                                   join empleado_resp_pre in db.tB_Empleados on proy.IdResponsablePreconstruccion equals empleado_resp_pre.NumEmpleadoRrHh into empleado_resp_preJoin
                                   from empleado_resp_preItem in empleado_resp_preJoin.DefaultIfEmpty()
                                   join persona_resp_pre in db.tB_Personas on empleado_resp_preItem.IdPersona equals persona_resp_pre.IdPersona into persona_resp_preJoin
                                   from persona_resp_preItem in persona_resp_preJoin.DefaultIfEmpty()

                                   join empleado_resp_cons in db.tB_Empleados on proy.IdResponsableConstruccion equals empleado_resp_cons.NumEmpleadoRrHh into empleado_resp_consJoin
                                   from empleado_resp_consItem in empleado_resp_consJoin.DefaultIfEmpty()
                                   join persona_resp_cons in db.tB_Personas on empleado_resp_consItem.IdPersona equals persona_resp_cons.IdPersona into persona_resp_consJoin
                                   from persona_resp_consItem in persona_resp_consJoin.DefaultIfEmpty()

                                   join empleado_resp_ehs in db.tB_Empleados on proy.IdResponsableEhs equals empleado_resp_ehs.NumEmpleadoRrHh into empleado_resp_ehsJoin
                                   from empleado_resp_ehsItem in empleado_resp_ehsJoin.DefaultIfEmpty()
                                   join persona_resp_ehs in db.tB_Personas on empleado_resp_ehsItem.IdPersona equals persona_resp_ehs.IdPersona into persona_resp_ehsJoin
                                   from persona_resp_ehsItem in persona_resp_ehsJoin.DefaultIfEmpty()

                                   join empleado_resp_sup in db.tB_Empleados on proy.IdResponsableSupervisor equals empleado_resp_sup.NumEmpleadoRrHh into empleado_resp_supJoin
                                   from empleado_resp_supItem in empleado_resp_supJoin.DefaultIfEmpty()
                                   join persona_resp_sup in db.tB_Personas on empleado_resp_supItem.IdPersona equals persona_resp_sup.IdPersona into persona_resp_supJoin
                                   from persona_resp_supItem in persona_resp_supJoin.DefaultIfEmpty()

                                   join cliente in db.tB_Clientes on proy.IdCliente equals cliente.IdCliente into clienteJoin
                                   from clienteItem in clienteJoin.DefaultIfEmpty()

                                   join empresa in db.tB_Empresas on proy.IdEmpresa equals empresa.IdEmpresa into empresaJoin
                                   from empresaItem in empresaJoin.DefaultIfEmpty()

                                   join empleado_director in db.tB_Empleados on proy.IdDirectorEjecutivo equals empleado_director.NumEmpleadoRrHh into empleado_directorJoin
                                   from empleado_directorItem in empleado_directorJoin.DefaultIfEmpty()
                                   join persona_director in db.tB_Personas on empleado_directorItem.IdPersona equals persona_director.IdPersona into persona_directorJoin
                                   from persona_directorItem in persona_directorJoin.DefaultIfEmpty()
                                   where (IdProyecto == 0 || proy.NumProyecto == IdProyecto)
                                   orderby proy.Proyecto ascending
                                   select new Proyecto_Detalle
                                   {
                                       nunum_proyecto = proy.NumProyecto,
                                       chproyecto = proy.Proyecto,
                                       chalcance = proy.Alcance,
                                       chcp = proy.Cp,
                                       chciudad = proy.Ciudad,
                                       nukidpais = proy.IdPais,
                                       chpais = paisItem.Pais ?? null,
                                       nukidestatus = proy.IdEstatus,
                                       chestatus = estatusItem.Estatus ?? null,
                                       nukidsector = proy.IdSector,
                                       chsector = sectorItem.Sector ?? null,
                                       nukidtipo_proyecto = proy.IdTipoProyecto,
                                       chtipo_proyecto = tipo_proyItem.TipoProyecto ?? null,
                                       nukidresponsable_preconstruccion = proy.IdResponsablePreconstruccion,
                                       chresponsable_preconstruccion = persona_resp_preItem != null ? persona_resp_preItem.Nombre + " " + persona_resp_preItem.ApPaterno + " " + persona_resp_preItem.ApMaterno : null,
                                       nukidresponsable_construccion = proy.IdResponsableConstruccion,
                                       chresponsable_construccion = persona_resp_consItem != null ? persona_resp_consItem.Nombre + " " + persona_resp_consItem.ApPaterno + " " + persona_resp_consItem.ApMaterno : null,
                                       nukidresponsable_ehs = proy.IdResponsableEhs,
                                       chresponsable_ehs = persona_resp_ehsItem != null ? persona_resp_ehsItem.Nombre + " " + persona_resp_ehsItem.ApPaterno + " " + persona_resp_ehsItem.ApMaterno : null,
                                       nukidresponsable_supervisor = proy.IdResponsableSupervisor,
                                       chresponsable_supervisor = persona_resp_supItem != null ? persona_resp_supItem.Nombre + " " + persona_resp_supItem.ApPaterno + " " + persona_resp_supItem.ApMaterno : null,
                                       nukidcliente = proy.IdCliente,
                                       chcliente = clienteItem.Cliente ?? null,
                                       nukidempresa = proy.IdEmpresa,
                                       chempresa = empresaItem.Empresa ?? null,
                                       nukiddirector_ejecutivo = proy.IdDirectorEjecutivo,
                                       chdirector_ejecutivo = persona_directorItem != null ? persona_directorItem.Nombre + " " + persona_directorItem.ApPaterno + " " + persona_resp_supItem.ApMaterno : null,
                                       nucosto_promedio_m2 = proy.CostoPromedioM2,
                                       dtfecha_ini = proy.FechaIni,
                                       dtfecha_fin = proy.FechaFin
                                   }).ToListAsync();

                return proyectos;
            }
        }
        public async Task<List<Tipo_Proyecto>> GetTipoProyectos()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var tipo_proyectos = await (from tipo in db.tB_Cat_TipoProyectos
                                            where tipo.Activo == true
                                            orderby tipo.TipoProyecto ascending
                                            select new Tipo_Proyecto
                                            {
                                                IdTipoProyecto = tipo.IdTipoProyecto,
                                                TipoProyecto = tipo.TipoProyecto
                                            }).ToListAsync();

                return tipo_proyectos;
            }
        }
        public async Task<(bool Success, string Message)> UpdateProyecto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
            string nombre_proyecto = registro["nombre_proyecto"].ToString();
            string alcance = registro["alcance"].ToString();
            string? codigo_postal = registro["codigo_postal"] != null ? registro["codigo_postal"].ToString() : null;
            string? ciudad = registro["ciudad"] != null ? registro["ciudad"].ToString() : null;
            int? id_pais = registro["id_pais"] != null ? Convert.ToInt32(registro["id_pais"].ToString()) : null;
            int? id_estatus = registro["id_estatus"] != null ? Convert.ToInt32(registro["id_estatus"].ToString()) : null;
            int? id_sector = registro["id_sector"] != null ? Convert.ToInt32(registro["id_sector"].ToString()) : null;
            int? id_tipo_proyecto = registro["id_tipo_proyecto"] != null ? Convert.ToInt32(registro["id_tipo_proyecto"].ToString()) : null;
            int? id_responsable_preconstruccion = registro["id_responsable_preconstruccion"] != null ? Convert.ToInt32(registro["id_responsable_preconstruccion"].ToString()) : null;
            int? id_responsable_construccion = registro["id_responsable_construccion"] != null ? Convert.ToInt32(registro["id_responsable_construccion"].ToString()) : null;
            int? id_responsable_ehs = registro["id_responsable_ehs"] != null ? Convert.ToInt32(registro["id_responsable_ehs"].ToString()) : null;
            int? id_responsable_supervisor = registro["id_responsable_supervisor"] != null ? Convert.ToInt32(registro["id_responsable_supervisor"].ToString()) : null;
            int? id_cliente = registro["id_cliente"] != null ? Convert.ToInt32(registro["id_cliente"].ToString()) : null;
            int? id_empresa = registro["id_empresa"] != null ? Convert.ToInt32(registro["id_empresa"].ToString()) : null;
            int? id_director_ejecutivo = registro["id_director_ejecutivo"] != null ? Convert.ToInt32(registro["id_director_ejecutivo"].ToString()) : null;
            decimal? costo_promedio_m2 = registro["costo_promedio_m2"] != null ? Convert.ToDecimal(registro["costo_promedio_m2"].ToString()) : null;
            DateTime fecha_inicio = Convert.ToDateTime(registro["fecha_inicio"].ToString());
            DateTime? fecha_fin = registro["fecha_fin"] != null ? Convert.ToDateTime(registro["fecha_fin"].ToString()) : null;
            string nombre_contacto = registro["contacto"]["nombre"].ToString();
            string posicion_contacto = registro["contacto"]["posicion"].ToString();
            string telefono_contacto = registro["contacto"]["telefono"].ToString();
            string correo_contacto = registro["contacto"]["correo"].ToString();

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_contacto = await db.tB_Contactos.Where(x => x.NumProyecto == num_proyecto)
                    .UpdateAsync(x => new TB_Contacto
                    {
                        NumProyecto = num_proyecto,
                        Nombre = nombre_contacto,
                        Posicion = posicion_contacto,
                        Telefono = telefono_contacto,
                        Correo = correo_contacto
                    }) > 0;

                resp.Success = res_update_contacto;
                resp.Message = res_update_contacto == default ? "Ocurrio un error al actualizar registro." : string.Empty;

                var res_update_proyecto = await db.tB_Proyectos.Where(x => x.NumProyecto == num_proyecto)
                    .UpdateAsync(x => new TB_Proyecto
                    {
                        Proyecto = nombre_proyecto,
                        Alcance = alcance,
                        Cp = codigo_postal,
                        Ciudad = ciudad,
                        IdPais = id_pais,
                        IdEstatus = id_estatus,
                        IdSector = id_sector,
                        IdTipoProyecto = id_tipo_proyecto,
                        IdResponsablePreconstruccion = id_responsable_preconstruccion,
                        IdResponsableConstruccion = id_responsable_construccion,
                        IdResponsableEhs = id_responsable_ehs,
                        IdResponsableSupervisor = id_responsable_supervisor,
                        IdCliente = id_cliente,
                        IdEmpresa = id_empresa,
                        IdDirectorEjecutivo = id_director_ejecutivo,
                        CostoPromedioM2 = costo_promedio_m2,
                        FechaIni = fecha_inicio,
                        FechaFin = fecha_fin
                    }) > 0;

                resp.Success = res_update_proyecto;
                resp.Message = res_update_proyecto == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        public async Task<(bool Success, string Message)> DeleteProyecto(int IdProyecto)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_delete_proyecto_contacto = await db.tB_Contactos.Where(x => x.NumProyecto == IdProyecto)
                    .DeleteAsync() > 0;

                resp.Success = res_delete_proyecto_contacto;
                resp.Message = res_delete_proyecto_contacto == default ? "Ocurrio un error al borrar registro." : string.Empty;

                var res_delete_proyecto = await db.tB_Proyectos.Where(x => x.NumProyecto == IdProyecto)
                    .DeleteAsync() > 0;

                resp.Success = res_delete_proyecto;
                resp.Message = res_delete_proyecto == default ? "Ocurrio un error al borrar registro." : string.Empty;
            }

            return resp;
        }
        #endregion Proyectos

        #region Etapas
        // Etapas se guardan en tb_proyecto_fase
        // Los empleados de una etapa, habrá que generarse una nueva tabla de relación.
        public async Task<PCS_Etapa_Detalle> AddEtapa(JsonObject registro)
        {
            PCS_Etapa_Detalle etapa = new PCS_Etapa_Detalle();

            int num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
            int orden = Convert.ToInt32(registro["orden"].ToString());
            string nombre_fase = registro["nombre_fase"].ToString();
            DateTime fecha_inicio = Convert.ToDateTime(registro["fecha_inicio"].ToString());
            DateTime fecha_fin = Convert.ToDateTime(registro["fecha_fin"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                var id_etapa = await db.tB_ProyectoFases
                                        .Value(x => x.NumProyecto, num_proyecto)
                                        .Value(x => x.Orden, orden)
                                        .Value(x => x.Fase, nombre_fase)
                                        .Value(x => x.FechaIni, fecha_inicio)
                                        .Value(x => x.FechaFin, fecha_fin)
                                        .InsertWithIdentityAsync();

                etapa.IdFase = Convert.ToInt32(id_etapa);
                etapa.Fase = nombre_fase;
                etapa.Orden = orden;
                etapa.FechaIni = fecha_inicio;
                etapa.FechaFin = fecha_fin;                
            }

            return etapa;
        }
        public async Task<PCS_Proyecto_Detalle> GetEtapas(int IdProyecto)
        {
            PCS_Proyecto_Detalle proyecto_etapas = new PCS_Proyecto_Detalle();

            using (var db = new ConnectionDB(dbConfig))
            {
                var proyecto = await (from p in db.tB_Proyectos
                                      where p.NumProyecto == IdProyecto
                                      select p).FirstOrDefaultAsync();

                proyecto_etapas.NumProyecto = IdProyecto;
                proyecto_etapas.FechaIni = proyecto?.FechaIni;
                proyecto_etapas.FechaFin = proyecto?.FechaFin;

                var etapas = await (from p in db.tB_ProyectoFases
                                    join proy in db.tB_Proyectos on p.NumProyecto equals proy.NumProyecto into proyJoin
                                    from proyItem in proyJoin.DefaultIfEmpty()
                                    where p.NumProyecto == IdProyecto
                                    orderby p.Fase ascending
                                    select new PCS_Etapa_Detalle
                                    {
                                        IdFase = p.IdFase,
                                        Orden = p.Orden,
                                        Fase = p.Fase,
                                        FechaIni = p.FechaIni,
                                        FechaFin = p.FechaFin
                                    }).ToListAsync();

                proyecto_etapas.Etapas = new List<PCS_Etapa_Detalle>();
                proyecto_etapas.Etapas.AddRange(etapas);

                foreach (var etapa in etapas)
                {
                    var empleados = await (from p in db.tB_ProyectoFaseEmpleados
                                           join e in db.tB_Empleados on p.NumEmpleado equals e.NumEmpleadoRrHh into eJoin
                                           from eItem in eJoin.DefaultIfEmpty()
                                           join per in db.tB_Personas on eItem.IdPersona equals per.IdPersona into perJoin
                                           from perItem in perJoin.DefaultIfEmpty()
                                           where p.IdFase == etapa.IdFase
                                           orderby p.NumEmpleado ascending
                                           group new PCS_Empleado_Detalle
                                           {
                                               Id = p.Id,
                                               IdFase = p.IdFase,
                                               NumempleadoRrHh = p.NumEmpleado,
                                               Empleado = perItem != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : string.Empty
                                           } by new { p.NumEmpleado } into g
                                           select new PCS_Empleado_Detalle
                                           {
                                               Id = g.First().Id,
                                               IdFase = g.First().IdFase,
                                               NumempleadoRrHh = g.Key.NumEmpleado,
                                               Empleado = g.First().Empleado
                                           }).ToListAsync();

                    etapa.Empleados = new List<PCS_Empleado_Detalle>();
                    etapa.Empleados.AddRange(empleados);

                    foreach (var empleado in empleados)
                    {
                        var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                            where p.NumEmpleado == empleado.NumempleadoRrHh
                                            && p.IdFase == etapa.IdFase
                                            select new PCS_Fecha_Detalle
                                            {
                                                Id = p.Id,
                                                Mes = p.Mes,
                                                Anio = p.Anio,
                                                Porcentaje = p.Porcentaje
                                            }).ToListAsync();

                        empleado.Fechas = new List<PCS_Fecha_Detalle>();
                        empleado.Fechas.AddRange(fechas);


                    }
                }

                return proyecto_etapas;
            }
        }
        public async Task<(bool Success, string Message)> UpdateEtapa(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_etapa = Convert.ToInt32(registro["id_etapa"].ToString());
            int num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
            int orden = Convert.ToInt32(registro["orden"].ToString());
            string nombre_fase = registro["nombre_fase"].ToString();
            DateTime fecha_inicio = Convert.ToDateTime(registro["fecha_inicio"].ToString());
            DateTime fecha_fin = Convert.ToDateTime(registro["fecha_fin"].ToString());

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_etapa = await db.tB_ProyectoFases.Where(x => x.IdFase == id_etapa)
                    .UpdateAsync(x => new TB_ProyectoFase
                    {
                        NumProyecto = x.NumProyecto,
                        Orden = x.Orden,
                        Fase = x.Fase,
                        FechaIni = fecha_inicio,
                        FechaFin = fecha_fin
                    }) > 0;

                resp.Success = res_update_etapa;
                resp.Message = res_update_etapa == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        public async Task<(bool Success, string Message)> DeleteEtapa(int IdEtapa)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                // Borrado de empleados de etapa
                var res_delete_empleado = await db.tB_ProyectoFaseEmpleados.Where(x => x.IdFase == IdEtapa)
                    .DeleteAsync() > 0;

                resp.Success = res_delete_empleado;
                resp.Message = res_delete_empleado == default ? "Ocurrio un error al borrar registro." : string.Empty;

                // Borrado de etapá
                var res_delete_etapa = await db.tB_ProyectoFases.Where(x => x.IdFase == IdEtapa)
                    .DeleteAsync() > 0;

                resp.Success = res_delete_etapa;
                resp.Message = res_delete_etapa == default ? "Ocurrio un error al borrar registro." : string.Empty;
            }

            return resp;
        }
        #endregion Etapas

        #region Empleados
        public async Task<(bool Success, string Message)> AddEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_fase = Convert.ToInt32(registro["id_fase"].ToString());
            int num_empleado = Convert.ToInt32(registro["num_empleado"].ToString());          

            using (var db = new ConnectionDB(dbConfig))
            {                
                foreach (var fecha in registro["fechas"].AsArray())
                {
                    int mes = Convert.ToInt32(fecha["mes"].ToString());
                    int anio = Convert.ToInt32(fecha["anio"].ToString());
                    int porcentaje = Convert.ToInt32(fecha["porcentaje"].ToString());

                    var res_insert_empleado = await db.tB_ProyectoFaseEmpleados
                        .Value(x => x.IdFase, id_fase)
                        .Value(x => x.NumEmpleado, num_empleado)
                        .Value(x => x.Mes, mes)
                        .Value(x => x.Anio, anio)
                        .Value(x => x.Porcentaje, porcentaje)
                        .InsertAsync() > 0;

                    resp.Success = res_insert_empleado;
                    resp.Message = res_insert_empleado == default ? "Ocurrio un error al insertar registro." : string.Empty;



                    // Se insertan los valores de los rubros, para gastos e ingresos.
                    var rubros = await (from rub in db.tB_CatRubros
                                        where rub.Activo == true
                                        select rub).ToListAsync();

                    foreach (var rubro in rubros)
                    {
                        var res_insert_rubro_valor = await db.tB_RubroValors
                                        .Value(x => x.IdRubro, rubro.IdRubro)
                                        .Value(x => x.Mes, mes)
                                        .Value(x => x.Anio, anio)
                                        //.Value(x => x.Porcentaje, porcentaje)
                                        .Value(x => x.Activo, true)
                                        .InsertAsync() > 0;

                        resp.Success = res_insert_rubro_valor;
                        resp.Message = res_insert_rubro_valor == default ? "Ocurrio un error al insertar registro." : string.Empty;
                    }
                }
            }

            return resp;
        }
        public async Task<List<PCS_Empleado_Detalle>> GetEmpleados(int IdFase)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var empleados = await (from p in db.tB_ProyectoFaseEmpleados
                                       join e in db.tB_Empleados on p.NumEmpleado equals e.NumEmpleadoRrHh into eJoin
                                       from eItem in eJoin.DefaultIfEmpty()
                                       join per in db.tB_Personas on eItem.IdPersona equals per.IdPersona into perJoin
                                       from perItem in perJoin.DefaultIfEmpty()
                                       where p.IdFase == IdFase
                                       orderby p.NumEmpleado ascending
                                       group new PCS_Empleado_Detalle
                                       {
                                           Id = p.Id,
                                           IdFase = p.IdFase,
                                           NumempleadoRrHh = p.NumEmpleado,
                                           Empleado = perItem != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : string.Empty
                                       } by new { p.NumEmpleado } into g
                                       select new PCS_Empleado_Detalle
                                       {
                                           Id = g.First().Id,
                                           IdFase = g.First().IdFase,
                                           NumempleadoRrHh = g.Key.NumEmpleado,
                                           Empleado = g.First().Empleado
                                       }).ToListAsync();

                foreach (var empleado in empleados)
                {
                    var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                        where p.NumEmpleado == empleado.NumempleadoRrHh
                                        && p.IdFase == IdFase
                                        select new PCS_Fecha_Detalle
                                        {
                                            Id = p.Id,
                                            Mes = p.Mes,
                                            Anio = p.Anio,
                                            Porcentaje = p.Porcentaje
                                        }).ToListAsync();

                    empleado.Fechas = new List<PCS_Fecha_Detalle>();
                    empleado.Fechas.AddRange(fechas);
                }

                return empleados;
            }
        }
        public async Task<(bool Success, string Message)> UpdateEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_fase = Convert.ToInt32(registro["id_fase"].ToString());
            int num_empleado = Convert.ToInt32(registro["num_empleado"].ToString());         

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {                
                var res_delete_empleado = await db.tB_ProyectoFaseEmpleados.Where(x => x.IdFase == id_fase && x.NumEmpleado == num_empleado)
                    .DeleteAsync() > 0;

                foreach (var fecha in registro["fechas"].AsArray())
                {
                    int mes = Convert.ToInt32(fecha["mes"].ToString());
                    int anio = Convert.ToInt32(fecha["anio"].ToString());
                    int porcentaje = Convert.ToInt32(fecha["porcentaje"].ToString());

                    var res_insert_empleado = await db.tB_ProyectoFaseEmpleados
                        .Value(x => x.IdFase, id_fase)
                        .Value(x => x.NumEmpleado, num_empleado)
                        .Value(x => x.Mes, mes)
                        .Value(x => x.Anio, anio)
                        .Value(x => x.Porcentaje, porcentaje)
                        .InsertAsync() > 0;

                    resp.Success = res_insert_empleado;
                    resp.Message = res_insert_empleado == default ? "Ocurrio un error al insertar registro." : string.Empty;
                }
            }

            return resp;
        }
        public async Task<(bool Success, string Message)> DeleteEmpleado(int IdFase, int NumEmpleado)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {                
                var res_delete_empleado = await db.tB_ProyectoFaseEmpleados.Where(x => x.IdFase == IdFase && x.NumEmpleado == NumEmpleado)
                    .DeleteAsync() > 0;

                resp.Success = res_delete_empleado;
                resp.Message = res_delete_empleado == default ? "Ocurrio un error al borrar registro." : string.Empty;
            }

            return resp;
        }
        #endregion Empleados

        #region Gastos / Ingresos
        public async Task<GastosIngresos_Detalle> GetGastosIngresos(int IdProyecto, string Tipo)
        {
            GastosIngresos_Detalle proyecto_gastos_ingresos = new GastosIngresos_Detalle();

            using (var db = new ConnectionDB(dbConfig))
            {
                var proyecto = await (from p in db.tB_Proyectos
                                      where p.NumProyecto == IdProyecto
                                      select p).FirstOrDefaultAsync();

                proyecto_gastos_ingresos.NumProyecto = IdProyecto;
                proyecto_gastos_ingresos.FechaIni = proyecto?.FechaIni;
                proyecto_gastos_ingresos.FechaFin = proyecto?.FechaFin;

                var secciones = await (from seccion in db.tB_GastoIngresoSeccions
                                       where seccion.Tipo == Tipo
                                       orderby seccion.Codigo ascending
                                       select new Seccion_Detalle
                                       {
                                           IdSeccion = seccion.IdSeccion,
                                           Codigo = seccion.Codigo,
                                           Seccion = seccion.Seccion
                                       })
                                      .ToListAsync();

                proyecto_gastos_ingresos.Secciones = new List<Seccion_Detalle>();
                proyecto_gastos_ingresos.Secciones.AddRange(secciones);

                foreach (var seccion in secciones)
                {
                    var rubros = await (from rubro in db.tB_Rubros
                                        join rel1 in db.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
                                        from rel1Item in rel1Join.DefaultIfEmpty()
                                        join rel2 in db.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
                                        where rubro.IdSeccion == seccion.IdSeccion
                                        && rubro.NumProyecto == IdProyecto
                                        && rel2.Tipo == Tipo
                                        select new Rubro_Detalle
                                        {
                                            IdRubro = rubro.IdRubro,
                                            Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
                                            Unidad = rubro.Unidad,
                                            Cantidad = rubro.Cantidad,
                                            Reembolsable = rubro.Reembolsable,
                                            AplicaTodosMeses = rubro.AplicaTodosMeses
                                        }).ToListAsync();

                    seccion.Rubros = new List<Rubro_Detalle>();
                    seccion.Rubros.AddRange(rubros);

                    foreach (var rubro in rubros)
                    {
                        var fechas = await (from valor in db.tB_RubroValors
                                            join cat in db.tB_CatRubros on valor.IdRubro equals cat.IdRubro
                                            join sec in db.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
                                            where valor.IdRubro == rubro.IdRubro
                                            && sec.Tipo == Tipo
                                            select new PCS_Fecha_Detalle
                                            {
                                                Id = valor.Id,
                                                Mes = valor.Mes,
                                                Anio = valor.Anio,
                                                Porcentaje = valor.Porcentaje
                                            }).ToListAsync();

                        rubro.Fechas = new List<PCS_Fecha_Detalle>();
                        rubro.Fechas.AddRange(fechas);
                    }
                }

                return proyecto_gastos_ingresos;
            }
        }

        public async Task<(bool Success, string Message)> UpdateGastosIngresos(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_rubro = Convert.ToInt32(registro["idRubro"].ToString());
            string unidad = registro["unidad"].ToString();
            decimal cantidad = Convert.ToDecimal(registro["cantidad"].ToString());
            bool reembolsable = Convert.ToBoolean(registro["reembolsable"].ToString());
            bool aplica_todos_meses = Convert.ToBoolean(registro["aplicaTodosMeses"].ToString());

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_rubro = await db.tB_Rubros.Where(x => x.Id == id_rubro)
                    .UpdateAsync(x => new TB_Rubro
                    {
                        Unidad = unidad,
                        Cantidad = cantidad,
                        Reembolsable = reembolsable,
                        AplicaTodosMeses = aplica_todos_meses
                    }) > 0;

                resp.Success = res_update_rubro;
                resp.Message = res_update_rubro == default ? "Ocurrio un error al actualizar registro." : string.Empty;



                var res_delete_valores = await db.tB_RubroValors.Where(x => x.IdRubro == id_rubro)
                    .DeleteAsync() > 0;

                resp.Success = res_delete_valores;
                resp.Message = res_delete_valores == default ? "Ocurrio un error al borrar registro." : string.Empty;

                foreach (var fecha in registro["fechas"].AsArray())
                {
                    int mes = Convert.ToInt32(fecha["mes"].ToString());
                    int anio = Convert.ToInt32(fecha["anio"].ToString());
                    decimal porcentaje = Convert.ToDecimal(fecha["porcentaje"].ToString());

                    var res_insert_valor = await db.tB_RubroValors
                        .Value(x => x.IdRubro, id_rubro)
                        .Value(x => x.Mes, mes)
                        .Value(x => x.Anio, anio)
                        .Value(x => x.Porcentaje, porcentaje)
                        .Value(x => x.Activo, true)
                        .InsertAsync() > 0;

                    resp.Success = res_insert_valor;
                    resp.Message = res_insert_valor == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                }
            }

            return resp;
        }
        #endregion Gastos / Ingresos
    }
}
