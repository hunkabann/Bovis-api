using System.Security.Claims;

namespace Bovis.API.Helper
{
	public class ClaimsJWT
	{
		private string TransactionId { set; get; }

		public ClaimsJWT(string TransactionId)
		{
			this.TransactionId = TransactionId;
		}

		public ClaimJWTModel GetClaimValues(IEnumerable<Claim> claims)
		{
			var userSettings = default(ClaimJWTModel);
			if (claims is not null && claims.Any())
			{
				userSettings = new ClaimJWTModel
				{
					nombre = claims.First(x => x.Type == "name").Value,
					correo = claims.First(x => x.Type == "preferred_username").Value,
					roles = claims.First(x => x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value,
					transactionId = TransactionId
				};
			}
			return userSettings ?? new ClaimJWTModel();
		}
	}
}
