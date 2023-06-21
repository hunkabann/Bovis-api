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

        #region Cuenta Data
        public async Task<List<CuentaContable_Detalle>> GetCuentaData(JsonObject cuentas)
        {
            List<CuentaContable_Detalle> list = new List<CuentaContable_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                foreach (var cuenta in cuentas["data"].AsArray())
                {
                    var res = await (from cta_c in db.tB_Cat_TipoCtaContables
                                     join cta in db.tB_Cat_TipoCuentas on cta_c.IdTipoCuenta equals cta.IdTipoCuenta into ctaJoin
                                     from ctaItem in ctaJoin.DefaultIfEmpty()
                                     join result in db.tB_Cat_TipoResultados on cta_c.IdTipoResultado equals result.IdTipoResultado into resultJoin
                                     from resultItem in resultJoin.DefaultIfEmpty()
                                     join pcs in db.tB_Cat_TipoPcs on cta_c.IdPcs equals pcs.IdTipoPcs into pcsJoin
                                     from pcsItem in pcsJoin.DefaultIfEmpty()
                                     join pcs2 in db.tB_Cat_TipoPcs2 on cta_c.IdPcs2 equals pcs2.IdTipoPcs2 into pcsJoin2
                                     from pcsItem2 in pcsJoin2.DefaultIfEmpty()
                                     where cta_c.CtaContable == cuenta.ToString()
                                     select new CuentaContable_Detalle
                                     {
                                         Cuenta = cuenta.ToString(),
                                         TipoCuenta = ctaItem != null ? ctaItem.TipoCuenta : string.Empty,
                                         TipoResultado = resultItem != null ? resultItem.TipoResultado : string.Empty,
                                         TipoPY = pcsItem != null ? pcsItem.TipoPcs : string.Empty,
                                         ClasificacionPY = pcsItem2 != null ? pcsItem2.TipoPcs2 : string.Empty
                                     }).FirstOrDefaultAsync();

                    if (res != null)
                        list.Add(res);
                }
            }

            return list;
        }
        #endregion Cuenta Data

        #region Proyecto
        public async Task<List<ProyectoData_Detalle>> GetProyectoData(JsonObject proyectos)
        {
            List<ProyectoData_Detalle> list = new List<ProyectoData_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                foreach (var proyecto in proyectos["data"].AsArray())
                {
                    var res = await (from proy in db.tB_Proyectos
                                     join emp in db.tB_Empleados on proy.IdDirectorEjecutivo equals emp.NumEmpleadoRrHh
                                     join per in db.tB_Personas on emp.IdPersona equals per.IdPersona into empPer
                                     from empPerItem in empPer.DefaultIfEmpty()
                                     join t_proy in db.tB_Cat_TipoProyectos on proy.IdTipoProyecto equals t_proy.IdTipoProyecto into proyTProy
                                     from proyTProyItem in proyTProy.DefaultIfEmpty()
                                     where proy.Proyecto == proyecto.ToString()
                                     select new ProyectoData_Detalle
                                     {
                                         Proyecto = proyecto.ToString(),
                                         NumProyecto = proy.NumProyecto,
                                         Responsable = empPerItem != null ? empPerItem.Nombre + " " + empPerItem.ApPaterno + " " + empPerItem.ApMaterno : string.Empty,
                                         TipoProyecto = proyTProyItem != null ? proyTProyItem.TipoProyecto : string.Empty
                                     }).FirstOrDefaultAsync();

                    if (res != null)
                        list.Add(res);
                }
            }

            return list;
        }
        #endregion Proyecto

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
                    int num_proyecto = registro["num_proyecto"] != null ? Convert.ToInt32(registro["num_proyecto"].ToString()) : 0;
                    string tipo_cuenta = registro["tipo_cuenta"] != null ? registro["tipo_cuenta"].ToString() : string.Empty;
                    string edo_resultados = registro["edo_resultados"] != null ? registro["edo_resultados"].ToString() : string.Empty;
                    string responsable = registro["responsable"] != null ? registro["responsable"].ToString() : string.Empty;
                    string tipo_proyecto = registro["tipo_proyecto"] != null ? registro["tipo_proyecto"].ToString() : string.Empty;
                    string tipo_py = registro["tipo_py"] != null ? registro["tipo_py"].ToString() : string.Empty;
                    string clasificacion_py = registro["clasificacion_py"] != null ? registro["clasificacion_py"].ToString() : string.Empty;
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
                int num_proyecto = registro["num_proyecto"] != null ? Convert.ToInt32(registro["num_proyecto"].ToString()) : 0;
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
