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

        public async Task<List<TB_Requerimiento>> GetRequerimientos(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Requerimientos
                                                                          where cat.Activo == activo
                                                                          select cat).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Requerimiento>();
        }
        public async Task<(bool existe, string mensaje)> AddRegistro(TB_Requerimiento registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                var insert = await db.tB_Requerimientos
                .Value(x => x.IdRequerimiento, registro.IdRequerimiento)
                .Value(x => x.IdCategoria, registro.IdCategoria)
                .Value(x => x.IdPuesto, registro.IdPuesto)
                .Value(x => x.IdNivelEstudios, registro.IdNivelEstudios)
                .Value(x => x.IdProfesion, registro.IdProfesion)
                .Value(x => x.IdJornada, registro.IdJornada)
                .Value(x => x.SueldoMin, registro.SueldoMin)
                .Value(x => x.SueldoMax, registro.SueldoMax)
                .Value(x => x.Habilidades, registro.Habilidades)
                .Value(x => x.Experiencias, registro.Experiencias)
                .InsertAsync() > 0;

                resp.Success = insert;
                resp.Message = insert == default ? "Ocurrio un error al agregar registro del requerimiento." : string.Empty;
            }
            return resp;
        }
    }
}
