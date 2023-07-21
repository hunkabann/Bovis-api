using Bovis.Common.Model;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;

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
        #endregion

        public async Task<List<TB_Proyecto>> GetProyectos()
        {
            //return await GetAllFromEntityAsync<TB_Proyecto>();
            using (var db = new ConnectionDB(dbConfig))
            {
                return await (from p in db.tB_Proyectos
                              orderby p.Proyecto ascending
                              select p).ToListAsync();
            }
        }

        public async Task<TB_Proyecto> GetProyecto(int numProyecto)
        {
            using (var db = new ConnectionDB(dbConfig)) return await (from p in db.tB_Proyectos
                                                                      where p.NumProyecto == numProyecto
                                                                      select p).FirstOrDefaultAsync();
        }

        public async Task<List<TB_Cliente>> GetClientes()
        {
            return await GetAllFromEntityAsync<TB_Cliente>();
        }
        public async Task<List<TB_Empresa>> GetEmpresas()
        {
            return await GetAllFromEntityAsync<TB_Empresa>();
        }
    }
}
