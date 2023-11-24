using Bovis.Common.Model;
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
using LinqToDB.Data;
using Bovis.Common.Model.NoTable;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Bovis.Data
{
    public class ReporteData : RepositoryLinq2DB<ConnectionDB>, IReporteData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public ReporteData()
        {
            this.ConfigurationDB = dbConfig;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region Custom Reports
        public async Task<object> ExecReportePersonalizado(JsonObject registro)
        {
            string custom_query = registro["query"].ToString();

            object custom_query_response;

            if (!custom_query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            {
                custom_query_response = "¡Consulta no permitida!";
                return custom_query_response;
            }

            using (var db = new ConnectionDB(dbConfig))
            {
                using (SqlConnection connection = new SqlConnection(db.ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(custom_query, connection))
                    {
                        try
                        {
                            using (SqlDataReader reader = await command.ExecuteReaderAsync())
                            {
                                var results = new List<Dictionary<string, object>>();

                                while (reader.Read())
                                {
                                    var row = new Dictionary<string, object>();
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        string columnName = reader.GetName(i);
                                        object columnValue = reader.IsDBNull(i) ? null : reader[i];
                                        row[columnName] = columnValue;
                                    }
                                    results.Add(row);
                                }

                                var settings = new JsonSerializerSettings
                                {
                                    ContractResolver = new DefaultContractResolver
                                    {
                                        NamingStrategy = new CamelCaseNamingStrategy()
                                    },
                                    NullValueHandling = NullValueHandling.Include
                                };

                                string json = JsonConvert.SerializeObject(results, settings);

                                custom_query_response = results!;
                            }
                        }
                        catch(Exception ex)
                        {
                            return ex.Message;
                        }
                    }
                }

                return custom_query_response;
            }
        }
        public async Task<(bool Success, string Message)> AddReportePersonalizado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            string nombre = registro["nombre"].ToString();
            string? descripcion = registro["descripcion"] != null ? registro["descripcion"].ToString() : null;
            string custom_query = registro["query"].ToString();
            int id_empleado_crea = Convert.ToInt32(registro["id_empleado_crea"].ToString());

            if (!custom_query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            {
                resp.Success = true;
                resp.Message = "¡Consulta no permitida!";
                return resp;
            }

            using (var db = new ConnectionDB(dbConfig))
            {
                var insert_custom_query = await db.tB_Reporte_Customs
                    .Value(x => x.Nombre, nombre)
                    .Value(x => x.Descripcion, descripcion)
                    .Value(x => x.Query, custom_query)
                    .Value(x => x.FechaCreacion, DateTime.Now)
                    .Value(x => x.IdEmpleadoCrea, id_empleado_crea)
                    .Value(x => x.Activo, true)
                    .InsertAsync() > 0;

                resp.Success = insert_custom_query;
                resp.Message = insert_custom_query == default ? "Ocurrio un error al agregar registro de reporte customizado." : string.Empty;

            }

            return resp;
        }
        public async Task<List<Reporte_Detalle>> GetReportesPersonalizados(int IdReporte)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var reportes = await (from a in db.tB_Reporte_Customs
                                      join b in db.tB_Empleados on a.IdEmpleadoCrea equals b.NumEmpleadoRrHh into bJoin
                                      from bItem in bJoin.DefaultIfEmpty()
                                      join c in db.tB_Personas on bItem.IdPersona equals c.IdPersona into cJoin
                                      from cItem in cJoin.DefaultIfEmpty()
                                      join d in db.tB_Empleados on a.IdEmpleadoActualiza equals d.NumEmpleadoRrHh into dJoin
                                      from dItem in dJoin.DefaultIfEmpty()
                                      join e in db.tB_Personas on dItem.IdPersona equals e.IdPersona into eJoin
                                      from eItem in eJoin.DefaultIfEmpty()
                                      where a.Activo == true
                                      && (IdReporte == 0 || a.IdReporte == IdReporte)
                                      orderby a.FechaCreacion descending
                                      select new Reporte_Detalle
                                      {
                                          IdReporte = a.IdReporte,
                                          Nombre = a.Nombre,
                                          Descripcion = a.Descripcion,
                                          Query = a.Query,
                                          FechaCreacion = a.FechaCreacion,
                                          IdEmpleadoCrea = a.IdEmpleadoCrea,
                                          EmpleadoCrea = cItem != null ? cItem.Nombre + " " + cItem.ApPaterno + " " + cItem.ApMaterno : string.Empty,
                                          FechaActualizacion = a.FechaActualizacion,
                                          IdEmpleadoActualiza = a.IdEmpleadoActualiza,
                                          EmpleadoActualiza = eItem != null ? eItem.Nombre + " " + eItem.ApPaterno + " " + eItem.ApMaterno : string.Empty,
                                          Activo = a.Activo
                                      }).ToListAsync();

                return reportes;
            }
        }
        public async Task<(bool Success, string Message)> UpdateReportePersonalizado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_reporte = Convert.ToInt32(registro["id_reporte"].ToString());
            string nombre = registro["nombre"].ToString();
            string? descripcion = registro["descripcion"] != null ? registro["descripcion"].ToString() : null;
            string custom_query = registro["query"].ToString();
            int? id_empleado_actualiza = registro["id_empleado_actualiza"] != null ? Convert.ToInt32(registro["id_empleado_actualiza"].ToString()) : null;

            if (!custom_query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            {
                resp.Success = true;
                resp.Message = "¡Consulta no permitida!";
                return resp;
            }

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_custom_query = await (db.tB_Reporte_Customs.Where(x => x.IdReporte == id_reporte)
                    .UpdateAsync(x => new TB_Reporte_Custom
                    {
                        Nombre = nombre,
                        Descripcion = descripcion,
                        Query = custom_query,
                        FechaActualizacion = DateTime.Now,
                        IdEmpleadoActualiza = id_empleado_actualiza
                    })) > 0;

                resp.Success = res_update_custom_query;
                resp.Message = res_update_custom_query == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        public async Task<(bool Success, string Message)> DeleteReportePersonalizado(int IdReporte)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_custom_query = await (db.tB_Reporte_Customs.Where(x => x.IdReporte == IdReporte)
                                .UpdateAsync(x => new TB_Reporte_Custom
                                {
                                    Activo = false
                                })) > 0;

                resp.Success = res_update_custom_query;
                resp.Message = res_update_custom_query == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        #endregion Custom Reports
    }
}
