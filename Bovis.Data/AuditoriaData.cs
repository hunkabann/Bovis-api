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
    }
}
