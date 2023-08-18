using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System.Text.Json.Nodes;

namespace Bovis.Data
{
    public class PcsData : RepositoryLinq2DB<ConnectionDB>, IPcsData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public PcsData()
        {
            this.ConfigurationDB = dbConfig;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<List<TB_Proyecto>> GetProyectos()
        {
            //return await GetAllFromEntityAsync<TB_Proyecto>();
            using (var db = new ConnectionDB(dbConfig))
            {
                var resp = await (from p in db.tB_Proyectos
                              orderby p.Proyecto ascending
                              select p).ToListAsync();

                return resp;
            }
        }

        public async Task<TB_Proyecto> GetProyecto(int numProyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var resp = await (from p in db.tB_Proyectos
                              where p.NumProyecto == numProyecto
                              select p).FirstOrDefaultAsync();

                return resp;
            }
        }

        public async Task<List<TB_Cliente>> GetClientes()
        {
            return await GetAllFromEntityAsync<TB_Cliente>();
        }
        public async Task<List<TB_Empresa>> GetEmpresas()
        {
            return await GetAllFromEntityAsync<TB_Empresa>();
        }

        #region Etapas
        public async Task<(bool Success, string Message)> AddEtapa(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_insert_etapa = true;

                resp.Success = res_insert_etapa;
                resp.Message = res_insert_etapa == default ? "Ocurrio un error al insertar registro." : string.Empty;
            }

            return resp;
        }
        public async Task<List<PCS_Etapa_Detalle>> GetEtapas(int IdProyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var etapas = new List<PCS_Etapa_Detalle>();

                return etapas;
            }
        }
        public async Task<(bool Success, string Message)> UpdateEtapa(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_etapa = true;

                resp.Success = res_update_etapa;
                resp.Message = res_update_etapa == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        public async Task<(bool Success, string Message)> DeleteEtapa(int IdEtapa)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_etapa = true;

                resp.Success = res_update_etapa;
                resp.Message = res_update_etapa == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        #endregion Etapas

        #region Empleados
        public async Task<(bool Success, string Message)> AddEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_insert_empleado = true;

                resp.Success = res_insert_empleado;
                resp.Message = res_insert_empleado == default ? "Ocurrio un error al insertar registro." : string.Empty;
            }

            return resp;
        }
        public async Task<List<PCS_Empleado_Detalle>> GetEmpleados(int IdProyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var empleados = new List<PCS_Empleado_Detalle>();

                return empleados;
            }
        }
        public async Task<(bool Success, string Message)> UpdateEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_empleado = true;

                resp.Success = res_update_empleado;
                resp.Message = res_update_empleado == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        public async Task<(bool Success, string Message)> DeleteEmpleado(int IdEmpleado)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_empleado = true;

                resp.Success = res_update_empleado;
                resp.Message = res_update_empleado == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        #endregion Empleados
    }
}
