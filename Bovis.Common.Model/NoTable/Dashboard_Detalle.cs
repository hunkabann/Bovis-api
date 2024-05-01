using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Dashboard_Detalle
    {
    }

    #region Proyectos Documentos
    public class ProyectosDocumentos
    {
        public string name { get; set; } = string.Empty;
        public List<Series> series { get; set; } = null;
    }
    public class Series
    {
        public string name { get; set; } = string.Empty;
        public int value { get; set; } = 0;
    }
    #endregion Proyectos Documentos
}
