using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
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

            int num_empleado = Convert.ToInt32(registro["num_empleado"].ToString());

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

                    perfiles.Add(perfil);
                }

                usuario_perfiles.Perfiles = perfiles;

                return usuario_perfiles;
            }
        }
        #endregion Usuarios

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
                                          SubModulo = mod.SubModulo,
                                          Activo = mod.Activo,
                                          IsTab = mod.IsTab
                                      }).ToListAsync();

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

                    perfiles.Add(perfil);
                }     
                
                modulo_perfiles.Perfiles = perfiles;               

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

                    permisos.Add(permiso);
                }

                perfil_permisos.Permisos = permisos;

                return perfil_permisos;
            }
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
