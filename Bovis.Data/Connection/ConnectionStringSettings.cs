using LinqToDB.Configuration;
using Newtonsoft.Json.Linq;

namespace Bovis.Data.Connection
{
	public class ConnectionStringSettings : IConnectionStringSettings
	{
		public string? ConnectionString { get; set; }
		public string Name { get; set; }
		public string ProviderName { get; set; }
		public bool IsGlobal => false;
	}
	public class DBSettings : ILinqToDBSettings
	{
		private readonly JObject Resource = JObject.Parse(File.ReadAllText("appsettings.json"));

		public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();
		public string? ConnectionString { get; set; }

		public string DefaultConfiguration => "DBConfig";
		public string DefaultDataProvider => "SqlServer";

		public IEnumerable<IConnectionStringSettings> ConnectionStrings
		{
			get
			{
				yield return new ConnectionStringSettings
				{
					Name = "DBConfig",
					ProviderName = "SqlServer",
					ConnectionString = Resource[nameof(ConnectionStrings)]?["DBConnection"]?.ToString()
				};
			}
		}
	}
}
