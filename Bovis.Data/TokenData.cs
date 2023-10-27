using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static LinqToDB.Reflection.Methods.LinqToDB;

namespace Bovis.Data
{
    public class TokenData : RepositoryLinq2DB<ConnectionDB>, ITokenData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public TokenData()
        {
            this.ConfigurationDB = dbConfig;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<(bool Success, string Message)> AddToken(string email, string str_token)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (var db = new ConnectionDB(dbConfig))
            {
                var empleado = await (from emp in db.tB_Empleados
                                      where emp.EmailBovis == email
                                      && emp.Activo == true
                                      select emp).FirstOrDefaultAsync();

                var res_update_user_token = await (db.tB_Usuarios.Where(x => x.NumEmpleadoRrHh == empleado!.NumEmpleadoRrHh)
                                .UpdateAsync(x => new TB_Usuario
                                {
                                    Token = str_token,
                                    FechaUltimaSesion = DateTime.Now
                                })) > 0;

                resp.Success = res_update_user_token;
                resp.Message = res_update_user_token == default ? "Ocurrio un error al actualizar token del usuario." : string.Empty;

            }

            return resp;
        }

    }
}
