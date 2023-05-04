namespace Bovis.API.Helper
{
	public class ClaimJWTModel
	{
		public string nombre { get; set; }
		public string correo { get; set; }
		public string roles { get; set; }
		public string transactionId { set; get; }
	}
}
