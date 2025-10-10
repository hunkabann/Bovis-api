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
using Microsoft.IdentityModel.Tokens;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Xml.Linq;


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
                //LEO FIX alcance se comenta el código actual
                //var query = from p in db.tB_Proyectos select p;
                //query = OrdenAlfabetico == true || OrdenAlfabetico == null ? query.OrderBy(p => p.Proyecto) : query.OrderBy(p => p.NumProyecto);

                //var resp = await query.ToListAsync();

                //LEO I FIX alcance, se agrega este nuevo para manipular los datos que se regresan
                var query = from p in db.tB_Proyectos
                            select new TB_Proyecto
                            {
                                NumProyecto = p.NumProyecto,
                                Proyecto = p.Proyecto,
                                Alcance = p.Alcance == null ? "" : p.Alcance,
                                Cp = p.Cp,
                                Ciudad = p.Ciudad,
                                IdPais = p.IdPais,
                                IdEstatus = p.IdEstatus,
                                IdSector = p.IdSector,
                                IdTipoProyecto = p.IdTipoProyecto,
                                IdResponsablePreconstruccion = p.IdResponsablePreconstruccion,
                                IdResponsableConstruccion = p.IdResponsableConstruccion,
                                IdResponsableEhs = p.IdResponsableEhs,
                                IdResponsableSupervisor = p.IdResponsableSupervisor,
                                IdEmpresa = p.IdEmpresa,
                                IdDirectorEjecutivo = p.IdDirectorEjecutivo,
                                CostoPromedioM2 = p.CostoPromedioM2,
                                FechaIni = p.FechaIni,
                                FechaFin = p.FechaFin,
                                FechaAuditoriaInicial = p.FechaAuditoriaInicial,
                                FechaProxAuditoria = p.FechaProxAuditoria,
                                ResponsableAsignado = p.ResponsableAsignado,
                                ImpuestoNomina = p.ImpuestoNomina,
                                IdUnidadDeNegocio = p.IdUnidadDeNegocio
                            };

                // Aplicar ordenamiento condicional
                query = (OrdenAlfabetico == true || OrdenAlfabetico == null)
                    ? query.OrderBy(p => p.Proyecto)
                    : query.OrderBy(p => p.NumProyecto);

                // Ejecutar la consulta
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
                //LEO FIX 
                //var proyectos = await (from p in db.tB_Proyectos
                //                       where p.IdEstatus != 3
                //                       select p).ToListAsync();

                //LEO I FIX alcance, se agrega este nuevo para manipular los datos que se regresan
                var proyectos = await (from p in db.tB_Proyectos
                                       where p.IdEstatus != 3
                                       select new TB_Proyecto
                                       {
                                           NumProyecto = p.NumProyecto,
                                           Proyecto = p.Proyecto,
                                           Alcance = p.Alcance == null ? "" : p.Alcance,
                                           Cp = p.Cp,
                                           Ciudad = p.Ciudad,
                                           IdPais = p.IdPais,
                                           IdEstatus = p.IdEstatus,
                                           IdSector = p.IdSector,
                                           IdTipoProyecto = p.IdTipoProyecto,
                                           IdResponsablePreconstruccion = p.IdResponsablePreconstruccion,
                                           IdResponsableConstruccion = p.IdResponsableConstruccion,
                                           IdResponsableEhs = p.IdResponsableEhs,
                                           IdResponsableSupervisor = p.IdResponsableSupervisor,
                                           IdEmpresa = p.IdEmpresa,
                                           IdDirectorEjecutivo = p.IdDirectorEjecutivo,
                                           CostoPromedioM2 = p.CostoPromedioM2,
                                           FechaIni = p.FechaIni,
                                           FechaFin = p.FechaFin,
                                           FechaAuditoriaInicial = p.FechaAuditoriaInicial,
                                           FechaProxAuditoria = p.FechaProxAuditoria,
                                           ResponsableAsignado = p.ResponsableAsignado,
                                           ImpuestoNomina = p.ImpuestoNomina,
                                           IdUnidadDeNegocio = p.IdUnidadDeNegocio
                                       }).ToListAsync();
                //LEO F FIX Alcance

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
                                       //chalcance = proy.Alcance,
                                       chalcance = proy.Alcance == null ? "" : proy.Alcance,
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
            //            string alcance = registro["alcance"].ToString();
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

        /**
         * Se ocupara para obtener el nombre del proyecto y las fases en la misma estructura
         */
        public async Task<PCS_GanttData> GetPEtapas(int IdProyecto)
        {
            PCS_Proyecto_Detalle proyecto_etapas = new PCS_Proyecto_Detalle();


            using (var db = new ConnectionDB(dbConfig))
            {
                var proyecto = await (from p in db.tB_Proyectos
                                      where p.NumProyecto == IdProyecto
                                      select p).FirstOrDefaultAsync();

                proyecto_etapas.FechaIni = new DateTime(proyecto.FechaIni.Year, proyecto.FechaIni.Month, 1);

                // Ajustamos la FechaFin sumando un día
                if (proyecto?.FechaFin != null)
                {
                    proyecto_etapas.FechaFin = proyecto.FechaFin.Value.AddDays(1);
                }
                proyecto_etapas.NumProyecto = IdProyecto;
                proyecto_etapas.NombreProyecto = proyecto.Proyecto;

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
                //proyecto_etapas.Etapas.AddRange(etapas);
                var fases = new List<PCS_Etapa_Detalle>();
                PCS_Etapa_Detalle fase = new PCS_Etapa_Detalle();
                fase.IdFase = proyecto_etapas.NumProyecto;
                fase.Orden = -100;
                fase.Fase = proyecto_etapas.NombreProyecto;
                fase.FechaIni = proyecto_etapas.FechaIni ?? DateTime.Now;
                fase.FechaFin = proyecto_etapas.FechaFin ?? DateTime.Now;
                proyecto_etapas.Etapas.Add(fase);
                PCS_GanttData ganttData = new PCS_GanttData();
                PCS_GanttDataFase ganttDataFase = new PCS_GanttDataFase();
                List<PCS_GanttDataFase> data = new List<PCS_GanttDataFase>();
                List<string> equis = new List<string>();
                equis.Add(String.Format("{0:yyyy-MM-dd}", fase.FechaIni));
                equis.Add(String.Format("{0:yyyy-MM-dd}", fase.FechaFin));
                ganttDataFase.X = equis.ToArray();
                ganttDataFase.Y = fase.Fase;
                data.Add(ganttDataFase);

                foreach (var etapa in etapas)
                {
                    // Sumar un día a FechaFin si no es nulo
                    /*
                    if (etapa.FechaFin != null)
                    {
                        etapa.FechaFin = etapa.FechaFin.AddDays(1);
                    }
                    */

                    proyecto_etapas.Etapas.Add(etapa);

                    ganttDataFase = new PCS_GanttDataFase();
                    equis = new List<string>();
                    equis.Add(String.Format("{0:yyyy-MM-dd}", etapa.FechaIni));
                    equis.Add(String.Format("{0:yyyy-MM-dd}", etapa.FechaFin));
                    ganttDataFase.X = equis.ToArray();
                    ganttDataFase.Y = etapa.Fase;
                    data.Add(ganttDataFase);
                }
                ganttData.data = data;

                return ganttData;
            }

        }   // GetPEtapas

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
                                           && !p.NumEmpleado.Contains("|TBD") //LEO TBD que siga buscando los Empleados normales o con num empleado
                                           && p.Activo == true //LEO TBD
                                           orderby p.NumEmpleado ascending
                                           group new PCS_Empleado_Detalle
                                           {
                                               Id = p.Id,
                                               IdFase = p.IdFase,
                                               NumempleadoRrHh = p.NumEmpleado,
                                               Empleado = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                               Cantidad = p.Cantidad,
                                               AplicaTodosMeses = p.AplicaTodosMeses,
                                               Fee = p.Fee,
                                               Reembolsable = p.boreembolsable ?? false,
                                               NuCostoIni = p.nucosto_ini,
                                               ChAlias = p.chalias
                                               , EtiquetaTBD = "" //LEO TBD
                                               , IdPuesto = eItem.CvePuesto.ToString() //LEO TBD
                                           } by new { p.NumEmpleado } into g
                                           select new PCS_Empleado_Detalle
                                           {
                                               Id = g.First().Id,
                                               IdFase = g.First().IdFase,
                                               NumempleadoRrHh = g.Key.NumEmpleado,
                                               Empleado = g.First().Empleado,
                                               Cantidad = g.First().Cantidad,
                                               AplicaTodosMeses = g.First().AplicaTodosMeses,
                                               Fee = g.First().Fee,
                                               Reembolsable = g.First().Reembolsable,
                                               NuCostoIni = g.First().NuCostoIni,
                                               ChAlias = g.First().ChAlias
                                               , EtiquetaTBD = "" //LEO TBD
                                               , IdPuesto = g.First().IdPuesto //LEO TBD
                                           }).ToListAsync();

                    //LEO TBD I Para que busque los empleados que son TBD y pueda asignar la etiqueta como Nombre
                    var empleadosTBD = await (from p in db.tB_ProyectoFaseEmpleados
                                           join e in db.tB_Empleados on p.NumEmpleado equals e.NumEmpleadoRrHh into eJoin
                                           from eItem in eJoin.DefaultIfEmpty()
                                           join per in db.tB_Personas on eItem.IdPersona equals per.IdPersona into perJoin
                                           from perItem in perJoin.DefaultIfEmpty()
                                           where p.IdFase == etapa.IdFase
                                           && p.NumEmpleado.Contains("TBD") //LEO TBD
                                           && p.Activo == true //LEO TBD
                                           orderby p.NumEmpleado ascending
                                           group new PCS_Empleado_Detalle
                                           {
                                               Id = p.Id,
                                               IdFase = p.IdFase,
                                               NumempleadoRrHh = p.NumEmpleado,
                                               Empleado = p.etiqueta,
                                               Cantidad = p.Cantidad,
                                               AplicaTodosMeses = p.AplicaTodosMeses,
                                               Fee = p.Fee,
                                               Reembolsable = p.boreembolsable ?? false,
                                               NuCostoIni = p.nucosto_ini,
                                               ChAlias = p.chalias
                                               ,EtiquetaTBD = p.etiqueta
                                               ,IdPuesto = ""
                                           } by new { p.NumEmpleado } into g
                                           select new PCS_Empleado_Detalle
                                           {
                                               Id = g.First().Id,
                                               IdFase = g.First().IdFase,
                                               NumempleadoRrHh = g.Key.NumEmpleado,
                                               Empleado = g.First().Empleado,
                                               Cantidad = g.First().Cantidad,
                                               AplicaTodosMeses = g.First().AplicaTodosMeses,
                                               Fee = g.First().Fee,
                                               Reembolsable = g.First().Reembolsable,
                                               NuCostoIni = g.First().NuCostoIni,
                                               ChAlias = g.First().ChAlias
                                               ,EtiquetaTBD = g.First().EtiquetaTBD
                                               ,IdPuesto = GetNumPuesto(g.Key.NumEmpleado) //para que en el caso del TBD indique el idPuesto que está inmerso en el NumempleadoRrHh en la posición 2 (cero based)
                                           }).ToListAsync();

                    etapa.Empleados = new List<PCS_Empleado_Detalle>();
                    etapa.Empleados.AddRange(empleados);
                    etapa.Empleados.AddRange(empleadosTBD);//LEO TBD

                    foreach (var empleado in empleados)
                    {
                        var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                            where p.NumEmpleado == empleado.NumempleadoRrHh
                                            && p.IdFase == etapa.IdFase
                                            && p.Activo == true //LEO TBD
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

                    //LEO TBD I que asigne las fechas a los Empleados TBD
                    foreach (var empleado in empleadosTBD)
                    {
                        var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                            where p.NumEmpleado == empleado.NumempleadoRrHh
                                            && p.IdFase == etapa.IdFase
                                            && p.Activo == true //LEO TBD
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
                    //LEO TBD F
                }

                return proyecto_etapas;
            }
        }

        //LEO TBD I 
        private string GetNumPuesto(string numEmpleado)
        {
            var parts = (numEmpleado ?? "").Split('|');
            return parts.Length > 2 ? parts[2] : "";
        }
        //LEO TBD F

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
                var res_delete_empleado = await db.tB_ProyectoFaseEmpleados.Where(x => x.IdFase == IdEtapa && x.Activo == true)
                    .DeleteAsync() > 0; //LEO TBD se agrega el activo

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
            bool? reembolsable = registro["reembolsable"] != null ? Convert.ToBoolean(registro["reembolsable"].ToString()) : false;
            string? chalias = registro["chalias"] != null ? registro["chalias"].ToString() : null;
            decimal? nucosto_ini = registro["nucosto_ini"] != null ? Convert.ToDecimal(registro["nucosto_ini"].ToString()) : null;

            //LEO TBD I
            string etiqueta = registro["etiqueta"] != null ? registro["etiqueta"].ToString() : "";
            string puesto = registro["puesto"] != null ? registro["puesto"].ToString() : null;
            string num_empleadoTBD = "";

            if (num_empleado == "000" || num_empleado == "0")
            {
                num_empleadoTBD = String.Format("{0}|{1}|{2}|TBD", id_fase, num_proyecto, puesto);
                num_empleado = num_empleadoTBD;
            }
            //LEO TBD F

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
                        .Value(x => x.nucosto_ini, nucosto_ini)
                        .Value(x => x.chalias, chalias)
                        .Value(x => x.boreembolsable, reembolsable)
                        .Value(x => x.etiqueta, etiqueta) //LEO TBD
                        .Value(x => x.Activo, true) //LEO TBD
                        .InsertAsync() > 0;

                    resp.Success = res_insert_empleado;
                    resp.Message = res_insert_empleado == default ? "Ocurrio un error al insertar registro." : string.Empty;

                    // Se insertan los valores de los rubros, para gastos e ingresos.
                    var rubros = await (from rub in db.tB_Rubros
                                        where rub.NumProyecto == num_proyecto
                                        && rub.Activo == true //LEO TBD
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
                                       && p.Activo == true //LEO TBD
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
                                        && p.Activo == true //LEO TBD
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
            bool? aplica_todos_meses = registro["aplicaTodosMeses"] != null ? Convert.ToBoolean(registro["aplicaTodosMeses"].ToString()) : false;
            decimal? fee = registro["FEE"] != null ? Convert.ToDecimal(registro["FEE"].ToString()) : null;
            bool? reembolsable = registro["reembolsable"] != null ? Convert.ToBoolean(registro["reembolsable"].ToString()) : false;
            string chalias = registro["chalias"] != null ? registro["chalias"].ToString() : string.Empty;
            decimal? nucosto_ini = registro["nucosto_ini"] != null ? Convert.ToDecimal(registro["nucosto_ini"].ToString()) : null;

            //LEO TBD I
            int num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
            string etiqueta = registro["etiqueta"] != null ? registro["etiqueta"].ToString() : "";
            string puesto = registro["puesto"] != null ? registro["puesto"].ToString() : null;
            string num_empleadoTBD = registro["num_empleadoDesdeElPadre"] != null ? registro["num_empleadoDesdeElPadre"].ToString() : "";

            if (num_empleado == "000" || num_empleado == "0")
            {
                //num_empleadoTBD = String.Format("{0}|{1}|{2}|TBD", id_fase, num_proyecto, puesto);
                num_empleado = num_empleadoTBD;
            }
            //LEO TBD F


            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                //LEO TBD I
                if (num_empleadoTBD.Contains("|TBD") && num_empleadoTBD.Trim() != num_empleado.Trim())
                {
                    //si hace el reemplazo de un TBD por un empleado exixtente
                    //entonces que inactive el TBD, Activo  = 0 
                    //después seguirá con la inserción de lo nuevo
                    var res_update_empleado = await db.tB_ProyectoFaseEmpleados
                        .Where(x => x.IdFase == id_fase && x.NumEmpleado == num_empleadoTBD && x.Activo == true)
                        .UpdateAsync(x => new TB_ProyectoFaseEmpleado
                        {
                            Activo = false
                        }) > 0;//LEO TBD se agrega el activo
                }
                else { //LEO TBD F
                    //sino entonces que continúe como siempre eliminando e insertando
                    var res_delete_empleado = await db.tB_ProyectoFaseEmpleados.Where(x => x.IdFase == id_fase && x.NumEmpleado == num_empleado && x.Activo == true)
                        .DeleteAsync() > 0; //LEO TBD
                }//LEO TBD
                
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
                        .Value(x => x.nucosto_ini, nucosto_ini)
                        .Value(x => x.Fee, fee)
                        .Value(x => x.chalias, chalias)
                        .Value(x => x.boreembolsable, reembolsable)
                        .Value(x => x.etiqueta, etiqueta) //LEO TBD
                        .Value(x => x.Activo, true) //LEO TBD
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
                var res_delete_empleado = await db.tB_ProyectoFaseEmpleados.Where(x => x.IdFase == IdFase && x.NumEmpleado == NumEmpleado && x.Activo == true)
                    .DeleteAsync() > 0;//LEO TBD se agrega el activo

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
        private async Task<List<PCS_Fecha_Detalle>> GetFechasGasto(int IdProyecto, List<PCS_Etapa_Detalle> Fases, Seccion_Detalle Seccion, List<Rubro_Detalle> Rubros, string Rubro, bool? reembolsable = false)
        {
            var fechas_gasto = new List<PCS_Fecha_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                List<Rubro_Detalle> rubros = new List<Rubro_Detalle>();

                if (Seccion.IdSeccion == 2 || Seccion.IdSeccion == 8)
                {
                    foreach (var fase in Fases)
                    {
                        rubros = await (from p in db.tB_ProyectoFaseEmpleados
                                        join e in db.tB_Empleados on p.NumEmpleado equals e.NumEmpleadoRrHh into eJoin
                                        from eItem in eJoin.DefaultIfEmpty()
                                        join per in db.tB_Personas on eItem.IdPersona equals per.IdPersona into perJoin
                                        from perItem in perJoin.DefaultIfEmpty()
                                        where p.IdFase == fase.IdFase
                                        && p.Activo == true //LEO TBD
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
                                            Rubro = g.First().Rubro == null ? "" : g.First().Rubro, //LEO TBD
                                            Empleado = g.First().Empleado == null ? "" : g.First().Empleado, //LEO TBD
                                            NumEmpleadoRrHh = g.Key.NumEmpleado,
                                            Reembolsable = g.First().Reembolsable
                                        }).ToListAsync();

                        fechas_gasto = new List<PCS_Fecha_Detalle>();

                        foreach (var rubro in rubros)
                        {
                            if (!rubro.NumEmpleadoRrHh.IsNullOrEmpty())
                            {
                                var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                                    where p.NumEmpleado == rubro.NumEmpleadoRrHh
                                                    && p.IdFase == fase.IdFase
                                                    && p.Activo == true //LEO TBD
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
                                    //FIx I
                                    if (fechas == null)
                                    {
                                        fechas_gasto = new List<PCS_Fecha_Detalle>();
                                    }
                                    else
                                    {//FIx F
                                        fechas_gasto.AddRange(fechas);
                                    }
                                    return fechas_gasto;
                                }
                            }
                        }
                    }
                }
                else
                {
                    //rubros = await (from rubro in db.tB_Rubros
                    //                join rel1 in db.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
                    //                from rel1Item in rel1Join.DefaultIfEmpty()
                    //                join rel2 in db.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
                    //                where rubro.IdSeccion == Seccion.IdSeccion
                    //                && rubro.NumProyecto == IdProyecto
                    //                //&& rel2.Tipo == "GASTO"
                    //                //&& (reembolsable != null ? rubro.Reembolsable == reembolsable : rubro.Reembolsable == false)
                    //                select new Rubro_Detalle
                    //                {
                    //                    Id = rubro.Id,
                    //                    IdRubro = rubro.IdRubro,
                    //                    Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
                    //                    Unidad = rubro.Unidad,
                    //                    Cantidad = rubro.Cantidad,
                    //                    Reembolsable = rubro.Reembolsable,
                    //                    AplicaTodosMeses = rubro.AplicaTodosMeses
                    //                }).ToListAsync();

                    rubros = Rubros;

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
                            //FIx I
                            if (fechas == null)
                            {
                                fechas_gasto = new List<PCS_Fecha_Detalle>();
                            }
                            else
                            {
                                //FIx F
                                fechas_gasto.AddRange(fechas);
                            }
                            
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

                var seccion = await (from secc in db.tB_GastoIngresoSeccions
                                     where secc.Tipo == "GASTO" //Tipo.ToUpper()
                                     && secc.Seccion == Seccion

                                     select new Seccion_Detalle
                                     {
                                         IdSeccion = secc.IdSeccion,
                                         Codigo = secc.Codigo,
                                         Seccion = secc.Seccion
                                     }).FirstOrDefaultAsync();

                proyecto_gastos_ingresos.Secciones.Add(seccion!);

                //LEO TBD I 
                //consultando la información de TBD del proyecto y sus fases relacionadas
                // soloc uando sea gasto/costos directos de salarios
                bool boEsCostoDirecto = false;
                List<Rubro_Detalle_Apoyo> lstRubrosTBD = new List<Rubro_Detalle_Apoyo>();
                List<PCS_Fecha_Detalle_Apoyo> lstFechasTBD = new List<PCS_Fecha_Detalle_Apoyo>();

                if (Seccion.ToLower() == "costos directos de salarios")
                {
                    boEsCostoDirecto = true;
                    getProyectoFaseEmpleadoTBD(IdProyecto, Tipo, out lstRubrosTBD, out lstFechasTBD);
                }
                //LEO TBD F

                List<Rubro_Detalle> rubros = new List<Rubro_Detalle>();
                seccion!.Rubros = new List<Rubro_Detalle>();

                if ((seccion.IdSeccion == 2) || (Tipo == "ingreso" && seccion.IdSeccion == 8))
                {//LEO TBD gasto
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
                                        && costempleItem.RegHistorico == false
                                        && p.Activo == true //LEO TBD
                                        orderby p.NumEmpleado ascending
                                        group new Rubro_Detalle
                                        {
                                            Id = p.Id,
                                            IdRubro = perItem != null ? perItem.IdPersona : 0,
                                            Rubro = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                            Empleado = perItem != null && perItem.ApMaterno != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : perItem.Nombre + " " + perItem.ApPaterno,
                                            NumEmpleadoRrHh = eItem != null ? eItem.NumEmpleadoRrHh : string.Empty,
                                            Cantidad = p.Fee,
                                            Reembolsable = p.boreembolsable ?? false, //(p.Fee == null || p.Fee == 0) ? false : true,
                                            CostoMensual = Tipo == "ingreso" ? p.Fee : costempleItem.CostoMensualEmpleado
                                        } by new { perItem.IdPersona, p.NumEmpleado, p.boreembolsable } into g
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
                                if (!rubro.NumEmpleadoRrHh.IsNullOrEmpty())
                                {
                                    var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                                        where p.NumEmpleado == rubro.NumEmpleadoRrHh
                                                        && p.IdFase == fase.IdFase
                                                        && p.Porcentaje > 0
                                                        && p.Activo == true //LEO TBD
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
                            }
                            else
                            {//LEO TBD ingreso
                                rubro.Fechas.AddRange(await GetFechasGasto(IdProyecto, fases, seccion, rubros, rubro.Rubro, rubro.Reembolsable));
                            }
                        }

                        //aqui aqui
                        //LEO TBD I
                        //si es costo directo de salarios que agregue los TBD que encuentre para cada fase del proyecto
                        if (boEsCostoDirecto)
                        {
                            AgregaRubroTBD(ref lstRubrosTBD, ref rubros, ref lstFechasTBD, fase.IdFase);
                        }
                        //LEO TBD F

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
                                    && rubro.Activo == true //LEO
                                    && rubro.NumProyecto == IdProyecto
                                    && rel2.Tipo == "GASTO" //Tipo.ToUpper()
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
                                                && valor.Activo == true //LEO
                                                && rub.Activo == true //LEO
                                                && sec.Tipo == Tipo.ToUpper()
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
                            //var fechasGasto = await GetFechasGasto(IdProyecto, fases, seccion, rubros, rubro.Rubro, rubro.Reembolsable) ?? new List<PCS_Fecha_Detalle>();
                            //rubro!.Fechas!.AddRange(fechasGasto);

                            var fechas = await (from valor in db.tB_RubroValors
                                                join rub in db.tB_Rubros on valor.IdRubro equals rubro.Id
                                                join cat in db.tB_CatRubros on rub.IdRubro equals cat.IdRubro
                                                join sec in db.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
                                                where rub.NumProyecto == IdProyecto
                                                && valor.Activo == true //LEO
                                                && rub.Activo == true //LEO
                                                && sec.Tipo == "GASTO"
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
                    }
                }

                // Agrupar y sumar los porcentajes por mes y año a nivel de sección
                var fechasAgrupadasSeccion = seccion.Rubros
                    //.SelectMany(r => r.Fechas)
                    .Where(r => r?.Fechas != null)
                    .SelectMany(r => r.Fechas.Where(f => f != null))
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
                        //LEO, se comenta todo el if y se deja la asignación directa
                        //if (rubro.Unidad == "pp")
                        //{
                        //    foreach (var fecha in rubro.Fechas)
                        //    {
                        //        foreach (var sumaFecha in proyecto_gastos_ingresos.Secciones[0].SumaFechas)
                        //        {
                        //            if (fecha.Mes == sumaFecha.Mes && fecha.Anio == sumaFecha.Anio)
                        //            {
                        //                fecha.Porcentaje = rubro.Cantidad * (sumaFecha.SumaPorcentaje / 100);
                        //            }
                        //        }
                        //    }
                        //}
                        //else if (rubro.Unidad == "mes")
                        //{
                        //    foreach (var fecha in rubro.Fechas)
                        //    {
                        //        fecha.Porcentaje = rubro.Cantidad;
                        //    }
                        //}
                    }
                }

                if (Tipo == "ingreso")
                {
                    // Calcular los Totales del Proyecto
                    /*
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
                    */
                    proyecto_gastos_ingresos.Totales = proyecto_gastos_ingresos.Secciones
                        .Where(s => s.Rubros != null)
                        .SelectMany(s => s.Rubros)
                        .Where(r => r.Fechas != null)
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

        private async Task<List<PCS_Fecha_Detalle>> GetFechasTotalesIngresos(int IdProyecto, string Rubro, List<Rubro_Detalle> rubros, List<Seccion_Detalle> secciones_gasto, List<PCS_Etapa_Detalle> etapas, bool? reembolsable = true)
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
                                if (!rubro.NumEmpleadoRrHh.IsNullOrEmpty())
                                {
                                    var fechas = await (from p in db.tB_ProyectoFaseEmpleados
                                                        where p.NumEmpleado == rubro.NumEmpleadoRrHh
                                                        && p.IdFase == etapa.IdFase
                                                        && p.Activo == true //LEO TBD
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

        }   // GetFechasTotalesIngresos



        /**
         * Obtiene los totales del rubro ingreso (reembolsables y no reembolsables)
         */
        private List<PCS_Fecha_Totales> getTotalesRubroIngreso(int IdProyecto)
        {
            List<PCS_Fecha_Totales> retorno = new List<PCS_Fecha_Totales>();
            PCS_Fecha_Totales tot = null;

            string sQuery = "";
            bool boOk = false;

            var db = new ConnectionDB(dbConfig);
            sQuery = "sp_rubro_ingreso";

            // Create a new SqlConnection object
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(db.ConnectionString))
            {
                try
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sQuery, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    System.Data.SqlClient.SqlParameter param01 = new System.Data.SqlClient.SqlParameter("@nunum_proyecto", SqlDbType.Int);
                    param01.Direction = ParameterDirection.Input;
                    param01.Value = IdProyecto;
                    cmd.Parameters.Add(param01);

                    System.Data.SqlClient.SqlDataAdapter cda = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    con.Open();
                    DataTable dt = new DataTable();
                    cda.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        tot = new PCS_Fecha_Totales();
                        tot.Reembolsable = (row.Field<int>("reembolsable") == 1 ? true : false);
                        tot.Anio = row.Field<int>("anio");
                        tot.Mes = row.Field<int>("mes");
                        tot.TotalPorcentaje = Convert.ToDecimal(row.Field<double>("totalPorcentaje"));

                        //Console.WriteLine("porcentaje: " + tot.TotalPorcentaje);

                        retorno.Add(tot);
                    }

                    con.Close();

                    boOk = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    if (con != null && con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    db = null;
                }
            }

            //Console.WriteLine("retorno: " + retorno.Count);

            return retorno;

        }   // getTotalesRubroIngreso





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
                        // Primera parte: Suma de nuporcentaje desde tb_rubro_valor
                        var sumaRubroValor = await db.tB_RubroValors
                            .Where(rv =>
                                db.tB_Rubros
                                    .Where(r => r.NumProyecto == IdProyecto && r.Reembolsable == true && r.Activo == true)
                                    .Select(r => r.Id)
                                    .Contains(rv.IdRubro)
                                && rv.Mes == mes
                                && rv.Anio == anio
                                && rv.Porcentaje != null
                                && rv.Activo == true
                            )
                            .SumAsync(rv => (decimal?)rv.Porcentaje); // nullable para evitar error si no hay resultados

                        // Segunda parte: Suma de (porcentaje * fee) desde tb_proyecto_fase_empleado
                        var sumaFaseEmpleado = await db.tB_ProyectoFaseEmpleados
                            .Where(pfe =>
                                db.tB_ProyectoFases
                                    .Where(pf => pf.NumProyecto == IdProyecto)
                                    .Select(pf => pf.IdFase)
                                    .Contains(pfe.IdFase)
                                && pfe.Mes == mes
                                && pfe.Anio == anio
                                && pfe.boreembolsable == true
                                && pfe.Activo == true //LEO TBD
                            )
                            .SumAsync(pfe => ((decimal?)Math.Round((decimal)pfe.Porcentaje, 1) / 100 * pfe.Fee));

                        // Resultado total
                        var resultadoTotal = (sumaRubroValor ?? 0) + (sumaFaseEmpleado ?? 0);

                        totalesCompletos.Add(new PCS_Fecha_Totales
                        {
                            Mes = mes,
                            Anio = anio,
                            Reembolsable = reembolsable,
                            TotalPorcentaje = resultadoTotal
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

                // LDTF
                proyecto_gastos_ingresos.Totales = getTotalesRubroIngreso(IdProyecto);

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
            bool bogastosPP = false;//LEO
            string stanios = ""; //LEO

            //LEO
            // leyendo los anios del json
            var aFechas = registro["fechas"].AsArray();
            stanios = ObtieneAnios(aFechas);

            if (unidad.ToLower().Trim() == "pp")
            {
                //actualizando e insertando en tb_rubro y tb_rubro_valor
                bogastosPP = gastosPP(numProyecto, id_rubro, id_seccion, unidad, cantidad, reembolsable, aplica_todos_meses, stanios);
                resp.Success = bogastosPP;
                resp.Message = bogastosPP == false ? "Ocurrio un error al actualizar registro unidad pp." : string.Empty;
                return resp;
            }

            //LEO se actualiza lo que esta 
            bogastosPP = gastosPPGeneral(numProyecto, id_rubro, id_seccion, unidad, cantidad, reembolsable, aplica_todos_meses, stanios);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                //LEO se comenta para que no actualice los registros y ya solo inserte
                //var res_update_rubro = await db.tB_Rubros
                //    .Where(x => x.IdSeccion == id_seccion && x.IdRubro == id_rubro && x.NumProyecto == numProyecto && x.Reembolsable == reembolsable)
                //    .UpdateAsync(x => new TB_Rubro
                //    {
                //        Unidad = unidad,
                //        Cantidad = cantidad,
                //        //Reembolsable = reembolsable,
                //        AplicaTodosMeses = aplica_todos_meses
                //    }) > 0;

                //resp.Success = res_update_rubro;
                //resp.Message = res_update_rubro == default ? "Ocurrio un error al actualizar registro." : string.Empty;

                var res_update_rubro = false; //LEO

                if (res_update_rubro)
                {
                    var updatedRubroIds = await db.tB_Rubros
                        .Where(x => x.IdSeccion == id_seccion && x.IdRubro == id_rubro && x.NumProyecto == numProyecto && x.Reembolsable == reembolsable)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();

                    rubro_record_id = updatedRubroIds;
                }
                else
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

                    //LEO para recuperar el id insertado en tb_rubro
                    res_insert_rubro = await db.tB_Rubros
                        .Where(x => x.IdSeccion == id_seccion && x.IdRubro == id_rubro && x.NumProyecto == numProyecto && x.Reembolsable == reembolsable && x.Activo == true)
                        .Select(x => x.Id)
                        .FirstOrDefaultAsync();

                    rubro_record_id = res_insert_rubro;
                }

                //LEO se comenta para que no elimine
                //var res_delete_valores = await db.tB_RubroValors.Where(x => x.IdRubro == rubro_record_id)
                //    .DeleteAsync() > 0;

                //resp.Success = res_delete_valores;
                //resp.Message = res_delete_valores == default ? "Ocurrio un error al borrar registro." : string.Empty;

                foreach (var fecha in registro["fechas"].AsArray())
                {
                    int mes = Convert.ToInt32(fecha["mes"].ToString());
                    int anio = Convert.ToInt32(fecha["anio"].ToString());
                    decimal porcentaje = Convert.ToDecimal(fecha["porcentaje"].ToString());
                    int mesTranscurrido = Convert.ToInt32(fecha["mesTranscurrido"].ToString());
                    decimal porcent = 0;

                    //LEO I si es el mes y anio actual que tome el valor de cantidad para que no lo guarde en 0
                    if (mes == DateTime.Now.Month && anio == DateTime.Now.Year)
                    {
                        porcentaje = cantidad;
                    }
                    //LEO F

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

        //LEO
        /// <summary>
        /// Función que inactiva lo que existe del rubro y rubro_valor y después inserta lo nuevo en ambas tablas con estado activo.
        /// </summary>
        /// <param name="nunumProyecto">Identificador del proyecto</param>
        /// <param name="nuid_rubro">Identificador del rubro</param>
        /// <param name="nuid_seccion">Identificador de la sección</param>
        /// <param name="stunidad">Unidad</param>
        /// <param name="nucantidad">Cantidad</param>
        /// <param name="boreembolsable">1 es reembolsable \ 0 no es reembolsable</param>
        /// <param name="boaplica_todos_meses">1 aplica \ 0 no aplica</param>
        /// <param name="stanios"></param>
        /// <returns>true todo estuvo ok \ false si hubo algún error en el proceso</returns>
        public bool gastosPP(int nunumProyecto, int nuid_rubro, int nuid_seccion, string stunidad, decimal nucantidad, bool boreembolsable, bool boaplica_todos_meses, string stanios)
        {
            string sQuery = "";
            bool boOk = false;

            var db = new ConnectionDB(dbConfig);
            sQuery = "sp_gastos_pp";

            // Create a new SqlConnection object
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(db.ConnectionString))
            {
                try
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sQuery, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    System.Data.SqlClient.SqlParameter param01 = new System.Data.SqlClient.SqlParameter("@pnumProyecto", SqlDbType.Int);
                    param01.Direction = ParameterDirection.Input;
                    param01.Value = nunumProyecto;
                    cmd.Parameters.Add(param01);

                    System.Data.SqlClient.SqlParameter param02 = new System.Data.SqlClient.SqlParameter("@pid_rubro", SqlDbType.Int);
                    param02.Direction = ParameterDirection.Input;
                    param02.Value = nuid_rubro;
                    cmd.Parameters.Add(param02);

                    System.Data.SqlClient.SqlParameter param03 = new System.Data.SqlClient.SqlParameter("@pid_seccion", SqlDbType.Int);
                    param03.Direction = ParameterDirection.Input;
                    param03.Value = nuid_seccion;
                    cmd.Parameters.Add(param03);

                    System.Data.SqlClient.SqlParameter param04 = new System.Data.SqlClient.SqlParameter("@punidad", SqlDbType.VarChar, 3);
                    param04.Direction = ParameterDirection.Input;
                    param04.Value = stunidad;
                    cmd.Parameters.Add(param04);

                    System.Data.SqlClient.SqlParameter param05 = new System.Data.SqlClient.SqlParameter("@pcantidad", SqlDbType.Decimal);
                    param05.Direction = ParameterDirection.Input;
                    param05.Value = nucantidad;
                    cmd.Parameters.Add(param05);

                    System.Data.SqlClient.SqlParameter param06 = new System.Data.SqlClient.SqlParameter("@preembolsable", SqlDbType.Bit);
                    param06.Direction = ParameterDirection.Input;
                    param06.Value = boreembolsable;
                    cmd.Parameters.Add(param06);

                    System.Data.SqlClient.SqlParameter param07 = new System.Data.SqlClient.SqlParameter("@paplica_todos_meses", SqlDbType.Bit);
                    param07.Direction = ParameterDirection.Input;
                    param07.Value = boaplica_todos_meses;
                    cmd.Parameters.Add(param07);

                    System.Data.SqlClient.SqlParameter param08 = new System.Data.SqlClient.SqlParameter("@panios", SqlDbType.VarChar, 30);
                    param08.Direction = ParameterDirection.Input;
                    param08.Value = stanios;
                    cmd.Parameters.Add(param08);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    con.Close();

                    boOk = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    if (con != null && con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    db = null;
                }
            }

            return boOk;
        }   // gastosPP

        public bool gastosPPGeneral(int nunumProyecto, int nuid_rubro, int nuid_seccion, string stunidad, decimal nucantidad, bool boreembolsable, bool boaplica_todos_meses, string stanios)
        {
            string sQuery = "";
            bool boOk = false;

            var db = new ConnectionDB(dbConfig);
            sQuery = "sp_gastos_pp_general";

            // Create a new SqlConnection object
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(db.ConnectionString))
            {
                try
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sQuery, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    System.Data.SqlClient.SqlParameter param01 = new System.Data.SqlClient.SqlParameter("@pnumProyecto", SqlDbType.Int);
                    param01.Direction = ParameterDirection.Input;
                    param01.Value = nunumProyecto;
                    cmd.Parameters.Add(param01);

                    System.Data.SqlClient.SqlParameter param02 = new System.Data.SqlClient.SqlParameter("@pid_rubro", SqlDbType.Int);
                    param02.Direction = ParameterDirection.Input;
                    param02.Value = nuid_rubro;
                    cmd.Parameters.Add(param02);

                    System.Data.SqlClient.SqlParameter param03 = new System.Data.SqlClient.SqlParameter("@pid_seccion", SqlDbType.Int);
                    param03.Direction = ParameterDirection.Input;
                    param03.Value = nuid_seccion;
                    cmd.Parameters.Add(param03);

                    System.Data.SqlClient.SqlParameter param04 = new System.Data.SqlClient.SqlParameter("@punidad", SqlDbType.VarChar, 3);
                    param04.Direction = ParameterDirection.Input;
                    param04.Value = stunidad;
                    cmd.Parameters.Add(param04);

                    System.Data.SqlClient.SqlParameter param05 = new System.Data.SqlClient.SqlParameter("@pcantidad", SqlDbType.Decimal);
                    param05.Direction = ParameterDirection.Input;
                    param05.Value = nucantidad;
                    cmd.Parameters.Add(param05);

                    System.Data.SqlClient.SqlParameter param06 = new System.Data.SqlClient.SqlParameter("@preembolsable", SqlDbType.Bit);
                    param06.Direction = ParameterDirection.Input;
                    param06.Value = boreembolsable;
                    cmd.Parameters.Add(param06);

                    System.Data.SqlClient.SqlParameter param07 = new System.Data.SqlClient.SqlParameter("@paplica_todos_meses", SqlDbType.Bit);
                    param07.Direction = ParameterDirection.Input;
                    param07.Value = boaplica_todos_meses;
                    cmd.Parameters.Add(param07);

                    System.Data.SqlClient.SqlParameter param08 = new System.Data.SqlClient.SqlParameter("@panios", SqlDbType.VarChar, 30);
                    param08.Direction = ParameterDirection.Input;
                    param08.Value = stanios;
                    cmd.Parameters.Add(param08);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    con.Close();

                    boOk = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    if (con != null && con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    db = null;
                }
            }

            return boOk;
        }   // gastosPPGeneral

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

        //LEO TBD I
        private void getProyectoFaseEmpleadoTBD(int IdProyecto, string sTipo, out List<Rubro_Detalle_Apoyo> retorno, out List<PCS_Fecha_Detalle_Apoyo> retorno2)
        {
            retorno = new List<Rubro_Detalle_Apoyo>();
            retorno2 = new List<PCS_Fecha_Detalle_Apoyo>();

            string sQuery = "";
            bool boOk = false;

            var db = new ConnectionDB(dbConfig);
            sQuery = "sp_proyecto_fase_empleado_tbd";

            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(db.ConnectionString))
            {
                try
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sQuery, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    System.Data.SqlClient.SqlParameter param01 = new System.Data.SqlClient.SqlParameter("@pnunum_proyecto", SqlDbType.Int);
                    param01.Direction = ParameterDirection.Input;
                    param01.Value = IdProyecto;
                    cmd.Parameters.Add(param01);

                    System.Data.SqlClient.SqlParameter param02 = new System.Data.SqlClient.SqlParameter("@ptipo", SqlDbType.VarChar, 20);
                    param02.Direction = ParameterDirection.Input;
                    param02.Value = sTipo;
                    cmd.Parameters.Add(param02);

                    System.Data.SqlClient.SqlDataAdapter cda = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    con.Open();
                    DataSet ds = new DataSet();
                    cda.Fill(ds);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Rubro_Detalle_Apoyo oElemento = new Rubro_Detalle_Apoyo();
                        oElemento.Id = row.Field<int>("Id");
                        oElemento.IdRubro = row.Field<int>("IdRubro");
                        oElemento.Rubro = row.Field<string>("Rubro");
                        oElemento.Empleado = row.Field<string>("Empleado");
                        oElemento.NumEmpleadoRrHh = row.Field<string>("NumEmpleadoRrHh");
                        oElemento.Cantidad = Convert.ToDecimal(row.Field<decimal>("Cantidad"));
                        oElemento.Reembolsable = row.Field<bool>("Reembolsable");
                        oElemento.CostoMensual = Convert.ToDecimal(row.Field<decimal>("CostoMensual"));
                        oElemento.IdFase = row.Field<int>("IdFase");

                        retorno.Add(oElemento);
                    }

                    foreach (DataRow row in ds.Tables[1].Rows)
                    {
                        PCS_Fecha_Detalle_Apoyo oElemento2 = new PCS_Fecha_Detalle_Apoyo();
                        oElemento2.Id = row.Field<int>("Id");
                        oElemento2.Rubro = row.Field<string>("Rubro");
                        oElemento2.RubroReembolsable = row.Field<bool>("RubroReembolsable");
                        oElemento2.Mes = row.Field<int>("Mes");
                        oElemento2.Anio = row.Field<int>("Anio");
                        oElemento2.Porcentaje = Convert.ToDecimal(row.Field<int>("Porcentaje"));
                        oElemento2.IdFase = row.Field<int>("IdFase");
                        oElemento2.NumEmpleadoRrHh = row.Field<string>("NumEmpleadoRrHh");

                        retorno2.Add(oElemento2);
                    }

                    con.Close();

                    boOk = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    if (con != null && con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    db = null;
                }
            }

        }// getProyectoFaseEmpleadoTBD

        private void AgregaRubroTBD(ref List<Rubro_Detalle_Apoyo> lstEntrada, ref List<Rubro_Detalle> lstSalida, ref List<PCS_Fecha_Detalle_Apoyo> lstFechas, int IdFase)
        {
            string chCadena = "";

            int nuRegistros = lstEntrada.Where<Rubro_Detalle_Apoyo>(x => x.IdFase == IdFase).Count();
            if (nuRegistros > 0)
            {
                var lstEncontrado = lstEntrada.Where<Rubro_Detalle_Apoyo>(x => x.IdFase == IdFase).ToList();
                foreach (Rubro_Detalle_Apoyo oEncontrado in lstEncontrado)
                {
                    chCadena = JsonConvert.SerializeObject(oEncontrado);

                    Rubro_Detalle oNuevo = JsonConvert.DeserializeObject<Rubro_Detalle>(chCadena);
                    oNuevo.Fechas = new List<PCS_Fecha_Detalle>();//Fix 
                    AgregaRubroFechasTBD(ref lstFechas, ref oNuevo, IdFase);

                    lstSalida.Add(oNuevo);
                }
                
            }

            chCadena = "";
        }//AgregaRubroTBD

        private void AgregaRubroFechasTBD(ref List<PCS_Fecha_Detalle_Apoyo> lstEntrada, ref Rubro_Detalle rubro, int IdFase)
        {
            string chCadena = "";
            List<PCS_Fecha_Detalle> lstNuevasFechas = new List<PCS_Fecha_Detalle>();

            int nuRegistros = lstEntrada.Where<PCS_Fecha_Detalle_Apoyo>(x => x.IdFase == IdFase).Count();
            if (nuRegistros > 0)
            {
                var lstEncontrado = lstEntrada.Where<PCS_Fecha_Detalle_Apoyo>(x => x.IdFase == IdFase).ToList();
                foreach (PCS_Fecha_Detalle_Apoyo oElem in lstEncontrado)
                {
                    chCadena = JsonConvert.SerializeObject(oElem);
                    PCS_Fecha_Detalle oNuevo = JsonConvert.DeserializeObject<PCS_Fecha_Detalle>(chCadena);

                    lstNuevasFechas.Add(oNuevo);
                }

                //FIX I
                if (lstNuevasFechas == null || lstNuevasFechas.Count() == 0)
                {
                    rubro.Fechas = new List<PCS_Fecha_Detalle>();
                }
                else
                {
                    //FIX F
                    rubro.Fechas = lstNuevasFechas;
                }
                
            }
        }//AgregaRubroFechasTBD

        //LEO TBD F

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
                                        && p.Activo == true //LEO TBD
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
                                            && p.Activo == true //LEO TBD
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
                //LEO I
                int iSeccionGga = -1, iSeccionGdpa = -1, iSeccionGo = -1, iSeccionVia = -1;
                int iSalarios = -1;
                List<Rubro_Detalle> rubros_viaticosgga = new List<Rubro_Detalle>();
                List<Control_Fechas> suma_fechas_viaticosgga = new List<Control_Fechas>();
                List<Rubro_Detalle> rubros_viaticosgdpa = new List<Rubro_Detalle>();
                List<Control_Fechas> suma_fechas_viaticosgdpa = new List<Control_Fechas>();
                List<Rubro_Detalle> rubros_viaticosgo = new List<Rubro_Detalle>();
                List<Control_Fechas> suma_fechas_viaticosgo = new List<Control_Fechas>();
                //LEO F

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
                        iSeccionVia = seccion.IdSeccion;//LEO

                        //LEO I
                        
                        ////Se comenta la forma anterior de cómo obtener los datos de las subsecciones
                        //foreach (var grupo in fechasAgrupadasPorRubro)
                        //{
                        //    var rubroNombre = grupo.Key;
                        //    var fechasAgrupadasPorMes = grupo
                        //        .GroupBy(f => new { f.Mes, f.Anio })
                        //        .Select(g => new Control_Fechas
                        //        {
                        //            ClasificacionPY = rubroNombre,
                        //            Mes = g.Key.Mes,
                        //            Anio = g.Key.Anio,
                        //            Porcentaje = g.Sum(f => f.Porcentaje),
                        //            Rubro = rubroNombre
                        //        }).ToList();

                        //    var subseccion = new Control_Subseccion
                        //    {
                        //        Slug = GenerateSlug(rubroNombre),
                        //        Seccion = rubroNombre,
                        //        Previsto = new Control_PrevistoReal
                        //        {
                        //            Fechas = fechasAgrupadasPorMes,
                        //            SubTotal = fechasAgrupadasPorMes.Sum(f => Convert.ToDecimal(f.Porcentaje))
                        //        }
                        //    };

                        //    control.Subsecciones.Add(subseccion);
                        //}
                        //LEO F

                        //foreach (var rubro in rubros_viaticos)
                        //{
                        //    var fechas = await (from valor in db.tB_RubroValors
                        //                        join rub in db.tB_Rubros on valor.IdRubro equals rubro.Id
                        //                        join cat in db.tB_CatRubros on rub.IdRubro equals cat.IdRubro
                        //                        join sec in db.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
                        //                        //join cie in db.tB_Cie_Datas on rub.NumProyecto equals cie.NumProyecto into cieJoin
                        //                        //from cieItem in cieJoin.DefaultIfEmpty()
                        //                        where rub.NumProyecto == IdProyecto
                        //                        && sec.Tipo == "gasto"
                        //                        orderby valor.Anio, valor.Mes ascending
                        //                        select new Control_Fechas
                        //                        {
                        //                            ClasificacionPY = rubro.Rubro,
                        //                            Mes = valor.Mes,
                        //                            Anio = valor.Anio,
                        //                            Porcentaje = valor.Porcentaje
                        //                        }).Distinct().ToListAsync();

                        //    suma_fechas_viaticos.AddRange(fechas);

                        //    // Add Prev Values To Subsections.
                        //    Control_Subseccion subseccion = new Control_Subseccion();
                        //    subseccion.Slug = GenerateSlug(rubro.Rubro);
                        //    subseccion.Seccion = rubro.Rubro;
                        //    subseccion.Previsto = new Control_PrevistoReal();
                        //    subseccion.Previsto.Fechas = new List<Control_Fechas>();
                        //    subseccion.Previsto.Fechas.AddRange(fechas);

                        //    var subseccionGroup = fechas
                        //       .GroupBy(f => new { f.Mes, f.Anio })
                        //       .Select(g => new Control_Fechas
                        //       {
                        //           Mes = g.Key.Mes,
                        //           Anio = g.Key.Anio,
                        //           Porcentaje = g.Sum(f => f.Porcentaje)
                        //       }).ToList();

                        //    subseccion.Previsto.SubTotal = subseccionGroup.Sum(f => Convert.ToDecimal(f.Porcentaje));

                        //    control.Subsecciones.Add(subseccion);
                        //}
                    }
                    else if (seccion.Seccion.ToUpper() == "CONDICIONES GENERALES - GASTO GENERAL")//LEO
                    {
                        iSeccionGga = seccion.IdSeccion;
                    }
                    else if (seccion.Seccion.ToUpper() == "CONDICIONES GENERALES - GASTOS DIRECTOS DE PROYECTO")
                    {
                        iSeccionGdpa = seccion.IdSeccion;
                    }
                    else if (seccion.Seccion.ToUpper() == "GASTOS OVERHEAD")
                    {
                        iSeccionGo = seccion.IdSeccion;
                    }

                    //LEO F
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
                                                && p.Activo == true //LEO TBD
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
                                                    && p.Activo == true //LEO TBD
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
                        //LEO I se comenta para usar el llenado mediante el sp
                        //var fechasSalariosAgrupadas = suma_fechas_salarios
                        //       .GroupBy(f => new { f.Mes, f.Anio })
                        //       .Select(g => new Control_Fechas
                        //       {
                        //           ClasificacionPY = g.First().ClasificacionPY,
                        //           Mes = g.Key.Mes,
                        //           Anio = g.Key.Anio,
                        //           Porcentaje = g.Sum(f => f.Porcentaje)
                        //       }).ToList();

                        //control.Previsto.SubTotal = fechasSalariosAgrupadas.Sum(f => Convert.ToDecimal(f.Porcentaje));
                        //control.Previsto.Fechas.AddRange(fechasSalariosAgrupadas);

                        LlenaPrevistoSeccionSinHijos(ref control, IdProyecto);
                        //LEO F

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

                        rubros_viaticos = LlenaRubros(IdProyecto, iSeccionVia, db, "gasto");
                        suma_fechas_viaticos = LlenaSumFechas(ref rubros_viaticos, IdProyecto, db, "gasto");

                        // Ahora agrupamos las fechas por Rubro (Sección)
                        var fechasAgrupadasPorRubro = suma_fechas_viaticos
                            .GroupBy(f => f.Rubro)
                            .ToList();
                        LlenaSecciones(ref control, IdProyecto, iSeccionVia);

                        //llenando los nodos de real
                        LlenaPrvistoReal(ref control, IdProyecto, db, suma_fechas_viaticos, rubros_viaticos);
                        break;
                    case "gga_condiciones_generales":
                        //LEO I
                        control.HasChildren = true;

                        rubros_viaticosgga = LlenaRubros(IdProyecto, iSeccionGga, db, "gasto");
                        suma_fechas_viaticosgga = LlenaSumFechas(ref rubros_viaticosgga, IdProyecto, db, "gasto");

                        // Ahora agrupamos las fechas por Rubro (Sección)
                        var fechasAgrupadasPorRubrogga = suma_fechas_viaticosgga
                            .GroupBy(f => f.Rubro)
                            .ToList();
                        LlenaSecciones(ref control, IdProyecto, iSeccionGga);

                        //llenando los nodos de real
                        LlenaPrvistoReal(ref control, IdProyecto, db, suma_fechas_viaticosgga, rubros_viaticosgga);

                        //LEO F
                        break;
                    case "gdpa_condiciones_generales":
                        //LEO I
                        control.HasChildren = true;

                        rubros_viaticosgdpa = LlenaRubros(IdProyecto, iSeccionGdpa, db, "gasto");
                        suma_fechas_viaticosgdpa = LlenaSumFechas(ref rubros_viaticosgdpa, IdProyecto, db, "gasto");

                        // Ahora agrupamos las fechas por Rubro (Sección)
                        var fechasAgrupadasPorRubrogdpa = suma_fechas_viaticosgdpa
                            .GroupBy(f => f.Rubro)
                            .ToList();
                        LlenaSecciones(ref control, IdProyecto, iSeccionGdpa);

                        //llenando los nodos de real
                        LlenaPrvistoReal(ref control, IdProyecto, db, suma_fechas_viaticosgdpa, rubros_viaticosgdpa);

                        //LEO F
                        break;
                    case "goa_gastos_overhead":
                        //LEO I
                        control.HasChildren = true;

                        rubros_viaticosgo = LlenaRubros(IdProyecto, iSeccionGo, db, "gasto");
                        suma_fechas_viaticosgo = LlenaSumFechas(ref rubros_viaticosgo, IdProyecto, db, "gasto");

                        // Ahora agrupamos las fechas por Rubro (Sección)
                        var fechasAgrupadasPorRubrogo = suma_fechas_viaticosgo
                            .GroupBy(f => f.Rubro)
                            .ToList();
                        LlenaSecciones(ref control, IdProyecto, iSeccionGo);

                        //llenando los nodos de real
                        LlenaPrvistoReal(ref control, IdProyecto, db, suma_fechas_viaticosgo, rubros_viaticosgo);
                        //LEO F
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

        //LEO I
        public bool controlData(int nunumProyecto, int nuid_seccion, out DataSet ds)
        {
            ds = new DataSet();
            string sQuery = "";
            bool boOk = false;
            
            var db = new ConnectionDB(dbConfig);
            sQuery = "sp_control_data";

            // Create a new SqlConnection object
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(db.ConnectionString))
            {
                try
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sQuery, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    System.Data.SqlClient.SqlParameter param01 = new System.Data.SqlClient.SqlParameter("@nunum_proyecto", SqlDbType.Int);
                    param01.Direction = ParameterDirection.Input;
                    param01.Value = nunumProyecto;
                    cmd.Parameters.Add(param01);

                    System.Data.SqlClient.SqlParameter param03 = new System.Data.SqlClient.SqlParameter("@nukid_seccion", SqlDbType.Int);
                    param03.Direction = ParameterDirection.Input;
                    param03.Value = nuid_seccion;
                    cmd.Parameters.Add(param03);

                    con.Open();
                    System.Data.SqlClient.SqlDataAdapter sdaAdaptador = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    sdaAdaptador.Fill(ds);

                    sdaAdaptador.Dispose();
                    cmd.Dispose();
                    con.Close();

                    boOk = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    if (con != null && con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    db = null;
                }
            }

            return boOk;
        }   // controlData

        public bool controlSalarios(int nunumProyecto, out DataSet ds)
        {
            ds = new DataSet();
            string sQuery = "";
            bool boOk = false;

            var db = new ConnectionDB(dbConfig);
            sQuery = "sp_control_salarios";

            // Create a new SqlConnection object
            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(db.ConnectionString))
            {
                try
                {
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(sQuery, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    System.Data.SqlClient.SqlParameter param01 = new System.Data.SqlClient.SqlParameter("@nunum_proyecto", SqlDbType.Int);
                    param01.Direction = ParameterDirection.Input;
                    param01.Value = nunumProyecto;
                    cmd.Parameters.Add(param01);

                    con.Open();
                    System.Data.SqlClient.SqlDataAdapter sdaAdaptador = new System.Data.SqlClient.SqlDataAdapter(cmd);
                    sdaAdaptador.Fill(ds);

                    sdaAdaptador.Dispose();
                    cmd.Dispose();
                    con.Close();

                    boOk = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    if (con != null && con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    db = null;
                }
            }

            return boOk;
        }   // controlData

        private bool LlenaSecciones(ref Control_Data control,int nunumProyecto, int nuid_seccion)
        {
            bool boOk = false;
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            DataTable dtFechas = new DataTable();
            int iIdRubro = -1;
            string sNombre = "";
            string sPaso = "", sMensaje = "";

            try
            {
                //consultando los datos de la BD
                sPaso = "controlData";
                boOk = controlData(nunumProyecto, nuid_seccion, out ds);

                //crea lista de fechas de acuerdo a los resultados del sp
                sPaso = "AsignaTablas";
                dtFechas = ds.Tables[0];

                sPaso = "AsignaTablas1";
                dt = ds.Tables[1];

                sPaso = "SeccionesCiclo";
                foreach (DataRow oSubTotal in dt.Rows)
                {
                    iIdRubro = Convert.ToInt32(oSubTotal["nukid_rubro"].ToString());
                    List<Control_Fechas> lstFechas = new List<Control_Fechas>();
                    Control_Fechas oFechas;

                    DataTable dtFechasPorRubro = dtFechas.Rows.Cast<DataRow>().Where(x => x["nukid_rubro"].ToString() == iIdRubro.ToString()).CopyToDataTable();

                    foreach (DataRow oElem in dtFechasPorRubro.Rows)
                    {
                        sNombre = oElem["rubro"].ToString();

                        Control_Fechas oPrevisto = new Control_Fechas()
                        {
                            ClasificacionPY = sNombre,
                            Mes = Convert.ToInt32(oElem["mes"].ToString()),
                            Anio = Convert.ToInt32(oElem["anio"].ToString()),
                            Porcentaje = Convert.ToDecimal(oElem["porcentaje"].ToString()),
                            Rubro = sNombre
                        };

                        lstFechas.Add(oPrevisto);
                    }

                    var subseccion = new Control_Subseccion
                    {
                        Slug = GenerateSlug(sNombre),
                        Seccion = sNombre,
                        Previsto = new Control_PrevistoReal
                        {
                            Fechas = lstFechas,
                            SubTotal = Convert.ToDecimal(oSubTotal["subtotal"].ToString())
                        }
                    };

                    control.Subsecciones.Add(subseccion);

                }

                sPaso = "SeccionesOk";
                boOk = true;
            }
            catch (Exception ex)
            {
                sMensaje = String.Format("LlenaSecciones Paso:{0} Ex:{1}", sPaso, ex.Message);
                throw new Exception(sMensaje);
            }

            return boOk;
        }

        private List<Rubro_Detalle> LlenaRubros(int iIdProyecto, int iSeccion, ConnectionDB dbCon, string sTipo)
        {
            //List<Rubro_Detalle> rubros = new List<Rubro_Detalle>();
            var rubros = from rubro in dbCon.tB_Rubros
                                    join rel1 in dbCon.tB_CatRubros on rubro.IdRubro equals rel1.IdRubro into rel1Join
                                    from rel1Item in rel1Join.DefaultIfEmpty()
                                    join rel2 in dbCon.tB_GastoIngresoSeccions on rel1Item.IdSeccion equals rel2.IdSeccion
                                    where rubro.IdSeccion == iSeccion
                                    && rubro.NumProyecto == iIdProyecto
                                    && rel2.Tipo == sTipo
                                    && rubro.Activo == true
                           select new Rubro_Detalle
                                    {
                                        Id = rubro.Id,
                                        Rubro = rel1Item != null ? rel1Item.Rubro : string.Empty,
                                    };

            return rubros.ToList();
        }

        private List<Control_Fechas> LlenaSumFechas(ref List<Rubro_Detalle> rubros_viaticos,int iIdProyecto, ConnectionDB dbCon, string sTipo)
        {
            List<Control_Fechas> suma_fechas = new List<Control_Fechas>();

            foreach (var rubro in rubros_viaticos)
            {
                var fechas = from valor in dbCon.tB_RubroValors
                                   join rub in dbCon.tB_Rubros on valor.IdRubro equals rub.Id
                                   join cat in dbCon.tB_CatRubros on rub.IdRubro equals cat.IdRubro
                                   join sec in dbCon.tB_GastoIngresoSeccions on cat.IdSeccion equals sec.IdSeccion
                                   where rub.NumProyecto == iIdProyecto
                                         && sec.Tipo == sTipo
                                         && rub.Id == rubro.Id
                                         && valor.Activo == true
                                         && rub.Activo == true
                                   orderby valor.Anio, valor.Mes
                                   select new Control_Fechas
                                   {
                                       ClasificacionPY = rubro.Rubro,
                                       Mes = valor.Mes,
                                       Anio = valor.Anio,
                                       Porcentaje = valor.Porcentaje,
                                       Rubro = rubro.Rubro
                                   };

                suma_fechas.AddRange(fechas.ToList());
            }

            return suma_fechas;
        }

        private void LlenaPrvistoReal(ref Control_Data control, int nunumProyecto, ConnectionDB dbCon, List<Control_Fechas> suma_fechas, List<Rubro_Detalle> rubros)
        {
            // Get Prev Values.
            var fechasViaticosAgrupadasgga = suma_fechas
               .GroupBy(f => new { f.Mes, f.Anio })
               .Select(g => new Control_Fechas
               {
                   ClasificacionPY = g.First().ClasificacionPY,
                   Mes = g.Key.Mes,
                   Anio = g.Key.Anio,
                   Porcentaje = g.Sum(f => f.Porcentaje)
               }).ToList();

            control.Previsto.SubTotal = fechasViaticosAgrupadasgga.Sum(f => Convert.ToDecimal(f.Porcentaje));
            control.Previsto.Fechas.AddRange(fechasViaticosAgrupadasgga);

            // Get Real Values.
            List<PCS_Fecha_Detalle> cie_viaticosgga = new List<PCS_Fecha_Detalle>();
            foreach (var viatico in rubros)
            {
                var viatic = from cie in dbCon.tB_Cie_Datas
                                   where cie.ClasificacionPY == viatico.Rubro
                                   && cie.NumProyecto == nunumProyecto
                                   group cie by new { cie.Fecha.Year, cie.Mes } into g
                                   orderby g.Key.Year, g.Key.Mes
                                   select new PCS_Fecha_Detalle
                                   {
                                       Rubro = viatico.Rubro,
                                       ClasificacionPY = viatico.Rubro,
                                       Mes = g.Key.Mes,
                                       Anio = g.Key.Year,
                                       Porcentaje = g.Sum(x => x.Movimiento)
                                   };


                cie_viaticosgga.AddRange(viatic);
            }

            var cie_viaticosgga_group = cie_viaticosgga
                .GroupBy(c => new { c.Mes, c.Anio })
                .Select(g => new Control_Fechas
                {
                    //Rubro = g.Key.Rubro,
                    ClasificacionPY = g.First().ClasificacionPY,
                    Mes = g.Key.Mes,
                    Anio = g.Key.Anio,
                    Porcentaje = g.Sum(c => c.Porcentaje)
                }).ToList();

            control.Real.SubTotal = cie_viaticosgga_group.Sum(f => Convert.ToDecimal(f.Porcentaje));
            control.Real.Fechas.AddRange(cie_viaticosgga_group);


            // Add Real Values To Subsections.
            foreach (var subsec in control.Subsecciones)
            {
                subsec.Real = new Control_PrevistoReal();
                subsec.Real.Fechas = new List<Control_Fechas>();

                foreach (var _viatic in cie_viaticosgga)
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
        }

        private void LlenaPrevistoSeccionSinHijos(ref Control_Data control, int nunumProyecto)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            List<Control_Fechas> fechasAgrupadas = new List<Control_Fechas>();
            string sPaso = "", sMensaje = "";

            try
            {
                sPaso = "controlSalarios";
                controlSalarios(nunumProyecto, out ds);

                sPaso = "AsignaDt0";
                dt = ds.Tables[0];

                sPaso = "CicloSeccionSinHijos";
                foreach (DataRow oElem in dt.Rows)
                {
                    Control_Fechas oFecha = new Control_Fechas();
                    oFecha.ClasificacionPY = "";
                    oFecha.Mes = Convert.ToInt32(oElem["mes"].ToString());
                    oFecha.Anio = Convert.ToInt32(oElem["anio"].ToString());
                    oFecha.Porcentaje = Math.Round(Convert.ToDecimal(oElem["porcentaje"].ToString()),2);
                    oFecha.Rubro = "";

                    sPaso = String.Format("Proyecto:{0} Anio:{1} Mes:{2}", nunumProyecto, oFecha.Anio, oFecha.Mes);
                    fechasAgrupadas.Add(oFecha);

                }

                sPaso = "PrevistoSubTotalSinHijos";
                control.Previsto.SubTotal = Math.Round(Convert.ToDecimal(ds.Tables[1].Rows[0][0].ToString()), 2);

                sPaso = "PrevistoFechasSinHijos";
                control.Previsto.Fechas.AddRange(fechasAgrupadas);
            }
            catch (Exception ex)
            {
                sMensaje = String.Format("LlenaPrevistoSeccion Paso:{0} Ex:{1}", sPaso, ex.Message);
                throw new Exception(sMensaje);
            }
            
        }

        // LEO F

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

        private string ObtieneAnios(JsonArray jaEntrada)
        {
            string stanios = "", staniosapoyo = "", staniosactual = ""; //LEO
            int nuregistros = 0; //LEO

            nuregistros = jaEntrada.Count();

            foreach (var fecha in jaEntrada)
            {
                staniosactual = fecha["anio"].ToString();
                if (staniosapoyo != staniosactual)
                {
                    staniosapoyo = staniosactual;
                    stanios = String.Format("{0}{1}|", stanios, fecha["anio"].ToString());
                }
            }

            //quitando el último pipe antes de enviarlo a la BD
            stanios = stanios.Trim().Substring(0, stanios.Length - 1);

            return stanios;
        }

        #endregion Ohter Functions
    }
}


