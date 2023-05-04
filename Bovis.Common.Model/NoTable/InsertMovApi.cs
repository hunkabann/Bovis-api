using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
	public class InsertMovApi
	{
		public string Usuario { get; set; }

		public string Nombre { get; set; }

		public string Roles { get; set; }

		public int Rel { get; set; }

		public string TransactionId { get; set; }
	}
}
