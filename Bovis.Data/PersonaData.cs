using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Data
{
    public class PersonaData : RepositoryLinq2DB<ConnectionDB>, IPersonaData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public PersonaData()
        {
            this.ConfigurationDB = dbConfig;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Personas
        public async Task<List<Persona_Detalle>> GetPersonas(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from per in db.tB_Personas
                                                                          join edo_civil in db.tB_Cat_EdoCivils on per.IdEdoCivil equals edo_civil.IdEdoCivil into edo_civilJoin
                                                                          from edo_civilItem in edo_civilJoin.DefaultIfEmpty()
                                                                          join sangre in db.tB_Cat_TipoSangres on per.IdTipoSangre equals sangre.IdTipoSangre into sangreJoin
                                                                          from sangreItem in sangreJoin.DefaultIfEmpty()
                                                                          join sexo in db.tB_Cat_Sexos on per.IdSexo equals sexo.IdSexo into sexoJoin
                                                                          from sexoItem in sexoJoin.DefaultIfEmpty()
                                                                          join tipo_per in db.tB_Cat_TipoPersonas on per.IdTipoPersona equals tipo_per.IdTipoPersona into tipo_perJoin
                                                                          from tipo_perItem in tipo_perJoin.DefaultIfEmpty()
                                                                          where per.Activo == activo
                                                                          select new Persona_Detalle
                                                                          {
                                                                              nukidpersona = per.IdPersona,
                                                                              nukidedo_civil = per.IdEdoCivil,
                                                                              chedo_civil = edo_civilItem != null ? edo_civilItem.EdoCivil : string.Empty,
                                                                              nukidtipo_sangre = per.IdTipoSangre,
                                                                              chtipo_sangre = sangreItem != null ? sangreItem.TipoSangre : string.Empty,
                                                                              chnombre = per.Nombre,
                                                                              chap_paterno = per.ApPaterno,
                                                                              chap_materno = per.ApMaterno,
                                                                              nukidsexo = per.IdSexo,
                                                                              chsexo = sexoItem != null ? sexoItem.Sexo : string.Empty,
                                                                              chrfc = per.Rfc,
                                                                              dtfecha_nacimiento = per.FechaNacimiento,
                                                                              chemail = per.Email,
                                                                              chtelefono = per.Telefono,
                                                                              chcelular = per.Celular,
                                                                              chcurp = per.Curp,
                                                                              nukidtipo_persona = per.IdTipoPersona,
                                                                              chtipo_persona = tipo_perItem != null ? tipo_perItem.TipoPersona : string.Empty,
                                                                              boactivo = per.Activo
                                                                          }).ToListAsync();
            }
            else return await GetAllFromEntityAsync<Persona_Detalle>();
        }

        public async Task<Persona_Detalle> GetPersona(int idPersona)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from per in db.tB_Personas
                                 join edo_civil in db.tB_Cat_EdoCivils on per.IdEdoCivil equals edo_civil.IdEdoCivil into edo_civilJoin
                                 from edo_civilItem in edo_civilJoin.DefaultIfEmpty()
                                 join sangre in db.tB_Cat_TipoSangres on per.IdTipoSangre equals sangre.IdTipoSangre into sangreJoin
                                 from sangreItem in sangreJoin.DefaultIfEmpty()
                                 join sexo in db.tB_Cat_Sexos on per.IdSexo equals sexo.IdSexo into sexoJoin
                                 from sexoItem in sexoJoin.DefaultIfEmpty()
                                 join tipo_per in db.tB_Cat_TipoPersonas on per.IdTipoPersona equals tipo_per.IdTipoPersona into tipo_perJoin
                                 from tipo_perItem in tipo_perJoin.DefaultIfEmpty()
                                 where per.IdPersona == idPersona
                                 select new Persona_Detalle
                                 {
                                     nukidpersona = per.IdPersona,
                                     nukidedo_civil = per.IdEdoCivil,
                                     chedo_civil = edo_civilItem != null ? edo_civilItem.EdoCivil : string.Empty,
                                     nukidtipo_sangre = per.IdTipoSangre,
                                     chtipo_sangre = sangreItem != null ? sangreItem.TipoSangre : string.Empty,
                                     chnombre = per.Nombre,
                                     chap_paterno = per.ApPaterno,
                                     chap_materno = per.ApMaterno,
                                     nukidsexo = per.IdSexo,
                                     chsexo = sexoItem != null ? sexoItem.Sexo : string.Empty,
                                     chrfc = per.Rfc,
                                     dtfecha_nacimiento = per.FechaNacimiento,
                                     chemail = per.Email,
                                     chtelefono = per.Telefono,
                                     chcelular = per.Celular,
                                     chcurp = per.Curp,
                                     nukidtipo_persona = per.IdTipoPersona,
                                     chtipo_persona = tipo_perItem != null ? tipo_perItem.TipoPersona : string.Empty,
                                     boactivo = per.Activo
                                 }).FirstOrDefaultAsync();

                return res;

            }
        }

        public async Task<(bool Success, string Message)> AddRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            string nombre = registro["nombre"].ToString();
            string apellido_paterno = registro["apellido_paterno"].ToString();
            string apellido_materno = registro["apellido_materno"] != null ? registro["apellido_materno"].ToString() : string.Empty;
            int id_edo_civil = Convert.ToInt32(registro["id_edo_civil"].ToString());
            DateTime fecha_nacimiento = Convert.ToDateTime(registro["fecha_nacimiento"].ToString());
            int id_tipo_sangre = Convert.ToInt32(registro["id_tipo_sangre"].ToString());
            int id_sexo = Convert.ToInt32(registro["id_sexo"].ToString());
            string rfc = registro["rfc"].ToString();
            int id_tipo_persona = Convert.ToInt32(registro["id_tipo_persona"].ToString());
            string email = registro["email"].ToString();
            string telefono = registro["telefono"].ToString();
            string celular = registro["celular"].ToString();
            string curp = registro["curp"].ToString();

            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from per in db.tB_Personas
                                 where per.Rfc == rfc
                                 select per).FirstOrDefaultAsync();

                if (res != null)
                {
                    resp.Success = true;
                    resp.Message = String.Format("Ya existe un registro de {0}, con RFC: {1}", nombre + " " + apellido_paterno + " " + apellido_materno, rfc);
                    return resp;
                }

                var insert_empleado = await db.tB_Personas
                    .Value(x => x.IdEdoCivil, id_edo_civil)
                    .Value(x => x.IdTipoSangre, id_tipo_sangre)
                    .Value(x => x.Nombre, nombre)
                    .Value(x => x.ApPaterno, apellido_paterno)
                    .Value(x => x.ApMaterno, apellido_materno)
                    .Value(x => x.IdSexo, id_sexo)
                    .Value(x => x.Rfc, rfc)
                    .Value(x => x.FechaNacimiento, fecha_nacimiento)
                    .Value(x => x.Email, email)
                    .Value(x => x.Telefono, telefono)
                    .Value(x => x.Celular, celular)
                    .Value(x => x.Curp, curp)
                    .Value(x => x.IdTipoPersona, id_tipo_persona)
                    .Value(x => x.Activo, true)
                    .InsertAsync() > 0;

                resp.Success = insert_empleado;
                resp.Message = insert_empleado == default ? "Ocurrio un error al agregar registro de Persona." : string.Empty;


            }
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_persona = Convert.ToInt32(registro["id_persona"].ToString());
            string nombre = registro["nombre"].ToString();
            string apellido_paterno = registro["apellido_paterno"].ToString();
            string apellido_materno = registro["apellido_materno"] != null ? registro["apellido_materno"].ToString() : string.Empty;
            int id_edo_civil = Convert.ToInt32(registro["id_edo_civil"].ToString());
            DateTime fecha_nacimiento = Convert.ToDateTime(registro["fecha_nacimiento"].ToString());
            int id_tipo_sangre = Convert.ToInt32(registro["id_tipo_sangre"].ToString());
            int id_sexo = Convert.ToInt32(registro["id_sexo"].ToString());
            string rfc = registro["rfc"].ToString();
            int id_tipo_persona = Convert.ToInt32(registro["id_tipo_persona"].ToString());
            string email = registro["email"].ToString();
            string telefono = registro["telefono"].ToString();
            string celular = registro["celular"].ToString();
            string curp = registro["curp"].ToString();

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_update_persona = await db.tB_Personas.Where(x => x.IdPersona == id_persona)
                    .UpdateAsync(x => new TB_Persona
                    {
                        Nombre = nombre,
                        ApPaterno = apellido_paterno,
                        ApMaterno = apellido_materno,
                        IdEdoCivil = id_edo_civil,
                        FechaNacimiento = fecha_nacimiento,
                        IdTipoSangre = id_tipo_sangre,
                        IdSexo = id_sexo,
                        Rfc = rfc,
                        IdTipoPersona = id_tipo_persona,
                        Email = email,
                        Telefono = telefono,
                        Celular = celular,
                        Curp = curp
                    }) > 0;

                resp.Success = res_update_persona;
                resp.Message = res_update_persona == default ? "Ocurrio un error al actualizar registro." : string.Empty;


            }
            return resp;
        }
        #endregion Personas
    }
}
