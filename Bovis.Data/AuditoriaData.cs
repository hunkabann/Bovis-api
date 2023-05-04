using Bovis.Common.Model;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Data
{
    public class AuditoriaData : RepositoryLinq2DB<ConnectionDB>, IAuditoriaData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public AuditoriaData()
        {
            this.ConfigurationDB = dbConfig;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion
    }
}
