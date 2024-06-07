using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using LinqToDB.Tools;
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

        public async Task<List<TB_Proyecto>> GetProyectos(string email_loged_user, string TipoAuditoria)
        {
            List<TB_Proyecto> proyectos = new List<TB_Proyecto>();

            using (var db = new ConnectionDB(dbConfig))
            {
                var rol_loged_user = await (from emp in db.tB_Empleados
                                            join usr in db.tB_Usuarios on emp.NumEmpleadoRrHh equals usr.NumEmpleadoRrHh
                                            join perf_usr in db.tB_PerfilUsuarios on usr.IdUsuario equals perf_usr.IdUsuario
                                            join perf in db.tB_Perfils on perf_usr.IdPerfil equals perf.IdPerfil
                                            where emp.EmailBovis == email_loged_user
                                            && perf.Perfil == "Administrador"
                                            select perf).FirstOrDefaultAsync();

                bool is_admin = rol_loged_user != null;


                if (TipoAuditoria == "calidad" || (is_admin == true && TipoAuditoria == "legal"))
                {
                    proyectos = await (from p in db.tB_Proyectos
                                       orderby p.Proyecto ascending
                                       select p).ToListAsync();
                }
                else if (is_admin == false && TipoAuditoria == "legal")
                {
                    var num_empleado_loged = await (from emp in db.tB_Empleados
                                                    where emp.EmailBovis == email_loged_user
                                                    select emp.NumEmpleadoRrHh).FirstOrDefaultAsync();

                    proyectos = await (from p in db.tB_Proyectos
                                       join e in db.tB_Empleados on p.NumProyecto equals e.NumProyectoPrincipal into eJoin
                                       from eItem in eJoin.DefaultIfEmpty()
                                       where eItem.NumEmpleadoRrHh == num_empleado_loged
                                       orderby p.Proyecto ascending
                                       select p).ToListAsync();
                }

                return proyectos;
            }
        }


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

        /*
        public async Task<List<Documentos_Auditoria_Proyecto_Detalle>> GetAuditoriasByProyecto(int IdProyecto, string TipoAuditoria)
        {
            List<Documentos_Auditoria_Proyecto_Detalle> documentos_auditoria = new List<Documentos_Auditoria_Proyecto_Detalle>();
            Documentos_Auditoria_Proyecto_Detalle documento_auditoria = null;

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
                    int totalDocumentos = 0;
                    int totalDocumentosValidados = 0;
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
                            if (audit.Aplica == true)
                            {
                                auditoria.Aplica = true;
                                auditoria.Auditorias.Add(audit);
                            }

                            var documentos = await (from documento in db.tB_Auditoria_Documentos
                                                    where documento.IdAuditoriaProyecto == audit.IdAuditoria
                                                    //&& documento.Fecha.Month == DateTime.Now.Month
                                                    //&& documento.Fecha.Year == DateTime.Now.Year
                                                    && documento.Activo == true
                                                    select documento).ToListAsync();

                            audit.TieneDocumento = documentos.Count > 0;
                            audit.CantidadDocumentos = documentos.Count;
                            audit.CantidadDocumentosValidados = documentos.Where(x => x.Valido == true).Count();
                            totalDocumentos += documentos.Count;
                            auditoria.TotalDocumentos = totalDocumentos;
                            totalDocumentosValidados += (int)audit.CantidadDocumentosValidados;
                            auditoria.TotalDocumentosValidados = totalDocumentosValidados;

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

                    if (auditoria.Auditorias.Count > 0)
                        documentos_auditoria.Add(auditoria);
                }

            }

            return documentos_auditoria;
        }
        */


        public async Task<List<Documentos_Auditoria_Proyecto_Detalle>> GetAuditoriasByProyecto(int IdProyecto, string TipoAuditoria)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var documentos_auditoria = await (
                    from audit in db.tB_Auditoria_Proyectos
                    join cat in db.tB_Cat_Auditorias on audit.IdAuditoria equals cat.IdAuditoria into catJoin
                    from catItem in catJoin.DefaultIfEmpty()
                    join sec in db.tB_Cat_Auditoria_Seccions on catItem.IdSeccion equals sec.IdSeccion into secJoin
                    from secItem in secJoin.DefaultIfEmpty()
                    where audit.IdProyecto == IdProyecto
                    && (catItem.TipoAuditoria == TipoAuditoria || catItem.TipoAuditoria == "ambos")
                    && audit.Aplica == true
                    select new
                    {
                        Auditoria = audit,
                        CatAuditoria = catItem,
                        Seccion = secItem
                    }).ToListAsync();

                var documentos_auditoria_detalle = documentos_auditoria.Select(a => new Auditoria_Detalle
                {
                    IdAuditoriaProyecto = a.Auditoria.IdAuditoriaProyecto,
                    IdAuditoria = a.Auditoria.IdAuditoria,
                    IdProyecto = a.Auditoria.IdProyecto,
                    IdDirector = a.CatAuditoria?.IdDirector,
                    Mes = a.CatAuditoria?.Mes,
                    Fecha = a.CatAuditoria?.Fecha,
                    Punto = a.CatAuditoria?.Punto,
                    IdSeccion = a.CatAuditoria?.IdSeccion ?? 0,
                    ChSeccion = a.Seccion?.Seccion ?? string.Empty,
                    Cumplimiento = TipoAuditoria == "calidad" ? a.CatAuditoria?.CumplimientoCalidad : TipoAuditoria == "legal" ? a.CatAuditoria?.CumplimientoLegal : a.CatAuditoria?.CumplimientoCalidad,
                    DocumentoRef = a.CatAuditoria?.DocumentoRef,
                    TipoAuditoria = a.CatAuditoria?.TipoAuditoria ?? string.Empty,
                    Aplica = a.Auditoria.Aplica,
                    TieneDocumento = db.tB_Auditoria_Documentos.Any(d => d.IdAuditoriaProyecto == a.Auditoria.IdAuditoriaProyecto && d.Activo),
                    CantidadDocumentos = db.tB_Auditoria_Documentos.Count(d => d.IdAuditoriaProyecto == a.Auditoria.IdAuditoriaProyecto && d.Activo),
                    CantidadDocumentosValidados = db.tB_Auditoria_Documentos.Count(d => d.IdAuditoriaProyecto == a.Auditoria.IdAuditoriaProyecto && (bool)d.Valido && d.Activo),
                    IdDocumento = db.tB_Auditoria_Documentos.OrderByDescending(d => d.Fecha).FirstOrDefault(d => d.IdAuditoriaProyecto == a.Auditoria.IdAuditoriaProyecto && d.Activo)?.IdDocumento ?? 0,
                    UltimoDocumentoValido = db.tB_Auditoria_Documentos.OrderByDescending(d => d.Fecha).FirstOrDefault(d => d.IdAuditoriaProyecto == a.Auditoria.IdAuditoriaProyecto && d.Activo)?.Valido ?? false
                }).ToList();

                var auditorias_agrupadas = documentos_auditoria_detalle.GroupBy(d => new { d.IdSeccion, d.ChSeccion })
                    .Select(group => new Documentos_Auditoria_Proyecto_Detalle
                    {
                        IdSeccion = group.Key.IdSeccion,
                        ChSeccion = group.Key.ChSeccion,
                        Auditorias = group.ToList(),
                        Aplica = group.Any(a => (bool)a.Aplica),
                        TotalDocumentos = group.Sum(a => (int)a.CantidadDocumentos),
                        TotalDocumentosValidados = group.Sum(a => (int)a.CantidadDocumentosValidados),
                        NuProcentaje = Math.Round(group.Count(a => (bool)a.Aplica) / (decimal)group.Count() * 100)
                    }).ToList();

                return auditorias_agrupadas;
            }
        }




        public async Task<List<TB_Cat_AuditoriaTipoComentario>> GetTipoComentarios()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var tipo_comentarios = await (from t in db.tB_Cat_AuditoriaTipoComentarios
                                              select t).ToListAsync();

                return tipo_comentarios;
            }
        }

        public async Task<List<Comentario_Detalle>> GetComentarios(int numProyecto)
        {
            List<Comentario_Detalle> comentarios = new List<Comentario_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                comentarios = await (from c in db.tB_AuditoriaComentarios
                                     join t in db.tB_Cat_AuditoriaTipoComentarios on c.IdTipoComentario equals t.IdTipoComentario into tJoin
                                     from tItem in tJoin.DefaultIfEmpty()
                                     join p in db.tB_Proyectos on c.NumProyecto equals p.NumProyecto into pJoin
                                     from pItem in pJoin.DefaultIfEmpty()
                                     join e in db.tB_Empleados on pItem.IdDirectorEjecutivo equals e.NumEmpleadoRrHh into eJoin
                                     from eItem in eJoin.DefaultIfEmpty()
                                     join per1 in db.tB_Personas on eItem.IdPersona equals per1.IdPersona into per1Join
                                     from per1Item in per1Join.DefaultIfEmpty()
                                     where c.NumProyecto == numProyecto
                                     select new Comentario_Detalle
                                     {
                                         IdComentario = c.IdComentario,
                                         NumProyecto = c.NumProyecto,
                                         Comentario = c.Comentario,
                                         Fecha = c.Fecha,
                                         IdTipoComentario = c.IdTipoComentario,
                                         TipoComentario = tItem != null ? tItem.TipoComentario : string.Empty,
                                         NombreAuditor = c.NombreAuditor,
                                         DirectorResponsable = per1Item != null ? per1Item.Nombre + " " + per1Item.ApPaterno + " " + per1Item.ApMaterno : string.Empty,
                                         ResponsableAsignado = pItem != null ? pItem.ResponsableAsignado : string.Empty,
                                         FechaAuditoriaInicial = pItem.FechaAuditoriaInicial,
                                         FechaAuditoria = pItem.FechaProxAuditoria
                                     }).ToListAsync();
            }

            return comentarios;
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

        public async Task<(bool Success, string Message)> AddComentarios(JsonObject registro, string usuario_logueado)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
            string comentario = registro["comentario"].ToString();
            int id_tipo_comentario = Convert.ToInt32(registro["id_tipo_comentario"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                var insert_auditoria_proyecto = await db.tB_AuditoriaComentarios
                                                            .Value(x => x.NumProyecto, num_proyecto)
                                                            .Value(x => x.Comentario, comentario)
                                                            .Value(x => x.Fecha, DateTime.Now)
                                                            .Value(x => x.IdTipoComentario, id_tipo_comentario)
                                                            .Value(x => x.NombreAuditor, usuario_logueado)
                                                            .InsertAsync() > 0;

                resp.Success = insert_auditoria_proyecto;
                resp.Message = insert_auditoria_proyecto == default ? "Ocurrio un error al agregar registro." : string.Empty;

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
            string? motivo = registro["motivo"] != null ? registro["motivo"].ToString() : null;
            string documento_base64 = registro["documento_base64"].ToString();
            string nombre_documento = registro["nombre_documento"].ToString();

            using (var db = new ConnectionDB(dbConfig))
            {
                var insert_auditoria_documento = await db.tB_Auditoria_Documentos
                                                                .Value(x => x.IdAuditoriaProyecto, id_auditoria_proyecto)
                                                                .Value(x => x.Motivo, motivo)
                                                                .Value(x => x.Fecha, DateTime.Now)
                                                                .Value(x => x.DocumentoBase64, documento_base64)
                                                                .Value(x => x.NombreDocumento, nombre_documento)
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
                                  //&& doc.Fecha.Month == DateTime.Now.Month
                                  //&& doc.Fecha.Year == DateTime.Now.Year
                                  && doc.Activo == true
                                  orderby doc.IdDocumento descending
                                  select new TB_AuditoriaDocumento
                                  {
                                      IdDocumento = doc.IdDocumento,
                                      IdAuditoriaProyecto = doc.IdAuditoriaProyecto,
                                      Motivo = doc.Motivo,
                                      Fecha = doc.Fecha,
                                      NombreDocumento = doc.NombreDocumento,
                                      ComentarioRechazo = doc.ComentarioRechazo,
                                      Valido = doc.Valido,
                                      Activo = doc.Activo
                                  })
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
                    string? comentario_rechazo = r["comentario_rechazo"] != null ? r["comentario_rechazo"].ToString() : null;


                    var res_valida_documento = await (db.tB_Auditoria_Documentos
                                                .Where(x => x.IdDocumento == id_documento)
                                                .UpdateAsync(x => new TB_AuditoriaDocumento
                                                {
                                                    ComentarioRechazo = comentario_rechazo,
                                                    Valido = valido
                                                })) > 0;

                    resp.Success = res_valida_documento;
                    resp.Message = res_valida_documento == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                }
            }

            return resp;
        }

    }
}