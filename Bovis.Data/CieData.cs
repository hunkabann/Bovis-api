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
    public class CieData : RepositoryLinq2DB<ConnectionDB>, ICieData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public CieData()
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
