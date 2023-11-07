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
    public class RolData : RepositoryLinq2DB<ConnectionDB>, IRolData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public RolData()
        {
            this.ConfigurationDB = dbConfig;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base


        static string RemoverAcentos(string input)
        {
            string normalizedString = input.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public async Task<Rol_Detalle> GetRoles(string email)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var empleado = await (from emp in db.tB_Empleados
                                      where emp.EmailBovis == email
                                      && emp.Activo == true
                                      select emp).FirstOrDefaultAsync();

                var persona = await (from pers in db.tB_Personas
                                     where pers.IdPersona == empleado!.IdPersona
                                     select pers).FirstOrDefaultAsync();

                var id_usuario = await (from usr in db.tB_Usuarios
                                        where usr.NumEmpleadoRrHh == empleado!.NumEmpleadoRrHh
                                        && usr.Activo == true
                                        select usr.IdUsuario).FirstOrDefaultAsync();

                Rol_Detalle rol = new Rol_Detalle();
                rol.nukidusuario = id_usuario;
                rol.chusuario = persona!.Nombre + " " + persona!.ApPaterno + " " + persona!.ApMaterno;
                rol.chemail = email;

                var roles = await (from perfil_usuario in db.tB_PerfilUsuarios
                                   join perfiles in db.tB_Perfils on perfil_usuario.IdPerfil equals perfiles.IdPerfil
                                   join perfil_modulo in db.tB_PerfilModulos on perfiles.IdPerfil equals perfil_modulo.IdPerfil
                                   join modulos in db.tB_Modulos on perfil_modulo.IdModulo equals modulos.IdModulo
                                   join perfil_permiso in db.tB_PerfilPermisos on perfil_usuario.IdPerfil equals perfil_permiso.IdPerfil
                                   join permisos in db.tB_Permisos on perfil_permiso.IdPermiso equals permisos.IdPermiso
                                   where perfil_usuario.IdUsuario == id_usuario
                                   select new
                                   {
                                       nukidmodulo = perfil_modulo.IdModulo,
                                       chmodulo = modulos.Modulo,
                                       chmodulo_slug = RemoverAcentos(modulos.Modulo.ToLower().Replace(" ", "-")),
                                       chsub_modulo = modulos.SubModulo,
                                       chsub_modulo_slug = RemoverAcentos(modulos.SubModulo.ToLower().Replace(" ", "-")),
                                       nukidperfil = perfil_usuario.IdPerfil,
                                       chperfil = perfiles.Perfil,
                                       chperfil_slug = RemoverAcentos(perfiles.Perfil.ToLower().Replace(" ", "-")),
                                       nukidpermiso = perfil_permiso.IdPermiso,
                                       chpermiso = permisos.Permiso,
                                       chpermiso_slug = RemoverAcentos(permisos.Permiso.ToLower().Replace(" ", "-")),
                                   }).ToListAsync();

                rol.permisos = roles.GroupBy(item => new { item.nukidmodulo, item.chmodulo, item.chsub_modulo })
                                    .Select(group =>
                                    {
                                        var chpermiso = group.Any(item => item.chpermiso == "Administrador") ? "Administrador" : group.First().chpermiso;

                                        return new Permiso_Modulo_Detalle
                                        {
                                            nukidmodulo = group.Key.nukidmodulo,
                                            chmodulo = group.Key.chmodulo,
                                            chmodulo_slug = group.First().chmodulo_slug,
                                            chsub_modulo = group.Key.chsub_modulo,
                                            chsub_modulo_slug = group.First().chsub_modulo_slug,
                                            chpermiso = chpermiso,
                                            chpermiso_slug = RemoverAcentos(chpermiso.ToLower().Replace(" ", "-")),
                                            //nukidperfil = group.Select(item => item.nukidperfil).ToList(),
                                            perfiles = group.Select(item => item.chperfil).Distinct().ToList(),
                                            permisos = group.Select(item => item.chpermiso).Distinct().ToList()
                                        };
                                    }).ToList();

                return rol;
            }
        }
    }
}
