using Azure.Core;
using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Bovis.Data
{
    public class ContratoData : RepositoryLinq2DB<ConnectionDB>, IContratoData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public ContratoData()
        {
            this.ConfigurationDB = dbConfig;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base


        #region Templates
        public async Task<List<TB_ContratoTemplate>> GetTemplates(string Estatus)
        {
            List<TB_ContratoTemplate> list = new List<TB_ContratoTemplate>();
            using (var db = new ConnectionDB(dbConfig))
            {
                switch (Estatus)
                {
                    case "todos":
                        list = await (from contrato in db.tB_Contrato_Templates
                                      orderby contrato.IdContratoTemplate descending
                                      select contrato).ToListAsync();
                        break;
                    case "activos":
                        list = await (from contrato in db.tB_Contrato_Templates
                                      where contrato.Activo == true
                                      orderby contrato.IdContratoTemplate descending
                                      select contrato).ToListAsync();
                        break;
                    case "inactivos":
                        list = await (from contrato in db.tB_Contrato_Templates
                                      where contrato.Activo == false
                                      orderby contrato.IdContratoTemplate descending
                                      select contrato).ToListAsync();
                        break;
                }

            }

            return list;
        }

        public async Task<TB_ContratoTemplate> GetTemplate(int IdTemplate)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var template = await (from contrato in db.tB_Contrato_Templates
                                      where contrato.IdContratoTemplate == IdTemplate
                                      select contrato).FirstOrDefaultAsync();

                return template;
            }
        }

        public async Task<(bool Success, string Message)> AddTemplate(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            string titulo = registro["titulo"].ToString();
            string template = registro["template"].ToString();

            using (var db = new ConnectionDB(dbConfig))
            {
                var insert_timesheet = await db.tB_Contrato_Templates
                    .Value(x => x.Titulo, titulo)
                    .Value(x => x.Template, template)
                    .Value(x => x.Activo, true)
                    .InsertAsync() > 0;

                resp.Success = insert_timesheet;
                resp.Message = insert_timesheet == default ? "Ocurrio un error al agregar registro." : string.Empty;
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateTemplate(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_contrato_template = Convert.ToInt32(registro["id_contrato_template"].ToString());
            string titulo = registro["titulo"].ToString();
            string template = registro["template"].ToString();

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_template = await db.tB_Contrato_Templates.Where(x => x.IdContratoTemplate == id_contrato_template)
                    .UpdateAsync(x => new TB_ContratoTemplate
                    {
                        Titulo = titulo,
                        Template = template
                    }) > 0;

                resp.Success = res_update_template;
                resp.Message = res_update_template == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateTemplateEstatus(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_contrato_template = Convert.ToInt32(registro["id_contrato_template"].ToString());
            bool activo = Convert.ToBoolean(registro["boactivo"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_update_template = await db.tB_Contrato_Templates.Where(x => x.IdContratoTemplate == id_contrato_template)
                    .UpdateAsync(x => new TB_ContratoTemplate
                    {
                        Activo = activo
                    }) > 0;

                resp.Success = res_update_template;
                resp.Message = res_update_template == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }
            return resp;
        }
        #endregion Templates

        #region Contratos Empleado
        public async Task<List<TB_ContratoEmpleado>> GetContratosEmpleado(int IdEmpleado)
        {
            if (IdEmpleado > 0)
            {
                List<TB_ContratoEmpleado> list = new List<TB_ContratoEmpleado>();
                using (var db = new ConnectionDB(dbConfig))
                {
                    list = await (from contrato in db.tB_Contrato_Empleados
                                  where contrato.NumEmpleadoRrHh == IdEmpleado
                                  && contrato.Activo == true
                                  orderby contrato.IdContratoEmpleado descending
                                  select contrato).ToListAsync();
                }

                return list;
            }
            else return await GetAllFromEntityAsync<TB_ContratoEmpleado>();
        }

        public async Task<TB_ContratoEmpleado> GetContratoEmpleado(int IdContratoEmpleado)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var contratoEmpleado = await (from contrato in db.tB_Contrato_Empleados
                                              where contrato.IdContratoEmpleado == IdContratoEmpleado
                                              select contrato).FirstOrDefaultAsync();

                return contratoEmpleado;
            }
        }

        public async Task<(bool Success, string Message)> AddContratoEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            string titulo = registro["titulo"].ToString();
            string contrato = registro["contrato"].ToString();
            int id_empleado = Convert.ToInt32(registro["id_empleado"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                var insert_timesheet = await db.tB_Contrato_Empleados
                    .Value(x => x.Titulo, titulo)
                    .Value(x => x.Contrato, contrato)
                    .Value(x => x.NumEmpleadoRrHh, id_empleado)
                    .Value(x => x.Activo, true)
                    .InsertAsync() > 0;

                resp.Success = insert_timesheet;
                resp.Message = insert_timesheet == default ? "Ocurrio un error al agregar registro." : string.Empty;
            }

            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateContratoEmpleado(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id_contrato_empleado = Convert.ToInt32(registro["id_contrato_empleado"].ToString());
            string titulo = registro["titulo"].ToString();
            string contrato = registro["contrato"].ToString();

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_template = await db.tB_Contrato_Empleados.Where(x => x.IdContratoEmpleado == id_contrato_empleado)
                    .UpdateAsync(x => new TB_ContratoEmpleado
                    {
                        Titulo = titulo,
                        Contrato = contrato
                    }) > 0;

                resp.Success = res_update_template;
                resp.Message = res_update_template == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        #endregion Contratos Empleado
    }
}
