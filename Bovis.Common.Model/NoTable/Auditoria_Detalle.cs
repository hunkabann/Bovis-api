using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Documentos_Auditoria_Cumplimiento_Detalle
    {
        public int IdSeccion { get; set; }
        public string ChSeccion { get; set; }
        public List<TB_Cat_Auditoria_Cumplimiento> Documentos { get; set; }
    }
}
