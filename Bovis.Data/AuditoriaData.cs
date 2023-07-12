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
        public async Task<List<TB_Cat_Auditoria_Contractual>> GetAuditoriasContractual()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var auditorias = await (from audit in db.tB_Cat_Auditoria_Contractuals
                                        select audit).ToListAsync();

                return auditorias;
            }
        }

        public async Task<(bool existe, string mensaje)> AddAuditoriasContractual(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_proyecto = Convert.ToInt32(registro["id_proyecto"].ToString());
            var auditorias = registro["auditorias"].AsArray();

            using (var db = new ConnectionDB(dbConfig))
            {
                foreach (var a in auditorias)
                {
                    var insert_auditoriacontractual_proyecto = await db.tB_Auditoria_Contractual_Proyectos
                                                                .Value(x => x.IdAuditoriaContractual, Convert.ToInt32(a.ToString()))
                                                                .Value(x => x.IdProyecto, id_proyecto)
                                                                .Value(x => x.FechaCarga, DateTime.Now)
                                                                .InsertAsync() > 0;

                    resp.Success = insert_auditoriacontractual_proyecto;
                    resp.Message = insert_auditoriacontractual_proyecto == default ? "Ocurrio un error al agregar registro." : string.Empty;
                }
            }

            return resp;
        }
        #endregion Auditoria Legal

        #region Auditoria de Calidad (Cumplimiento)
        public async Task<List<Documentos_Auditoria_Cumplimiento_Detalle>> GetAuditoriasCumplimiento()
        {
            List<Documentos_Auditoria_Cumplimiento_Detalle> auditorias = new List<Documentos_Auditoria_Cumplimiento_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                var secciones = await (from seccion in db.tB_Cat_Auditoria_Cumplimiento_Seccions
                                       select seccion).ToListAsync();

                foreach (var seccion in secciones)
                {
                    Documentos_Auditoria_Cumplimiento_Detalle auditoria = new Documentos_Auditoria_Cumplimiento_Detalle();
                    auditoria.IdSeccion = seccion.IdSeccion;
                    auditoria.ChSeccion = seccion.Seccion;
                    auditoria.Auditorias = new List<TB_Cat_Auditoria_Cumplimiento>();

                    var docs = await (from doc in db.tB_Cat_Auditoria_Cumplimientos
                                      where doc.IdSeccion == seccion.IdSeccion
                                      select doc).ToListAsync();

                    auditoria.Auditorias.AddRange(docs);

                    auditorias.Add(auditoria);
                }
            }

            return auditorias;
        }

        public async Task<List<Documentos_Auditoria_Cumplimiento_Proyecto_Detalle>> GetAuditoriasCumplimientoByProyecto(int IdProyecto)
        {
            List<Documentos_Auditoria_Cumplimiento_Proyecto_Detalle> auditorias = new List<Documentos_Auditoria_Cumplimiento_Proyecto_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                var docs = await (from doc in db.tB_Auditoria_Cumplimiento_Proyectos
                                  join cat in db.tB_Cat_Auditoria_Cumplimientos on doc.IdAuditoriaCumplimiento equals cat.IdAuditoriaCumplimiento into catJoin
                                  from catItem in catJoin.DefaultIfEmpty()
                                  where doc.IdProyecto == IdProyecto
                                  select new TB_Cat_Auditoria_Cumplimiento
                                  {
                                      IdAuditoriaCumplimiento = doc.IdAuditoriaCumplimiento,
                                      IdProyecto = doc.IdProyecto,
                                      IdDirector = catItem.IdDirector,
                                      Mes = catItem.Mes,
                                      Fecha = catItem.Fecha,
                                      Punto = catItem.Punto,
                                      IdSeccion = catItem.IdSeccion,
                                      Cumplimiento = catItem.Cumplimiento,
                                      DocumentoRef = catItem.DocumentoRef,
                                      Aplica = doc.Aplica,
                                      Motivo = doc.Motivo
                                  }).ToListAsync();

                Documentos_Auditoria_Cumplimiento_Proyecto_Detalle auditoria = new Documentos_Auditoria_Cumplimiento_Proyecto_Detalle();
                int count_aplica = 0;
                foreach (var doc in docs)
                {
                    auditoria.IdSeccion = doc.IdSeccion;

                    var seccion = await (from secc in db.tB_Cat_Auditoria_Cumplimiento_Seccions
                                         where secc.IdSeccion == doc.IdSeccion
                                         select secc).FirstOrDefaultAsync();

                    auditoria.ChSeccion = seccion.Seccion;

                    if (auditoria.Auditorias == null)
                        auditoria.Auditorias = new List<TB_Cat_Auditoria_Cumplimiento>();
                    auditoria.Auditorias.Add(doc);

                    if (doc.Aplica == true)
                        count_aplica++;
                }

                auditoria.NuProcentaje = (count_aplica / docs.Count) * 100;

                auditorias.Add(auditoria);
            }

            return auditorias;
        }

        public async Task<(bool existe, string mensaje)> AddAuditoriasCumplimiento(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_proyecto = Convert.ToInt32(registro["id_proyecto"].ToString());
            var auditorias = registro["auditorias"].AsArray();

            using (var db = new ConnectionDB(dbConfig))
            {
                var delete_auditoriacumplimiento_proyecto = await db.tB_Auditoria_Cumplimiento_Proyectos
                                                                .Where(x => x.IdProyecto == id_proyecto)
                                                                .DeleteAsync() > 0;

                foreach (var a in auditorias)
                {
                    int id_auditoria = Convert.ToInt32(a["id_auditoria"].ToString());
                    bool aplica = Convert.ToBoolean(a["aplica"].ToString());
                    string motivo = a["motivo"].ToString();

                    var insert_auditoriacumplimiento_proyecto = await db.tB_Auditoria_Cumplimiento_Proyectos
                                                                .Value(x => x.IdAuditoriaCumplimiento, id_auditoria)
                                                                .Value(x => x.IdProyecto, id_proyecto)
                                                                .Value(x => x.Aplica, aplica)
                                                                .Value(x => x.Motivo, motivo)
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

            int id_proyecto = Convert.ToInt32(registro["id_proyecto"].ToString());
            var auditorias = registro["auditorias"].AsArray();

            using (var db = new ConnectionDB(dbConfig))
            {
                var delete_auditoriacumplimiento_proyecto = await db.tB_Auditoria_Cumplimiento_Proyectos
                                                                .Where(x => x.IdProyecto == id_proyecto)
                                                                .DeleteAsync() > 0;

                foreach (var a in auditorias)
                {
                    int id_auditoria = Convert.ToInt32(a["id_auditoria"].ToString());
                    bool aplica = Convert.ToBoolean(a["aplica"].ToString());
                    string motivo = a["motivo"].ToString();

                    var insert_auditoriacumplimiento_proyecto = await db.tB_Auditoria_Cumplimiento_Proyectos
                                                                .Value(x => x.IdAuditoriaCumplimiento, id_auditoria)
                                                                .Value(x => x.IdProyecto, id_proyecto)
                                                                .Value(x => x.Aplica, aplica)
                                                                .Value(x => x.Motivo, motivo)
                                                                .InsertAsync() > 0;

                    resp.Success = insert_auditoriacumplimiento_proyecto;
                    resp.Message = insert_auditoriacumplimiento_proyecto == default ? "Ocurrio un error al agregar registro." : string.Empty;
                }
            }

            return resp;
        }
        #endregion Auditoria de Calidad (Cumplimiento)
    }
}
