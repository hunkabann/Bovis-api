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
        public List<TB_Cat_Auditoria_Cumplimiento> Auditorias { get; set; }
    }

    public class Documentos_Auditoria_Cumplimiento_Proyecto_Detalle
    {
        public int IdSeccion { get; set; }
        public string ChSeccion { get; set; }
        public int NuProcentaje { get; set; }
        public List<TB_Cat_Auditoria_Cumplimiento> Auditorias { get; set; }
    }

    public class Auditoria_Cumplimiento_Detalle
    {
        public int IdAuditoriaCumplimiento { get; set; }
        public int IdProyecto { get; set; }
        public int IdDirector { get; set; }
        public int Mes { get; set; }
        public DateTime Fecha { get; set; }
        public string Punto { get; set; }
        public int IdSeccion { get; set; }
        public string Cumplimiento { get; set; }
        public string DocumentoRef { get; set; }
        public bool Aplica { get; set; }
        public string Motivo { get; set; }
    }
}
