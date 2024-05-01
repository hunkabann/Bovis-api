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
using System.Linq.Dynamic.Core;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB.Insert;

namespace Bovis.Data
{
    public class DashboardData : RepositoryLinq2DB<ConnectionDB>, IDashboardData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public DashboardData()
        {
            this.ConfigurationDB = dbConfig;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base



        #region Proyectos Documentos
        public async Task<List<ProyectosDocumentos>> GetProyectosDocumentos()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                List<ProyectosDocumentos> proyectosDocumentos = new List<ProyectosDocumentos>();                

                var proyectosAuditorias = await db.tB_Proyectos
                                        .Select(p => new
                                        {
                                            NumProyecto = p.NumProyecto,
                                            Proyecto = p.Proyecto,
                                            TotalDocumentos = db.tB_Auditoria_Proyectos.Count(ap => ap.IdProyecto == p.NumProyecto && ap.Aplica == true),
                                        })
                                        //.Where(proy => proy.NumProyecto == 244)
                                        .ToListAsync();

                foreach (var proyectoAuditorias in proyectosAuditorias)
                {
                    ProyectosDocumentos proyectoDocumentos = new ProyectosDocumentos();
                    Series serie_cargados = new Series { name = "Cargados" };
                    Series serie_total_documentos = new Series { name = "Total de documentos" };
                    proyectoDocumentos.series = new List<Series>();

                    proyectoDocumentos.name = proyectoAuditorias.Proyecto;
                    serie_total_documentos.value = proyectoAuditorias.TotalDocumentos;

                    var documentosCargados = await db.tB_Auditoria_Proyectos
                                                        .Where(audit => audit.IdProyecto == proyectoAuditorias.NumProyecto && audit.Aplica == true)
                                                        .SelectMany(audit => db.tB_Auditoria_Documentos
                                                            .Where(doc => doc.IdAuditoriaProyecto == audit.IdAuditoriaProyecto && doc.Valido == true)
                                                            .Select(doc => doc.IdDocumento))
                                                        .ToListAsync();

                    serie_cargados.value = documentosCargados.Count();



                    proyectoDocumentos.series.Add(serie_cargados);
                    proyectoDocumentos.series.Add(serie_total_documentos);
                    proyectosDocumentos.Add(proyectoDocumentos);
                }

                return proyectosDocumentos;
            }
        }
        #endregion Proyectos Documentos
    }
}
