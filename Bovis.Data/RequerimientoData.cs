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

        public async Task<List<TB_Requerimiento>> GetRequerimientos(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from req in db.tB_Requerimientos
                                                                          where req.Activo == activo
                                                                          select req).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Requerimiento>();
        }

        public async Task<TB_Requerimiento> GetRequerimiento(int idRequerimiento)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = from req in db.tB_Requerimientos
                          where req.IdRequerimiento == idRequerimiento
                          select req;

                return await res.FirstOrDefaultAsync();

            }
        }

        public async Task<(bool existe, string mensaje)> AgregarRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_categoria = Convert.ToInt32(registro["categoria"].ToString());
            int id_puesto = Convert.ToInt32(registro["puesto"].ToString());
            int id_nivel_estudios = Convert.ToInt32(registro["nivelEstudios"].ToString());
            string profesion = registro["profesion"].ToString();
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
                .Value(x => x.Profesion, profesion)
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
                    .InsertAsync() > 0;

                    resp.Success = insert_experiencia;
                    resp.Message = insert_experiencia == default ? "Ocurrio un error al agregar registro de la experiencia." : string.Empty;
                }
            }
            return resp;
        }
    }
}
