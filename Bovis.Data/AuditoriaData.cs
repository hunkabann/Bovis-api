using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Data
{
    public class AuditoriaData : RepositoryLinq2DB<ConnectionDB>, IAuditoriaData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public AuditoriaData()
        {
            this.ConfigurationDB = dbConfig;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

        #region Auditoria Legal
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        public async Task<List<Documentos_Auditoria_Cumplimiento_Detalle>> GetDocumentosAuditoriaCumplimiento()
        {
            List<Documentos_Auditoria_Cumplimiento_Detalle> documentos = new List<Documentos_Auditoria_Cumplimiento_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                var secciones = await (from seccion in db.tB_Cat_Auditoria_Cumplimiento_Seccions
                                       select seccion).ToListAsync();

                foreach (var seccion in secciones)
                {
                    Documentos_Auditoria_Cumplimiento_Detalle documento = new Documentos_Auditoria_Cumplimiento_Detalle();
                    documento.IdSeccion = seccion.IdSeccion;
                    documento.ChSeccion = seccion.Seccion;
                    documento.Documentos = new List<TB_Cat_Auditoria_Cumplimiento>();

                    var docs = await (from doc in db.tB_Cat_Auditoria_Cumplimientos
                                      where doc.IdSeccion == seccion.IdSeccion
                                      select doc).ToListAsync();

                    documento.Documentos.AddRange(docs);

                    documentos.Add(documento);
                }
            }

            return documentos;
        }

        public async Task<(bool existe, string mensaje)> AddDocumentosAuditoriaCumplimiento(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_proyecto = Convert.ToInt32(registro["id_proyecto"].ToString());
            var cumplimientos = registro["cumplimientos"].AsArray();

            using (var db = new ConnectionDB(dbConfig))
            {
                foreach (var c in cumplimientos)
                {
                    var insert_auditoriacumplimiento_proyecto = await db.tB_Auditoria_Cumplimiento_Proyectos
                                                                .Value(x => x.IdProyecto, id_proyecto)
                                                                .Value(x => x.IdAuditoriaCumplimiento, Convert.ToInt32(c.ToString()))
                                                                .InsertAsync() > 0;

                    resp.Success = insert_auditoriacumplimiento_proyecto;
                    resp.Message = insert_auditoriacumplimiento_proyecto == default ? "Ocurrio un error al agregar registro." : string.Empty;
                }
            }

            return resp;
        }

        public async Task<(bool existe, string mensaje)> UpdateAuditoriaCumplimientoProyecto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            

            return resp;
        }
        #endregion Auditoria de Calidad (Cumplimiento)
    }
}
