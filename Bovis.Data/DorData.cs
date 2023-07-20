using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System.Collections.Generic;
using static LinqToDB.SqlQuery.SqlPredicate;

namespace Bovis.Data
{
    public class DorData : RepositoryLinq2DB<ConnectionDB>, IDorData
    {
        private readonly string dbConfig = "DBConfig";

        public DorData()
        {
            this.ConfigurationDB = dbConfig;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        public async Task<DOR_Empleados?> GetDorEjecutivoCorreo(string email)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                return await (from cat in db.dOR_Empleados
                              where cat.CorreoElec == email
                              select cat).FirstOrDefaultAsync();
            }
        }
        public async Task<Dor_Subordinados> GetDorEmpleadoCorreo(string email)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from a in db.dOR_Empleados
                          join b in db.tB_Cat_Dor_PuestoNivel on a.Puesto equals b.Puesto
                          where a.CorreoElec == email
                          select new Dor_Subordinados
                          {
                              Nombre = a.Nombre,
                              Puesto = a.Puesto,
                              NoEmpleado = a.NoEmpleado,
                              Proyecto = a.Proyecto,
                              DireccionEjecutiva = a.DireccionEjecutiva,
                              UnidadDeNegocio = a.UnidadDeNegocio,
                              Nivel = b.Nivel,
                              CentrosdeCostos = a.CentrosdeCostos,
                              JefeDirecto = a.JefeDirecto
                          }).FirstAsync();

                return res;
            }
        }

        public async Task<List<Dor_Subordinados>> GetDorListaSubordinados(string name)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from a in db.dOR_Empleados
                                 join b in db.tB_Cat_Dor_PuestoNivel on a.Puesto equals b.Puesto
                                 where a.JefeDirecto == name
                                 select new Dor_Subordinados
                                 {
                                     Nombre = a.Nombre,
                                     Puesto = a.Puesto,
                                     NoEmpleado = a.NoEmpleado,
                                     Proyecto = a.Proyecto,
                                     DireccionEjecutiva = a.DireccionEjecutiva,
                                     UnidadDeNegocio = a.UnidadDeNegocio,
                                     Nivel = b.Nivel,
                                     CentrosdeCostos = a.CentrosdeCostos,
                                     JefeDirecto = a.JefeDirecto
                                 }).ToListAsync();

                return res;
            }
        }

        public async Task<List<Dor_ObjetivosGenerales>> GetDorObjetivosGenerales(string nivel, string unidadNegocio, int mes, string seccion)
        {
            List<Dor_ObjetivosGenerales> res = null;

            using (var db = new ConnectionDB(dbConfig))
            {
                if (seccion == "Carga")
                {
                    res = await (from a in db.dOR_Objetivos_Gral
                                 join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                                 join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                                 join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Mes, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Mes, d.Año } into dJoin
                                 from dItem in dJoin.DefaultIfEmpty()
                                 where a.UnidadDeNegocio == unidadNegocio
                                 && b.Nivel == nivel
                                 && (mes == 0 || a.Mes == mes)
                                 select new Dor_ObjetivosGenerales
                                 {
                                     Id = a.Id,
                                     UnidadDeNegocio = a.UnidadDeNegocio,
                                     Concepto = a.Concepto,
                                     Descripcion = a.Descripcion,
                                     Meta = a.Meta,
                                     Real = a.Real != null ? a.Real : "0",
                                     PorcentajeEstimado = b.Valor,
                                     PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                     MetaMensual = (dItem != null && dItem.Gasto > 0) ? dItem.Ingreso - dItem.Gasto / dItem.Gasto : 0,
                                     Nivel = b.Nivel,
                                     Valor = b.Valor,
                                     Tooltip = c.Tooltip
                                 }).ToListAsync();
                }
                else
                {
                    if (mes > 0)
                    {
                        res = await (from a in db.dOR_Objetivos_Gral
                                     join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                                     join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                                     join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Mes, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Mes, d.Año } into dJoin
                                     from dItem in dJoin.DefaultIfEmpty()
                                     where a.UnidadDeNegocio == unidadNegocio
                                     && b.Nivel == nivel
                                     && a.Mes == mes
                                     group new Dor_ObjetivosGenerales
                                     {
                                         Id = a.Id,
                                         UnidadDeNegocio = a.UnidadDeNegocio,
                                         Concepto = a.Concepto,
                                         Descripcion = a.Descripcion,
                                         Meta = a.Meta.ToString().Trim(),
                                         Real = a.Real != null ? a.Real : "0",
                                         PorcentajeEstimado = b.Valor,
                                         PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                         MetaMensual = (dItem != null && dItem.Gasto > 0) ? dItem.Ingreso - dItem.Gasto / dItem.Gasto : 0,
                                         Nivel = b.Nivel,
                                         Valor = b.Valor,
                                         Tooltip = c.Tooltip
                                     } by a.Descripcion into g
                                     select new Dor_ObjetivosGenerales
                                     {
                                         Id = g.First().Id,
                                         UnidadDeNegocio = g.First().UnidadDeNegocio,
                                         Concepto = g.First().Concepto,
                                         Descripcion = g.Key,
                                         Meta = g.First().Meta,
                                         Real = g.Sum(x => Convert.ToDecimal(x.Real)).ToString(),
                                         PorcentajeEstimado = g.First().PorcentajeEstimado,
                                         PorcentajeReal = g.Sum(x => Convert.ToDecimal(x.PorcentajeReal)).ToString(),
                                         MetaMensual = g.Sum(x => Convert.ToDecimal(x.MetaMensual)),
                                         Nivel = g.First().Nivel,
                                         Valor = g.First().Valor,
                                         Tooltip = g.First().Tooltip
                                     }).ToListAsync();
                    }
                    else
                    {
                        int currentMonth = DateTime.Now.Month;
                        int targetMonth = currentMonth == 1 ? 1 : currentMonth - 1;

                        res = await (from a in db.dOR_Objetivos_Gral
                                     join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                                     join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                                     join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Mes, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Mes, d.Año } into dJoin
                                     from dItem in dJoin.DefaultIfEmpty()
                                     where a.UnidadDeNegocio == unidadNegocio
                                     && b.Nivel == nivel
                                     && ((currentMonth == 1 && a.Mes == targetMonth) || (currentMonth > 1 && a.Mes >= 1 && a.Mes <= targetMonth))
                                     group new Dor_ObjetivosGenerales
                                     {
                                         Id = a.Id,
                                         UnidadDeNegocio = a.UnidadDeNegocio,
                                         Concepto = a.Concepto,
                                         Descripcion = a.Descripcion,
                                         Meta = a.Meta,
                                         Real = a.Real != null ? a.Real : "0",
                                         PorcentajeEstimado = b.Valor,
                                         PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                         MetaMensual = (dItem != null && dItem.Gasto > 0) ? dItem.Ingreso - dItem.Gasto / dItem.Gasto : 0,
                                         Nivel = b.Nivel,
                                         Valor = b.Valor,
                                         Tooltip = c.Tooltip
                                     } by a.Descripcion into g
                                     select new Dor_ObjetivosGenerales
                                     {
                                         Id = g.First().Id,
                                         UnidadDeNegocio = g.First().UnidadDeNegocio,
                                         Concepto = g.First().Concepto,
                                         Descripcion = g.Key,
                                         Meta = g.First().Meta,
                                         Real = g.Sum(x => Convert.ToDecimal(x.Real)).ToString(),
                                         PorcentajeEstimado = g.First().PorcentajeEstimado,
                                         PorcentajeReal = g.Sum(x => Convert.ToDecimal(x.PorcentajeReal)).ToString(),
                                         MetaMensual = g.Sum(x => Convert.ToDecimal(x.MetaMensual)),
                                         Nivel = g.First().Nivel,
                                         Valor = g.First().Valor,
                                         Tooltip = g.First().Tooltip
                                     }).ToListAsync();
                    }
                }

                //var res = await (from a in db.dOR_Objetivos_Gral
                //                 join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                //                 join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                //                 where a.UnidadDeNegocio == unidadNegocio
                //                 && b.Nivel == nivel
                //                 && (mes == 0 || a.Mes == mes)
                //                 select new Dor_ObjetivosGenerales
                //                 {
                //                     Id = a.Id,
                //                     UnidadDeNegocio = a.UnidadDeNegocio,
                //                     Concepto = a.Concepto,
                //                     Descripcion = a.Descripcion,
                //                     Meta = a.Meta,
                //                     Real = a.Real != null ? a.Real : "0",
                //                     PorcentajeEstimado = b.Valor,
                //                     PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                //                     Nivel = b.Nivel,
                //                     Valor = b.Valor,
                //                     Tooltip = c.Tooltip
                //                 }).ToListAsync();

                return res;
            }
        }

        public async Task<List<Dor_ObjetivosGenerales>> GetDorGpmProyecto(int proyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from a in db.dOR_Gpm_Proyecto
                                 join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                                 where a.Proyecto == proyecto
                                 select new Dor_ObjetivosGenerales
                                 {
                                     Id = a.Id,
                                     UnidadDeNegocio = a.UnidadDeNegocio,
                                     Concepto = a.Concepto,
                                     Descripcion = a.Descripcion,
                                     Meta = a.Meta.ToString().Trim(),
                                     Nivel = null,
                                     Valor = null,
                                     Tooltip = c.Tooltip
                                 }).ToListAsync();

                return res;
            }
        }

        public async Task<List<Dor_ObjetivosGenerales>> GetDorMetasProyecto(int proyecto, int nivel, int mes, string seccion)
        {
            List<Dor_ObjetivosGenerales> res = null;

            using (var db = new ConnectionDB(dbConfig))
            {
                if (seccion == "Carga")
                {
                    res = await (from a in db.dOR_Meta_Proyecto
                                 join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                                 join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                                 join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Mes, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Mes, d.Año } into dJoin
                                 from dItem in dJoin.DefaultIfEmpty()
                                 where a.NoProyecto == proyecto
                                 && b.Nivel == nivel.ToString().Trim()
                                 && (mes == 0 || a.Mes == mes)
                                 select new Dor_ObjetivosGenerales
                                 {
                                     Id = a.Id,
                                     UnidadDeNegocio = a.UnidadDeNegocio,
                                     Concepto = a.Concepto,
                                     Descripcion = a.Descripcion,
                                     Meta = a.Meta.ToString().Trim(),
                                     Real = a.Real != null ? a.Real : "0",
                                     PorcentajeEstimado = b.Valor,
                                     PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                     MetaMensual = (dItem != null && dItem.Gasto > 0) ? dItem.Ingreso - dItem.Gasto / dItem.Gasto : 0,
                                     Nivel = b.Nivel,
                                     Valor = b.Valor,
                                     Tooltip = c.Tooltip
                                 }).ToListAsync();
                }
                else
                {
                    if (mes > 0)
                    {
                        res = await (from a in db.dOR_Meta_Proyecto
                                     join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                                     join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                                     join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Mes, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Mes, d.Año } into dJoin
                                     from dItem in dJoin.DefaultIfEmpty()
                                     where a.NoProyecto == proyecto
                                     && b.Nivel == nivel.ToString().Trim()
                                     && a.Mes == mes
                                     group new Dor_ObjetivosGenerales
                                     {
                                         Id = a.Id,
                                         UnidadDeNegocio = a.UnidadDeNegocio,
                                         Concepto = a.Concepto,
                                         Descripcion = a.Descripcion,
                                         Meta = a.Meta.ToString().Trim(),
                                         Real = a.Real != null ? a.Real : "0",
                                         PorcentajeEstimado = b.Valor,
                                         PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                         MetaMensual = (dItem != null && dItem.Gasto > 0) ? dItem.Ingreso - dItem.Gasto / dItem.Gasto : 0,
                                         Nivel = b.Nivel,
                                         Valor = b.Valor,
                                         Tooltip = c.Tooltip
                                     } by a.Descripcion into g
                                     select new Dor_ObjetivosGenerales
                                     {
                                         Id = g.First().Id,
                                         UnidadDeNegocio = g.First().UnidadDeNegocio,
                                         Concepto = g.First().Concepto,
                                         Descripcion = g.Key,
                                         Meta = g.First().Meta,
                                         Real = g.Sum(x => Convert.ToDecimal(x.Real)).ToString(),
                                         PorcentajeEstimado = g.First().PorcentajeEstimado,
                                         PorcentajeReal = g.Sum(x => Convert.ToDecimal(x.PorcentajeReal)).ToString(),
                                         MetaMensual = g.Sum(x => Convert.ToDecimal(x.MetaMensual)),
                                         Nivel = g.First().Nivel,
                                         Valor = g.First().Valor,
                                         Tooltip = g.First().Tooltip
                                     }).ToListAsync();
                    }
                    else
                    {
                        int currentMonth = DateTime.Now.Month;
                        int targetMonth = currentMonth == 1 ? 1 : currentMonth - 1;

                        res = await (from a in db.dOR_Meta_Proyecto
                                     join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                                     join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                                     join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Mes, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Mes, d.Año } into dJoin
                                     from dItem in dJoin.DefaultIfEmpty()
                                     where a.NoProyecto == proyecto
                                     && b.Nivel == nivel.ToString().Trim()
                                     && ((currentMonth == 1 && a.Mes == targetMonth) || (currentMonth > 1 && a.Mes >= 1 && a.Mes <= targetMonth))
                                     group new Dor_ObjetivosGenerales
                                     {
                                         Id = a.Id,
                                         UnidadDeNegocio = a.UnidadDeNegocio,
                                         Concepto = a.Concepto,
                                         Descripcion = a.Descripcion,
                                         Meta = a.Meta.ToString().Trim(),
                                         Real = a.Real != null ? a.Real : "0",
                                         PorcentajeEstimado = b.Valor,
                                         PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                         MetaMensual = (dItem != null && dItem.Gasto > 0) ? dItem.Ingreso - dItem.Gasto / dItem.Gasto : 0,
                                         Nivel = b.Nivel,
                                         Valor = b.Valor,
                                         Tooltip = c.Tooltip
                                     } by a.Descripcion into g
                                     select new Dor_ObjetivosGenerales
                                     {
                                         Id = g.First().Id,
                                         UnidadDeNegocio = g.First().UnidadDeNegocio,
                                         Concepto = g.First().Concepto,
                                         Descripcion = g.Key,
                                         Meta = g.First().Meta,
                                         Real = g.Sum(x => Convert.ToDecimal(x.Real)).ToString(),
                                         PorcentajeEstimado = g.First().PorcentajeEstimado,
                                         PorcentajeReal = g.Sum(x => Convert.ToDecimal(x.PorcentajeReal)).ToString(),
                                         MetaMensual = g.Sum(x => Convert.ToDecimal(x.MetaMensual)),
                                         Nivel = g.First().Nivel,
                                         Valor = g.First().Valor,
                                         Tooltip = g.First().Tooltip
                                     }).ToListAsync();
                    }
                }

                //var res = await (from a in db.dOR_Meta_Proyecto
                //                 join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                //                 join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                //                 where a.NoProyecto == proyecto
                //                 && b.Nivel == nivel.ToString().Trim()
                //                 && (mes == 0 || a.Mes == mes)
                //                 select new Dor_ObjetivosGenerales
                //                 {
                //                     Id = a.Id,
                //                     UnidadDeNegocio = a.UnidadDeNegocio,
                //                     Concepto = a.Concepto,
                //                     Descripcion = a.Descripcion,
                //                     Meta = a.Meta.ToString().Trim(),
                //                     Real = a.Real != null ? a.Real : "0",
                //                     PorcentajeEstimado = b.Valor,
                //                     PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                //                     Nivel = b.Nivel,
                //                     Valor = b.Valor,
                //                     Tooltip = c.Tooltip
                //                 }).ToListAsync();

                return res;
            }
        }

        public async Task<List<Dor_ObjetivosEmpleado>> GetDorObjetivosDesepeno(int anio, int proyecto, int empleado, int nivel, int? acepto, int mes)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                if (acepto >= 1)
                {
                    var subQuery = await (from a in db.dOR_ObjetivosDesepenos
                                          join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                                          where a.Empleado == empleado.ToString().Trim()
                                          && a.Anio == anio
                                          && a.Proyecto == proyecto.ToString().Trim()
                                          && a.Acepto == acepto.ToString().Trim()
                                          //&& b.Nivel == nivel.ToString().Trim()
                                          //&& (mes == 0 || a.Mes == mes)
                                          select new Dor_ObjetivosEmpleado
                                          {
                                              IdEmpOb = a.IdEmpOb,
                                              UnidadDeNegocio = a.UnidadDeNegocio,
                                              Concepto = a.Concepto,
                                              Descripcion = a.Descripcion,
                                              Meta = a.Meta,
                                              Real = a.Real != null ? a.Real : "0",
                                              PorcentajeEstimado = b.Valor,
                                              PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                              Acepto = a.Acepto,
                                              MotivoR = a.MotivoR,
                                              FechaCarga = a.FechaCarga,
                                              FechaAceptado = a.FechaAceptado,
                                              FechaRechazo = a.FechaRechazo,
                                              Nivel = null,
                                              Valor = a.Nivel,
                                              Tooltip = null
                                          }).ToListAsync();

                    return subQuery;
                }
                else
                {
                    var resBase = await (from a in db.dOR_ObjetivosDesepenos
                                         join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                                         where a.Empleado == empleado.ToString().Trim()
                                         && a.Anio == anio
                                         && a.Proyecto == proyecto.ToString().Trim()
                                         && a.Nivel != null
                                         //&& (mes == 0 || a.Mes == mes)
                                         select new Dor_ObjetivosEmpleado
                                         {
                                             IdEmpOb = a.IdEmpOb,
                                             UnidadDeNegocio = a.UnidadDeNegocio,
                                             Concepto = a.Concepto,
                                             Descripcion = a.Descripcion,
                                             Meta = a.Meta,
                                             Real = a.Real != null ? a.Real : "0",
                                             PorcentajeEstimado = b.Valor,
                                             PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                             Acepto = a.Acepto,
                                             MotivoR = a.MotivoR,
                                             FechaCarga = a.FechaCarga,
                                             FechaAceptado = a.FechaAceptado,
                                             FechaRechazo = a.FechaRechazo,
                                             Nivel = null,
                                             Valor = a.Nivel,
                                             Tooltip = null
                                         }).ToListAsync();

                    var res = await (from a in db.dOR_ObjetivosDesepenos
                                     join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                                     join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                                     where a.Empleado == empleado.ToString().Trim()
                                     && a.Anio == anio
                                     && a.Proyecto == proyecto.ToString().Trim()
                                     && b.Nivel == nivel.ToString().Trim()
                                     && a.Acepto == acepto.ToString().Trim()
                                     //&& (mes == 0 || a.Mes == mes)
                                     select new Dor_ObjetivosEmpleado
                                     {
                                         IdEmpOb = a.IdEmpOb,
                                         UnidadDeNegocio = a.UnidadDeNegocio,
                                         Concepto = a.Concepto,
                                         Descripcion = a.Descripcion,
                                         Meta = a.Meta,
                                         Real = a.Real != null ? a.Real : "0",
                                         PorcentajeEstimado = b.Valor,
                                         PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                         Acepto = a.Acepto,
                                         MotivoR = a.MotivoR,
                                         FechaCarga = a.FechaCarga,
                                         FechaAceptado = a.FechaAceptado,
                                         FechaRechazo = a.FechaRechazo,
                                         Nivel = b.Nivel,
                                         Valor = b.Valor,
                                         Tooltip = c.Tooltip
                                     }).ToListAsync();
                    if (resBase == null)
                        return res;

                    var dict = res.ToDictionary(p => p.IdEmpOb);
                    foreach (var item in resBase)
                    {
                        dict[item.IdEmpOb] = item;
                    }
                    var merged = dict.Values.ToList();
                    return merged;
                    //return await resBase.Union(res).ToListAsync();
                }
            }
        }


        public async Task<List<DOR_ObjetivosDesepeno>> GetDorObjetivosDesepeno(int anio, int proyecto, string concepto, int? empleado)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var query = await (from cat in db.dOR_ObjetivosDesepenos
                             where cat.Anio == anio
                             && cat.Proyecto == proyecto.ToString()
                             && cat.Concepto == concepto
                             //&& cat.Empleado == (empleado.HasValue ? empleado.ToString() : default(string))
                             //|| cat.Empleado == null
                             select cat).ToListAsync();

                return query;
            }
        }


        public async Task<(bool existe, string mensaje)> UpdObjetivo(DOR_ObjetivosDesepeno objetivo)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                var objetivoDB = await db.dOR_ObjetivosDesepenos.Where(x => x.IdEmpOb == objetivo.IdEmpOb)
                                    .UpdateAsync(x => new DOR_ObjetivosDesepeno
                                    {
                                        Meta = objetivo.Meta,
                                        Acepto = objetivo.Acepto,
                                        Descripcion = objetivo.Descripcion,
                                        MotivoR = objetivo.MotivoR,
                                        FechaCarga = objetivo.FechaCarga,
                                        FechaAceptado = objetivo.FechaAceptado,
                                        FechaRechazo = objetivo.FechaRechazo,
                                        Nivel = objetivo.Nivel,
                                    }) > 0;

                resp.Success = objetivoDB;
            }
            return resp;
        }
        public async Task<(bool existe, string mensaje)> AddObjetivo(DOR_ObjetivosDesepeno objetivo)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                //var objetivoDB = await db.dOR_ObjetivosDesepenos.Where(x => x.Anio == objetivo.Anio && x.Empleado == objetivo.Empleado && x.Proyecto == objetivo.Proyecto && x.Descripcion == objetivo.Descripcion).FirstOrDefaultAsync();
                //if(objetivoDB is  null)
                //{
                var inseert = await db.dOR_ObjetivosDesepenos
                                .Value(x => x.UnidadDeNegocio, objetivo.UnidadDeNegocio)
                                .Value(x => x.Concepto, objetivo.Concepto)
                                .Value(x => x.Descripcion, objetivo.Descripcion)
                                .Value(x => x.Meta, objetivo.Meta)
                                .Value(x => x.Real, objetivo.Real)
                                .Value(x => x.Ponderado, objetivo.Ponderado)
                                .Value(x => x.Calificacion, objetivo.Calificacion)
                                .Value(x => x.Nivel, objetivo.Nivel)
                                .Value(x => x.Anio, objetivo.Anio)
                                .Value(x => x.Proyecto, objetivo.Proyecto)
                                .Value(x => x.Empleado, objetivo.Empleado)
                                //.UpdateAsync() > 0;
                                .InsertAsync() > 0;
                resp.Success = inseert;
                resp.Message = inseert == default ? "Ocurrio un error al agregar el objetivo." : string.Empty;
                //}
                //else
                //{
                //	resp.Success = default;
                //	resp.Message = "ya existe un registro con los datos proporcionados.";
                //}
            }
            return resp;
        }        

    }
}
