﻿using Azure.Core;
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
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Nodes;

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
                                     join pcs2 in db.tB_Cat_TipoPcs2 on cta_c.IdPcs2 equals pcs2.IdTipoPcs2 into pcs2Join
                                     from pcs2Item in pcs2Join.DefaultIfEmpty()
                                     join pcs3 in db.tB_Cat_TipoPcs3 on cta_c.IdPcs3 equals pcs3.IdTipoPcs3 into pcs3Join
                                     from pcs3Item in pcs3Join.DefaultIfEmpty()
                                     where cta_c.CtaContable == cuenta.ToString()
                                     select new CuentaContable_Detalle
                                     {
                                         Cuenta = cuenta.ToString(),
                                         TipoCuenta = ctaItem != null ? ctaItem.TipoCuenta : string.Empty,
                                         TipoResultado = resultItem != null ? resultItem.TipoResultado : string.Empty,
                                         TipoPY = pcs2Item != null ? pcs2Item.TipoPcs2 : string.Empty,
                                         ClasificacionPY = pcs3Item != null ? pcs3Item.TipoPcs3 : string.Empty
                                     }).FirstOrDefaultAsync();

                    if (res != null)
                        list.Add(res);
                }
            }

            return list;
        }

        public async Task<List<CtaContableRespuesta_Detalle>> AddCuentas(JsonObject registros)
        {
            List<CtaContableRespuesta_Detalle> cuentas = new List<CtaContableRespuesta_Detalle>();

            using (var db = new ConnectionDB(dbConfig))
            {
                foreach (var registro in registros["data"].AsArray())
                {
                    string cuenta_contable = registro["cuenta"].ToString();
                    string nombre_cuenta = registro["nombre_cuenta"].ToString();
                    string concepto = registro["concepto"].ToString();

                    string cuenta_anterior = string.Empty;
                    TB_Cat_TipoCtaContable? anterior = null;

                    if (Convert.ToInt32(cuenta_contable) > 0)
                        cuenta_anterior = (Convert.ToInt32(cuenta_contable) - 1).ToString().PadLeft(9, '0');

                    if (cuenta_contable.Substring(0, 3) == cuenta_anterior.Substring(0, 3) && cuenta_contable.Substring(3, 3) == cuenta_anterior.Substring(3, 3))
                        anterior = await (from cta in db.tB_Cat_TipoCtaContables
                                          where cta.CtaContable == cuenta_anterior
                                          select cta).FirstOrDefaultAsync();

                    if (anterior == null)
                    {
                        anterior = await (from cta in db.tB_Cat_TipoCtaContables
                                          where cta.CtaContable == cuenta_contable.Substring(0, 3) + cuenta_contable.Substring(3, 3) + "000"
                                          select cta).FirstOrDefaultAsync();
                    }

                    //if (cuenta_contable != "703002003")
                    {
                        var insertedId = await db.tB_Cat_TipoCtaContables
                            .Value(x => x.CtaContable, cuenta_contable)
                            .Value(x => x.Concepto, concepto)
                            .Value(x => x.TipoCtaContableMayor, cuenta_contable.Substring(0, 3))
                            .Value(x => x.TipoCtaContablePrimerNivel, cuenta_contable.Substring(3, 3))
                            .Value(x => x.TipoCtaContableSegundoNivel, cuenta_contable.Substring(6))
                            .Value(x => x.IdTipoCuenta, anterior != null ? anterior.IdTipoCuenta : 1)
                            .Value(x => x.IdTipoResultado, anterior != null ? anterior.IdTipoResultado : 1)
                            .Value(x => x.IdPcs, anterior != null ? anterior.IdPcs : 1)
                            .Value(x => x.IdPcs2, anterior != null ? anterior.IdPcs2 : 1)
                            .Value(x => x.IdPcs3, anterior != null ? anterior.IdPcs3 : 1)
                            .Value(x => x.Activo, true)
                            .InsertWithIdentityAsync();

                        var cuenta = await (from cta in db.tB_Cat_TipoCtaContables
                                            join tipocta in db.tB_Cat_TipoCuentas on cta.IdTipoCuenta equals tipocta.IdTipoCuenta into tipoctaJoin
                                            from tipoctaItem in tipoctaJoin.DefaultIfEmpty()
                                            join tipores in db.tB_Cat_TipoResultados on cta.IdTipoResultado equals tipores.IdTipoResultado into tiporesJoin
                                            from tiporesItem in tiporesJoin.DefaultIfEmpty()
                                            join pcs in db.tB_Cat_TipoPcs on cta.IdPcs equals pcs.IdTipoPcs into pcsJoin
                                            from pcsItem in pcsJoin.DefaultIfEmpty()
                                            join pcs2 in db.tB_Cat_TipoPcs2 on cta.IdPcs2 equals pcs2.IdTipoPcs2 into pcs2Join
                                            from pcs2Item in pcs2Join.DefaultIfEmpty()
                                            join pcs3 in db.tB_Cat_TipoPcs3 on cta.IdPcs3 equals pcs3.IdTipoPcs3 into pcs3Join
                                            from pcs3Item in pcs3Join.DefaultIfEmpty()
                                            where cta.IdTipoCtaContable == Convert.ToInt32(insertedId)
                                            select new CtaContableRespuesta_Detalle
                                            {
                                                CtaContable = cta.CtaContable,
                                                NombreCtaContable = nombre_cuenta,
                                                Concepto = cta.Concepto,
                                                TipoCtaContableMayor = cta.TipoCtaContableMayor,
                                                TipoCtaContablePrimerNivel = cta.TipoCtaContablePrimerNivel,
                                                TipoCtaContableSegundoNivel = cta.TipoCtaContableSegundoNivel,
                                                IdTipoCuenta = cta.IdTipoCuenta,
                                                TipoCuenta = tipoctaItem != null ? tipoctaItem.TipoCuenta : string.Empty,
                                                IdTipoResultado = cta.IdTipoResultado,
                                                TipoResultado = tiporesItem != null ? tiporesItem.TipoResultado : string.Empty,
                                                Pcs = pcsItem != null ? pcsItem.TipoPcs : string.Empty,
                                                Pcs2 = pcs2Item != null ? pcs2Item.TipoPcs2 : string.Empty,
                                                Pcs3 = pcs3Item != null ? pcs3Item.TipoPcs3 : string.Empty
                                            }).FirstOrDefaultAsync();

                        cuentas.Add(cuenta);
                    }
                }
            }

            return cuentas;
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

                nombres_cuenta.Add("Facturación / Cobranza");

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
                                      && !string.IsNullOrEmpty(cie.ClasificacionPY)
                                      orderby cie.ClasificacionPY ascending
                                      select cie.ClasificacionPY)
                                      .Distinct()
                                      .ToListAsync();

                return clasificacionesPY;
            }
        }
        public async Task<List<string>> GetTiposPY()
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var clasificacionesPY = await (from cie in db.tB_Cie_Datas
                                      where cie.Activo == true
                                      && !string.IsNullOrEmpty(cie.TipoPY)
                                      orderby cie.TipoPY ascending
                                      select cie.TipoPY)
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
                              Proyecto = cie.Proyecto,
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
        public async Task<Cie_Registros> GetRegistros(JsonObject registro)
        {
            Cie_Registros registros = new Cie_Registros();

            string? nombre_cuenta = registro["nombre_cuenta"] != null ? registro["nombre_cuenta"].ToString() : null;
            int? mes_inicio = registro["mes_inicio"] != null ? Convert.ToInt32(registro["mes_inicio"].ToString()) : null;
            int? anio_inicio = registro["anio_inicio"] != null ? Convert.ToInt32(registro["anio_inicio"].ToString()) : null;
            int? mes_fin = registro["mes_fin"] != null ? Convert.ToInt32(registro["mes_fin"].ToString()) : null;
            int? anio_fin = registro["anio_fin"] != null ? Convert.ToInt32(registro["anio_fin"].ToString()) : null;
            string? concepto = registro["concepto"] != null ? registro["concepto"].ToString() : null;
            string? empresa = registro["empresa"] != null ? registro["empresa"].ToString() : null;
            int? num_proyecto = registro["num_proyecto"] != null ? Convert.ToInt32(registro["num_proyecto"].ToString()) : null;
            string? responsable = registro["responsable"] != null ? registro["responsable"].ToString() : null;
            string? clasificacion_py = registro["clasificacion_py"] != null ? registro["clasificacion_py"].ToString() : null;
            int limit = Convert.ToInt32(registro["limit"].ToString());
            int offset = Convert.ToInt32(registro["offset"].ToString());
            string? sort_field = registro["sort_field"] != null ? registro["sort_field"].ToString() : null;
            string? sort_order = registro["sort_order"] != null ? registro["sort_order"].ToString() : null;

            using (var db = new ConnectionDB(dbConfig))
            {
                if (nombre_cuenta != "Facturación / Cobranza")
                {
                    registros.Registros = await (from cie in db.tB_Cie_Datas
                                                 join archivo in db.tB_Cie_Archivos on cie.IdArchivo equals archivo.IdArchivo into archivoJoin
                                                 from archivoItem in archivoJoin.DefaultIfEmpty()
                                                 join proyecto in db.tB_Proyectos on new { cie.NumProyecto, cie.Proyecto } equals new { proyecto.NumProyecto, proyecto.Proyecto } into proyectoJoin
                                                 from proyectoItem in proyectoJoin.DefaultIfEmpty()
                                                 where cie.Activo == true
                                                 && (nombre_cuenta == null || cie.NombreCuenta == nombre_cuenta)
                                                 && (mes_inicio == null || Convert.ToDateTime(cie.Fecha).Month >= mes_inicio)
                                                 && (anio_inicio == null || Convert.ToDateTime(cie.Fecha).Year >= anio_inicio)
                                                 && (mes_fin == null || Convert.ToDateTime(cie.Fecha).Month <= mes_fin)
                                                 && (anio_fin == null || Convert.ToDateTime(cie.Fecha).Year <= anio_fin)
                                                 && (concepto == null || cie.Concepto == concepto)
                                                 && (empresa == null || cie.Empresa == empresa)
                                                 && (num_proyecto == null || cie.NumProyecto == num_proyecto)
                                                 && (responsable == null || cie.Responsable == responsable)
                                                 && (clasificacion_py == null || cie.ClasificacionPY == clasificacion_py)
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
                                                     Proyecto = cie.Proyecto,
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
                                                     NombreArchivo = archivoItem.NombreArchivo ?? null,
                                                     Inconsistente = proyectoItem == null                                                     
                                                 }).ToListAsync();


                    // Omisión de filtro de inconsistencia para cuentas específicas.
                    foreach (var reg in registros.Registros)
                    {
                        if (!string.IsNullOrEmpty(reg.CentroCostos) && reg.CentroCostos.Contains("."))
                        {
                            reg.Inconsistente = reg.NumProyecto != Convert.ToInt32(reg.CentroCostos.Split('.')[0]);

                            if ((reg.NumProyecto == 110 && Convert.ToInt32(reg.CentroCostos.Split('.')[0]) == 236)
                                || (reg.NumProyecto == 112 && Convert.ToInt32(reg.CentroCostos.Split('.')[0]) == 261))
                            {
                                reg.Inconsistente = false;
                            }
                        }
                        else
                            reg.Inconsistente = true;
                    }
                }
                else
                {
                    registros.Registros = new List<Cie_Detalle>();
                }


                ///
                /// Registros de facturación
                ///
                List<int> lstProyectosEmpresa = null;


                if (empresa != null)
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
                                          join e in db.tB_Empresas on cItem.IdEmpresa equals e.IdEmpresa into eJoin
                                          from eItem in eJoin.DefaultIfEmpty()
                                          where (num_proyecto == null || a.NumProyecto == num_proyecto)
                                          //&& (lstProyectosEmpresa == null || a.NumProyecto.In(lstProyectosEmpresa))
                                          && ((mes_inicio == null && anio_inicio == null && mes_fin == null && anio_fin == null) || a.FechaCancelacion == null ? ((a.FechaEmision.Month >= mes_inicio && a.FechaEmision.Year >= anio_inicio) && (a.FechaEmision.Month <= mes_fin && a.FechaEmision.Year <= anio_fin)) : ((a.FechaCancelacion.Value.Month >= mes_inicio && a.FechaCancelacion.Value.Year >= anio_inicio) && (a.FechaCancelacion.Value.Month <= mes_fin && a.FechaCancelacion.Value.Year <= anio_fin)))
                                          && (num_proyecto == null || a.NumProyecto == num_proyecto)
                                          && (empresa == null || eItem.Empresa == empresa)
                                          orderby a.FechaEmision ascending
                                          select new Cie_Detalle
                                          {
                                              IdCie = 0,
                                              NombreCuenta = "Facturación",
                                              Cuenta = "105001001",
                                              Numero = a.NoFactura,
                                              //Fecha = (a.FechaCancelacion != null) ? a.FechaCancelacion : a.FechaEmision,
                                              //Mes = (a.FechaCancelacion != null) ? a.FechaCancelacion.Value.Month : a.FechaEmision.Month,
                                              Fecha = a.FechaEmision,
                                              Mes  = a.FechaEmision.Month,
                                              Concepto = a.Concepto,
                                              Proyecto = cItem != null ? cItem.Proyecto : string.Empty,
                                              Debe = (a.FechaCancelacion != null) ? (a.IdMoneda == "MXN" ? a.Importe : a.Importe * a.TipoCambio) : null,
                                              Haber = (a.FechaCancelacion != null) ? null : Math.Abs(a.IdMoneda == "MXN" ? a.Importe : a.Importe * (decimal)a.TipoCambio!),
                                              Movimiento = Math.Abs(a.IdMoneda == "MXN" ? a.Importe : a.Importe * (decimal)a.TipoCambio!),
                                              Empresa = eItem != null ? eItem.Empresa : string.Empty,
                                              NumProyecto = a.NumProyecto,
                                              ClasificacionPy = "Facturación",
                                              FechaCancelacion = a.FechaCancelacion,
                                              NoFactura = a.NoFactura
                                          }).ToListAsync();

                registros.Registros.AddRange(res_facturas);


                var res_notas = await (from notas in db.tB_ProyectoFacturasNotaCredito
                                       join facts in db.tB_ProyectoFacturas on notas.IdFactura equals facts.Id into factsJoin
                                       from factsItem in factsJoin.DefaultIfEmpty()
                                       join proys in db.tB_Proyectos on factsItem.NumProyecto equals proys.NumProyecto into proysJoin
                                       from proysItem in proysJoin.DefaultIfEmpty()
                                       join empr in db.tB_Empresas on proysItem.IdEmpresa equals empr.IdEmpresa into emprJoin
                                       from emprItem in emprJoin.DefaultIfEmpty()
                                       where
                                       ((mes_inicio == null && anio_inicio == null && mes_fin == null && anio_fin == null) || notas.FechaCancelacion == null ? ((notas.FechaNotaCredito.Month >= mes_inicio && notas.FechaNotaCredito.Year >= anio_inicio) && (notas.FechaNotaCredito.Month <= mes_fin && notas.FechaNotaCredito.Year <= anio_fin)) : ((notas.FechaCancelacion.Value.Month >= mes_inicio && notas.FechaCancelacion.Value.Year >= anio_inicio) && (notas.FechaCancelacion.Value.Month <= mes_fin && notas.FechaCancelacion.Value.Year <= anio_fin)))
                                       && (num_proyecto == null || factsItem.NumProyecto == num_proyecto)
                                       && (empresa == null || emprItem.Empresa == empresa)
                                       orderby notas.FechaNotaCredito ascending
                                       select new Cie_Detalle
                                       {
                                           IdCie = 0,
                                           NombreCuenta = "Facturación",
                                           Cuenta = "105001001",
                                           Numero = notas.NotaCredito,
                                           //Fecha = notas.FechaCancelacion != null ? notas.FechaCancelacion : notas.FechaNotaCredito,
                                           //Mes = notas.FechaCancelacion != null ? notas.FechaCancelacion.Value.Month : notas.FechaNotaCredito.Month,
                                           Fecha = notas.FechaNotaCredito,
                                           Mes = notas.FechaNotaCredito.Month,
                                           Concepto = notas.Concepto,
                                           Proyecto = proysItem != null ? proysItem.Proyecto : string.Empty,
                                           Debe = notas.FechaCancelacion == null ? (notas.IdMoneda == "MXN" ? notas.Importe : notas.Importe * notas.TipoCambio) : null,
                                           Haber = notas.FechaCancelacion == null ? null : Math.Abs(notas.IdMoneda == "MXN" ? notas.Importe : notas.Importe * (decimal)notas.TipoCambio!),
                                           Movimiento = Math.Abs(notas.IdMoneda == "MXN" ? notas.Importe : notas.Importe * (decimal)notas.TipoCambio!),
                                           Empresa = emprItem != null ? emprItem.Empresa : string.Empty,
                                           NumProyecto = factsItem != null ? factsItem.NumProyecto : 0,
                                           ClasificacionPy = "Facturación",
                                           FechaCancelacion = notas.FechaCancelacion,
                                           NoFactura = notas.NotaCredito
                                       }).ToListAsync();

                registros.Registros.AddRange(res_notas);


                var res_cobranzas = await (from cobr in db.tB_ProyectoFacturasCobranza
                                           join facts in db.tB_ProyectoFacturas on cobr.IdFactura equals facts.Id into factsJoin
                                           from factsItem in factsJoin.DefaultIfEmpty()
                                           join proys in db.tB_Proyectos on factsItem.NumProyecto equals proys.NumProyecto into proysJoin
                                           from proysItem in proysJoin.DefaultIfEmpty()
                                           join empr in db.tB_Empresas on proysItem.IdEmpresa equals empr.IdEmpresa into emprJoin
                                           from emprItem in emprJoin.DefaultIfEmpty()
                                           where
                                           ((mes_inicio == null && anio_inicio == null && mes_fin == null && anio_fin == null) || cobr.FechaCancelacion == null ? ((cobr.FechaPago.Month >= mes_inicio && cobr.FechaPago.Year >= anio_inicio) && (cobr.FechaPago.Month <= mes_fin && cobr.FechaPago.Year <= anio_fin)) : ((cobr.FechaCancelacion.Value.Month >= mes_inicio && cobr.FechaCancelacion.Value.Year >= anio_inicio) && (cobr.FechaCancelacion.Value.Month <= mes_fin && cobr.FechaCancelacion.Value.Year <= anio_fin)))
                                           && (num_proyecto == null || factsItem.NumProyecto == num_proyecto)
                                           && (empresa == null || emprItem.Empresa == empresa)
                                           orderby cobr.FechaPago ascending
                                           select new Cie_Detalle
                                           {
                                               IdCie = 0,
                                               NombreCuenta = "Cobranza",
                                               Numero = cobr.CRP,
                                               //Fecha = cobr.FechaCancelacion != null ? cobr.FechaCancelacion : cobr.FechaPago,
                                               //Mes = cobr.FechaCancelacion != null ? cobr.FechaCancelacion.Value.Month : cobr.FechaPago.Month,
                                               Fecha = cobr.FechaPago,
                                               Mes = cobr.FechaPago.Month,
                                               Proyecto = proysItem != null ? proysItem.Proyecto : string.Empty,
                                               Debe = cobr.FechaCancelacion != null ? (cobr.IdMonedaP == "MXN" ? cobr.ImportePagado : cobr.ImportePagado * cobr.TipoCambioP) : null,
                                               Haber = cobr.FechaCancelacion != null ? null : Math.Abs(cobr.IdMonedaP == "MXN" ? cobr.ImportePagado : cobr.ImportePagado * (decimal)cobr.TipoCambioP!),
                                               Movimiento = Math.Abs(cobr.IdMonedaP == "MXN" ? cobr.ImportePagado : cobr.ImportePagado * (decimal)cobr.TipoCambioP!),
                                               Empresa = emprItem != null ? emprItem.Empresa : string.Empty,
                                               NumProyecto = factsItem != null ? factsItem.NumProyecto : 0,
                                               ClasificacionPy = "Cobranza",
                                               FechaCancelacion = cobr.FechaCancelacion,
                                               NoFactura = cobr.CRP
                                           }).ToListAsync();

                registros.Registros.AddRange(res_cobranzas);

                registros.TotalRegistros = registros.Registros.Count();

                if (sort_field == null)
                {
                    if (limit == -1)
                        registros.Registros = registros.Registros.OrderByDescending(x => x.IdCie).ToList();
                    else
                        registros.Registros = registros.Registros.OrderByDescending(x => x.IdCie).Skip((offset - 1) * limit).Take(limit).ToList();
                }
                else
                {
                    string orderExpression = sort_order == "ASC" ? $"{sort_field} asc" : $"{sort_field} desc";

                    if (limit == -1)
                        registros.Registros = OrderByField(registros.Registros.AsQueryable(), sort_field, sort_order)
                                                    .ToList();

                    else
                        registros.Registros = OrderByField(registros.Registros.AsQueryable(), sort_field, sort_order)
                                                .Skip((offset - 1) * limit)
                                                .Take(limit)
                                                .ToList();
                }

                return registros;
            }
        }

        public static IQueryable<T> OrderByField<T>(IQueryable<T> source, string fieldName, string sortOrder)
        {
            string orderExpression = sortOrder == "ASC" ? $"{fieldName} asc" : $"{fieldName} desc";
            return source.OrderBy(orderExpression);
        }

        public async Task<(bool Success, string Message)> AddRegistros(JsonObject registros)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int? inserted_file_id = 0;

            using (var db = new ConnectionDB(dbConfig))
            {
                string nombre_archivo = registros["nombre_archivo"].ToString();
                bool paquete_inicial = Convert.ToBoolean(registros["paquete_inicial"].ToString());

                /*
                 * Se inserta el registro del nombre del archivo.
                 */
                // Busca si ya existe el archivo.
                inserted_file_id = await (from file in db.tB_Cie_Archivos
                                          where file.NombreArchivo == nombre_archivo
                                          select file.IdArchivo).FirstOrDefaultAsync();

                if (inserted_file_id != 0)
                {
                    resp.Success = true;
                    resp.Message = $"El archivo \"{nombre_archivo}\" ya fue cargado anteriormente.";

                    if (paquete_inicial == true)
                        return resp;
                }
                else
                {
                    inserted_file_id = await db.tB_Cie_Archivos
                            .Value(x => x.NombreArchivo, nombre_archivo)
                            .InsertWithInt32IdentityAsync();

                    resp.Success = inserted_file_id.HasValue;
                    resp.Message = inserted_file_id == default ? "Ocurrio un error al agregar registro." : string.Empty;
                }



                /*
                 * Se consultan todos los registros guardados en la tabla tb_cie_data y se almacenan en un HashSet.
                 */
                //List<string> registrosCie = await (from records in db.tB_Cie_Datas
                //                                   select records.Numero + "¨" + records.Fecha + "¨" + records.Concepto.Trim()).ToListAsync();

                //HashSet<string> hashs = new HashSet<string>(registrosCie);



                bool insert = false;
                foreach (var registro in registros["data"].AsArray())
                {
                    string? nombre_cuenta = registro["nombre_cuenta"] != null ? registro["nombre_cuenta"].ToString() : null;
                    string? cuenta = registro["cuenta"] != null ? registro["cuenta"].ToString() : null;
                    string? tipo_poliza = registro["tipo_poliza"].ToString();
                    int? numero = registro["numero"] != null ? Convert.ToInt32(registro["numero"].ToString()) : null;
                    string fecha_str = registro["fecha"].ToString();
                    int? mes = registro["mes"] != null ? Convert.ToInt32(registro["mes"].ToString()) : null;
                    string? concepto = registro["concepto"] != null ? registro["concepto"].ToString() : null;
                    string? centro_costos = registro["centro_costos"] != null ? registro["centro_costos"].ToString() : null;
                    string proyectos = registro["proyectos"].ToString().Trim();
                    decimal? saldo_inicial = registro["saldo_inicial"] != null ? Convert.ToDecimal(registro["saldo_inicial"].ToString()) : null;
                    decimal? debe = registro["debe"] != null ? Convert.ToDecimal(registro["debe"].ToString()) : null;
                    decimal? haber = registro["haber"] != null ? Convert.ToDecimal(registro["haber"].ToString()) : null;
                    decimal? movimiento = registro["movimiento"] != null ? Convert.ToDecimal(registro["movimiento"].ToString()) : null;
                    string? empresa = registro["empresa"] != null ? registro["empresa"].ToString() : null;
                    int num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
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

                    /*
                     * Se crea un objeto de tipo TB_CieData para luego compararlo con los Hashs y verificar si no hay duplicidad de datos.
                     */
                    //string cta = numero.ToString() + "¨" + fecha.ToString() + "¨" + concepto.Trim();

                    //if (!hashs.Contains(cta)) // && cuenta != "703002003")
                    {
                        //hashs.Add(cta);

                        insert = await db.tB_Cie_Datas
                            .Value(x => x.NombreCuenta, nombre_cuenta)
                            .Value(x => x.Cuenta, cuenta)
                            .Value(x => x.TipoPoliza, tipo_poliza)
                            .Value(x => x.Numero, numero)
                            .Value(x => x.Fecha, fecha)
                            .Value(x => x.Mes, mes)
                            .Value(x => x.Concepto, concepto)
                            .Value(x => x.CentroCostos, centro_costos)
                            .Value(x => x.Proyecto, proyectos)
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
                            .Value(x => x.IdArchivo, inserted_file_id)
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
                int? mes = registro["mes"] != null ? Convert.ToInt32(registro["mes"].ToString()) : null;
                string? concepto = registro["concepto"] != null ? registro["concepto"].ToString() : null;
                string? centro_costos = registro["centro_costos"] != null ? registro["centro_costos"].ToString() : null;
                string proyectos = registro["proyectos"].ToString().Trim();
                decimal? saldo_inicial = registro["saldo_inicial"] != null ? Convert.ToDecimal(registro["saldo_inicial"].ToString()) : null;
                decimal? debe = registro["debe"] != null ? Convert.ToDecimal(registro["debe"].ToString()) : null;
                decimal? haber = registro["haber"] != null ? Convert.ToDecimal(registro["haber"].ToString()) : null;
                decimal? movimiento = registro["movimiento"] != null ? Convert.ToDecimal(registro["movimiento"].ToString()) : null;
                string? empresa = registro["empresa"] != null ? registro["empresa"].ToString() : null;
                int num_proyecto = Convert.ToInt32(registro["num_proyecto"].ToString());
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
                    .UpdateAsync(x => new TB_CieData
                    {
                        NombreCuenta = nombre_cuenta,
                        Cuenta = cuenta,
                        TipoPoliza = tipo_poliza,
                        Numero = numero,
                        Fecha = fecha,
                        Mes = mes,
                        Concepto = concepto,
                        CentroCostos = centro_costos,
                        Proyecto = proyectos,
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
                                .UpdateAsync(x => new TB_CieData
                                {
                                    Activo = false
                                }) > 0;

                resp.Success = res_update_timesheet;
                resp.Message = res_update_timesheet == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        
        public async Task<(bool Success, string Message)> DeleteArchivo(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            using (ConnectionDB db = new ConnectionDB(dbConfig))
            {
                string nombre_archivo = registro["nombre_archivo"].ToString();

                var files_id = await (from file in db.tB_Cie_Archivos
                                          where file.NombreArchivo == nombre_archivo
                                          select file.IdArchivo).ToListAsync();

                foreach (var id in files_id)
                {

                    var delete_records = await db.tB_Cie_Datas.Where(x => x.IdArchivo == id)
                                    .DeleteAsync() > 0;

                    resp.Success = delete_records;
                    resp.Message = delete_records == default ? "Ocurrio un error al actualizar registro." : string.Empty;

                    var delete_file = await db.tB_Cie_Archivos.Where(x => x.IdArchivo == id)
                                    .DeleteAsync() > 0;

                    resp.Success = delete_file;
                    resp.Message = delete_file == default ? "Ocurrio un error al actualizar registro." : string.Empty;
                }
            }

            return resp;
        }

        #endregion Registros CIE
    }
}
