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
    public class TimesheetData : RepositoryLinq2DB<ConnectionDB>, ITimesheetData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public TimesheetData()
        {
            this.ConfigurationDB = dbConfig;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        public async Task<Dias_Timesheet_Detalle> GetDiasHabiles(int mes, int anio, bool sabados)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = from timeS in db.tB_Dias_Timesheets
                          where timeS.Mes == mes
                          && timeS.Anio == anio
                          select new Dias_Timesheet_Detalle
                          {
                              id = timeS.Id,
                              mes = timeS.Mes,
                              dias = timeS.Dias,
                              feriados = timeS.Feriados,
                              sabados = timeS.Sabados,
                              anio = timeS.Anio,
                              dias_habiles = (sabados == true) ? timeS.Dias - timeS.Feriados - timeS.Sabados : timeS.Dias - timeS.Feriados
                          };

                return await res.FirstOrDefaultAsync();

            }
        }
    }
}
