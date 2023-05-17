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
    public class CieData : RepositoryLinq2DB<ConnectionDB>, ICieData
    {
        #region base
        private readonly string dbConfig = "DBConfig";

        public CieData()
        {
            this.ConfigurationDB = dbConfig;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion

        #region Empresas
        public async Task<List<TB_Empresa>> GetEmpresas(bool? activo)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cat in db.tB_Empresas
                                                                          where cat.Activo == activo
                                                                          select cat).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Empresa>();
        }
        #endregion Empresas

        #region Registros
        public async Task<CieRegistro> GetInfoRegistro(int? idRegistro)
        {
            using (var db = new ConnectionDB(dbConfig))
            {

                var res = from a in db.tB_Cies
                          where a.IdCie == idRegistro
                          select new CieRegistro
                          {
                              IdCie = a.IdCie,
                              NumProyecto = a.NumProyecto,
                              IdTipoCie = a.IdTipoCie,
                              IdTipoPoliza = a.IdTipoPoliza,
                              Fecha = a.Fecha,
                              FechaCaptura = a.FechaCaptura,
                              Concepto = a.Concepto,
                              SaldoIni = a.SaldoIni,
                              Debe = a.Debe,
                              Haber = a.Haber,
                              Movimiento = a.Movimiento,
                              EdoResultados = a.EdoResultados,
                              Mes = a.Mes,
                              IdCentroCostos = a.IdCentroCostos,
                              IdTipoCtaContable = a.IdTipoCtaContable
                          };

                return await res.FirstOrDefaultAsync();

            }
        }

        public async Task<List<TB_Cie>> GetRegistros(byte? estatus)
        {
            if (estatus.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cie in db.tB_Cies
                                                                          where cie.Estatus == estatus
                                                                          select cie).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Cie>();
        }
        #endregion Registros
    }
}
