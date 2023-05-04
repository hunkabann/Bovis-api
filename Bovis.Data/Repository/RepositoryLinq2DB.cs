using LinqToDB;
using LinqToDB.Data;
using System.Data;
using System.Linq.Expressions;

namespace Bovis.Data.Repository
{
	public abstract class RepositoryLinq2DB<X> where X : DataConnection, new()
	{
		public string? ConfigurationDB { set; get; }

		public virtual bool UpdateEntity<T>(T entity) where T : class
		{
			var resp = default(bool);
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				resp = db.Update<T>(entity) > 0;
				db.Close();
			}

			return resp;
		}

		public virtual async Task<bool> UpdateEntityAsync<T>(T entity) where T : class
		{
			var resp = default(bool);
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				resp = await db.UpdateAsync<T>(entity) > 0;
				db.Close();
			}
			return resp;
		}

		public virtual object InsertEntity<T>(T entity) where T : class
		{
			object resp = null;

			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				resp = db.InsertWithIdentity<T>(entity);
				db.Close();
			}
			return resp;
		}

		public virtual async Task<object> InsertEntityAsync<T>(T entity) where T : class
		{
			object? resp = default;
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				resp = await db.InsertWithIdentityAsync<T>(entity);
				db.Close();
			}
			return resp;
		}

		public virtual bool InsertEntityId<T>(T entity) where T : class
		{
			var tmpResp = -1;
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				tmpResp = db.Insert(entity);
				db.Close();
			}
			return tmpResp >= 0;

		}

		public virtual async Task<bool> InsertEntityIdAsync<T>(T entity) where T : class
		{
			var resp = default(bool);
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				int tmpResp = await db.InsertAsync(entity);
				resp = tmpResp >= 0;
				db.Close();
			}
			return resp;
		}

		public virtual bool DeleteEntity<T>(T entity) where T : class
		{
			var tmpResp = -1;
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				tmpResp = db.Delete<T>(entity);
				db.Close();
			}
			return tmpResp >= 0;
		}

		public virtual async Task<bool> DeleteEntityAsync<T>(T entity) where T : class
		{
			var resp = default(bool);
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				int tmpResp = await db.DeleteAsync(entity);
				resp = tmpResp >= 0;
				db.Close();
			}
			return resp;
		}

		public virtual List<T> GetAllFromEntity<T>() where T : class
		{
			List<T>? resp = null;
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				resp = db.GetTable<T>().ToList();
				db.Close();
			}
			return resp;
		}

		public virtual async Task<List<T>> GetAllFromEntityAsync<T>() where T : class
		{
			List<T>? resp = null;
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				resp = await db.GetTable<T>().ToListAsync();
				db.Close();
			}
			return resp;
		}

		public virtual T GetEntityByPK<T>(object pk) where T : class
		{
			T? resp = null;
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				var pkName = typeof(T).GetProperties().Where(prop => prop.GetCustomAttributes(typeof(LinqToDB.Mapping.PrimaryKeyAttribute), false).Count() > 0).First();
				var expression = SimpleComparison<T>(pkName.Name, pk);

				resp = db.GetTable<T>().Where(expression).FirstOrDefault();
				db.Close();
			}
			return resp ?? Activator.CreateInstance<T>();
		}

		public virtual async Task<T> GetEntityByPKAsync<T>(object pk) where T : class
		{
			T? resp = null;
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				var pkName = typeof(T).GetProperties().Where(prop => prop.GetCustomAttributes(typeof(LinqToDB.Mapping.PrimaryKeyAttribute), false).Count() > 0).First();
				var expression = SimpleComparison<T>(pkName.Name, pk);
				resp = await db.GetTable<T>().Where(expression).FirstOrDefaultAsync();
				db.Close();
			}
			return resp ?? Activator.CreateInstance<T>();
		}

		public virtual List<T> GetAllEntititiesByPropertyValue<T, D>(string propertyName, D valueToFilter) where T : class
		{
			if (string.IsNullOrWhiteSpace(propertyName)) return GetAllFromEntity<T>();
			var expression = SimpleComparison<T, D>(propertyName, valueToFilter);
			List<T>? resp = null;
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				resp = db.GetTable<T>().Where(expression).ToList();
				db.Close();
			}
			return resp;
		}

		public virtual async Task<List<T>> GetAllEntititiesByPropertyValueAsync<T, D>(string propertyName, D valueToFilter) where T : class
		{
			if (string.IsNullOrWhiteSpace(propertyName)) { return await GetAllFromEntityAsync<T>(); }

			var expression = SimpleComparison<T, D>(propertyName, valueToFilter);
			List<T>? resp = null;
			using (var db = Activator.CreateInstance(typeof(X), new string[] { ConfigurationDB ?? string.Empty }) as X)
			{
				resp = await db.GetTable<T>().Where(expression).ToListAsync();
				db.Close();
			}
			return resp;
		}

		public virtual Expression<Func<T, bool>> SimpleComparison<T>(string property, object value) where T : class
		{
			var type = typeof(T);
			var pe = Expression.Parameter(type, "p");
			var propertyReference = Expression.Property(pe, property);
			var constantReference = Expression.Constant(value);
			return Expression.Lambda<Func<T, bool>>(Expression.Equal(propertyReference, Expression.Convert(constantReference, value.GetType())), new[] { pe });
		}

		public virtual Expression<Func<T, bool>> SimpleComparison<T, D>(string propertyName, D value) where T : class
		{
			var type = typeof(T);
			var pe = Expression.Parameter(type, "p");
			var constantReference = Expression.Constant(value);
			var propertyReference = Expression.Property(pe, propertyName);
			return Expression.Lambda<Func<T, bool>>(Expression.Equal(propertyReference, Expression.Convert(constantReference, typeof(D))), new[] { pe });
		}
	}
}
