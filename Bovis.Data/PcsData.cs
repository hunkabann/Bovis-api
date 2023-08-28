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

        public async Task<List<TB_Proyecto>> GetProyectos()
        {
            //return await GetAllFromEntityAsync<TB_Proyecto>();
            using (var db = new ConnectionDB(dbConfig))
            {
                var resp = await (from p in db.tB_Proyectos
                              orderby p.Proyecto ascending
                              select p).ToListAsync();

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

        public async Task<List<TB_Cliente>> GetClientes()
        {
            return await GetAllFromEntityAsync<TB_Cliente>();
        }
        public async Task<List<TB_Empresa>> GetEmpresas()
        {
            return await GetAllFromEntityAsync<TB_Empresa>();
        }


        #region Proyectos
        public async Task<(bool Success, string Message)> AddProyecto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            
            string? nombre_proyecto = registro["nombre_proyecto"].ToString();
            string? alcance = registro["alcance"].ToString();
            string? codigo_postal = registro["codigo_postal"].ToString();
            string? ciudad = registro["ciudad"].ToString();
            int id_pais = Convert.ToInt32(registro["id_pais"].ToString());
            int id_estatus = Convert.ToInt32(registro["id_estatus"].ToString());
            int id_sector = Convert.ToInt32(registro["id_sector"].ToString());
            int id_tipo_proyecto = Convert.ToInt32(registro["id_tipo_proyecto"].ToString());
            int? id_responsable_preconstruccion = Convert.ToInt32(registro["id_responsable_preconstruccion"].ToString());
            int? id_responsable_construccion = Convert.ToInt32(registro["id_responsable_construccion"].ToString());
            int? id_responsable_ehs = Convert.ToInt32(registro["id_responsable_ehs"].ToString());
            int? id_responsable_supervisor = Convert.ToInt32(registro["id_responsable_supervisor"].ToString());
            int id_cliente = Convert.ToInt32(registro["id_cliente"].ToString());
            int id_empresa = Convert.ToInt32(registro["id_empresa"].ToString());
            int id_director_ejecutivo = Convert.ToInt32(registro["id_director_ejecutivo"].ToString());
            decimal costo_promedio_m2 = Convert.ToDecimal(registro["costo_promedio_m2"].ToString());
            DateTime fecha_inicio = Convert.ToDateTime(registro["fecha_inicio"].ToString());
            DateTime? fecha_fin = registro["fecha_fin"] != null ? Convert.ToDateTime(registro["fecha_fin"].ToString()) : null;

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_insert_etapa = await db.tB_Proyectos
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

                resp.Success = res_insert_etapa;
                resp.Message = res_insert_etapa == default ? "Ocurrio un error al insertar registro." : string.Empty;
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
        public async Task<(bool Success, string Message)> UpdateProyecto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int? num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
            string? nombre_proyecto = registro["nombre_proyecto"].ToString();
            string? alcance = registro["alcance"].ToString();
            string? codigo_postal = registro["codigo_postal"].ToString();
            string? ciudad = registro["ciudad"].ToString();
            int id_pais = Convert.ToInt32(registro["id_pais"].ToString());
            int id_estatus = Convert.ToInt32(registro["id_estatus"].ToString());
            int id_sector = Convert.ToInt32(registro["id_sector"].ToString());
            int id_tipo_proyecto = Convert.ToInt32(registro["id_tipo_proyecto"].ToString());
            int? id_responsable_preconstruccion = Convert.ToInt32(registro["id_responsable_preconstruccion"].ToString());
            int? id_responsable_construccion = Convert.ToInt32(registro["id_responsable_construccion"].ToString());
            int? id_responsable_ehs = Convert.ToInt32(registro["id_responsable_ehs"].ToString());
            int? id_responsable_supervisor = Convert.ToInt32(registro["id_responsable_supervisor"].ToString());
            int id_cliente = Convert.ToInt32(registro["id_cliente"].ToString());
            int id_empresa = Convert.ToInt32(registro["id_empresa"].ToString());
            int id_director_ejecutivo = Convert.ToInt32(registro["id_director_ejecutivo"].ToString());
            decimal costo_promedio_m2 = Convert.ToDecimal(registro["costo_promedio_m2"].ToString());
            DateTime fecha_inicio = Convert.ToDateTime(registro["fecha_inicio"].ToString());
            DateTime? fecha_fin = registro["fecha_fin"] != null ? Convert.ToDateTime(registro["fecha_fin"].ToString()) : null;

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
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
                var res_update_etapa = true;

                resp.Success = res_update_etapa;
                resp.Message = res_update_etapa == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        #endregion Proyectos

        #region Etapas
        public async Task<(bool Success, string Message)> AddEtapa(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_insert_etapa = true;

                resp.Success = res_insert_etapa;
                resp.Message = res_insert_etapa == default ? "Ocurrio un error al insertar registro." : string.Empty;
            }

            return resp;
        }
        public async Task<List<PCS_Etapa_Detalle>> GetEtapas(int IdProyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var etapas = new List<PCS_Etapa_Detalle>();

                return etapas;
            }
        }
        public async Task<(bool Success, string Message)> UpdateEtapa(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_etapa = true;

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
                var res_update_etapa = true;

                resp.Success = res_update_etapa;
                resp.Message = res_update_etapa == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        #endregion Etapas

        #region Empleados
        public async Task<(bool Success, string Message)> AddEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_insert_empleado = true;

                resp.Success = res_insert_empleado;
                resp.Message = res_insert_empleado == default ? "Ocurrio un error al insertar registro." : string.Empty;
            }

            return resp;
        }
        public async Task<List<PCS_Empleado_Detalle>> GetEmpleados(int IdProyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var empleados = new List<PCS_Empleado_Detalle>();

                return empleados;
            }
        }
        public async Task<(bool Success, string Message)> UpdateEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_empleado = true;

                resp.Success = res_update_empleado;
                resp.Message = res_update_empleado == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        public async Task<(bool Success, string Message)> DeleteEmpleado(int IdEmpleado)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_empleado = true;

                resp.Success = res_update_empleado;
                resp.Message = res_update_empleado == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        #endregion Empleados
    }
}
