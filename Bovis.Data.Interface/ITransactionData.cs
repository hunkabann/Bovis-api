using Bovis.Common.Model.Tables;

namespace Bovis.Data.Interface
{
	public interface ITransactionData : IDisposable
	{
		Task<bool> AddMovApi(Mov_Api MovApi);
	}
}
