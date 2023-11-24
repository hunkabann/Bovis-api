using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using LinqToDB.Tools;
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

        #region Catálogos
        public async Task<List<string>> GetNombresCuenta()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var nombres_cuenta = await (from cie in db.tB_Cie_Datas
                                      where cie.Activo == true
                                      orderby cie.NombreCuenta ascending
                                      select cie.NombreCuenta)
                                      .Distinct()
                                      .ToListAsync();

                return nombres_cuenta;
            }
        }
        public async Task<List<string>> GetConceptos()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var conceptos = await (from cie in db.tB_Cie_Datas
                                      where cie.Activo == true
                                       orderby cie.Concepto ascending
                                       select cie.Concepto)
                                      .Distinct()
                                      .ToListAsync();

                return conceptos;
            }
        }
        public async Task<List<int>> GetNumsProyecto()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var nums_proyecto = await (from cie in db.tB_Cie_Datas
                                      where cie.Activo == true
                                      orderby cie.NumProyecto ascending
                                      select Convert.ToInt32(cie.NumProyecto))
                                      .Distinct()
                                      .ToListAsync();

                return nums_proyecto;
            }
        }
        public async Task<List<string>> GetResponsables()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var responsables = await (from cie in db.tB_Cie_Datas
                                      where cie.Activo == true
                                          orderby cie.Responsable ascending
                                          select cie.Responsable)
                                      .Distinct()
                                      .ToListAsync();

                return responsables;
            }
        }
        public async Task<List<string>> GetClasificacionesPY()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var clasificacionesPY = await (from cie in db.tB_Cie_Datas
                                      where cie.Activo == true
                                      orderby cie.ClasificacionPY ascending
                                      select cie.ClasificacionPY)
                                      .Distinct()
                                      .ToListAsync();

                return clasificacionesPY;
            }
        }
        #endregion Catálogos

        #region Registros CIE
        public async Task<Cie_Detalle> GetRegistro(int? idRegistro)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from cie in db.tB_Cie_Datas
                          join archivo in db.tB_Cie_Archivos on cie.IdArchivo equals archivo.IdArchivo into archivoJoin
                          from archivoItem in archivoJoin.DefaultIfEmpty()
                          where cie.IdCieData == idRegistro
                          select new Cie_Detalle
                          {
                              IdCie = cie.IdCieData,
                              NombreCuenta = cie.NombreCuenta,
                              Cuenta = cie.Cuenta,
                              TipoPoliza = cie.TipoPoliza,
                              Numero = cie.Numero.ToString(),
                              Fecha = cie.Fecha,
                              Mes = cie.Mes,
                              Concepto = cie.Concepto,
                              CentroCostos = cie.CentroCostos,
                              Proyectos = cie.Proyectos,
                              SaldoInicial = cie.SaldoInicial,
                              Debe = cie.Debe,
                              Haber = cie.Haber,
                              Movimiento = cie.Movimiento,
                              Empresa = cie.Empresa,
                              NumProyecto = cie.NumProyecto,
                              TipoCuenta = cie.TipoCuenta,
                              EdoResultados = cie.EdoResultados,
                              Responsable = cie.Responsable,
                              TipoProyecto = cie.TipoProyecto,
                              TipoPy = cie.TipoPY,
                              ClasificacionPy = cie.ClasificacionPY,
                              Activo = cie.Activo,
                              IdArchivo = cie.IdArchivo,
                              NombreArchivo = archivoItem.NombreArchivo ?? null
                          }).FirstOrDefaultAsync();

                return res;
            }
        }
        public async Task<Cie_Registros> GetRegistros(bool? activo, string nombre_cuenta, int mes, int anio, string concepto, string empresa, int num_proyecto, string responsable, string clasificacionPY, int offset, int limit)
        {
            Cie_Registros registros = new Cie_Registros();

            using (var db = new ConnectionDB(dbConfig))
            {
                registros.Registros = await (from cie in db.tB_Cie_Datas
                                             join archivo in db.tB_Cie_Archivos on cie.IdArchivo equals archivo.IdArchivo into archivoJoin
                                             from archivoItem in archivoJoin.DefaultIfEmpty()
                                             where cie.Activo == activo
                                             && (nombre_cuenta == "-" || cie.NombreCuenta == nombre_cuenta)
                                             && (mes == 0 || Convert.ToDateTime(cie.Fecha).Month == mes)
                                             && (anio == 0 || Convert.ToDateTime(cie.Fecha).Year == anio)
                                             && (concepto == "-" || cie.Concepto == concepto)
                                             && (empresa == "-" || cie.Empresa == empresa)
                                             && (num_proyecto == 0 || cie.NumProyecto == num_proyecto)
                                             && (responsable == "-" || cie.Responsable == responsable)
                                             && (clasificacionPY == "-" || cie.ClasificacionPY == clasificacionPY)
                                             orderby cie.IdCieData ascending
                                             select new Cie_Detalle
                                             {
                                                 IdCie = cie.IdCieData,
                                                 NombreCuenta = cie.NombreCuenta,
                                                 Cuenta = cie.Cuenta,
                                                 TipoPoliza = cie.TipoPoliza,
                                                 Numero = cie.Numero.ToString(),
                                                 Fecha = cie.Fecha,
                                                 Mes = cie.Mes,
                                                 Concepto = cie.Concepto,
                                                 CentroCostos = cie.CentroCostos,
                                                 Proyectos = cie.Proyectos,
                                                 SaldoInicial = cie.SaldoInicial,
                                                 Debe = cie.Debe,
                                                 Haber = cie.Haber,
                                                 Movimiento = cie.Movimiento,
                                                 Empresa = cie.Empresa,
                                                 NumProyecto = cie.NumProyecto,
                                                 TipoCuenta = cie.TipoCuenta,
                                                 EdoResultados = cie.EdoResultados,
                                                 Responsable = cie.Responsable,
                                                 TipoProyecto = cie.TipoProyecto,
                                                 TipoPy = cie.TipoPY,
                                                 ClasificacionPy = cie.ClasificacionPY,
                                                 Activo = cie.Activo,
                                                 IdArchivo = cie.IdArchivo,
                                                 NombreArchivo = archivoItem.NombreArchivo ?? null
                                             }).ToListAsync();

                ///
                /// Registros de facturación
                ///
                List<int> lstProyectosEmpresa = null;


                if (empresa != "-")
                {
                    lstProyectosEmpresa = await (from a in db.tB_Proyectos
                                                 join b in db.tB_Empresas on a.IdEmpresa equals b.IdEmpresa into bJoin
                                                 from bItem in bJoin.DefaultIfEmpty()
                                                 where bItem.Empresa == empresa
                                                 select a.NumProyecto).ToListAsync();
                }

                var res_facturas = await (from a in db.tB_ProyectoFacturas
                                          join b in db.tB_ProyectoFacturasNotaCredito on a.Id equals b.IdFactura into factNC
                                          from ab in factNC.DefaultIfEmpty()
                                          join c in db.tB_ProyectoFacturasCobranza on a.Id equals c.IdFactura into factC
                                          from ac in factC.DefaultIfEmpty()
                                          join c in db.tB_Proyectos on a.NumProyecto equals c.NumProyecto into cJoin
                                          from cItem in cJoin.DefaultIfEmpty()
                                          join d in db.tB_Clientes on cItem.IdCliente equals d.IdCliente into dJoin
                                          from dItem in dJoin.DefaultIfEmpty()
                                          join e in db.tB_Empresas on cItem.IdEmpresa equals e.IdEmpresa into eJoin
                                          from eItem in eJoin.DefaultIfEmpty()
                                          where (num_proyecto == 0 || a.NumProyecto == num_proyecto)
                                          && (lstProyectosEmpresa == null || a.NumProyecto.In(lstProyectosEmpresa))
                                          && (mes == 0 || a.FechaEmision.Month == mes)
                                          && (anio == 0 || a.FechaEmision.Year == anio)
                                          && (num_proyecto == 0 || a.NumProyecto == num_proyecto)
                                          && (empresa == "-" || eItem.Empresa == empresa)
                                          orderby a.Id descending
                                          select new Cie_Detalle
                                          {
                                              IdCie = 0,
                                              NombreCuenta = "Facturación",
                                              Cuenta = "105001001",
                                              Numero = a.NoFactura,
                                              Fecha = a.FechaEmision,
                                              Mes = a.FechaEmision.Month,
                                              Concepto = a.Concepto,
                                              Proyectos = cItem != null ? cItem.Proyecto : string.Empty,
                                              Haber = a.Total,
                                              Movimiento = a.Total * -1,
                                              Empresa = eItem != null ? eItem.Empresa : string.Empty,
                                              NumProyecto = a.NumProyecto,
                                              ClasificacionPy = "Facturación"
                                          }).ToListAsync();

                registros.Registros.AddRange(res_facturas);                


                var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                       join facts in db.tB_ProyectoFacturas on notas.IdFactura equals facts.Id into factsJoin
                                       from factsItem in factsJoin.DefaultIfEmpty()
                                       join proys in db.tB_Proyectos on factsItem.NumProyecto equals proys.NumProyecto into proysJoin
                                       from proysItem in proysJoin.DefaultIfEmpty()
                                       join empr in db.tB_Empresas on proysItem.IdEmpresa equals empr.IdEmpresa into emprJoin
                                       from emprItem in emprJoin.DefaultIfEmpty()
                                       where notas.FechaCancelacion == null
                                       && (mes == 0 || notas.FechaNotaCredito.Month == mes)
                                       && (anio == 0 || notas.FechaNotaCredito.Year == anio)
                                       && (num_proyecto == 0 || factsItem.NumProyecto == num_proyecto)
                                       && (empresa == "-" || emprItem.Empresa == empresa)
                                       select new Cie_Detalle
                                       {
                                           IdCie = 0,
                                           NombreCuenta = "Facturación",
                                           Cuenta = "105001001",
                                           Numero = notas.NotaCredito,
                                           Fecha = notas.FechaNotaCredito,
                                           Mes = notas.FechaNotaCredito.Month,
                                           Concepto = notas.Concepto,
                                           Proyectos = proysItem != null ? proysItem.Proyecto : string.Empty,
                                           Debe = notas.Total,
                                           Movimiento = notas.Total,
                                           Empresa = emprItem != null ? emprItem.Empresa : string.Empty,
                                           NumProyecto = factsItem != null ? factsItem.NumProyecto : 0,
                                           ClasificacionPy = "Facturación"
                                       }).ToListAsync();

                registros.Registros.AddRange(res_notas);


                var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                           join facts in db.tB_ProyectoFacturas on cobr.IdFactura equals facts.Id into factsJoin
                                           from factsItem in factsJoin.DefaultIfEmpty()
                                           join proys in db.tB_Proyectos on factsItem.NumProyecto equals proys.NumProyecto into proysJoin
                                           from proysItem in proysJoin.DefaultIfEmpty()
                                           join empr in db.tB_Empresas on proysItem.IdEmpresa equals empr.IdEmpresa into emprJoin
                                           from emprItem in emprJoin.DefaultIfEmpty()
                                           where cobr.FechaCancelacion == null
                                           && (mes == 0 || cobr.FechaPago.Month == mes)
                                           && (anio == 0 || cobr.FechaPago.Year == anio)
                                           && (num_proyecto == 0 || factsItem.NumProyecto == num_proyecto)
                                           && (empresa == "-" || emprItem.Empresa == empresa)
                                           select new Cie_Detalle
                                           {
                                               IdCie = 0,
                                               NombreCuenta = "Cobranza",
                                               Fecha = cobr.FechaPago,
                                               Mes = cobr.FechaPago.Month,
                                               Proyectos = proysItem != null ? proysItem.Proyecto : string.Empty,
                                               Haber = cobr.ImportePagado,
                                               Movimiento = cobr.ImportePagado * -1,
                                               Empresa = emprItem != null ? emprItem.Empresa : string.Empty,
                                               NumProyecto = factsItem != null ? factsItem.NumProyecto : 0,
                                               ClasificacionPy = "Cobranza"
                                           }).ToListAsync();

                registros.Registros.AddRange(res_cobranzas);

                registros.TotalRegistros = registros.Registros.Count();

                registros.Registros = registros.Registros.OrderByDescending(x => x.IdCie).Skip((offset - 1) * limit).Take(limit).ToList();

                return registros;
            }
        }

        public async Task<(bool Success, string Message)> AddRegistros(JsonObject registros)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (var db = new ConnectionDB(dbConfig))
            {
                string nombre_archivo = registros["nombre_archivo"].ToString();

                int last_inserted_id = 0;

                var insert_archivo = await db.tB_Cie_Archivos
                    .Value(x => x.NombreArchivo, nombre_archivo)                    
                    .InsertAsync() > 0;

                resp.Success = insert_archivo;
                resp.Message = insert_archivo == default ? "Ocurrio un error al agregar registro." : string.Empty;

                if (insert_archivo != null)
                {
                    var lastInsertedRecord = db.tB_Cie_Archivos.OrderByDescending(x => x.IdArchivo).FirstOrDefault();
                    last_inserted_id = lastInsertedRecord.IdArchivo;

                    bool insert = false;
                    foreach (var registro in registros["data"].AsArray())
                    {
                        string? nombre_cuenta = registro["nombre_cuenta"] != null ? registro["nombre_cuenta"].ToString() : null;
                        string? cuenta = registro["cuenta"] != null ? registro["cuenta"].ToString() : null;
                        string? tipo_poliza = registro["tipo_poliza"].ToString();
                        int? numero = registro["numero"] != null ? Convert.ToInt32(registro["numero"].ToString()) : null;
                        string fecha_str = registro["fecha"].ToString();
                        //DateTime fecha = Convert.ToDateTime(registro["fecha"].ToString());
                        int? mes = registro["mes"] != null ? Convert.ToInt32(registro["mes"].ToString()) : null;
                        string? concepto = registro["concepto"] != null ? registro["concepto"].ToString() : null;
                        string? centro_costos = registro["centro_costos"] != null ? registro["centro_costos"].ToString() : null;
                        string? proyectos = registro["proyectos"] != null ? registro["proyectos"].ToString() : null;
                        decimal? saldo_inicial = registro["saldo_inicial"] != null ? Convert.ToDecimal(registro["saldo_inicial"].ToString()) : null;
                        decimal? debe = registro["debe"] != null ? Convert.ToDecimal(registro["debe"].ToString()) : null;
                        decimal? haber = registro["haber"] != null ? Convert.ToDecimal(registro["haber"].ToString()) : null;
                        decimal? movimiento = registro["movimiento"] != null ? Convert.ToDecimal(registro["movimiento"].ToString()) : null;
                        string? empresa = registro["empresa"] != null ? registro["empresa"].ToString() : null;
                        int? num_proyecto = registro["num_proyecto"] != null ? Convert.ToInt32(registro["num_proyecto"].ToString()) : null;
                        string? tipo_cuenta = registro["tipo_cuenta"] != null ? registro["tipo_cuenta"].ToString() : null;
                        string? edo_resultados = registro["edo_resultados"] != null ? registro["edo_resultados"].ToString() : null;
                        string? responsable = registro["responsable"] != null ? registro["responsable"].ToString() : null;
                        string? tipo_proyecto = registro["tipo_proyecto"] != null ? registro["tipo_proyecto"].ToString() : null;
                        string? tipo_py = registro["tipo_py"] != null ? registro["tipo_py"].ToString() : null;
                        string? clasificacion_py = registro["clasificacion_py"] != null ? registro["clasificacion_py"].ToString() : null;
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
                            .Value(x => x.IdArchivo, last_inserted_id)
                            .InsertAsync() > 0;

                        resp.Success = insert;
                        resp.Message = insert == default ? "Ocurrio un error al agregar registro Cie." : string.Empty;
                    }
                }
            }
            return resp;
        }

        public async Task<(bool Success, string Message)> UpdateRegistro(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (var db = new ConnectionDB(dbConfig))
            {
                bool update = false;
                int id_cie = Convert.ToInt32(registro["id_cie"].ToString());
                string? nombre_cuenta = registro["nombre_cuenta"] != null ? registro["nombre_cuenta"].ToString() : null;
                string? cuenta = registro["cuenta"] != null ? registro["cuenta"].ToString() : null;
                string? tipo_poliza = registro["tipo_poliza"].ToString();
                int? numero = registro["numero"] != null ? Convert.ToInt32(registro["numero"].ToString()) : null;
                string fecha_str = registro["fecha"].ToString();
                //DateTime fecha = Convert.ToDateTime(registro["fecha"].ToString());
                int? mes = registro["mes"] != null ? Convert.ToInt32(registro["mes"].ToString()) : null;
                string? concepto = registro["concepto"] != null ? registro["concepto"].ToString() : null;
                string? centro_costos = registro["centro_costos"] != null ? registro["centro_costos"].ToString() : null;
                string? proyectos = registro["proyectos"] != null ? registro["proyectos"].ToString() : null;
                decimal? saldo_inicial = registro["saldo_inicial"] != null ? Convert.ToDecimal(registro["saldo_inicial"].ToString()) : null;
                decimal? debe = registro["debe"] != null ? Convert.ToDecimal(registro["debe"].ToString()) : null;
                decimal? haber = registro["haber"] != null ? Convert.ToDecimal(registro["haber"].ToString()) : null;
                decimal? movimiento = registro["movimiento"] != null ? Convert.ToDecimal(registro["movimiento"].ToString()) : null;
                string? empresa = registro["empresa"] != null ? registro["empresa"].ToString() : null;
                int? num_proyecto = registro["num_proyecto"] != null ? Convert.ToInt32(registro["num_proyecto"].ToString()) : null;
                string? tipo_cuenta = registro["tipo_cuenta"] != null ? registro["tipo_cuenta"].ToString() : null;
                string? edo_resultados = registro["edo_resultados"] != null ? registro["edo_resultados"].ToString() : null;
                string? responsable = registro["responsable"] != null ? registro["responsable"].ToString() : null;
                string? tipo_proyecto = registro["tipo_proyecto"] != null ? registro["tipo_proyecto"].ToString() : null;
                string? tipo_py = registro["tipo_py"] != null ? registro["tipo_py"].ToString() : null;
                string? clasificacion_py = registro["clasificacion_py"] != null ? registro["clasificacion_py"].ToString() : null;
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

        public async Task<(bool Success, string Message)> DeleteRegistro(int idRegistro)
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

        #endregion Registros CIE
    }
}
