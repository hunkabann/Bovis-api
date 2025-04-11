using Azure;
using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using LinqToDB.SqlQuery;
using static System.Collections.Specialized.BitVector32;
using LinqToDB.DataProvider.DB2;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Text;
using System.Drawing;

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
                                  where p.Activo == true
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

        //atc 09-11-2024
        public async Task<List<TB_Proyecto>> GetProyectosNoClose(bool? OrdenAlfabetico)
        {
            //return await GetAllFromEntityAsync<TB_Proyecto>();
           

                using (var db = new ConnectionDB(dbConfig))
            {
                var proyectos = await (from p in db.tB_Proyectos
                                       where p.IdEstatus != 3
                                       select p).ToListAsync();
                return proyectos;
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
            string? alcance = registro["alcance"] != null ? registro["alcance"].ToString() : null;
            string? codigo_postal = registro["codigo_postal"] != null ? registro["codigo_postal"].ToString() : null;
            string? ciudad = registro["ciudad"] != null ? registro["ciudad"].ToString() : null;
            int? id_pais = registro["id_pais"] != null ? Convert.ToInt32(registro["id_pais"].ToString()) : null;
            int? id_estatus = registro["id_estatus"] != null ? Convert.ToInt32(registro["id_estatus"].ToString()) : null;
            int? id_sector = registro["id_sector"] != null ? Convert.ToInt32(registro["id_sector"].ToString()) : null;
            int? id_tipo_proyecto = registro["id_tipo_proyecto"] != null ? Convert.ToInt32(registro["id_tipo_proyecto"].ToString()) : null;
            string? id_responsable_preconstruccion = registro["id_responsable_preconstruccion"] != null ? registro["id_responsable_preconstruccion"].ToString() : null;
            string? id_responsable_construccion = registro["id_responsable_construccion"] != null ? registro["id_responsable_construccion"].ToString() : null;
            string? id_responsable_ehs = registro["id_responsable_ehs"] != null ? registro["id_responsable_ehs"].ToString() : null;
            string? id_responsable_supervisor = registro["id_responsable_supervisor"] != null ? registro["id_responsable_supervisor"].ToString() : null;
            //int? id_cliente = registro["id_cliente"] != null ? Convert.ToInt32(registro["id_cliente"].ToString()) : null;
            int? id_empresa = registro["id_empresa"] != null ? Convert.ToInt32(registro["id_empresa"].ToString()) : null;
            string? id_director_ejecutivo = registro["id_director_ejecutivo"] != null ? registro["id_director_ejecutivo"].ToString() : null;
            decimal? costo_promedio_m2 = registro["costo_promedio_m2"] != null ? Convert.ToDecimal(registro["costo_promedio_m2"].ToString()) : null;
            DateTime fecha_inicio = Convert.ToDateTime(registro["fecha_inicio"].ToString());
            DateTime? fecha_fin = registro["fecha_fin"] != null ? Convert.ToDateTime(registro["fecha_fin"].ToString()) : null;
            string? nombre_contacto = registro["nombre_contacto"] != null ? registro["nombre_contacto"].ToString() : null;
            string? posicion_contacto = registro["posicion_contacto"] != null ? registro["posicion_contacto"].ToString() : null;
            string? telefono_contacto = registro["telefono_contacto"] != null ? registro["telefono_contacto"].ToString() : null;
            string? correo_contacto = registro["correo_contacto"] != null ? registro["correo_contacto"].ToString() : null;
            int impuesto_nomina = Convert.ToInt32(registro["impuesto_nomina"].ToString());

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
                    .Value(x => x.IdEmpresa, id_empresa)
                    .Value(x => x.IdDirectorEjecutivo, id_director_ejecutivo)
                    .Value(x => x.CostoPromedioM2, costo_promedio_m2)
                    .Value(x => x.FechaIni, fecha_inicio)
                    .Value(x => x.FechaFin, fecha_fin)
                    .Value(x => x.ImpuestoNomina, impuesto_nomina)
                    .InsertAsync() > 0;

                resp.Success = res_insert_proyecto;
                resp.Message = res_insert_proyecto == default ? "Ocurrio un error al insertar registro." : string.Empty;

                /*
                 * Se agregan los clientes del proyecto.
                 */
                foreach (var id_cliente in registro["ids_clientes"].AsArray())
                {
                    var res_insert_cliente = await db.tB_ClienteProyectos
                        .Value(x => x.IdCliente, Convert.ToInt32(id_cliente.ToString()))
                        .Value(x => x.NumProyecto, num_proyecto)
                    .InsertAsync() > 0;

                    resp.Success = res_insert_cliente;
                    resp.Message = res_insert_cliente == default ? "Ocurrio un error al insertar registro." : string.Empty;
                }


                 /*
                  * Se agregan las secciones y rubros para gastos e ingresos Rembolsabes.
                  */
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
                                         .Value(x => x.Reembolsable, true)
                                         .Value(x => x.Activo, true)
                                         .InsertAsync() > 0;
                
                         resp.Success = res_insert_rubro;
                         resp.Message = res_insert_rubro == default ? "Ocurrio un error al insertar registro." : string.Empty;
                     }
                 }
                
                 /*
                 * Se duplica las secciones y rubros para gastos e ingresos No Rembolsabes.  ATC 13-01-2025
                 */
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
                                         .Value(x => x.Reembolsable, false)
                                         .Value(x => x.Activo, true)
                                         .InsertAsync() > 0;
                
                         resp.Success = res_insert_rubro;
                         resp.Message = res_insert_rubro == default ? "Ocurrio un error al insertar registro." : string.Empty;
                     }
                 } 
                 

                /*
                 * Se agregan sus documentos de auditorías, default.
                 */
                AuditoriaData audit = new AuditoriaData();
                JsonObject json = new JsonObject
                {
                    ["id_proyecto"] = num_proyecto,
                    ["auditorias"] = new JsonArray()
                };
                JsonArray jsonArray = new JsonArray();
                HashSet<string> uniqueIds = new HashSet<string>();

                var auditorias = await audit.GetAuditorias("ambos");
                auditorias.AddRange(await audit.GetAuditorias("calidad"));
                auditorias.AddRange(await audit.GetAuditorias("legal"));

                foreach (var sections in auditorias)
                {
                    foreach (var documento in sections.Auditorias)
                    {
                        string idAuditoria = documento.IdAuditoria.ToString();

                        if (uniqueIds.Add(idAuditoria))
                        {
                            jsonArray.Add(new JsonObject
                            {
                                ["id_auditoria"] = documento.IdAuditoria,
                                ["aplica"] = (documento.Punto == "Reportes Mensuales" ||
                                          documento.Punto == "Certificación mensual de Servicios:" ||
                                          documento.Punto == "Comunicación escrita y/o electrónica con el Cliente" ||
                                          documento.Punto == "Contrato de Bovis"),
                                ["motivo"] = "Documento por default"
                            });
                        }
                    }
                }

                json["auditorias"] = jsonArray;

                await audit.AddAuditorias(json);
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

                                   join empresa in db.tB_Empresas on proy.IdEmpresa equals empresa.IdEmpresa into empresaJoin
                                   from empresaItem in empresaJoin.DefaultIfEmpty()

                                   join empleado_director in db.tB_Empleados on proy.IdDirectorEjecutivo equals empleado_director.NumEmpleadoRrHh into empleado_directorJoin
                                   from empleado_directorItem in empleado_directorJoin.DefaultIfEmpty()
                                   join persona_director in db.tB_Personas on empleado_directorItem.IdPersona equals persona_director.IdPersona into persona_directorJoin
                                   from persona_directorItem in persona_directorJoin.DefaultIfEmpty()

                                   join contacto in db.tB_Contactos on proy.NumProyecto equals contacto.NumProyecto into contactoJoin
                                   from contactoItem in contactoJoin.DefaultIfEmpty()

                                   join unidadnegocio in db.tB_Cat_UnidadNegocios on proy.IdUnidadDeNegocio equals unidadnegocio.IdUnidadNegocio into unidadnegocioJoin
                                   from unidadnegocioItem in unidadnegocioJoin.DefaultIfEmpty()   //atc

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
                                       nukidempresa = proy.IdEmpresa,
                                       chempresa = empresaItem.Empresa ?? null,
                                       nukiddirector_ejecutivo = proy.IdDirectorEjecutivo,
                                       chdirector_ejecutivo = persona_directorItem != null ? persona_directorItem.Nombre + " " + persona_directorItem.ApPaterno + " " + persona_resp_supItem.ApMaterno : null,
                                       nucosto_promedio_m2 = proy.CostoPromedioM2,
                                       dtfecha_ini = proy.FechaIni,
                                       dtfecha_fin = proy.FechaFin,
                                       impuesto_nomina = proy.ImpuestoNomina,
                                       nukidunidadnegocio = proy.IdUnidadDeNegocio,  //atc
                                       chunidadnegocio = unidadnegocioItem.UnidadNegocio ?? null,  //atc
                                       chcontacto_nombre = contactoItem != null ? contactoItem.Nombre : string.Empty,
                                       chcontacto_posicion = contactoItem != null ? contactoItem.Posicion : string.Empty,
                                       chcontacto_telefono = contactoItem != null ? contactoItem.Telefono : string.Empty,
                                       chcontacto_correo = contactoItem != null ? contactoItem.Correo : string.Empty

                                   }).ToListAsync();

                foreach (var proyecto in proyectos)
                {
                    proyecto.Clientes = new List<InfoCliente>();
                    proyecto.Clientes.AddRange(await (from c in db.tB_Clientes
                                                      join cp in db.tB_ClienteProyectos on c.IdCliente equals cp.IdCliente into cpJoin
                                                      from cpItem in cpJoin.DefaultIfEmpty()
                                                      where cpItem.NumProyecto == proyecto.nunum_proyecto
                                                      && c.Activo == true
                                                      select new InfoCliente
                                                      {
                                                          IdCliente = cpItem.IdCliente,
                                                          Cliente = c.Cliente,
                                                          Rfc = c.Rfc
                                                      }).ToListAsync());
                }

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
            string? id_responsable_preconstruccion = registro["id_responsable_preconstruccion"] != null ? registro["id_responsable_preconstruccion"].ToString() : null;
            string? id_responsable_construccion = registro["id_responsable_construccion"] != null ? registro["id_responsable_construccion"].ToString() : null;
            string? id_responsable_ehs = registro["id_responsable_ehs"] != null ? registro["id_responsable_ehs"].ToString() : null;
            string? id_responsable_supervisor = registro["id_responsable_supervisor"] != null ? registro["id_responsable_supervisor"].ToString() : null;
            int? id_empresa = registro["id_empresa"] != null ? Convert.ToInt32(registro["id_empresa"].ToString()) : null;
            string? id_director_ejecutivo = registro["id_director_ejecutivo"] != null ? registro["id_director_ejecutivo"].ToString() : null;
            decimal? costo_promedio_m2 = registro["costo_promedio_m2"] != null ? Convert.ToDecimal(registro["costo_promedio_m2"].ToString()) : null;
            DateTime fecha_inicio = Convert.ToDateTime(registro["fecha_inicio"].ToString());
            DateTime? fecha_fin = registro["fecha_fin"] != null ? Convert.ToDateTime(registro["fecha_fin"].ToString()) : null;
            string? nombre_contacto = registro["nombre_contacto"] != null ? registro["nombre_contacto"].ToString() : null;
            string? posicion_contacto = registro["posicion_contacto"] != null ? registro["posicion_contacto"].ToString() : null;
            string? telefono_contacto = registro["telefono_contacto"] != null ? registro["telefono_contacto"].ToString() : null;
            string? correo_contacto = registro["correo_contacto"] != null ? registro["correo_contacto"].ToString() : null;
            int? id_unidad_negocio = registro["id_unidad_negocio"] != null ? Convert.ToInt32(registro["id_unidad_negocio"].ToString()) : null; //atc

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var contacto = await (from c in db.tB_Contactos
                                      where c.NumProyecto == num_proyecto
                                      select c).FirstOrDefaultAsync();

                if (contacto != null)
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
                }
                else
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
                }

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
                        IdEmpresa = id_empresa,
                        IdDirectorEjecutivo = id_director_ejecutivo,
                        CostoPromedioM2 = costo_promedio_m2,
                        FechaIni = fecha_inicio,
                        FechaFin = fecha_fin,
                        IdUnidadDeNegocio = id_unidad_negocio //atc
                    }) > 0;

                resp.Success = res_update_proyecto;
                resp.Message = res_update_proyecto == default ? "Ocurrio un error al actualizar registro." : string.Empty;


                /*
                 * Se actualizan los clientes del proyecto.
                 */
                var res_delete_clientes_proyecto = await db.tB_ClienteProyectos.Where(x => x.NumProyecto == num_proyecto)
                    .DeleteAsync() > 0;

                resp.Success = res_delete_clientes_proyecto;
                resp.Message = res_delete_clientes_proyecto == default ? "Ocurrio un error al borrar registro." : string.Empty;

                foreach (var id_cliente in registro["ids_clientes"].AsArray())
                {
                    var res_insert_cliente = await db.tB_ClienteProyectos
                        .Value(x => x.IdCliente, Convert.ToInt32(id_cliente.ToString()))
                        .Value(x => x.NumProyecto, num_proyecto)
                    .InsertAsync() > 0;

                    resp.Success = res_insert_cliente;
                    resp.Message = res_insert_cliente == default ? "Ocurrio un error al insertar registro." : string.Empty;
                }
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

        public async Task<(bool Success, string Message)> UpdateProyectoFechaAuditoria(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int num_proyecto = Convert.ToInt32(registro["numProyecto"].ToString());
            DateTime? fecha_auditoria_inicial = registro["fechaAuditoriaInicial"] != null ? Convert.ToDateTime(registro["fechaAuditoriaInicial"].ToString()) : null;
            DateTime? fecha_prox_auditoria = registro["fechaAuditoria"] != null ? Convert.ToDateTime(registro["fechaAuditoria"].ToString()) : null;
            string? responsable_asignado = registro["responsableAsignado"] != null ? registro["responsableAsignado"].ToString() : null;

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_proyecto = await db.tB_Proyectos.Where(x => x.NumProyecto == num_proyecto)
                    .UpdateAsync(x => new TB_Proyecto
                    {
                        FechaAuditoriaInicial = fecha_auditoria_inicial,
                        FechaProxAuditoria = fecha_prox_auditoria,
                        ResponsableAsignado = responsable_asignado
                    }) > 0;

                resp.Success = res_update_proyecto;
                resp.Message = res_update_proyecto == default ? "Ocurrio un error al actualizar registro." : string.Empty;
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
                                    orderby p.FechaIni ascending
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
                                               Empleado = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                               Cantidad = p.Cantidad,
                                               AplicaTodosMeses = p.AplicaTodosMeses,
                                               Fee = p.Fee
                                           } by new { p.NumEmpleado } into g
                                           select new PCS_Empleado_Detalle
                                           {
                                               Id = g.First().Id,
                                               IdFase = g.First().IdFase,
                                               NumempleadoRrHh = g.Key.NumEmpleado,
                                               Empleado = g.First().Empleado,
                                               Cantidad = g.First().Cantidad,
                                               AplicaTodosMeses = g.First().AplicaTodosMeses,
                                               Fee = g.First().Fee
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
                        Fase = nombre_fase,
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
            string num_empleado = registro["num_empleado"].ToString();
            int num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
            decimal? cantidad = registro["cantidad"] != null ? Convert.ToDecimal(registro["cantidad"].ToString()) : null;
            bool? aplica_todos_meses = registro["aplicaTodosMeses"] != null ? Convert.ToBoolean(registro["aplicaTodosMeses"].ToString()) : null;
            decimal? fee = registro["FEE"] != null ? Convert.ToDecimal(registro["FEE"].ToString()) : null;

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
                        .Value(x => x.Cantidad, cantidad)
                        .Value(x => x.AplicaTodosMeses, aplica_todos_meses)
                        .Value(x => x.Fee, fee)
                        .InsertAsync() > 0;

                    resp.Success = res_insert_empleado;
                    resp.Message = res_insert_empleado == default ? "Ocurrio un error al insertar registro." : string.Empty;



                    // Se insertan los valores de los rubros, para gastos e ingresos.
                    var rubros = await (from rub in db.tB_Rubros
                                        where rub.NumProyecto == num_proyecto
                                        select rub).ToListAsync();

                    foreach (var rubro in rubros)
                    {
                        var res_insert_rubro_valor = await db.tB_RubroValors
                                        .Value(x => x.IdRubro, rubro.Id)
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
                                           Empleado = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                           Cantidad = p.Cantidad,
                                           AplicaTodosMeses = p.AplicaTodosMeses,
                                           Fee = p.Fee
                                       } by new { p.NumEmpleado } into g
                                       select new PCS_Empleado_Detalle
                                       {
                                           Id = g.First().Id,
                                           IdFase = g.First().IdFase,
                                           NumempleadoRrHh = g.Key.NumEmpleado,
                                           Empleado = g.First().Empleado,
                                           Cantidad = g.First().Cantidad,
                                           AplicaTodosMeses = g.First().AplicaTodosMeses,
                                           Fee = g.First().Fee
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
            string num_empleado = registro["num_empleado"].ToString();
            decimal? cantidad = registro["cantidad"] != null ? Convert.ToDecimal(registro["cantidad"].ToString()) : null;
            bool? aplica_todos_meses = registro["aplicaTodosMeses"] != null ? Convert.ToBoolean(registro["aplicaTodosMeses"].ToString()) : null;
            decimal? fee = registro["FEE"] != null ? Convert.ToDecimal(registro["FEE"].ToString()) : null;

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
                        .Value(x => x.Cantidad, cantidad)
                        .Value(x => x.AplicaTodosMeses, aplica_todos_meses)
                        .Value(x => x.Fee, fee)
                        .InsertAsync() > 0;

                    resp.Success = res_insert_empleado;
                    resp.Message = res_insert_empleado == default ? "Ocurrio un error al insertar registro." : string.Empty;
                }
            }

            return resp;
        }
        public async Task<(bool Success, string Message)> DeleteEmpleado(int IdFase, string NumEmpleado)
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
        public async Task<List<Seccion_Detalle>> GetGastosIngresosSecciones(int IdProyecto, string Tipo)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
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

                return secciones;
            }
        }
        private async Task<List<PCS_Fecha_Detalle>> GetFechasGasto(int IdProyecto, List<PCS_Etapa_Detalle> Fases, Seccion_Detalle Seccion, string Rubro, bool? reembolsable = false)
        {
            var fechas_gasto = new List<PCS_Fecha_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                List<Rubro_Detalle> rubros = new List<Rubro_Detalle>();

                if (Seccion.IdSeccion == 2)
                {
                    foreach (var fase in Fases)
                    {
                        rubros = await (from p in db.tB_ProyectoFaseEmpleados
                                        join e in db.tB_Empleados on p.NumEmpleado equals e.NumEmpleadoRrHh into eJoin
                                        from eItem in eJoin.DefaultIfEmpty()
                                        join per in db.tB_Personas on eItem.IdPersona equals per.IdPersona into perJoin
                                        from perItem in perJoin.DefaultIfEmpty()
                                        where p.IdFase == fase.IdFase
                                        orderby p.NumEmpleado ascending
                                        group new Rubro_Detalle
                                        {
                                            Id = p.Id,
                                            IdRubro = perItem != null ? perItem.IdPersona : 0,
                                            Rubro = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                            Empleado = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                            NumEmpleadoRrHh = eItem != null ? eItem.NumEmpleadoRrHh : string.Empty,
                                            Reembolsable = (p.Fee == null || p.Fee == 0) ? false : true

                                        } by new { p.NumEmpleado } into g
                                        select new Rubro_Detalle
                                        {
                                            Id = g.First().Id,
                                            IdRubro = g.First().IdRubro,
                                            Rubro = g.First().Rubro,
                                            Empleado = g.First().Empleado,
                                            NumEmpleadoRrHh = g.Key.NumEmpleado,
                                            Reembolsable = g.First().Reembolsable
                                        }).ToListAsync();

                        fechas_gasto = new List<PCS_Fecha_Detalle>();

                        foreach (var rubro in rubros)
                        {
                            var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                                where p.NumEmpleado == rubro.NumEmpleadoRrHh
                                                && p.IdFase == fase.IdFase
                                                orderby p.Anio, p.Mes ascending
                                                select new PCS_Fecha_Detalle
                                                {
                                                    Id = p.Id,
                                                    Rubro = rubro.Rubro,
                                                    RubroReembolsable = rubro.Reembolsable,
                                                    Mes = p.Mes,
                                                    Anio = p.Anio,
                                                    Porcentaje = p.Porcentaje
                                                }).ToListAsync();

                            if (rubro.Rubro.Trim() == Rubro.Trim())
                            {
                                fechas_gasto.AddRange(fechas);
                                return fechas_gasto;
                            }
                        }
                    }
                }
                else
                {
                    rubros = await (from rubro in db.tB_Rubros
                                    join rel1 in db.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
                                    from rel1Item in rel1Join.DefaultIfEmpty()
                                    join rel2 in db.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
                                    where rubro.IdSeccion == Seccion.IdSeccion
                                    && rubro.NumProyecto == IdProyecto
                                    //&& rel2.Tipo == "GASTO"
                                    //&& (reembolsable != null ? rubro.Reembolsable == reembolsable : rubro.Reembolsable == false)
                                    select new Rubro_Detalle
                                    {
                                        Id = rubro.Id,
                                        IdRubro = rubro.IdRubro,
                                        Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
                                        Unidad = rubro.Unidad,
                                        Cantidad = rubro.Cantidad,
                                        Reembolsable = rubro.Reembolsable,
                                        AplicaTodosMeses = rubro.AplicaTodosMeses
                                    }).ToListAsync();

                    fechas_gasto = new List<PCS_Fecha_Detalle>();

                    foreach (var r in rubros)
                    {
                        var fechas = await (from valor in db.tB_RubroValors
                                            join rub in db.tB_Rubros on r.IdRubro equals rub.IdRubro
                                            join cat in db.tB_CatRubros on r.IdRubro equals cat.IdRubro
                                            join sec in db.tB_GastoIngresoSeccions on Seccion.IdSeccion equals sec.IdSeccion into secRel
                                            from secItem in secRel.DefaultIfEmpty()
                                            where rub.NumProyecto == IdProyecto
                                            && secItem.Tipo == "GASTO"
                                            && rub.Reembolsable == reembolsable
                                            orderby valor.Anio, valor.Mes ascending
                                            select new PCS_Fecha_Detalle
                                            {
                                                Id = valor.Id,
                                                Rubro = cat.Rubro,
                                                RubroReembolsable = rub.Reembolsable,
                                                Mes = valor.Mes,
                                                Anio = valor.Anio,
                                                Porcentaje = valor.Porcentaje
                                            }).Distinct().ToListAsync();

                        if (r.Rubro.Trim() == Rubro.Trim())
                        {
                            fechas_gasto.AddRange(fechas);
                            return fechas_gasto;
                        }
                    }

                }
            }

            return fechas_gasto;
        }


        
        public async Task<GastosIngresos_Detalle> GetGastosIngresos(int IdProyecto, string Tipo, string Seccion)
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
                proyecto_gastos_ingresos.Secciones = new List<Seccion_Detalle>();

                var fases = await (from p in db.tB_ProyectoFases
                                   join proy in db.tB_Proyectos on p.NumProyecto equals proy.NumProyecto into proyJoin
                                   from proyItem in proyJoin.DefaultIfEmpty()
                                   where p.NumProyecto == IdProyecto
                                   orderby p.FechaIni ascending
                                   select new PCS_Etapa_Detalle
                                   {
                                       IdFase = p.IdFase,
                                       Orden = p.Orden,
                                       Fase = p.Fase,
                                       FechaIni = p.FechaIni,
                                       FechaFin = p.FechaFin
                                   }).ToListAsync();

                var seccion = await(from secc in db.tB_GastoIngresoSeccions
                                    where secc.Tipo == Tipo.ToUpper()
                                    && secc.Seccion == Seccion
                                    select new Seccion_Detalle
                                    {
                                        IdSeccion = secc.IdSeccion,
                                        Codigo = secc.Codigo,
                                        Seccion = secc.Seccion
                                    }).FirstOrDefaultAsync();

                proyecto_gastos_ingresos.Secciones.Add(seccion!);

                List<Rubro_Detalle> rubros = new List<Rubro_Detalle>();
                seccion!.Rubros = new List<Rubro_Detalle>();


                if ((seccion.IdSeccion == 2) || (Tipo == "ingreso" && seccion.IdSeccion == 8))
                {
                    foreach (var fase in fases)
                    {
                        rubros = await (from p in db.tB_ProyectoFaseEmpleados
                                         join e in db.tB_Empleados on p.NumEmpleado equals e.NumEmpleadoRrHh into eJoin
                                         from eItem in eJoin.DefaultIfEmpty()
                                         join per in db.tB_Personas on eItem.IdPersona equals per.IdPersona into perJoin
                                         from perItem in perJoin.DefaultIfEmpty()
                                         join costemple in db.tB_Costo_Por_Empleados on eItem.NumEmpleadoRrHh equals costemple.NumEmpleadoRrHh into costempleJoin
                                         from costempleItem in costempleJoin.DefaultIfEmpty()
                                         where p.IdFase == fase.IdFase
                                         orderby p.NumEmpleado ascending
                                         group new Rubro_Detalle
                                         {
                                             Id = p.Id,
                                             IdRubro = perItem != null ? perItem.IdPersona : 0,
                                             Rubro = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                             Empleado = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                             NumEmpleadoRrHh = eItem != null ? eItem.NumEmpleadoRrHh : string.Empty,
                                             Cantidad = p.Fee,
                                             Reembolsable = (p.Fee == null || p.Fee == 0) ? false : true,
                                             CostoMensual = costempleItem.CostoMensualEmpleado
                                         } by new { p.NumEmpleado } into g
                                         select new Rubro_Detalle
                                         {
                                             Id = g.First().Id,
                                             IdRubro = g.First().IdRubro,
                                             Rubro = g.First().Rubro,
                                             Empleado = g.First().Empleado,
                                             NumEmpleadoRrHh = g.Key.NumEmpleado,
                                             Cantidad = g.First().Cantidad,
                                             Reembolsable = g.First().Reembolsable,
                                             CostoMensual = g.First().CostoMensual
                                         }).ToListAsync();

                        foreach (var rubro in rubros)
                        {
                            rubro.Fechas = new List<PCS_Fecha_Detalle>();
                            if (Tipo == "gasto")
                            {
                                var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                                    where p.NumEmpleado == rubro.NumEmpleadoRrHh
                                                    && p.IdFase == fase.IdFase
                                                    //ATC
                                                    && p.Porcentaje > 0
                                                    orderby p.Anio, p.Mes ascending
                                                    select new PCS_Fecha_Detalle
                                                    {
                                                        Id = p.Id,
                                                        Rubro = rubro.Rubro,
                                                        RubroReembolsable = rubro.Reembolsable,
                                                        Mes = p.Mes,
                                                        Anio = p.Anio,
                                                        Porcentaje = p.Porcentaje
                                                    }).ToListAsync();

                                rubro.Fechas.AddRange(fechas);
                            }
                            else
                            {
                                rubro.Fechas.AddRange(await GetFechasGasto(IdProyecto, fases, seccion, rubro.Rubro, rubro.Reembolsable));
                            }
                        }

                        seccion.Rubros.AddRange(rubros);
                    }
                }
                else
                {
                    rubros = await (from rubro in db.tB_Rubros
                                     join rel1 in db.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
                                     from rel1Item in rel1Join.DefaultIfEmpty()
                                     join rel2 in db.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
                                     where rubro.IdSeccion == seccion.IdSeccion
                                     && rubro.NumProyecto == IdProyecto
                                     && rel2.Tipo == Tipo.ToUpper()
                                     select new Rubro_Detalle
                                     {
                                         Id = rubro.Id,
                                         IdRubro = rubro.IdRubro,
                                         Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
                                         Unidad = rubro.Unidad,
                                         Cantidad = rubro.Cantidad,
                                         Reembolsable = rubro.Reembolsable,
                                         AplicaTodosMeses = rubro.AplicaTodosMeses,
                                         Fechas = new List<PCS_Fecha_Detalle>()
                                     }).ToListAsync();

                    rubros = rubros.Where(r => r != null).ToList();
                    seccion.Rubros.AddRange(rubros);

                    foreach (var rubro in seccion.Rubros.Where(r => r != null))
                    {

                        if (Tipo == "gasto")
                        {
                            var fechas = await (from valor in db.tB_RubroValors
                                                join rub in db.tB_Rubros on valor.IdRubro equals rubro.Id
                                                join cat in db.tB_CatRubros on rub.IdRubro equals cat.IdRubro
                                                join sec in db.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
                                                where rub.NumProyecto == IdProyecto
                                                && sec.Tipo == Tipo.ToUpper()
                                                //ATC
                                                && valor.Porcentaje > 0
                                                orderby valor.Anio, valor.Mes ascending
                                                select new PCS_Fecha_Detalle
                                                {
                                                    Id = valor.Id,
                                                    Rubro = rubro.Rubro,
                                                    RubroReembolsable = rubro.Reembolsable,
                                                    Mes = valor.Mes,
                                                    Anio = valor.Anio,
                                                    Porcentaje = valor.Porcentaje
                                                }).Distinct().ToListAsync();

                            rubro!.Fechas!.AddRange(fechas ?? new List<PCS_Fecha_Detalle>());
                        }
                        else
                        {                          
                            rubro.Fechas ??= new List<PCS_Fecha_Detalle>();
                            var fechasGasto = await GetFechasGasto(IdProyecto, fases, seccion, rubro.Rubro, rubro.Reembolsable) ?? new List<PCS_Fecha_Detalle>();
                            rubro!.Fechas!.AddRange(fechasGasto);
                        }
                    }
                }

                // Agrupar y sumar los porcentajes por mes y año a nivel de sección
                var fechasAgrupadasSeccion = seccion.Rubros
                    .SelectMany(r => r.Fechas)
                    .GroupBy(f => new { f.Mes, f.Anio })
                    .Select(g => new PCS_Fecha_Suma
                    {
                        Mes = g.Key.Mes,
                        Anio = g.Key.Anio,
                        SumaPorcentaje = g.Sum(f => f.Porcentaje)
                    }).ToList();

                seccion.SumaFechas = fechasAgrupadasSeccion;


                if (seccion.IdSeccion > 2)
                {
                    foreach (var rubro in rubros)
                    {
                        if (rubro.Unidad == "pp")
                        {
                            foreach (var fecha in rubro.Fechas)
                            {
                                foreach (var sumaFecha in proyecto_gastos_ingresos.Secciones[0].SumaFechas)
                                {
                                    if (fecha.Mes == sumaFecha.Mes && fecha.Anio == sumaFecha.Anio)
                                    {
                                        fecha.Porcentaje = rubro.Cantidad * (sumaFecha.SumaPorcentaje / 100);
                                    }
                                }
                            }
                        }
                        else if (rubro.Unidad == "mes")
                        {
                            foreach (var fecha in rubro.Fechas)
                            {
                                fecha.Porcentaje = rubro.Cantidad;
                            }
                        }
                    }
                }

                if (Tipo == "ingreso")
                {
                    // Calcular los Totales del Proyecto
                    proyecto_gastos_ingresos.Totales = proyecto_gastos_ingresos.Secciones
                        .SelectMany(s => s.Rubros)
                        .SelectMany(r => r.Fechas, (r, f) => new { r.Reembolsable, f })
                        .GroupBy(x => new { x.f.Mes, x.f.Anio, Reembolsable = x.Reembolsable ?? false })
                        .Select(g => new PCS_Fecha_Totales
                        {
                            Mes = g.Key.Mes,
                            Anio = g.Key.Anio,
                            Reembolsable = g.Key.Reembolsable,
                            TotalPorcentaje = g.Sum(x => x.f.Porcentaje)
                        }).ToList();
                }


                return proyecto_gastos_ingresos;
            }
        }

        //private async Task<List<PCS_Fecha_Detalle>> GetFechasGasto(int IdProyecto, string Rubro, bool? reembolsable = false)
        //{
        //    var fechas_gasto = new List<PCS_Fecha_Detalle>();

        //    using (var db = new ConnectionDB(dbConfig))
        //    {

        //        var secciones = await (from seccion in db.tB_GastoIngresoSeccions
        //                               where seccion.Tipo == "gasto"
        //                               orderby seccion.Codigo ascending
        //                               select new Seccion_Detalle
        //                               {
        //                                   IdSeccion = seccion.IdSeccion,
        //                                   Codigo = seccion.Codigo,
        //                                   Seccion = seccion.Seccion
        //                               })
        //                                  .ToListAsync();

        //        foreach (var seccion in secciones)
        //        {
        //            List<Rubro_Detalle> rubros = null;
        //            seccion.Rubros = new List<Rubro_Detalle>();

        //            if (seccion.IdSeccion == 2)
        //            {
        //                var etapas = await (from p in db.tB_ProyectoFases
        //                                    join proy in db.tB_Proyectos on p.NumProyecto equals proy.NumProyecto into proyJoin
        //                                    from proyItem in proyJoin.DefaultIfEmpty()
        //                                    where p.NumProyecto == IdProyecto
        //                                    orderby p.FechaIni ascending
        //                                    select new PCS_Etapa_Detalle
        //                                    {
        //                                        IdFase = p.IdFase,
        //                                        Orden = p.Orden,
        //                                        Fase = p.Fase,
        //                                        FechaIni = p.FechaIni,
        //                                        FechaFin = p.FechaFin
        //                                    }).ToListAsync();

        //                foreach (var etapa in etapas)
        //                {
        //                    rubros = await (from p in db.tB_ProyectoFaseEmpleados
        //                                    join e in db.tB_Empleados on p.NumEmpleado equals e.NumEmpleadoRrHh into eJoin
        //                                    from eItem in eJoin.DefaultIfEmpty()
        //                                    join per in db.tB_Personas on eItem.IdPersona equals per.IdPersona into perJoin
        //                                    from perItem in perJoin.DefaultIfEmpty()
        //                                    where p.IdFase == etapa.IdFase
        //                                    orderby p.NumEmpleado ascending
        //                                    group new Rubro_Detalle
        //                                    {
        //                                        Id = p.Id,
        //                                        IdRubro = perItem != null ? perItem.IdPersona : 0,
        //                                        Rubro = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
        //                                        Empleado = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
        //                                        NumEmpleadoRrHh = eItem != null ? eItem.NumEmpleadoRrHh : string.Empty,
        //                                        Reembolsable = (p.Fee == null || p.Fee == 0) ? false : true

        //                                    } by new { p.NumEmpleado } into g
        //                                    select new Rubro_Detalle
        //                                    {
        //                                        Id = g.First().Id,
        //                                        IdRubro = g.First().IdRubro,
        //                                        Rubro = g.First().Rubro,
        //                                        Empleado = g.First().Empleado,
        //                                        NumEmpleadoRrHh = g.Key.NumEmpleado,
        //                                        Reembolsable = g.First().Reembolsable
        //                                    }).ToListAsync();

        //                    fechas_gasto = new List<PCS_Fecha_Detalle>();

        //                    foreach (var rubro in rubros)
        //                    {
        //                        var fechas = await (from p in db.tB_ProyectoFaseEmpleados
        //                                            where p.NumEmpleado == rubro.NumEmpleadoRrHh
        //                                            && p.IdFase == etapa.IdFase
        //                                            orderby p.Anio, p.Mes ascending
        //                                            select new PCS_Fecha_Detalle
        //                                            {
        //                                                Id = p.Id,
        //                                                Rubro = rubro.Rubro,
        //                                                RubroReembolsable = rubro.Reembolsable,
        //                                                Mes = p.Mes,
        //                                                Anio = p.Anio,
        //                                                Porcentaje = p.Porcentaje
        //                                            }).ToListAsync();

        //                        if (rubro.Rubro.Trim() == Rubro.Trim())
        //                        {
        //                            fechas_gasto.AddRange(fechas);
        //                            return fechas_gasto;
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                rubros = await (from rubro in db.tB_Rubros
        //                                join rel1 in db.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
        //                                from rel1Item in rel1Join.DefaultIfEmpty()
        //                                join rel2 in db.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
        //                                where rubro.IdSeccion == seccion.IdSeccion
        //                                && rubro.NumProyecto == IdProyecto
        //                                && rel2.Tipo == "gasto"
        //                                && rubro.Reembolsable == reembolsable
        //                                select new Rubro_Detalle
        //                                {
        //                                    Id = rubro.Id,
        //                                    IdRubro = rubro.IdRubro,
        //                                    Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
        //                                    Unidad = rubro.Unidad,
        //                                    Cantidad = rubro.Cantidad,
        //                                    Reembolsable = rubro.Reembolsable,
        //                                    AplicaTodosMeses = rubro.AplicaTodosMeses
        //                                }).ToListAsync();

        //                fechas_gasto = new List<PCS_Fecha_Detalle>();

        //                foreach (var r in rubros)
        //                {
        //                    var fechas = await (from valor in db.tB_RubroValors
        //                                        join rub in db.tB_Rubros on valor.IdRubro equals r.Id
        //                                        join cat in db.tB_CatRubros on rub.IdRubro equals cat.IdRubro
        //                                        join sec in db.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
        //                                        where rub.NumProyecto == IdProyecto
        //                                        && sec.Tipo == "gasto"
        //                                        && rub.Reembolsable == reembolsable
        //                                        orderby valor.Anio, valor.Mes ascending
        //                                        select new PCS_Fecha_Detalle
        //                                        {
        //                                            Id = valor.Id,
        //                                            RubroReembolsable = rub.Reembolsable,
        //                                            Mes = valor.Mes,
        //                                            Anio = valor.Anio,
        //                                            Porcentaje = valor.Porcentaje
        //                                        }).Distinct().ToListAsync();

        //                    if (r.Rubro.Trim() == Rubro.Trim())
        //                    {
        //                        fechas_gasto.AddRange(fechas);
        //                        return fechas_gasto;
        //                    }
        //                }

        //            }
        //        }
        //    }

        //    return fechas_gasto;
        //}

        //public async Task<GastosIngresos_Detalle> GetGastosIngresos(int IdProyecto, string Tipo)
        //{
        //    GastosIngresos_Detalle proyecto_gastos_ingresos = new GastosIngresos_Detalle();

        //    using (var db = new ConnectionDB(dbConfig))
        //    {
        //        var proyecto = await (from p in db.tB_Proyectos
        //                              where p.NumProyecto == IdProyecto
        //                              select p).FirstOrDefaultAsync();

        //        proyecto_gastos_ingresos.NumProyecto = IdProyecto;
        //        proyecto_gastos_ingresos.FechaIni = proyecto?.FechaIni;
        //        proyecto_gastos_ingresos.FechaFin = proyecto?.FechaFin;


        //        var secciones = await (from seccion in db.tB_GastoIngresoSeccions
        //                               where seccion.Tipo == Tipo
        //                               orderby seccion.Codigo ascending
        //                               select new Seccion_Detalle
        //                               {
        //                                   IdSeccion = seccion.IdSeccion,
        //                                   Codigo = seccion.Codigo,
        //                                   Seccion = seccion.Seccion
        //                               })
        //                              .ToListAsync();

        //        proyecto_gastos_ingresos.Secciones = new List<Seccion_Detalle>();
        //        proyecto_gastos_ingresos.Secciones.AddRange(secciones);

        //        foreach (var seccion in secciones)
        //        {
        //            List<Rubro_Detalle> rubros = null;
        //            seccion.Rubros = new List<Rubro_Detalle>();

        //            if (seccion.IdSeccion == 2 || (Tipo == "ingreso" && seccion.IdSeccion == 8))
        //            {
        //                var etapas = await (from p in db.tB_ProyectoFases
        //                                    join proy in db.tB_Proyectos on p.NumProyecto equals proy.NumProyecto into proyJoin
        //                                    from proyItem in proyJoin.DefaultIfEmpty()
        //                                    where p.NumProyecto == IdProyecto
        //                                    orderby p.FechaIni ascending
        //                                    select new PCS_Etapa_Detalle
        //                                    {
        //                                        IdFase = p.IdFase,
        //                                        Orden = p.Orden,
        //                                        Fase = p.Fase,
        //                                        FechaIni = p.FechaIni,
        //                                        FechaFin = p.FechaFin
        //                                    }).ToListAsync();

        //                foreach (var etapa in etapas)
        //                {
        //                    rubros = await (from p in db.tB_ProyectoFaseEmpleados
        //                                    join e in db.tB_Empleados on p.NumEmpleado equals e.NumEmpleadoRrHh into eJoin
        //                                    from eItem in eJoin.DefaultIfEmpty()
        //                                    join per in db.tB_Personas on eItem.IdPersona equals per.IdPersona into perJoin
        //                                    from perItem in perJoin.DefaultIfEmpty()
        //                                    join costemple in db.tB_Costo_Por_Empleados on eItem.NumEmpleadoRrHh equals costemple.NumEmpleadoRrHh into costempleJoin
        //                                    from costempleItem in costempleJoin.DefaultIfEmpty()
        //                                    where p.IdFase == etapa.IdFase
        //                                    orderby p.NumEmpleado ascending
        //                                    group new Rubro_Detalle
        //                                    {
        //                                        Id = p.Id,
        //                                        IdRubro = perItem != null ? perItem.IdPersona : 0,
        //                                        Rubro = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
        //                                        Empleado = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
        //                                        NumEmpleadoRrHh = eItem != null ? eItem.NumEmpleadoRrHh : string.Empty,
        //                                        Cantidad = p.Fee,
        //                                        Reembolsable = (p.Fee == null || p.Fee == 0) ? false : true,
        //                                        CostoMensual = costempleItem.CostoMensualEmpleado
        //                                    } by new { p.NumEmpleado } into g
        //                                    select new Rubro_Detalle
        //                                    {
        //                                        Id = g.First().Id,
        //                                        IdRubro = g.First().IdRubro,
        //                                        Rubro = g.First().Rubro,
        //                                        Empleado = g.First().Empleado,
        //                                        NumEmpleadoRrHh = g.Key.NumEmpleado,
        //                                        Cantidad = g.First().Cantidad,
        //                                        Reembolsable = g.First().Reembolsable,
        //                                        CostoMensual = g.First().CostoMensual
        //                                    }).ToListAsync();

        //                    foreach (var rubro in rubros)
        //                    {
        //                        rubro.Fechas = new List<PCS_Fecha_Detalle>();
        //                        if (Tipo == "gasto")
        //                        {
        //                            var fechas = await (from p in db.tB_ProyectoFaseEmpleados
        //                                                where p.NumEmpleado == rubro.NumEmpleadoRrHh
        //                                                && p.IdFase == etapa.IdFase
        //                                                //ATC
        //                                                && p.Porcentaje > 0
        //                                                orderby p.Anio, p.Mes ascending
        //                                                select new PCS_Fecha_Detalle
        //                                                {
        //                                                    Id = p.Id,
        //                                                    Rubro = rubro.Rubro,
        //                                                    RubroReembolsable = rubro.Reembolsable,
        //                                                    Mes = p.Mes,
        //                                                    Anio = p.Anio,
        //                                                    Porcentaje = p.Porcentaje
        //                                                }).ToListAsync();

        //                            rubro.Fechas.AddRange(fechas);
        //                        }
        //                        else
        //                        {
        //                            rubro.Fechas.AddRange(await GetFechasGasto(IdProyecto, rubro.Rubro, rubro.Reembolsable));
        //                        }
        //                    }

        //                    seccion.Rubros.AddRange(rubros);
        //                }
        //            }
        //            else
        //            {
        //                rubros = await (from rubro in db.tB_Rubros
        //                                join rel1 in db.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
        //                                from rel1Item in rel1Join.DefaultIfEmpty()
        //                                join rel2 in db.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
        //                                where rubro.IdSeccion == seccion.IdSeccion
        //                                && rubro.NumProyecto == IdProyecto
        //                                && rel2.Tipo == Tipo
        //                                select new Rubro_Detalle
        //                                {
        //                                    Id = rubro.Id,
        //                                    IdRubro = rubro.IdRubro,
        //                                    Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
        //                                    Unidad = rubro.Unidad,
        //                                    Cantidad = rubro.Cantidad,
        //                                    Reembolsable = rubro.Reembolsable,
        //                                    AplicaTodosMeses = rubro.AplicaTodosMeses
        //                                }).ToListAsync();

        //                //seccion.Rubros = new List<Rubro_Detalle>();
        //                seccion.Rubros.AddRange(rubros);

        //                foreach (var rubro in rubros)
        //                {
        //                    rubro.Fechas = new List<PCS_Fecha_Detalle>();
        //                    if (Tipo == "gasto")
        //                    {
        //                        var fechas = await (from valor in db.tB_RubroValors
        //                                            join rub in db.tB_Rubros on valor.IdRubro equals rubro.Id
        //                                            join cat in db.tB_CatRubros on rub.IdRubro equals cat.IdRubro
        //                                            join sec in db.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
        //                                            where rub.NumProyecto == IdProyecto
        //                                            && sec.Tipo == Tipo
        //                                            //ATC
        //                                                && valor.Porcentaje > 0
        //                                            orderby valor.Anio, valor.Mes ascending
        //                                            select new PCS_Fecha_Detalle
        //                                            {
        //                                                Id = valor.Id,
        //                                                Rubro = rubro.Rubro,
        //                                                RubroReembolsable = rubro.Reembolsable,
        //                                                Mes = valor.Mes,
        //                                                Anio = valor.Anio,
        //                                                Porcentaje = valor.Porcentaje
        //                                            }).Distinct().ToListAsync();

        //                        rubro.Fechas.AddRange(fechas);
        //                    }
        //                    else
        //                    {
        //                        rubro.Fechas.AddRange(await GetFechasGasto(IdProyecto, rubro.Rubro, rubro.Reembolsable));
        //                    }
        //                }
        //            }

        //            // Agrupar y sumar los porcentajes por mes y año a nivel de sección
        //            var fechasAgrupadasSeccion = seccion.Rubros
        //                .SelectMany(r => r.Fechas)
        //                .GroupBy(f => new { f.Mes, f.Anio })
        //                .Select(g => new PCS_Fecha_Suma
        //                {
        //                    Mes = g.Key.Mes,
        //                    Anio = g.Key.Anio,
        //                    SumaPorcentaje = g.Sum(f => f.Porcentaje)
        //                }).ToList();

        //            seccion.SumaFechas = fechasAgrupadasSeccion;


        //            if (seccion.IdSeccion > 2)
        //            {
        //                foreach (var rubro in rubros)
        //                {
        //                    if (rubro.Unidad == "pp")
        //                    {
        //                        foreach (var fecha in rubro.Fechas)
        //                        {
        //                            foreach (var sumaFecha in proyecto_gastos_ingresos.Secciones[1].SumaFechas)
        //                            {
        //                                if (fecha.Mes == sumaFecha.Mes && fecha.Anio == sumaFecha.Anio)
        //                                {
        //                                    fecha.Porcentaje = rubro.Cantidad * (sumaFecha.SumaPorcentaje / 100);
        //                                }
        //                            }
        //                        }
        //                    }
        //                    else if (rubro.Unidad == "mes")
        //                    {
        //                        foreach (var fecha in rubro.Fechas)
        //                        {
        //                            fecha.Porcentaje = rubro.Cantidad;
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        if (Tipo == "ingreso")
        //        {
        //            // Calcular los Totales del Proyecto
        //            proyecto_gastos_ingresos.Totales = proyecto_gastos_ingresos.Secciones
        //                .SelectMany(s => s.Rubros)
        //                .SelectMany(r => r.Fechas, (r, f) => new { r.Reembolsable, f })
        //                .GroupBy(x => new { x.f.Mes, x.f.Anio, Reembolsable = x.Reembolsable ?? false })
        //                .Select(g => new PCS_Fecha_Totales
        //                {
        //                    Mes = g.Key.Mes,
        //                    Anio = g.Key.Anio,
        //                    Reembolsable = g.Key.Reembolsable,
        //                    TotalPorcentaje = g.Sum(x => x.f.Porcentaje)
        //                }).ToList();
        //        }


        //        return proyecto_gastos_ingresos;
        //    }
        //}

        private async Task<List<PCS_Fecha_Detalle>> GetFechasTotalesIngresos(int IdProyecto, string Rubro, List<Rubro_Detalle> rubros, List<Seccion_Detalle> secciones_gasto, List<PCS_Etapa_Detalle> etapas, bool? reembolsable = false)
        {
            var fechas_gasto = new List<PCS_Fecha_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                foreach (var seccion in secciones_gasto)
                {
                    seccion.Rubros = new List<Rubro_Detalle>();

                    if (seccion.IdSeccion == 2)
                    {
                        foreach (var etapa in etapas)
                        {
                            fechas_gasto = new List<PCS_Fecha_Detalle>();

                            foreach (var rubro in rubros)
                            {
                                var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                                    where p.NumEmpleado == rubro.NumEmpleadoRrHh
                                                    && p.IdFase == etapa.IdFase
                                                    orderby p.Anio, p.Mes ascending
                                                    select new PCS_Fecha_Detalle
                                                    {
                                                        Id = p.Id,
                                                        Rubro = rubro.Rubro,
                                                        RubroReembolsable = rubro.Reembolsable,
                                                        Mes = p.Mes,
                                                        Anio = p.Anio,
                                                        Porcentaje = p.Porcentaje
                                                    }).ToListAsync();

                                if (rubro.Rubro.Trim() == Rubro.Trim())
                                {
                                    fechas_gasto.AddRange(fechas);
                                    return fechas_gasto;
                                }
                            }
                        }
                    }
                    else
                    {
                        var rubros_gastos = rubros != null ? rubros.Where(x => x.Tipo != null && x.Tipo == "GASTO").ToList() : new List<Rubro_Detalle>();

                        fechas_gasto = new List<PCS_Fecha_Detalle>();

                        foreach (var rubro in rubros_gastos)
                        {
                            var fechas = await (from valor in db.tB_RubroValors
                                                join rub in db.tB_Rubros on valor.IdRubro equals rubro.Id
                                                join cat in db.tB_CatRubros on rub.IdRubro equals cat.IdRubro
                                                join sec in db.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
                                                where rub.NumProyecto == IdProyecto
                                                && sec.Tipo == "GASTO"
                                                && rub.Reembolsable == reembolsable
                                                orderby valor.Anio, valor.Mes ascending
                                                select new PCS_Fecha_Detalle
                                                {
                                                    Id = valor.Id,
                                                    RubroReembolsable = rub.Reembolsable,
                                                    Mes = valor.Mes,
                                                    Anio = valor.Anio,
                                                    Porcentaje = valor.Porcentaje
                                                }).Distinct().ToListAsync();

                            if (rubro.Rubro.Trim() == Rubro.Trim())
                            {
                                fechas_gasto.AddRange(fechas);
                                return fechas_gasto;
                            }
                        }
                    }
                }
            }

            return fechas_gasto;
        }

        public async Task<GastosIngresos_Detalle> GetTotalesIngresos(int IdProyecto)
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
                                       orderby seccion.Codigo ascending
                                       select new Seccion_Detalle
                                       {
                                           IdSeccion = seccion.IdSeccion,
                                           Codigo = seccion.Codigo,
                                           Seccion = seccion.Seccion,
                                           Tipo = seccion.Tipo
                                       })
                                       .ToListAsync();

                var secciones_ingreso = secciones.Where(x => x.Tipo == "INGRESO").ToList();
                var secciones_gasto = secciones.Where(x => x.Tipo == "GASTO").ToList();

                var etapas = await (from p in db.tB_ProyectoFases
                                    join proy in db.tB_Proyectos on p.NumProyecto equals proy.NumProyecto into proyJoin
                                    from proyItem in proyJoin.DefaultIfEmpty()
                                    where p.NumProyecto == IdProyecto
                                    orderby p.FechaIni ascending
                                    select new PCS_Etapa_Detalle
                                    {
                                        IdFase = p.IdFase,
                                        Orden = p.Orden,
                                        Fase = p.Fase,
                                        FechaIni = p.FechaIni,
                                        FechaFin = p.FechaFin
                                    }).ToListAsync();

                proyecto_gastos_ingresos.Secciones = new List<Seccion_Detalle>();
                proyecto_gastos_ingresos.Secciones.AddRange(secciones_ingreso);

                foreach (var seccion in secciones_ingreso)
                {
                    List<Rubro_Detalle> rubros = null;
                    seccion.Rubros = new List<Rubro_Detalle>();

                    if (seccion.IdSeccion == 8)
                    {
                        foreach (var etapa in etapas)
                        {
                            rubros = await (from p in db.tB_ProyectoFaseEmpleados
                                            join e in db.tB_Empleados on p.NumEmpleado equals e.NumEmpleadoRrHh into eJoin
                                            from eItem in eJoin.DefaultIfEmpty()
                                            join per in db.tB_Personas on eItem.IdPersona equals per.IdPersona into perJoin
                                            from perItem in perJoin.DefaultIfEmpty()
                                            join costemple in db.tB_Costo_Por_Empleados on eItem.NumEmpleadoRrHh equals costemple.NumEmpleadoRrHh into costempleJoin
                                            from costempleItem in costempleJoin.DefaultIfEmpty()
                                            where p.IdFase == etapa.IdFase
                                            orderby p.NumEmpleado ascending
                                            group new Rubro_Detalle
                                            {
                                                Id = p.Id,
                                                IdRubro = perItem != null ? perItem.IdPersona : 0,
                                                Rubro = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                                Empleado = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                                NumEmpleadoRrHh = eItem != null ? eItem.NumEmpleadoRrHh : string.Empty,
                                                Cantidad = p.Fee,
                                                Reembolsable = (p.Fee == null || p.Fee == 0) ? false : true,
                                                CostoMensual = costempleItem.CostoMensualEmpleado
                                            } by new { p.NumEmpleado } into g
                                            select new Rubro_Detalle
                                            {
                                                Id = g.First().Id,
                                                IdRubro = g.First().IdRubro,
                                                Rubro = g.First().Rubro ?? "",
                                                Empleado = g.First().Empleado ?? "",
                                                NumEmpleadoRrHh = g.Key.NumEmpleado ?? "",
                                                Cantidad = g.First().Cantidad ?? 0,
                                                Reembolsable = g.First().Reembolsable ?? false,
                                                CostoMensual = g.First().CostoMensual ?? 0
                                            }).ToListAsync();

                            foreach (var rubro in rubros)
                            {
                                rubro.Fechas = new List<PCS_Fecha_Detalle>();
                                rubro.Fechas.AddRange(await GetFechasTotalesIngresos(IdProyecto, rubro.Rubro, rubros, secciones_gasto, etapas, rubro.Reembolsable));
                            }

                            seccion.Rubros.AddRange(rubros);
                        }
                    }
                    else
                    {
                        rubros = await (from rubro in db.tB_Rubros
                                        join rel1 in db.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
                                        from rel1Item in rel1Join.DefaultIfEmpty()
                                        join rel2 in db.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
                                        where rubro.IdSeccion == seccion.IdSeccion
                                        && rubro.NumProyecto == IdProyecto
                                        select new Rubro_Detalle
                                        {
                                            Id = rubro.Id,
                                            IdRubro = rubro.IdRubro,
                                            Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
                                            Tipo = rel2.Tipo,
                                            Unidad = rubro.Unidad,
                                            Cantidad = rubro.Cantidad,
                                            Reembolsable = rubro.Reembolsable,
                                            AplicaTodosMeses = rubro.AplicaTodosMeses
                                        }).ToListAsync();

                        var rubros_ingresos = rubros.Where(x => x.Tipo == "INGRESO").ToList();
                        seccion.Rubros.AddRange(rubros_ingresos);

                        foreach (var rubro in rubros_ingresos)
                        {
                            rubro.Fechas = new List<PCS_Fecha_Detalle>();
                            rubro.Fechas.AddRange(await GetFechasTotalesIngresos(IdProyecto, rubro.Rubro, rubros, secciones_gasto, etapas, rubro.Reembolsable));
                        }
                    }

                    // Agrupar y sumar los porcentajes por mes y año a nivel de sección
                    var fechasAgrupadasSeccion = seccion.Rubros
                        .SelectMany(r => r.Fechas!)
                        .GroupBy(f => new { f.Mes, f.Anio })
                        .Select(g => new PCS_Fecha_Suma
                        {
                            Mes = g.Key.Mes,
                            Anio = g.Key.Anio,
                            SumaPorcentaje = g.Sum(f => f.Porcentaje)
                        }).ToList();

                    seccion.SumaFechas = fechasAgrupadasSeccion;

                    if (seccion.IdSeccion > 2 && rubros != null)
                    {
                        foreach (var rubro in rubros)
                        {
                            if (rubro.Unidad == "pp")
                            {
                                foreach (var fecha in rubro.Fechas!)
                                {
                                    foreach (var sumaFecha in proyecto_gastos_ingresos.Secciones![1].SumaFechas!)
                                    {
                                        if (fecha.Mes == sumaFecha.Mes && fecha.Anio == sumaFecha.Anio)
                                        {
                                            fecha.Porcentaje = rubro.Cantidad * (sumaFecha.SumaPorcentaje / 100);
                                        }
                                    }
                                }
                            }
                            else if (rubro.Unidad == "mes" && rubro.Fechas != null)
                            {
                                foreach (var fecha in rubro.Fechas)
                                {
                                    fecha.Porcentaje = rubro.Cantidad;
                                }
                            }
                        }
                    }
                }

                // Calcular los Totales del Proyecto
                proyecto_gastos_ingresos.Totales = proyecto_gastos_ingresos.Secciones
                    .SelectMany(s => s.Rubros!)
                    .SelectMany(r => r.Fechas!, (r, f) => new { r.Reembolsable, f })
                    .GroupBy(x => new { x.f.Mes, x.f.Anio, Reembolsable = x.Reembolsable ?? false })
                    .Select(g => new PCS_Fecha_Totales
                    {
                        Mes = g.Key.Mes,
                        Anio = g.Key.Anio,
                        Reembolsable = g.Key.Reembolsable,
                        TotalPorcentaje = g.Sum(x => x.f.Porcentaje)
                    }).ToList();


                // 1. Generar todos los meses entre inicio y fin
                DateTime? inicio = proyecto_gastos_ingresos.FechaIni;
                DateTime? fin = proyecto_gastos_ingresos.FechaFin;
                var mesesTotales = new List<(int Mes, int Anio)>();
                for (var fecha = new DateTime(inicio.Value.Year, inicio.Value.Month, 1);
                     fecha <= fin;
                     fecha = fecha.AddMonths(1))
                {
                    mesesTotales.Add((fecha.Month, fecha.Year));
                }
                // 2. Crear versiones completas con todos los meses para Reembolsable true y false
                var totalesCompletos = new List<PCS_Fecha_Totales>();
                foreach (var reembolsable in new[] { true, false })
                {
                    foreach (var (mes, anio) in mesesTotales)
                    {
                        var existente = proyecto_gastos_ingresos.Totales
                            .FirstOrDefault(t => t.Mes == mes && t.Anio == anio && t.Reembolsable == reembolsable);

                        totalesCompletos.Add(new PCS_Fecha_Totales
                        {
                            Mes = mes,
                            Anio = anio,
                            Reembolsable = reembolsable,
                            TotalPorcentaje = existente?.TotalPorcentaje ?? 0
                        });
                    }
                }
                // 3. Sobrescribir Totales con los completados
                proyecto_gastos_ingresos.Totales = totalesCompletos;

                var gruposPorReembolsable = proyecto_gastos_ingresos.Totales
                    .GroupBy(t => t.Reembolsable);




                // INGRESO (+0 desplazamiento)
                proyecto_gastos_ingresos.Ingreso = proyecto_gastos_ingresos.Totales;

                proyecto_gastos_ingresos.Facturacion ??= new List<PCS_Fecha_Totales>();
                proyecto_gastos_ingresos.Cobranza ??= new List<PCS_Fecha_Totales>();
                foreach (var grupo in gruposPorReembolsable)
                {
                    var totalesOrdenados = grupo
                        .OrderBy(t => t.Anio)
                        .ThenBy(t => t.Mes)
                        .ToList();

                    // FACTURACIÓN (+1 desplazamiento)
                    for (int i = 0; i < totalesOrdenados.Count; i++)
                    {
                        decimal? porcentaje = (i == 0) ? 0 : totalesOrdenados[i - 1].TotalPorcentaje;

                        proyecto_gastos_ingresos.Facturacion.Add(new PCS_Fecha_Totales
                        {
                            Mes = totalesOrdenados[i].Mes,
                            Anio = totalesOrdenados[i].Anio,
                            Reembolsable = totalesOrdenados[i].Reembolsable,
                            TotalPorcentaje = porcentaje
                        });
                    }

                    // COBRANZA (+2 desplazamiento)
                    for (int i = 0; i < totalesOrdenados.Count; i++)
                    {
                        decimal? porcentaje = (i < 2) ? 0 : totalesOrdenados[i - 2].TotalPorcentaje;

                        proyecto_gastos_ingresos.Cobranza.Add(new PCS_Fecha_Totales
                        {
                            Mes = totalesOrdenados[i].Mes,
                            Anio = totalesOrdenados[i].Anio,
                            Reembolsable = totalesOrdenados[i].Reembolsable,
                            TotalPorcentaje = porcentaje
                        });
                    }
                }


                // Limpia datos innecesarios
                proyecto_gastos_ingresos.Secciones = new List<Seccion_Detalle>();


                return proyecto_gastos_ingresos;
            }
        }



        public async Task<(bool Success, string Message)> UpdateGastosIngresos(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int numProyecto = Convert.ToInt32(registro["numProyecto"].ToString());
            int id_rubro = Convert.ToInt32(registro["idRubro"].ToString());
            int id_seccion = Convert.ToInt32(registro["idSeccion"].ToString());
            string unidad = registro["unidad"].ToString();
            decimal cantidad = Convert.ToDecimal(registro["cantidad"].ToString());
            bool reembolsable = Convert.ToBoolean(registro["reembolsable"].ToString());
            bool aplica_todos_meses = Convert.ToBoolean(registro["aplicaTodosMeses"].ToString());

            int rubro_record_id = 0;

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_rubro = await db.tB_Rubros
                    .Where(x => x.IdSeccion == id_seccion && x.IdRubro == id_rubro && x.NumProyecto == numProyecto && x.Reembolsable == reembolsable)
                    .UpdateAsync(x => new TB_Rubro
                    {
                        Unidad = unidad,
                        Cantidad = cantidad,
                        //Reembolsable = reembolsable,
                        AplicaTodosMeses = aplica_todos_meses
                    }) > 0;

                resp.Success = res_update_rubro;
                resp.Message = res_update_rubro == default ? "Ocurrio un error al actualizar registro." : string.Empty;

                if (res_update_rubro)
                {
                    var updatedRubroIds = await db.tB_Rubros
                        .Where(x => x.IdSeccion == id_seccion && x.IdRubro == id_rubro && x.NumProyecto == numProyecto && x.Reembolsable == reembolsable)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();

                    rubro_record_id = updatedRubroIds;
                } else
                {
                    var res_insert_rubro = await db.tB_Rubros
                        .Value(x => x.NumProyecto, numProyecto)
                        .Value(x => x.IdRubro, id_rubro)
                        .Value(x => x.IdSeccion, id_seccion)
                        .Value(x => x.Reembolsable, reembolsable)
                        .Value(x => x.Unidad, unidad)
                        .Value(x => x.Cantidad, cantidad)
                        .Value(x => x.AplicaTodosMeses, aplica_todos_meses)
                        .Value(x => x.Activo, true)
                        .InsertAsync();

                    rubro_record_id = res_insert_rubro;
                }

                var res_delete_valores = await db.tB_RubroValors.Where(x => x.IdRubro == rubro_record_id)
                    .DeleteAsync() > 0;

                resp.Success = res_delete_valores;
                resp.Message = res_delete_valores == default ? "Ocurrio un error al borrar registro." : string.Empty;

                foreach (var fecha in registro["fechas"].AsArray())
                {
                    int mes = Convert.ToInt32(fecha["mes"].ToString());
                    int anio = Convert.ToInt32(fecha["anio"].ToString());
                    decimal porcentaje = Convert.ToDecimal(fecha["porcentaje"].ToString());
                    int mesTranscurrido = Convert.ToInt32(fecha["mesTranscurrido"].ToString());
                    decimal porcent = 0;

                    porcent = (id_rubro != 2) ? porcentaje : (Math.Ceiling(Convert.ToDecimal(mesTranscurrido + 1 / 12)) * cantidad);

                    var res_insert_valor = await db.tB_RubroValors
                        .Value(x => x.IdRubro, rubro_record_id)
                        .Value(x => x.Mes, mes)
                        .Value(x => x.Anio, anio)
                        .Value(x => x.Porcentaje, porcent)
                        .Value(x => x.Activo, true)
                        .InsertAsync() > 0;

                    resp.Success = res_insert_valor;
                    resp.Message = res_insert_valor == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                }
            }

            return resp;
        }

        public async Task<GastosIngresos_Detalle> GetTotalFacturacion(int IdProyecto)
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

                Seccion_Detalle facturacion = new Seccion_Detalle();
                facturacion.Seccion = "Facturación";
                facturacion.Codigo = "FACT";
                Seccion_Detalle cobranza = new Seccion_Detalle();
                cobranza.Seccion = "Cobranza";
                cobranza.Codigo = "COBR";

                proyecto_gastos_ingresos.Secciones = new List<Seccion_Detalle>();
                proyecto_gastos_ingresos.Secciones.Add(facturacion);
                proyecto_gastos_ingresos.Secciones.Add(cobranza);

                foreach (var seccion in proyecto_gastos_ingresos.Secciones)
                {
                    seccion.SumaFechas = new List<PCS_Fecha_Suma>();

                    var intervalo_proyecto = await (from p in db.tB_Proyectos
                                                    where p.NumProyecto == IdProyecto
                                                    select new
                                                    {
                                                        p.FechaIni,
                                                        p.FechaFin
                                                    }).FirstOrDefaultAsync();

                    DateTime fechaActual = intervalo_proyecto!.FechaIni;
                    List<PCS_Fecha_Suma> fechas_proyecto = new List<PCS_Fecha_Suma>();
                    while (fechaActual <= intervalo_proyecto.FechaFin)
                    {
                        fechas_proyecto.Add(new PCS_Fecha_Suma
                        {
                            Mes = fechaActual.Month,
                            Anio = fechaActual.Year,
                            SumaPorcentaje = null
                        });

                        fechaActual = fechaActual.AddMonths(1);
                    }

                    List<PCS_Fecha_Suma> fechas_facturas = new List<PCS_Fecha_Suma>();
                    if (seccion.Seccion == "Facturación")
                    {
                        fechas_facturas = await (from fact in db.tB_ProyectoFacturas
                                                 where fact.NumProyecto == IdProyecto
                                                 && fact.FechaCancelacion == null
                                                 group fact by new { fact.Mes, fact.Anio } into g
                                                 select new PCS_Fecha_Suma
                                                 {
                                                     Mes = g.Key.Mes,
                                                     Anio = g.Key.Anio,
                                                     SumaPorcentaje = g.Sum(f => f.Total)
                                                 }).Distinct().ToListAsync();
                    }
                    else
                    {
                        fechas_facturas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                                 join fact in db.tB_ProyectoFacturas on cobr.UuidCobranza equals fact.Uuid into factJoin
                                                 from factItem in factJoin.DefaultIfEmpty()
                                                 where factItem.NumProyecto == IdProyecto
                                                 && factItem.FechaCancelacion == null
                                                 && cobr.FechaCancelacion == null
                                                 group cobr by new { cobr.FechaPago.Month, cobr.FechaPago.Year } into g
                                                 select new PCS_Fecha_Suma
                                                 {
                                                     Mes = g.Key.Month,
                                                     Anio = g.Key.Year,
                                                     SumaPorcentaje = g.Sum(f => f.ImportePagado)
                                                 }).ToListAsync();
                    }

                    foreach (var fecha_proyecto in fechas_proyecto)
                    {
                        foreach (var fecha_factura in fechas_facturas)
                        {
                            if (fecha_proyecto.Mes == fecha_factura.Mes && fecha_proyecto.Anio == fecha_factura.Anio)
                            {
                                fecha_proyecto.SumaPorcentaje = fecha_factura.SumaPorcentaje;
                            }
                        }
                    }

                    seccion.SumaFechas.AddRange(fechas_proyecto);
                }

            }

            return proyecto_gastos_ingresos;
        }
        #endregion Gastos / Ingresos



        #region Control
        public async Task<Control_Detalle> GetControl(int IdProyecto)
        {
            Control_Detalle control = new Control_Detalle();
            control.Salarios = new ControlRubro_Detalle();
            control.Viaticos = new ControlRubro_Detalle();
            control.Gastos = new Gasto_Detalle();

            using (var db = new ConnectionDB(dbConfig))
            {
                var secciones = await (from seccion in db.tB_GastoIngresoSeccions
                                       where seccion.Tipo == "gasto"
                                       && seccion.Activo == true
                                       orderby seccion.Codigo ascending
                                       select new Seccion_Detalle
                                       {
                                           IdSeccion = seccion.IdSeccion,
                                           Seccion = seccion.Seccion,
                                       })
                                      .ToListAsync();

                List<PCS_Fecha_Detalle> suma_fechas_salarios = new List<PCS_Fecha_Detalle>();
                List<PCS_Fecha_Detalle> suma_fechas_viaticos = new List<PCS_Fecha_Detalle>();
                List<Rubro_Detalle> rubros_viaticos = new List<Rubro_Detalle>();

                foreach (var seccion in secciones)
                {
                    if (seccion.Seccion == "COSTOS INDIRECTOS DE SALARIOS")
                    {
                        var rubros = await (from rubro in db.tB_Rubros
                                            join rel1 in db.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
                                            from rel1Item in rel1Join.DefaultIfEmpty()
                                            join rel2 in db.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
                                            where rubro.IdSeccion == seccion.IdSeccion
                                            && rubro.NumProyecto == IdProyecto
                                            && rel2.Tipo == "gasto"
                                            select new Rubro_Detalle
                                            {
                                                Id = rubro.Id,
                                                Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
                                            }).ToListAsync();

                        foreach (var rubro in rubros)
                        {
                            var fechas = await (from valor in db.tB_RubroValors
                                                join rub in db.tB_Rubros on valor.IdRubro equals rubro.Id
                                                join cat in db.tB_CatRubros on rub.IdRubro equals cat.IdRubro
                                                join sec in db.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
                                                join cie in db.tB_Cie_Datas on rub.NumProyecto equals cie.NumProyecto
                                                where rub.NumProyecto == IdProyecto
                                                && sec.Tipo == "gasto"
                                                orderby valor.Anio, valor.Mes ascending
                                                select new PCS_Fecha_Detalle
                                                {
                                                    Rubro = rubro.Rubro,
                                                    ClasificacionPY = cie.ClasificacionPY,
                                                    Mes = valor.Mes,
                                                    Anio = valor.Anio,
                                                    Porcentaje = valor.Porcentaje
                                                }).Distinct().ToListAsync();

                            suma_fechas_salarios.AddRange(fechas);
                        }
                    }
                    else if (seccion.Seccion == "VIÁTICOS")
                    {
                        rubros_viaticos = await (from rubro in db.tB_Rubros
                                                 join rel1 in db.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
                                                 from rel1Item in rel1Join.DefaultIfEmpty()
                                                 join rel2 in db.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
                                                 where rubro.IdSeccion == seccion.IdSeccion
                                                 && rubro.NumProyecto == IdProyecto
                                                 && rel2.Tipo == "gasto"
                                                 select new Rubro_Detalle
                                                 {
                                                     Id = rubro.Id,
                                                     Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
                                                 }).ToListAsync();

                        foreach (var rubro in rubros_viaticos)
                        {
                            var fechas = await (from valor in db.tB_RubroValors
                                                join rub in db.tB_Rubros on valor.IdRubro equals rubro.Id
                                                join cat in db.tB_CatRubros on rub.IdRubro equals cat.IdRubro
                                                join sec in db.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
                                                join cie in db.tB_Cie_Datas on rub.NumProyecto equals cie.NumProyecto
                                                where rub.NumProyecto == IdProyecto
                                                && sec.Tipo == "gasto"
                                                orderby valor.Anio, valor.Mes ascending
                                                select new PCS_Fecha_Detalle
                                                {
                                                    Rubro = rubro.Rubro,
                                                    ClasificacionPY = cie.ClasificacionPY,
                                                    Mes = valor.Mes,
                                                    Anio = valor.Anio,
                                                    Porcentaje = valor.Porcentaje
                                                }).Distinct().ToListAsync();

                            suma_fechas_viaticos.AddRange(fechas);
                        }
                    }
                }

                ////////////////
                ////SALARIOS////
                ////////////////
                var etapas = await (from p in db.tB_ProyectoFases
                                    join proy in db.tB_Proyectos on p.NumProyecto equals proy.NumProyecto into proyJoin
                                    from proyItem in proyJoin.DefaultIfEmpty()
                                    where p.NumProyecto == IdProyecto
                                    orderby p.FechaIni ascending
                                    select new PCS_Etapa_Detalle
                                    {
                                        IdFase = p.IdFase,
                                    }).ToListAsync();

                foreach (var etapa in etapas)
                {
                    var rubros = await (from p in db.tB_ProyectoFaseEmpleados
                                        join e in db.tB_Empleados on p.NumEmpleado equals e.NumEmpleadoRrHh into eJoin
                                        from eItem in eJoin.DefaultIfEmpty()
                                        join per in db.tB_Personas on eItem.IdPersona equals per.IdPersona into perJoin
                                        from perItem in perJoin.DefaultIfEmpty()
                                        where p.IdFase == etapa.IdFase
                                        orderby p.NumEmpleado ascending
                                        group new Rubro_Detalle
                                        {
                                            Id = p.Id,
                                            IdRubro = perItem != null ? perItem.IdPersona : 0,
                                            Rubro = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                            NumEmpleadoRrHh = eItem != null ? eItem.NumEmpleadoRrHh : string.Empty,
                                        } by new { p.NumEmpleado } into g
                                        select new Rubro_Detalle
                                        {
                                            Id = g.First().Id,
                                            IdRubro = g.First().IdRubro,
                                            Rubro = g.First().Rubro,
                                            NumEmpleadoRrHh = g.Key.NumEmpleado,
                                        }).ToListAsync();

                    foreach (var rubro in rubros)
                    {
                        var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                            where p.NumEmpleado == rubro.NumEmpleadoRrHh
                                            && p.IdFase == etapa.IdFase
                                            orderby p.Anio, p.Mes ascending
                                            select new PCS_Fecha_Detalle
                                            {
                                                Id = p.Id,
                                                Mes = p.Mes,
                                                Anio = p.Anio,
                                                Porcentaje = p.Porcentaje
                                            }).ToListAsync();

                        suma_fechas_salarios.AddRange(fechas);
                    }
                }

                var fechasSalariosAgrupadas = suma_fechas_salarios
                       .GroupBy(f => new { f.Mes, f.Anio })
                       .Select(g => new PCS_Fecha_Suma
                       {
                           Mes = g.Key.Mes,
                           Anio = g.Key.Anio,
                           SumaPorcentaje = g.Sum(f => f.Porcentaje)
                       }).ToList();

                control.Salarios.Previsto = new ValoresRubro_Detalle();
                control.Salarios.Previsto.SumaFechas = new List<PCS_Fecha_Suma>();
                control.Salarios.Previsto.SumaFechas.AddRange(fechasSalariosAgrupadas);
                control.Salarios.Previsto.SubTotal = fechasSalariosAgrupadas.Sum(f => Convert.ToDecimal(f.SumaPorcentaje));


                var cie_salarios = await (from cie in db.tB_Cie_Datas
                                          where cie.ClasificacionPY == "salarios"
                                          && cie.NumProyecto == IdProyecto
                                          select new PCS_Fecha_Detalle
                                          {
                                              ClasificacionPY = cie.ClasificacionPY,
                                              Mes = cie.Mes,
                                              Anio = cie.Fecha.Year,
                                              Porcentaje = cie.Movimiento
                                          }).ToListAsync();


                var cie_salarios_group = cie_salarios
                                    .GroupBy(c => new { c.Mes, c.Anio })
                                    .Select(g => new PCS_Fecha_Suma
                                    {
                                        Mes = g.Key.Mes,
                                        Anio = g.Key.Anio,
                                        SumaPorcentaje = g.Sum(c => c.Porcentaje)
                                    })
                                    .ToList();

                control.Salarios.Real = new ValoresRubro_Detalle();
                control.Salarios.Real.SumaFechas = new List<PCS_Fecha_Suma>();
                control.Salarios.Real.SumaFechas.AddRange(cie_salarios_group);
                control.Salarios.Real.SubTotal = cie_salarios_group.Sum(f => Convert.ToDecimal(f.SumaPorcentaje));



                ////////////////
                ////VIÁTICOS////
                ////////////////
                var fechasViaticosAgrupadas = suma_fechas_viaticos
                       .GroupBy(f => new { f.Rubro, f.Mes, f.Anio })
                       .Select(g => new PCS_Fecha_Suma
                       {
                           Rubro = g.Key.Rubro,
                           Mes = g.Key.Mes,
                           Anio = g.Key.Anio,
                           SumaPorcentaje = g.Sum(f => f.Porcentaje)
                       }).ToList();

                control.Viaticos.Previsto = new ValoresRubro_Detalle();
                control.Viaticos.Previsto.SumaFechas = new List<PCS_Fecha_Suma>();
                control.Viaticos.Previsto.SumaFechas.AddRange(fechasViaticosAgrupadas);
                control.Viaticos.Previsto.SubTotal = fechasViaticosAgrupadas.Sum(f => Convert.ToDecimal(f.SumaPorcentaje));

                List<PCS_Fecha_Detalle> cie_viaticos = new List<PCS_Fecha_Detalle>();
                foreach (var viatico in rubros_viaticos)
                {
                    var viatic = await (from cie in db.tB_Cie_Datas
                                        where cie.ClasificacionPY == viatico.Rubro
                                        && cie.NumProyecto == IdProyecto
                                        select new PCS_Fecha_Detalle
                                        {
                                            Rubro = viatico.Rubro,
                                            ClasificacionPY = cie.ClasificacionPY,
                                            Mes = cie.Mes,
                                            Anio = cie.Fecha.Year,
                                            Porcentaje = cie.Movimiento
                                        }).ToListAsync();

                    cie_viaticos.AddRange(viatic);
                }

                var cie_viaticos_group = cie_viaticos
                    .GroupBy(c => new { c.Rubro, c.Mes, c.Anio })
                    .Select(g => new PCS_Fecha_Suma
                    {
                        Rubro = g.Key.Rubro,
                        Mes = g.Key.Mes,
                        Anio = g.Key.Anio,
                        SumaPorcentaje = g.Sum(c => c.Porcentaje)
                    }).ToList();

                control.Viaticos.Real = new ValoresRubro_Detalle();
                control.Viaticos.Real.SumaFechas = new List<PCS_Fecha_Suma>();
                control.Viaticos.Real.SumaFechas.AddRange(cie_viaticos_group);
                control.Viaticos.Real.SubTotal = cie_viaticos_group.Sum(f => Convert.ToDecimal(f.SumaPorcentaje));

                ////////////////
                /////GASTOS/////
                ////////////////
                // Obtener los gastos previstos
                var fechasGastosPrevistos = suma_fechas_salarios
                    .Concat(suma_fechas_viaticos)
                    .GroupBy(f => new { f.Rubro, f.Mes, f.Anio })
                    .Select(g => new PCS_Fecha_Suma
                    {
                        Rubro = g.Key.Rubro,
                        Mes = g.Key.Mes,
                        Anio = g.Key.Anio,
                        SumaPorcentaje = g.Sum(f => f.Porcentaje)
                    }).ToList();

                // Asignar los gastos previstos al objeto de detalle de gastos
                control.Gastos.Previstos = new List<ValoresRubro_Detalle>
                {
                    new ValoresRubro_Detalle
                    {
                        SumaFechas = fechasGastosPrevistos,
                        SubTotal = fechasGastosPrevistos.Sum(f => Convert.ToDecimal(f.SumaPorcentaje))
                    }
                };

                // Obtener los gastos reales
                var fechasGastosReales = cie_salarios_group
                    .Concat(cie_viaticos_group)
                    .GroupBy(c => new { c.Rubro, c.Mes, c.Anio })
                    .Select(g => new PCS_Fecha_Suma
                    {
                        Rubro = g.Key.Rubro,
                        Mes = g.Key.Mes,
                        Anio = g.Key.Anio,
                        SumaPorcentaje = g.Sum(c => c.SumaPorcentaje)
                    }).ToList();

                // Asignar los gastos reales al objeto de detalle de gastos
                control.Gastos.Reales = new List<ValoresRubro_Detalle>
                {
                    new ValoresRubro_Detalle
                    {
                        SumaFechas = fechasGastosReales,
                        SubTotal = fechasGastosReales.Sum(f => Convert.ToDecimal(f.SumaPorcentaje))
                    }
                };



            }

            return control;
        }


        public async Task<Control_Data> GetSeccionControl(int IdProyecto, string Seccion)
        {
            Control_Data control = new Control_Data();
            control.Previsto = new Control_PrevistoReal();
            control.Previsto.Fechas = new List<Control_Fechas>();
            control.Real = new Control_PrevistoReal();
            control.Real.Fechas = new List<Control_Fechas>();
            control.Subsecciones = new List<Control_Subseccion>();
            control.Seccion = Seccion;

            using (var db = new ConnectionDB(dbConfig))
            {
                var secciones = await (from seccion in db.tB_GastoIngresoSeccions
                                       where seccion.Tipo == "gasto"
                                       && seccion.Activo == true
                                       orderby seccion.Codigo ascending
                                       select new Seccion_Detalle
                                       {
                                           IdSeccion = seccion.IdSeccion,
                                           Seccion = seccion.Seccion,
                                       })
                                      .ToListAsync();

                List<Control_Fechas> suma_fechas_salarios = new List<Control_Fechas>();
                List<Control_Fechas> suma_fechas_viaticos = new List<Control_Fechas>();
                List<Rubro_Detalle> rubros_viaticos = new List<Rubro_Detalle>();

                foreach (var seccion in secciones)
                {
                    if (seccion.Seccion == "COSTOS INDIRECTOS DE SALARIOS")
                    {
                        var rubros_costos_indirectos = await (from rubro in db.tB_Rubros
                                                              join rel1 in db.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
                                                              from rel1Item in rel1Join.DefaultIfEmpty()
                                                              join rel2 in db.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
                                                              where rubro.IdSeccion == seccion.IdSeccion
                                                              && rubro.NumProyecto == IdProyecto
                                                              && rel2.Tipo == "gasto"
                                                              select new Rubro_Detalle
                                                              {
                                                                  Id = rubro.Id,
                                                                  Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
                                                              }).ToListAsync();

                        foreach (var rubro in rubros_costos_indirectos)
                        {
                            var fechas = await (from valor in db.tB_RubroValors
                                                join rub in db.tB_Rubros on valor.IdRubro equals rubro.Id
                                                join cat in db.tB_CatRubros on rub.IdRubro equals cat.IdRubro
                                                join sec in db.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
                                                join cie in db.tB_Cie_Datas on rub.NumProyecto equals cie.NumProyecto into cieJoin
                                                from cieItem in cieJoin.DefaultIfEmpty()
                                                where rub.NumProyecto == IdProyecto
                                                && sec.Tipo == "gasto"
                                                && cieItem.ClasificacionPY == "Salarios"
                                                orderby valor.Anio, valor.Mes ascending
                                                select new Control_Fechas
                                                {
                                                    ClasificacionPY = cieItem.ClasificacionPY,
                                                    Mes = valor.Mes,
                                                    Anio = valor.Anio,
                                                    Porcentaje = valor.Porcentaje
                                                }).Distinct().ToListAsync();

                            suma_fechas_salarios.AddRange(fechas);
                        }
                    }
                    else if (seccion.Seccion == "VIÁTICOS")
                    {
                        rubros_viaticos = await (from rubro in db.tB_Rubros
                                                 join rel1 in db.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
                                                 from rel1Item in rel1Join.DefaultIfEmpty()
                                                 join rel2 in db.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
                                                 where rubro.IdSeccion == seccion.IdSeccion
                                                 && rubro.NumProyecto == IdProyecto
                                                 && rel2.Tipo == "gasto"
                                                 select new Rubro_Detalle
                                                 {
                                                     Id = rubro.Id,
                                                     Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
                                                 }).ToListAsync();

                        foreach (var rubro in rubros_viaticos)
                        {
                            var fechas = await (from valor in db.tB_RubroValors
                                                join rub in db.tB_Rubros on valor.IdRubro equals rubro.Id
                                                join cat in db.tB_CatRubros on rub.IdRubro equals cat.IdRubro
                                                join sec in db.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
                                                //join cie in db.tB_Cie_Datas on rub.NumProyecto equals cie.NumProyecto into cieJoin
                                                //from cieItem in cieJoin.DefaultIfEmpty()
                                                where rub.NumProyecto == IdProyecto
                                                && sec.Tipo == "gasto"
                                                orderby valor.Anio, valor.Mes ascending
                                                select new Control_Fechas
                                                {
                                                    ClasificacionPY = rubro.Rubro,
                                                    Mes = valor.Mes,
                                                    Anio = valor.Anio,
                                                    Porcentaje = valor.Porcentaje
                                                }).Distinct().ToListAsync();

                            suma_fechas_viaticos.AddRange(fechas);


                            // Add Prev Values To Subsections.
                            Control_Subseccion subseccion = new Control_Subseccion();
                            subseccion.Slug = GenerateSlug(rubro.Rubro);
                            subseccion.Seccion = rubro.Rubro;
                            subseccion.Previsto = new Control_PrevistoReal();
                            subseccion.Previsto.Fechas = new List<Control_Fechas>();
                            subseccion.Previsto.Fechas.AddRange(fechas);

                            var subseccionGroup = fechas
                               .GroupBy(f => new { f.Mes, f.Anio })
                               .Select(g => new Control_Fechas
                               {
                                   Mes = g.Key.Mes,
                                   Anio = g.Key.Anio,
                                   Porcentaje = g.Sum(f => f.Porcentaje)
                               }).ToList();

                            subseccion.Previsto.SubTotal = subseccionGroup.Sum(f => Convert.ToDecimal(f.Porcentaje));

                            control.Subsecciones.Add(subseccion);
                        }
                    }
                }

                switch (Seccion)
                {
                    case "sa_salarios":
                        control.HasChildren = false;

                        var etapas = await (from p in db.tB_ProyectoFases
                                            join proy in db.tB_Proyectos on p.NumProyecto equals proy.NumProyecto into proyJoin
                                            from proyItem in proyJoin.DefaultIfEmpty()
                                            where p.NumProyecto == IdProyecto
                                            orderby p.FechaIni ascending
                                            select new PCS_Etapa_Detalle
                                            {
                                                IdFase = p.IdFase,
                                            }).ToListAsync();

                        foreach (var etapa in etapas)
                        {
                            var rubros = await (from p in db.tB_ProyectoFaseEmpleados
                                                join e in db.tB_Empleados on p.NumEmpleado equals e.NumEmpleadoRrHh into eJoin
                                                from eItem in eJoin.DefaultIfEmpty()
                                                join per in db.tB_Personas on eItem.IdPersona equals per.IdPersona into perJoin
                                                from perItem in perJoin.DefaultIfEmpty()
                                                where p.IdFase == etapa.IdFase
                                                orderby p.NumEmpleado ascending
                                                group new Rubro_Detalle
                                                {
                                                    Id = p.Id,
                                                    IdRubro = perItem != null ? perItem.IdPersona : 0,
                                                    Rubro = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                                    NumEmpleadoRrHh = eItem != null ? eItem.NumEmpleadoRrHh : string.Empty,
                                                } by new { p.NumEmpleado } into g
                                                select new Rubro_Detalle
                                                {
                                                    Id = g.First().Id,
                                                    IdRubro = g.First().IdRubro,
                                                    Rubro = g.First().Rubro,
                                                    NumEmpleadoRrHh = g.Key.NumEmpleado,
                                                }).ToListAsync();

                            foreach (var rubro in rubros)
                            {
                                var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                                    where p.NumEmpleado == rubro.NumEmpleadoRrHh
                                                    && p.IdFase == etapa.IdFase
                                                    orderby p.Anio, p.Mes ascending
                                                    select new Control_Fechas
                                                    {
                                                        ClasificacionPY = rubro.Rubro,
                                                        Mes = p.Mes,
                                                        Anio = p.Anio,
                                                        Porcentaje = p.Porcentaje
                                                    }).ToListAsync();

                                suma_fechas_salarios.AddRange(fechas);
                            }
                        }

                        // Get Prev Values.
                        var fechasSalariosAgrupadas = suma_fechas_salarios
                               .GroupBy(f => new { f.Mes, f.Anio })
                               .Select(g => new Control_Fechas
                               {
                                   ClasificacionPY = g.First().ClasificacionPY,
                                   Mes = g.Key.Mes,
                                   Anio = g.Key.Anio,
                                   Porcentaje = g.Sum(f => f.Porcentaje)
                               }).ToList();

                        control.Previsto.SubTotal = fechasSalariosAgrupadas.Sum(f => Convert.ToDecimal(f.Porcentaje));
                        control.Previsto.Fechas.AddRange(fechasSalariosAgrupadas);

                        // Get Real Values.
                        var cie_salarios = await (from cie in db.tB_Cie_Datas
                                                  where cie.ClasificacionPY == "salarios"
                                                  && cie.NumProyecto == IdProyecto
                                                  orderby cie.Fecha.Year, cie.Mes ascending
                                                  select new Control_Fechas
                                                  {
                                                      ClasificacionPY = cie.ClasificacionPY,
                                                      Mes = cie.Mes,
                                                      Anio = cie.Fecha.Year,
                                                      Porcentaje = cie.Movimiento
                                                  }).ToListAsync();

                        var cie_salarios_group = cie_salarios
                                            .GroupBy(c => new { c.Mes, c.Anio })
                                            .Select(g => new Control_Fechas
                                            {
                                                ClasificacionPY = g.First().ClasificacionPY,
                                                Mes = g.Key.Mes,
                                                Anio = g.Key.Anio,
                                                Porcentaje = g.Sum(c => c.Porcentaje)
                                            })
                                            .ToList();

                        control.Real.SubTotal = cie_salarios_group.Sum(f => Convert.ToDecimal(f.Porcentaje));
                        control.Real.Fechas.AddRange(cie_salarios_group);
                        break;
                    case "va_viaticos":
                        control.HasChildren = true;

                        // Get Prev Values.
                        var fechasViaticosAgrupadas = suma_fechas_viaticos
                           .GroupBy(f => new { f.Mes, f.Anio })
                           .Select(g => new Control_Fechas
                           {
                               ClasificacionPY = g.First().ClasificacionPY,
                               Mes = g.Key.Mes,
                               Anio = g.Key.Anio,
                               Porcentaje = g.Sum(f => f.Porcentaje)
                           }).ToList();

                        control.Previsto.SubTotal = fechasViaticosAgrupadas.Sum(f => Convert.ToDecimal(f.Porcentaje));
                        control.Previsto.Fechas.AddRange(fechasViaticosAgrupadas);

                        // Get Real Values.
                        List<PCS_Fecha_Detalle> cie_viaticos = new List<PCS_Fecha_Detalle>();
                        foreach (var viatico in rubros_viaticos)
                        {
                            var viatic = await (from cie in db.tB_Cie_Datas
                                                where cie.ClasificacionPY == viatico.Rubro
                                                && cie.NumProyecto == IdProyecto
                                                group cie by new { cie.Fecha.Year, cie.Mes } into g
                                                orderby g.Key.Year, g.Key.Mes
                                                select new PCS_Fecha_Detalle
                                                {
                                                    Rubro = viatico.Rubro,
                                                    ClasificacionPY = viatico.Rubro,
                                                    Mes = g.Key.Mes,
                                                    Anio = g.Key.Year,
                                                    Porcentaje = g.Sum(x => x.Movimiento)
                                                }).ToListAsync();


                            cie_viaticos.AddRange(viatic);
                        }

                        var cie_viaticos_group = cie_viaticos
                            .GroupBy(c => new { c.Mes, c.Anio })
                            .Select(g => new Control_Fechas
                            {
                                //Rubro = g.Key.Rubro,
                                ClasificacionPY = g.First().ClasificacionPY,
                                Mes = g.Key.Mes,
                                Anio = g.Key.Anio,
                                Porcentaje = g.Sum(c => c.Porcentaje)
                            }).ToList();

                        control.Real.SubTotal = cie_viaticos_group.Sum(f => Convert.ToDecimal(f.Porcentaje));
                        control.Real.Fechas.AddRange(cie_viaticos_group);


                        // Add Real Values To Subsections.
                        foreach (var subsec in control.Subsecciones)
                        {
                            subsec.Real = new Control_PrevistoReal();
                            subsec.Real.Fechas = new List<Control_Fechas>();

                            foreach (var _viatic in cie_viaticos)
                            {
                                if (subsec.Seccion == _viatic.Rubro)
                                {
                                    Control_Fechas control_fechas = new Control_Fechas();
                                    control_fechas.ClasificacionPY = _viatic.ClasificacionPY;
                                    control_fechas.Mes = _viatic.Mes;
                                    control_fechas.Anio = _viatic.Anio;
                                    control_fechas.Porcentaje = _viatic.Porcentaje;
                                    subsec.Real.Fechas.Add(control_fechas);
                                }
                            }

                            var subseccionGroup = subsec.Real.Fechas
                                   .GroupBy(f => new { f.Rubro, f.Mes, f.Anio })
                                   .Select(g => new Control_Fechas
                                   {
                                       ClasificacionPY = g.First().ClasificacionPY,
                                       Mes = g.Key.Mes,
                                       Anio = g.Key.Anio,
                                       Porcentaje = g.Sum(f => f.Porcentaje)
                                   }).ToList();

                            subsec.Real.SubTotal = subseccionGroup.Sum(f => Convert.ToDecimal(f.Porcentaje));
                        }
                        break;
                    case "gga_condiciones_generales":
                        break;
                    case "gdpa_condiciones_generales":
                        break;
                    case "goa_gastos_overhead":
                        break;
                    case "ga_gastos":
                        var cie_salaries = await (from cie in db.tB_Cie_Datas
                                                  where cie.ClasificacionPY == "salarios"
                                                  && cie.NumProyecto == IdProyecto
                                                  select new PCS_Fecha_Detalle
                                                  {
                                                      Mes = cie.Mes,
                                                      Anio = cie.Fecha.Year,
                                                      Porcentaje = cie.Movimiento
                                                  }).ToListAsync();


                        var cie_salaries_group = cie_salaries
                                            .GroupBy(c => new { c.Rubro, c.Mes, c.Anio })
                                            .Select(g => new Control_Fechas
                                            {
                                                Rubro = g.Key.Rubro,
                                                Mes = g.Key.Mes,
                                                Anio = g.Key.Anio,
                                                Porcentaje = g.Sum(c => c.Porcentaje)
                                            })
                                            .ToList();

                        List<PCS_Fecha_Detalle> cie_viatics = new List<PCS_Fecha_Detalle>();
                        foreach (var viatico in rubros_viaticos)
                        {
                            var viatic = await (from cie in db.tB_Cie_Datas
                                                where cie.ClasificacionPY == viatico.Rubro
                                                && cie.NumProyecto == IdProyecto
                                                group cie by new { cie.Fecha.Year, cie.Mes } into g
                                                orderby g.Key.Year, g.Key.Mes
                                                select new PCS_Fecha_Detalle
                                                {
                                                    Rubro = viatico.Rubro,
                                                    Mes = g.Key.Mes,
                                                    Anio = g.Key.Year,
                                                    Porcentaje = g.Sum(x => x.Movimiento)
                                                }).ToListAsync();


                            cie_viatics.AddRange(viatic);
                        }

                        var cie_viatics_group = cie_viatics
                            .GroupBy(c => new { c.Rubro, c.Mes, c.Anio })
                            .Select(g => new Control_Fechas
                            {
                                Rubro = g.Key.Rubro,
                                Mes = g.Key.Mes,
                                Anio = g.Key.Anio,
                                Porcentaje = g.Sum(c => c.Porcentaje)
                            }).ToList();

                        // Obtener los gastos previstos
                        var fechasGastosPrevistos = suma_fechas_salarios
                            .Concat(suma_fechas_viaticos)
                            .GroupBy(f => new { f.Rubro, f.Mes, f.Anio })
                            .Select(g => new Control_Fechas
                            {
                                Rubro = g.Key.Rubro,
                                Mes = g.Key.Mes,
                                Anio = g.Key.Anio,
                                Porcentaje = g.Sum(f => f.Porcentaje)
                            }).ToList();

                        // Asignar los gastos previstos al objeto de detalle de gastos
                        control.Previsto = new Control_PrevistoReal
                        {
                            SubTotal = fechasGastosPrevistos.Sum(f => Convert.ToDecimal(f.Porcentaje)),
                            Fechas = fechasGastosPrevistos
                        };

                        // Obtener los gastos reales
                        var fechasGastosReales = cie_salaries_group
                                                .Concat(cie_viatics_group)
                                                .GroupBy(c => new { c.Rubro, c.Mes, c.Anio })
                                                .Select(g => new Control_Fechas
                                                {
                                                    Rubro = g.Key.Rubro,
                                                    Mes = g.Key.Mes,
                                                    Anio = g.Key.Anio,
                                                    Porcentaje = g.Sum(c => c.Porcentaje)
                                                }).ToList();


                        // Asignar los gastos reales al objeto de detalle de gastos
                        control.Real = new Control_PrevistoReal
                        {
                            SubTotal = fechasGastosReales.Sum(f => Convert.ToDecimal(f.Porcentaje)),
                            Fechas = fechasGastosReales

                        };
                        break;
                    case "f_ingresos":
                        break;
                    case "g_facturacion":
                        break;
                    case "h_bie_cie":
                        break;
                    case "cobranza":
                        break;
                    case "j_posicion_caja":
                        break;
                }

            }
            return control;
        }
        #endregion Control





        #region Ohter Functions
        private static string GenerateSlug(string input)
        {
            string slug = input.ToLowerInvariant();

            slug = RemoveDiacritics(slug);
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"[\s-]+", "_").Trim();

            return slug;
        }

        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
        #endregion Ohter Functions
    }
}
