using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace Bovis.Data
{
    public class AutorizacionData : RepositoryLinq2DB<ConnectionDB>, IAutorizacionData
    {
        #region base
        private readonly string dbConfig = "DBConfig";        

        public AutorizacionData()
        {
            this.ConfigurationDB = dbConfig;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base


        #region Usuarios
        public async Task<List<Usuario_Detalle>> GetUsuarios()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var usuarios = await (from usr in db.tB_Usuarios
                                      join emp in db.tB_Empleados on usr.NumEmpleadoRrHh equals emp.NumEmpleadoRrHh into empJoin
                                      from empItem in empJoin.DefaultIfEmpty()
                                      join per in db.tB_Personas on empItem.IdPersona equals per.IdPersona into perJoin
                                      from perItem in perJoin.DefaultIfEmpty()
                                      where usr.Activo == true
                                      orderby perItem.Nombre ascending
                                      select new Usuario_Detalle
                                      {
                                          IdUsuario = usr.IdUsuario,
                                          NumEmpleado = usr.NumEmpleadoRrHh,
                                          Empleado = perItem != null ? perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno : string.Empty,
                                          Activo = usr.Activo,
                                          UltimaSesion = usr.FechaUltimaSesion
                                      }).ToListAsync();

                return usuarios;
            }
        }

        public async Task<(bool Success, string Message)> AddUsuario(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            // string num_empleado = Convert.ToInt32(registro["num_empleado"].ToString());
            string num_empleado = (string)registro["num_empleado"];

            using (var db = new ConnectionDB(dbConfig))
            {
                var insert_usuario = await db.tB_Usuarios
                        .Value(x => x.NumEmpleadoRrHh, num_empleado)
                        .Value(x => x.Activo, true)
                        .InsertAsync() > 0;

                resp.Success = insert_usuario;
                resp.Message = insert_usuario == default ? "Ocurrio un error al agregar registro." : string.Empty;
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> DeleteUsuario(int idUsuario)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_delete_perfil_usuario = await db.tB_PerfilUsuarios.Where(x => x.IdUsuario == idUsuario)
                                .UpdateAsync(x => new TB_PerfilUsuario
                                {
                                    Activo = false
                                }) > 0;

                var res_delete_usuario = await db.tB_Usuarios.Where(x => x.IdUsuario == idUsuario)
                                .UpdateAsync(x => new TB_Usuario
                                {
                                    Activo = false
                                }) > 0;

                resp.Success = res_delete_usuario;
                resp.Message = res_delete_usuario == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }

        public async Task<Usuario_Perfiles_Detalle> GetUsuarioPerfiles(int idUsuario)
        {
            Usuario_Perfiles_Detalle usuario_perfiles = new Usuario_Perfiles_Detalle();
            List<Perfil_Detalle> perfiles = new List<Perfil_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                var usuario = await (from usr in db.tB_Usuarios
                                     join emp in db.tB_Empleados on usr.NumEmpleadoRrHh equals emp.NumEmpleadoRrHh into empJoin
                                     from empItem in empJoin.DefaultIfEmpty()
                                     join per in db.tB_Personas on empItem.IdPersona equals per.IdPersona into perJoin
                                     from perItem in perJoin.DefaultIfEmpty()
                                     where usr.IdUsuario == idUsuario
                                     select perItem.Nombre + " " + perItem.ApPaterno + " " + perItem.ApMaterno).FirstOrDefaultAsync();

                usuario_perfiles.IdUsuario = idUsuario;
                usuario_perfiles.Usuario = usuario;

                var usr_perfs = await (from usr_perf in db.tB_PerfilUsuarios
                                       where usr_perf.IdUsuario == idUsuario
                                       select usr_perf).ToListAsync();

                foreach (var usr_perf in usr_perfs)
                {
                    var perfil = await (from perf in db.tB_Perfils
                                        where perf.IdPerfil == usr_perf.IdPerfil
                                        select new Perfil_Detalle
                                        {
                                            IdPerfil = perf.IdPerfil,
                                            Perfil = perf.Perfil,
                                            Descripcion = perf.Descripcion,
                                            Activo = perf.Activo
                                        }).FirstOrDefaultAsync();

                    if(perfil != null)
                        perfiles.Add(perfil);
                }

                usuario_perfiles.Perfiles = perfiles.OrderBy(x => x.Perfil).ToList();

                return usuario_perfiles;
            }
        }

        public async Task<(bool Success, string Message)> UpdateUsuarioPerfiles(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_usuario = Convert.ToInt32(registro["id_usuario"].ToString());
            JsonArray perfiles = registro["perfiles"].AsArray();

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var delete_perfiles = await (db.tB_PerfilUsuarios.Where(x => x.IdUsuario == id_usuario)
                                            .DeleteAsync()) > 0;

                resp.Success = delete_perfiles;
                resp.Message = delete_perfiles == default ? "Ocurrio un error al actualizar registro." : string.Empty;

                foreach (var perfil in perfiles)
                {
                    var insert_perfil_usuario = await db.tB_PerfilUsuarios
                        .Value(x => x.IdPerfil, Convert.ToInt32(perfil.ToString()))
                        .Value(x => x.IdUsuario, id_usuario)
                        .InsertAsync() > 0;

                    resp.Success = insert_perfil_usuario;
                    resp.Message = insert_perfil_usuario == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                }                
            }

            return resp;
        }
        #endregion Usuarios


        #region Empleados
        public async Task<List<Empleado_BasicData>> GetEmpleadosNoUsuarios()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var usuarios = await (from emp in db.tB_Empleados
                                      join per in db.tB_Personas on emp.IdPersona equals per.IdPersona into perJoin
                                      from perItem in perJoin.DefaultIfEmpty()
                                      join pue in db.tB_Cat_Puestos on emp.CvePuesto equals pue.IdPuesto into pueJoin
                                      from pueItem in pueJoin.DefaultIfEmpty()
                                      join usr in db.tB_Usuarios on emp.NumEmpleadoRrHh equals usr.NumEmpleadoRrHh into usrJoin
                                      from usrItem in usrJoin.DefaultIfEmpty()
                                      where usrItem != null
                                      orderby perItem.Nombre ascending
                                      select new Empleado_BasicData
                                      {
                                          nukid_empleado = emp.NumEmpleadoRrHh,
                                          chnombre = perItem.Nombre ?? string.Empty,
                                          chap_paterno = perItem.ApPaterno ?? string.Empty,
                                          chap_materno = perItem.ApMaterno ?? string.Empty,
                                          chpuesto = pueItem.Puesto ?? string.Empty,
                                          chemailbovis = emp.EmailBovis
                                      }).ToListAsync();

                return usuarios;
            }
        }
        #endregion Empleados


        #region Módulos
        public async Task<List<Modulo_Detalle>> GetModulos()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var modulos = await (from mod in db.tB_Modulos
                                     where mod.Activo == true
                                     orderby mod.Modulo ascending
                                     select new Modulo_Detalle
                                     {
                                         IdModulo = mod.IdModulo,
                                         Modulo = mod.Modulo,
                                         Activo = mod.Activo
                                     }).ToListAsync();

                modulos = modulos.GroupBy(mod => mod.Modulo)
                                            .Select(group => group.First())
                                            .ToList();

                foreach (var modulo in modulos)
                {
                    var submodulos = await (from sub in db.tB_Modulos
                                            where sub.Activo == true
                                            && sub.Modulo == modulo.Modulo
                                            orderby sub.SubModulo ascending
                                            select new Submodulo_Detalle
                                            {
                                                IdSubmodulo = sub.IdModulo,
                                                SubModulo = sub.SubModulo,
                                                Activo = sub.Activo
                                            }).ToListAsync();

                    submodulos = submodulos.GroupBy(sub => sub.SubModulo)
                                                .Select(group => group.First())
                                                .ToList();

                    modulo.Submodulos = new List<Submodulo_Detalle>();
                    modulo.Submodulos.AddRange(submodulos);

                    foreach (var submodulo in submodulos)
                    {
                        var tabs = await (from tab in db.tB_Modulos
                                          where tab.Activo == true
                                          && tab.IsTab == true
                                          && tab.SubModulo == submodulo.SubModulo
                                          orderby tab.SubModulo ascending
                                          select new Tab_Detalle
                                          {
                                              IdTab = tab.IdModulo,
                                              Tab = tab.Tab,
                                              IsTab = tab.IsTab,
                                              Activo = tab.Activo
                                          }).ToListAsync();                        

                        submodulo.Tabs = new List<Tab_Detalle>();
                        submodulo.Tabs.AddRange(tabs);
                    }
                }


                return modulos;
            }
        }

        public async Task<Modulo_Perfiles_Detalle> GetModuloPerfiles(int idModulo)
        {
            Modulo_Perfiles_Detalle modulo_perfiles = new Modulo_Perfiles_Detalle();
            List<Perfil_Detalle> perfiles = new List<Perfil_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                var modulo = await (from mod in db.tB_Modulos
                                    where mod.IdModulo == idModulo
                                    select mod).FirstOrDefaultAsync();

                modulo_perfiles.IdModulo = idModulo;
                modulo_perfiles.Modulo = modulo.Modulo;
                modulo_perfiles.SubModulo = modulo.SubModulo;

                var mod_perfs = await (from mod_perf in db.tB_PerfilModulos
                                    where mod_perf.IdModulo == idModulo
                                    select mod_perf).ToListAsync();

                
                foreach (var mod_perf in mod_perfs)
                {
                    var perfil = await (from perf in db.tB_Perfils
                                      where perf.IdPerfil == mod_perf.IdPerfil
                                      select new Perfil_Detalle
                                      {
                                          IdPerfil = perf.IdPerfil,
                                          Perfil = perf.Perfil,
                                          Descripcion = perf.Descripcion,
                                          Activo = perf.Activo
                                      }).FirstOrDefaultAsync();

                    if(perfil != null)
                        perfiles.Add(perfil);
                }     
                
                modulo_perfiles.Perfiles = perfiles.OrderBy(x => x.Perfil).ToList();               

                return modulo_perfiles;
            }
        }
        #endregion Módulos


        #region Perfiles
        public async Task<List<Perfil_Detalle>> GetPerfiles()
        {            
            using (var db = new ConnectionDB(dbConfig))
            {
                var perfiles = await (from perf in db.tB_Perfils
                                     where perf.Activo == true
                                     orderby perf.Perfil ascending
                                     select new Perfil_Detalle
                                     {
                                         IdPerfil = perf.IdPerfil,
                                         Perfil = perf.Perfil,
                                         Descripcion = perf.Descripcion,
                                         Activo = perf.Activo
                                     }).ToListAsync();

                return perfiles;
            }
        }

        public async Task<Perfil_Detalle> AddPerfil(JsonObject registro)
        {
            string perfil = registro["perfil"].ToString();
            string descripcion = registro["descripcion"].ToString();

            using (var db = new ConnectionDB(dbConfig))
            {
                var insert_perfil = await db.tB_Perfils
                        .Value(x => x.Perfil, perfil)
                        .Value(x => x.Descripcion, descripcion)
                        .Value(x => x.Activo, true)
                        .InsertWithIdentityAsync();

                var newPerfil = new Perfil_Detalle
                {
                    IdPerfil = Convert.ToInt32(insert_perfil),
                    Perfil = perfil,
                    Descripcion = descripcion,
                    Activo = true
                };

                return newPerfil;
            }
        }

        public async Task<(bool Success, string Message)> DeletePerfil(int idPerfil)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var delete_perfil_modulo = await (db.tB_PerfilModulos.Where(x => x.IdPerfil == idPerfil)
                                            .DeleteAsync()) > 0;

                var delete_perfil_permiso = await (db.tB_PerfilPermisos.Where(x => x.IdPerfil == idPerfil)
                                            .DeleteAsync()) > 0;

                var delete_perfil_usuario = await (db.tB_PerfilUsuarios.Where(x => x.IdPerfil == idPerfil)
                                            .DeleteAsync()) > 0;

                var res_delete_perfil = await db.tB_Perfils.Where(x => x.IdPerfil == idPerfil)
                                            .UpdateAsync(x => new TB_Perfil
                                            {
                                                Activo = false
                                            }) > 0;

                resp.Success = res_delete_perfil;
                resp.Message = res_delete_perfil == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }

        public async Task<Perfil_Permisos_Detalle> GetPerfilPermisos(int idPerfil)
        {
            Perfil_Permisos_Detalle perfil_permisos = new Perfil_Permisos_Detalle();
            List<Permiso_Detalle> permisos = new List<Permiso_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                var perfil = await (from perf in db.tB_Perfils
                                    where perf.IdPerfil == idPerfil
                                    select perf).FirstOrDefaultAsync();

                perfil_permisos.IdPerfil = idPerfil;
                perfil_permisos.Perfil = perfil.Perfil;
                perfil_permisos.Descripcion = perfil.Descripcion;

                var perf_permisos = await (from perf_perm in db.tB_PerfilPermisos
                                           where perf_perm.IdPerfil == idPerfil
                                           select perf_perm).ToListAsync();

                foreach(var perf_perm in perf_permisos)
                {
                    var permiso = await (from perm in db.tB_Permisos
                                         where perm.IdPermiso == perf_perm.IdPermiso
                                         select new Permiso_Detalle
                                         {
                                             IdPermiso = perm.IdPermiso,
                                             Permiso = perm.Permiso,
                                             Activo = perm.Activo
                                         }).FirstOrDefaultAsync();

                    if(permiso != null)
                        permisos.Add(permiso);
                }

                perfil_permisos.Permisos = permisos.OrderBy(x => x.Permiso).ToList();

                return perfil_permisos;
            }
        }

        public async Task<Perfil_Modulos_Detalle> GetPerfilModulos(int idPerfil)
        {
            Perfil_Modulos_Detalle perfil_modulos = new Perfil_Modulos_Detalle();
            List<Modulo_Detalle> modulos = new List<Modulo_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                var perfil = await (from perf in db.tB_Perfils
                                    where perf.IdPerfil == idPerfil
                                    select perf).FirstOrDefaultAsync();

                perfil_modulos.IdPerfil = idPerfil;
                perfil_modulos.Perfil = perfil.Perfil;
                perfil_modulos.Descripcion = perfil.Descripcion;

                var perf_modulos = await (from perf_mod in db.tB_PerfilModulos
                                          where perf_mod.IdPerfil == idPerfil
                                          select perf_mod).ToListAsync();

                foreach (var perf_modulo in perf_modulos)
                {
                    var modulo = await (from mod in db.tB_Modulos
                                        where mod.IdModulo == perf_modulo.IdModulo
                                        select new Modulo_Detalle
                                        {
                                            IdModulo = mod.IdModulo,
                                            Modulo = mod.Modulo,
                                            Activo = mod.Activo
                                        }).FirstOrDefaultAsync();

                    var submodulos = await (from sub in db.tB_Modulos
                                            where sub.Activo == true
                                            && sub.Modulo == modulo.Modulo
                                            && sub.IdModulo == perf_modulo.IdModulo
                                            orderby sub.SubModulo ascending
                                            select new Submodulo_Detalle
                                            {
                                                IdSubmodulo = sub.IdModulo,
                                                SubModulo = sub.SubModulo,
                                                Activo = sub.Activo
                                            }).ToListAsync();

                    submodulos = submodulos.GroupBy(sub => sub.SubModulo)
                                                .Select(group => group.First())
                                                .ToList();

                    modulo.Submodulos = new List<Submodulo_Detalle>();
                    modulo.Submodulos.AddRange(submodulos);

                    foreach (var submodulo in submodulos)
                    {
                        var tabs = await (from tab in db.tB_Modulos
                                          where tab.Activo == true
                                          && tab.IsTab == true
                                          && tab.SubModulo == submodulo.SubModulo
                                          && tab.IdModulo == perf_modulo.IdModulo
                                          orderby tab.SubModulo ascending
                                          select new Tab_Detalle
                                          {
                                              IdTab = tab.IdModulo,
                                              Tab = tab.Tab,
                                              IsTab = tab.IsTab,
                                              Activo = tab.Activo
                                          }).ToListAsync();

                        submodulo.Tabs = new List<Tab_Detalle>();
                        submodulo.Tabs.AddRange(tabs);
                    }

                    if (modulo != null)
                        modulos.Add(modulo);
                }

                perfil_modulos.Modulos = modulos.OrderBy(x => x.Modulo).ToList();

                return perfil_modulos;
            }
        }




        public async Task<(bool Success, string Message)> UpdatePerfilModulos(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_perfil = Convert.ToInt32(registro["id_perfil"].ToString());
            JsonArray modulos = registro["modulos"].AsArray();

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var delete_modulos = await (db.tB_PerfilModulos.Where(x => x.IdPerfil == id_perfil)
                                            .DeleteAsync()) > 0;

                resp.Success = delete_modulos;
                resp.Message = delete_modulos == default ? "Ocurrio un error al actualizar registro." : string.Empty;

                foreach (var modulo in modulos)
                {
                    var insert_modulo_perfil = await db.tB_PerfilModulos
                        .Value(x => x.IdPerfil, id_perfil)
                        .Value(x => x.IdModulo, Convert.ToInt32(modulo.ToString()))
                        .InsertAsync() > 0;

                    resp.Success = insert_modulo_perfil;
                    resp.Message = insert_modulo_perfil == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                }


            }

            return resp;
        }
        
        public async Task<(bool Success, string Message)> UpdatePerfilPermisos(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_perfil = Convert.ToInt32(registro["id_perfil"].ToString());
            JsonArray permisos = registro["permisos"].AsArray();

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var delete_permisos = await (db.tB_PerfilPermisos.Where(x => x.IdPerfil == id_perfil)
                                            .DeleteAsync()) > 0;

                resp.Success = delete_permisos;
                resp.Message = delete_permisos == default ? "Ocurrio un error al actualizar registro." : string.Empty;

                foreach (var permiso in permisos)
                {
                    var insert_permiso_perfil = await db.tB_PerfilPermisos
                        .Value(x => x.IdPerfil, id_perfil)
                        .Value(x => x.IdPermiso, Convert.ToInt32(permiso.ToString()))
                        .InsertAsync() > 0;

                    resp.Success = insert_permiso_perfil;
                    resp.Message = insert_permiso_perfil == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                }


            }

            return resp;
        }
        #endregion Perfiles


        #region Permisos
        public async Task<List<Permiso_Detalle>> GetPermisos()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var permisos = await (from per in db.tB_Permisos
                                      where per.Activo == true
                                      orderby per.Permiso ascending
                                      select new Permiso_Detalle
                                      {
                                          IdPermiso = per.IdPermiso,
                                          Permiso = per.Permiso,
                                          Activo = per.Activo
                                      }).ToListAsync();

                return permisos;
            }
        }
        #endregion Permisos
    }
}
