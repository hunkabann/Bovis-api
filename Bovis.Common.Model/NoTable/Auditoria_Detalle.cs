using Bovis.Common.Model.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Common.Model.NoTable
{
    public class Documentos_Auditoria_Detalle
    {
        public int? IdSeccion { get; set; }
        public string? ChSeccion { get; set; }
        public List<TB_Cat_Auditoria>? Auditorias { get; set; }
    }

    public class Periodos_Auditoria_Detalle
    {
        public int IdRegistro { get; set; }
        public int IdProyecto { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
    }

    public class Documentos_Auditoria_Proyecto_Detalle
    {
        public int? IdSeccion { get; set; }
        public string? ChSeccion { get; set; }
        public decimal NuProcentaje { get; set; }       
        public int TotalDocumentos { get; set; }
        public int TotalDocumentosValidados { get; set; }
        public bool Aplica { get; set; }
        public List<Auditoria_Detalle>? Auditorias { get; set; }
    }

    public class Auditoria_Detalle
    {
        public int? IdAuditoriaProyecto { get; set; }
        public int? IdAuditoria { get; set; }
        public int? IdProyecto { get; set; }
        public int? IdDirector { get; set; }
        public int? Mes { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Punto { get; set; }
        public int? IdSeccion { get; set; }
        public string ChSeccion { get; set; }
        public string? Cumplimiento { get; set; }
        public string DocumentoRef { get; set; }
        public bool? Aplica { get; set; }
        public string TipoAuditoria { get; set; }
        public bool? TieneDocumento { get; set; }
        public int? IdDocumento { get; set; }
        public bool? UltimoDocumentoValido { get; set; }
        public int? CantidadDocumentos { get; set; }
        public int? CantidadDocumentosValidados { get; set; }
    }

    public class Comentario_Detalle
    {
        public int IdComentario { get; set; }
        public int NumProyecto { get; set; }
        public string Comentario { get; set; }
        public DateTime Fecha { get; set; }
        public int IdTipoComentario { get; set; }
        public string TipoComentario { get; set; }
        public string NombreAuditor { get; set; }
        public string DirectorResponsable { get; set; }
        public string ResponsableAsignado { get; set; }
        public DateTime? FechaAuditoriaInicial { get; set; }
        public DateTime? FechaAuditoria { get; set; }
    }

    public class TablasAuditoria_Detalle
    {
        public TB_AuditoriaProyecto Auditoria { get; set; }
        public TB_Cat_Auditoria CatAuditoria { get; set; }
        public TB_Cat_AuditoriaSeccion Seccion { get; set; }
    }

}
