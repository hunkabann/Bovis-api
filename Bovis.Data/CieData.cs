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
using static LinqToDB.Reflection.Methods.LinqToDB;

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
        #endregion base

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


        public async Task<(bool existe, string mensaje)> AddRegistro(TB_Cie registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                var insert = await db.tB_Cies
                .Value(x => x.IdCie, registro.IdCie)
                .Value(x => x.NumProyecto, registro.NumProyecto)
                .Value(x => x.IdTipoCie, registro.IdTipoCie)
                .Value(x => x.IdTipoPoliza, registro.IdTipoPoliza)
                .Value(x => x.Fecha, registro.Fecha)
                .Value(x => x.FechaCaptura, registro.FechaCaptura)
                .Value(x => x.Concepto, registro.Concepto)
                .Value(x => x.SaldoIni, registro.SaldoIni)
                .Value(x => x.Debe, registro.Debe)
                .Value(x => x.Haber, registro.Haber)
                .Value(x => x.Movimiento, registro.Movimiento)
                .Value(x => x.EdoResultados, registro.EdoResultados)
                .Value(x => x.Mes, registro.Mes)
                .Value(x => x.IdCentroCostos, registro.IdCentroCostos)
                .Value(x => x.IdTipoCtaContable, registro.IdTipoCtaContable)
                .Value(x => x.Estatus, registro.Estatus)
                .InsertAsync() > 0;

                resp.Success = insert;
                resp.Message = insert == default ? "Ocurrio un error al agregar registro Cie." : string.Empty;
            }
            return resp;
        }

        public async Task<(bool existe, string mensaje)> AddRegistros(List<TB_Cie> registros)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                bool insert = false;
                foreach (var registro in registros)
                {
                    insert = await db.tB_Cies
                    .Value(x => x.IdCie, registro.IdCie)
                    .Value(x => x.NumProyecto, registro.NumProyecto)
                    .Value(x => x.IdTipoCie, registro.IdTipoCie)
                    .Value(x => x.IdTipoPoliza, registro.IdTipoPoliza)
                    .Value(x => x.Fecha, registro.Fecha)
                    .Value(x => x.FechaCaptura, registro.FechaCaptura)
                    .Value(x => x.Concepto, registro.Concepto)
                    .Value(x => x.SaldoIni, registro.SaldoIni)
                    .Value(x => x.Debe, registro.Debe)
                    .Value(x => x.Haber, registro.Haber)
                    .Value(x => x.Movimiento, registro.Movimiento)
                    .Value(x => x.EdoResultados, registro.EdoResultados)
                    .Value(x => x.Mes, registro.Mes)
                    .Value(x => x.IdCentroCostos, registro.IdCentroCostos)
                    .Value(x => x.IdTipoCtaContable, registro.IdTipoCtaContable)
                    .Value(x => x.Estatus, registro.Estatus)
                    .InsertAsync() > 0;
                }
                resp.Success = insert;
                resp.Message = insert == default ? "Ocurrio un error al agregar registro Cie." : string.Empty;
            }
            return resp;
        }

        #endregion Registros
    }
}
