using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
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
                using (var db = new ConnectionDB(dbConfig)) return await (from emp in db.tB_Empresas
                                                                          where emp.Activo == activo
                                                                          select emp).ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Empresa>();
        }
        #endregion Empresas

        #region Registros
        public async Task<TB_Cie_Data> GetRegistro(int? idRegistro)
        {
            using (var db = new ConnectionDB(dbConfig))
            {

                var res = from a in db.tB_Cie_Datas
                          where a.IdCieData == idRegistro
                          select a;

                return await res.FirstOrDefaultAsync();

            }
        }
        public async Task<List<TB_Cie_Data>> GetRegistros(bool? activo, int offset, int limit)
        {
            if (activo.HasValue)
            {
                using (var db = new ConnectionDB(dbConfig)) return await (from cie in db.tB_Cie_Datas
                                                                          where cie.Activo == activo
                                                                          select cie)
                                                                          .Skip((offset - 1) * limit)
                                                                          .Take(limit)
                                                                          .ToListAsync();
            }
            else return await GetAllFromEntityAsync<TB_Cie_Data>();
        }

        public async Task<(bool existe, string mensaje)> AddRegistros(JsonObject registros)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (var db = new ConnectionDB(dbConfig))
            {
                bool insert = false;
                foreach (var registro in registros["data"].AsArray())
                {
                    string nombre_cuenta = registro["nombre_cuenta"].ToString();
                    string cuenta = registro["cuenta"].ToString();
                    string tipo_poliza = registro["tipo_poliza"].ToString();
                    int numero = Convert.ToInt32(registro["numero"].ToString());
                    string fecha_str = registro["fecha"].ToString();
                    //DateTime fecha = Convert.ToDateTime(registro["fecha"].ToString());
                    int mes = Convert.ToInt32(registro["mes"].ToString());
                    string concepto = registro["concepto"].ToString();
                    string centro_costos = registro["centro_costos"].ToString();
                    string proyectos = registro["proyectos"].ToString();
                    decimal saldo_inicial = Convert.ToDecimal(registro["saldo_inicial"].ToString());
                    decimal debe = Convert.ToDecimal(registro["debe"].ToString());
                    decimal haber = Convert.ToDecimal(registro["haber"].ToString());
                    decimal movimiento = Convert.ToDecimal(registro["movimiento"].ToString());
                    string empresa = registro["empresa"].ToString();
                    int num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
                    string tipo_cuenta = registro["tipo_cuenta"].ToString();
                    string edo_resultados = registro["edo_resultados"].ToString();
                    string responsable = registro["responsable"].ToString();
                    string tipo_proyecto = registro["tipo_proyecto"].ToString();
                    string tipo_py = registro["tipo_py"].ToString();
                    string clasificacion_py = registro["clasificacion_py"].ToString();
                    DateTime fecha;

                    if (!DateTime.TryParseExact(fecha_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                    {
                        resp.Success = insert;
                        resp.Message = insert == default ? "Ocurrio un error al agregar registro Cie." : string.Empty;
                        return resp;
                    }

                    insert = await db.tB_Cie_Datas
                        .Value(x => x.NombreCuenta, nombre_cuenta)
                        .Value(x => x.Cuenta, cuenta)
                        .Value(x => x.TipoPoliza, tipo_poliza)
                        .Value(x => x.Numero, numero)
                        .Value(x => x.Fecha, fecha)
                        .Value(x => x.Mes, mes)
                        .Value(x => x.Concepto, concepto)
                        .Value(x => x.CentroCostos, centro_costos)
                        .Value(x => x.Proyectos, proyectos)
                        .Value(x => x.SaldoInicial, saldo_inicial)
                        .Value(x => x.Debe, debe)
                        .Value(x => x.Haber, haber)
                        .Value(x => x.Movimiento, movimiento)
                        .Value(x => x.Empresa, empresa)
                        .Value(x => x.NumProyecto, num_proyecto)
                        .Value(x => x.TipoCuenta, tipo_cuenta)
                        .Value(x => x.EdoResultados, edo_resultados)
                        .Value(x => x.Responsable, responsable)
                        .Value(x => x.TipoProyecto, tipo_proyecto)
                        .Value(x => x.TipoPY, tipo_py)
                        .Value(x => x.ClasificacionPY, clasificacion_py)
                        .Value(x => x.Activo, true)
                        .InsertAsync() > 0;

                    resp.Success = insert;
                    resp.Message = insert == default ? "Ocurrio un error al agregar registro Cie." : string.Empty;
                }
            }
            return resp;
        }

        public async Task<(bool existe, string mensaje)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (var db = new ConnectionDB(dbConfig))
            {
                bool update = false;
                int id_cie = Convert.ToInt32(registro["id_cie"].ToString());
                string nombre_cuenta = registro["nombre_cuenta"].ToString();
                string cuenta = registro["cuenta"].ToString();
                string tipo_poliza = registro["tipo_poliza"].ToString();
                int numero = Convert.ToInt32(registro["numero"].ToString());
                string fecha_str = registro["fecha"].ToString();
                //DateTime fecha = Convert.ToDateTime(registro["fecha"].ToString());
                int mes = Convert.ToInt32(registro["mes"].ToString());
                string concepto = registro["concepto"].ToString();
                string centro_costos = registro["centro_costos"].ToString();
                string proyectos = registro["proyectos"].ToString();
                decimal saldo_inicial = Convert.ToDecimal(registro["saldo_inicial"].ToString());
                decimal debe = Convert.ToDecimal(registro["debe"].ToString());
                decimal haber = Convert.ToDecimal(registro["haber"].ToString());
                decimal movimiento = Convert.ToDecimal(registro["movimiento"].ToString());
                string empresa = registro["empresa"].ToString();
                int num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
                string tipo_cuenta = registro["tipo_cuenta"].ToString();
                string edo_resultados = registro["edo_resultados"].ToString();
                string responsable = registro["responsable"].ToString();
                string tipo_proyecto = registro["tipo_proyecto"].ToString();
                string tipo_py = registro["tipo_py"].ToString();
                string clasificacion_py = registro["clasificacion_py"].ToString();
                DateTime fecha;

                if (!DateTime.TryParseExact(fecha_str, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fecha))
                {
                    resp.Success = update;
                    resp.Message = update == default ? "Ocurrio un error al agregar registro Cie." : string.Empty;
                    return resp;
                }

                var res_update_cie = await db.tB_Cie_Datas.Where(x => x.IdCieData == id_cie)
                    .UpdateAsync(x => new TB_Cie_Data
                    {
                        NombreCuenta = nombre_cuenta,
                        Cuenta = cuenta,
                        TipoPoliza = tipo_poliza,
                        Numero = numero,
                        Fecha = fecha,
                        Mes = mes,
                        Concepto = concepto,
                        CentroCostos = centro_costos,
                        Proyectos = proyectos,
                        SaldoInicial = saldo_inicial,
                        Debe = debe,
                        Haber = haber,
                        Movimiento = movimiento,
                        Empresa = empresa,
                        NumProyecto = num_proyecto,
                        TipoCuenta = tipo_cuenta,
                        EdoResultados = edo_resultados,
                        Responsable = responsable,
                        TipoProyecto = tipo_proyecto,
                        TipoPY = tipo_py,
                        ClasificacionPY = clasificacion_py
                    }) > 0;

                resp.Success = res_update_cie;
                resp.Message = res_update_cie == default ? "Ocurrio un error al agregar registro Cie." : string.Empty;

            }
            return resp;
        }

        public async Task<(bool existe, string mensaje)> DeleteRegistro(int idRegistro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                var res_update_timesheet = await db.tB_Cie_Datas.Where(x => x.IdCieData == idRegistro)
                                .UpdateAsync(x => new TB_Cie_Data
                                {
                                    Activo = false
                                }) > 0;

                resp.Success = res_update_timesheet;
                resp.Message = res_update_timesheet == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }

        #endregion Registros
    }
}
