using Bovis.Common.Model;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;

namespace Bovis.Data
{
	public class TransactionData : RepositoryLinq2DB<ConnectionDB>, ITransactionData
	{
		private readonly string dbConfig = "DBConfig";

		public TransactionData()
		{
			this.ConfigurationDB = dbConfig;
		}

		public Task<bool> AddMovApi(Mov_Api MovApi)=> InsertEntityIdAsync<Mov_Api>(MovApi);

		#region Destructor

		public void Dispose()
		{
			GC.SuppressFinalize(this);
			GC.Collect();
		}

		#endregion
	}
}
