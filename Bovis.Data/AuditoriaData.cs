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
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

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


        public async Task<List<Documentos_Auditoria_Detalle>> GetAuditorias(string TipoAuditoria)
        {
            List<Documentos_Auditoria_Detalle> auditorias = new List<Documentos_Auditoria_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                var secciones = await (from seccion in db.tB_Cat_Auditoria_Seccions
                                       where seccion.TipoAuditoria == TipoAuditoria || seccion.TipoAuditoria == "ambos"
                                       select seccion).ToListAsync();

                foreach (var seccion in secciones)
                {
                    Documentos_Auditoria_Detalle auditoria = new Documentos_Auditoria_Detalle();
                    auditoria.IdSeccion = seccion.IdSeccion;
                    auditoria.ChSeccion = seccion.Seccion;
                    auditoria.Auditorias = new List<TB_Cat_Auditoria>();

                    var docs = await (from doc in db.tB_Cat_Auditorias
                                      where doc.IdSeccion == seccion.IdSeccion
                                      && (doc.TipoAuditoria == TipoAuditoria || doc.TipoAuditoria == "ambos")
                                      select doc).ToListAsync();

                    auditoria.Auditorias.AddRange(docs);

                    auditorias.Add(auditoria);
                }
            }

            return auditorias;
        }

        public async Task<List<Documentos_Auditoria_Proyecto_Detalle>> GetAuditoriasByProyecto(int IdProyecto, string TipoAuditoria)
        {
            List<Documentos_Auditoria_Proyecto_Detalle> documentos_auditoria = new List<Documentos_Auditoria_Proyecto_Detalle>();
            Documentos_Auditoria_Proyecto_Detalle documento_auditoria = null;
            int totalDocumentos = 0;

            using (var db = new ConnectionDB(dbConfig))
            {
                var audits = await (from audit in db.tB_Auditoria_Proyectos
                                  join cat in db.tB_Cat_Auditorias on audit.IdAuditoria equals cat.IdAuditoria into catJoin
                                  from catItem in catJoin.DefaultIfEmpty()
                                  join sec in db.tB_Cat_Auditoria_Seccions on catItem.IdSeccion equals sec.IdSeccion into secJoin
                                  from secItem in secJoin.DefaultIfEmpty()
                                  where audit.IdProyecto == IdProyecto
                                  && (catItem.TipoAuditoria == TipoAuditoria || catItem.TipoAuditoria == "ambos")
                                  select new Auditoria_Detalle
                                  {
                                      IdAuditoriaProyecto = audit.IdAuditoriaProyecto,
                                      IdAuditoria = audit.IdAuditoria,
                                      IdProyecto = audit.IdProyecto,
                                      IdDirector = catItem.IdDirector,
                                      Mes = catItem.Mes,
                                      Fecha = catItem.Fecha,
                                      Punto = catItem.Punto,
                                      IdSeccion = catItem != null ? catItem.IdSeccion : 0,
                                      ChSeccion = secItem != null ? secItem.Seccion : string.Empty,
                                      Cumplimiento = TipoAuditoria == "calidad" ? catItem.CumplimientoCalidad
                                                                                : TipoAuditoria == "legal" ? catItem.CumplimientoLegal
                                                                                : catItem.CumplimientoCalidad,
                                      DocumentoRef = catItem.DocumentoRef,
                                      TipoAuditoria = catItem.TipoAuditoria ?? string.Empty,
                                      Aplica = audit.Aplica
                                  }).ToListAsync();

                var secciones = await (from seccion in db.tB_Cat_Auditoria_Seccions
                                       where seccion.TipoAuditoria == TipoAuditoria
                                       || seccion.TipoAuditoria == "ambos"
                                       select seccion).ToListAsync(); 


                foreach (var seccion in secciones)
                {
                    int count_aplica = 0;
                    int count_auditorias_seccion = 0;
                    Documentos_Auditoria_Proyecto_Detalle auditoria = new Documentos_Auditoria_Proyecto_Detalle();
                    auditoria.IdSeccion = seccion.IdSeccion;
                    auditoria.ChSeccion = seccion.Seccion;
                    auditoria.Auditorias = new List<Auditoria_Detalle>();

                    foreach (var audit in audits)
                    {
                        if (seccion.IdSeccion == audit.IdSeccion)
                        {
                            if (auditoria.Auditorias == null)
                                auditoria.Auditorias = new List<Auditoria_Detalle>();
                            auditoria.Auditorias.Add(audit);

                            // Se obtiene el conteo de documntos por Auditoría
                            var documentos = await (from documento in db.tB_Auditoria_Documentos
                                                    where documento.IdAuditoriaProyecto == audit.IdAuditoria
                                                    && documento.Fecha.Month == DateTime.Now.Month
                                                    && documento.Fecha.Year == DateTime.Now.Year
                                                    && documento.Activo == true
                                                    select documento).ToListAsync();

                            audit.TieneDocumento = documentos.Count > 0 ? true : false;
                            audit.CantidadDocumentos = documentos.Count;
                            audit.CantidadDocumentosValidados = documentos.Where(x => x.Valido == true).Count();
                            totalDocumentos += documentos.Count;
                            auditoria.TotalDocumentos = totalDocumentos;

                            // Se obtiene la validación del último documento subido a la Auditoría
                            var ultimoDocumento = await (from documento in db.tB_Auditoria_Documentos
                                                         where documento.IdAuditoriaProyecto == audit.IdAuditoria
                                                         && documento.Activo == true
                                                         orderby documento.Fecha descending
                                                         select documento).FirstOrDefaultAsync();

                            if (ultimoDocumento != null)
                            {
                                audit.IdDocumento = ultimoDocumento.IdDocumento;
                                audit.UltimoDocumentoValido = ultimoDocumento.Valido;
                            }

                            count_auditorias_seccion++;

                            if (audit.Aplica == true)
                                count_aplica++;
                        }
                    }

                    decimal porcentaje = (count_aplica > 0 && count_auditorias_seccion > 0) ? (((decimal)count_aplica / count_auditorias_seccion) * 100) : 0;
                    auditoria.NuProcentaje = Math.Round(porcentaje);
                    documentos_auditoria.Add(auditoria);
                }
            }

            return documentos_auditoria;
        }

        public async Task<(bool Success, string Message)> AddAuditorias(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_proyecto = Convert.ToInt32(registro["id_proyecto"].ToString());
            var auditorias = registro["auditorias"].AsArray();

            using (var db = new ConnectionDB(dbConfig))
            {
                var delete_auditoria_proyecto = await db.tB_Auditoria_Proyectos
                                                                .Where(x => x.IdProyecto == id_proyecto)
                                                                .DeleteAsync() > 0;

                foreach (var a in auditorias)
                {
                    int? id_auditoria = a["id_auditoria"] != null ? Convert.ToInt32(a["id_auditoria"].ToString()) : null;
                    bool? aplica = a["aplica"] != null ? Convert.ToBoolean(a["aplica"].ToString()) : null;
                    string? motivo = a["motivo"] != null ? a["motivo"].ToString() : null;

                    var insert_auditoria_proyecto = await db.tB_Auditoria_Proyectos
                                                                .Value(x => x.IdAuditoria, id_auditoria)
                                                                .Value(x => x.IdProyecto, id_proyecto)
                                                                .Value(x => x.Aplica, aplica)
                                                                .InsertAsync() > 0;

                    resp.Success = insert_auditoria_proyecto;
                    resp.Message = insert_auditoria_proyecto == default ? "Ocurrio un error al agregar registro." : string.Empty;
                }
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateAuditoriaProyecto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_proyecto = Convert.ToInt32(registro["id_proyecto"].ToString());
            var auditorias = registro["auditorias"].AsArray();

            using (var db = new ConnectionDB(dbConfig))
            {
                var delete_auditoria_proyecto = await db.tB_Auditoria_Proyectos
                                                                .Where(x => x.IdProyecto == id_proyecto)
                                                                .DeleteAsync() > 0;

                foreach (var a in auditorias)
                {
                    int? id_auditoria = a["id_auditoria"] != null ? Convert.ToInt32(a["id_auditoria"].ToString()) : null;
                    bool? aplica = a["aplica"] != null ? Convert.ToBoolean(a["aplica"].ToString()) : null;
                    string? motivo = a["motivo"] != null ? a["motivo"].ToString() : null;

                    var insert_auditoria_proyecto = await db.tB_Auditoria_Proyectos
                                                                .Value(x => x.IdAuditoria, id_auditoria)
                                                                .Value(x => x.IdProyecto, id_proyecto)
                                                                .Value(x => x.Aplica, aplica)
                                                                .InsertAsync() > 0;

                    resp.Success = insert_auditoria_proyecto;
                    resp.Message = insert_auditoria_proyecto == default ? "Ocurrio un error al agregar registro." : string.Empty;
                }
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> AddAuditoriaDocumento(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_auditoria_proyecto = Convert.ToInt32(registro["id_auditoria_proyecto"].ToString());
            string? motivo = registro["motivo"] != null ? registro["motivo"].ToString() :  null;
            string documento_base64 = registro["documento_base64"].ToString();

            using (var db = new ConnectionDB(dbConfig))
            {
                var insert_auditoria_documento = await db.tB_Auditoria_Documentos
                                                                .Value(x => x.IdAuditoriaProyecto, id_auditoria_proyecto)
                                                                .Value(x => x.Motivo, motivo)
                                                                .Value(x => x.Fecha, DateTime.Now)
                                                                .Value(x => x.DocumentoBase64, documento_base64)
                                                                .Value(x => x.Valido, true)
                                                                .Value(x => x.Activo, true)
                                                                .InsertAsync() > 0;

                resp.Success = insert_auditoria_documento;
                resp.Message = insert_auditoria_documento == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }

        public async Task<List<TB_AuditoriaDocumento>> GetDocumentosAuditoria(int IdAuditoria, int offset, int limit)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var docs = await (from doc in db.tB_Auditoria_Documentos
                                  where doc.IdAuditoriaProyecto == IdAuditoria
                                  && doc.Activo == true
                                  orderby doc.IdDocumento descending
                                  select doc)
                                  .Skip((offset - 1) * limit)
                                  .Take(limit)
                                  .ToListAsync();

                return docs;
            }
        }

        public async Task<TB_AuditoriaDocumento> GetDocumentoAuditoria(int IdDocumento)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var document = await (from doc in db.tB_Auditoria_Documentos
                                      where doc.IdDocumento == IdDocumento
                                      select doc)
                                  .FirstOrDefaultAsync();

                return document;
            }
        }

        public async Task<(bool Success, string Message)> AddAuditoriaDocumentoValidacion(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (var db = new ConnectionDB(dbConfig))
            {
                foreach (var r in registro["data"].AsArray())
                {
                    int id_documento = Convert.ToInt32(r["id_documento"].ToString());
                    bool valido = Convert.ToBoolean(r["valido"].ToString());

                    var res_valida_documento = await (db.tB_Auditoria_Documentos
                                                .Where(x => x.IdDocumento == id_documento)
                                                .UpdateAsync(x => new TB_AuditoriaDocumento
                                                {
                                                    Valido = valido
                                                })) > 0;

                    if (valido == false)
                    {
                        var delete_documento = await (db.tB_Auditoria_Documentos
                            .Where(x => x.IdDocumento == id_documento)
                            .UpdateAsync(x => new TB_AuditoriaDocumento
                            {
                                Activo = false
                            })) > 0;
                    }

                    resp.Success = res_valida_documento;
                    resp.Message = res_valida_documento == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                }
            }

            return resp;
        }

    }
}
