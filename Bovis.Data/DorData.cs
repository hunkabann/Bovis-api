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
                res = await (from a in db.dOR_Objetivos_Gral
                             join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                             join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                             join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Mes, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Mes, d.Año } into dJoin
                             from dItem in dJoin.DefaultIfEmpty()
                             where a.UnidadDeNegocio == unidadNegocio
                             && b.Nivel == nivel
                             group new Dor_ObjetivosGenerales
                             {
                                 Id = a.Id,
                                 UnidadDeNegocio = a.UnidadDeNegocio,
                                 Concepto = a.Concepto,
                                 Descripcion = a.Descripcion,
                                 Meta = a.Meta,
                                 PorcentajeEstimado = b.Valor,
                                 //PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                 //Ingreso = dItem != null ? dItem.Ingreso : 0,
                                 //Gasto = dItem != null ? dItem.Gasto : 0,
                                 Nivel = b.Nivel,
                                 Valor = b.Valor,
                                 Tooltip = c.Tooltip,
                                 ENE = a.ENE,
                                 FEB = a.FEB,
                                 MAR = a.MAR,
                                 ABR = a.ABR,
                                 MAY = a.MAY,
                                 JUN = a.JUN,
                                 JUL = a.JUL,
                                 AGO = a.AGO,
                                 SEP = a.SEP,
                                 OCT = a.OCT,
                                 NOV = a.NOV,
                                 DIC = a.DIC
                             } by new { a.Descripcion, a.Concepto } into g
                             select new Dor_ObjetivosGenerales
                             {
                                 Id = g.First().Id,
                                 UnidadDeNegocio = g.First().UnidadDeNegocio,
                                 Concepto = g.Key.Concepto,
                                 Descripcion = g.Key.Descripcion,
                                 Meta = g.First().Meta,
                                 PromedioReal = g.Average(item => Convert.ToDecimal(item.Real)).ToString(),
                                 PorcentajeEstimado = g.First().PorcentajeEstimado,
                                 //PorcentajeReal = g.Sum(x => Convert.ToDecimal(x.PorcentajeReal)).ToString(),
                                 //Ingreso = g.Sum(x => Convert.ToDecimal(x.Ingreso)),
                                 //Gasto = g.Sum(x => Convert.ToDecimal(x.Gasto)),
                                 Nivel = g.First().Nivel,
                                 Valor = g.First().Valor,
                                 Tooltip = g.First().Tooltip,
                                 ENE = g.First().ENE,
                                 FEB = g.First().FEB,
                                 MAR = g.First().MAR,
                                 ABR = g.First().ABR,
                                 MAY = g.First().MAY,
                                 JUN = g.First().JUN,
                                 JUL = g.First().JUL,
                                 AGO = g.First().AGO,
                                 SEP = g.First().SEP,
                                 OCT = g.First().OCT,
                                 NOV = g.First().NOV,
                                 DIC = g.First().DIC,
                                 Real = mes == 0
                                     ? g.Sum(item => Convert.ToDecimal(item.ENE.GetValueOrDefault()) + Convert.ToDecimal(item.FEB.GetValueOrDefault()) + Convert.ToDecimal(item.MAR.GetValueOrDefault()) + Convert.ToDecimal(item.ABR.GetValueOrDefault()) + Convert.ToDecimal(item.MAY.GetValueOrDefault()) + Convert.ToDecimal(item.JUN.GetValueOrDefault()) + Convert.ToDecimal(item.JUL.GetValueOrDefault()) + Convert.ToDecimal(item.AGO.GetValueOrDefault()) + Convert.ToDecimal(item.SEP.GetValueOrDefault()) + Convert.ToDecimal(item.OCT.GetValueOrDefault()) + Convert.ToDecimal(item.NOV.GetValueOrDefault()) + Convert.ToDecimal(item.DIC.GetValueOrDefault()))
                                     : mes == 1 ? g.Sum(item => Convert.ToDecimal(item.ENE.GetValueOrDefault()))
                                         : mes == 2 ? g.Sum(item => Convert.ToDecimal(item.FEB.GetValueOrDefault()))
                                         : mes == 3 ? g.Sum(item => Convert.ToDecimal(item.MAR.GetValueOrDefault()))
                                         : mes == 4 ? g.Sum(item => Convert.ToDecimal(item.ABR.GetValueOrDefault()))
                                         : mes == 5 ? g.Sum(item => Convert.ToDecimal(item.MAY.GetValueOrDefault()))
                                         : mes == 6 ? g.Sum(item => Convert.ToDecimal(item.JUN.GetValueOrDefault()))
                                         : mes == 7 ? g.Sum(item => Convert.ToDecimal(item.JUL.GetValueOrDefault()))
                                         : mes == 8 ? g.Sum(item => Convert.ToDecimal(item.AGO.GetValueOrDefault()))
                                         : mes == 9 ? g.Sum(item => Convert.ToDecimal(item.SEP.GetValueOrDefault()))
                                         : mes == 10 ? g.Sum(item => Convert.ToDecimal(item.OCT.GetValueOrDefault()))
                                         : mes == 11 ? g.Sum(item => Convert.ToDecimal(item.NOV.GetValueOrDefault()))
                                         : mes == 12 ? g.Sum(item => Convert.ToDecimal(item.DIC.GetValueOrDefault()))
                                     : 0
                             }).ToListAsync();

                foreach (var r in res)
                {
                    r.PorcentajeReal = r.Real != null && r.Valor != null && r.Meta != null && r.Meta != 0 ? (Convert.ToDecimal(r.Real) * Convert.ToDecimal(r.Valor) / Convert.ToDecimal(r.Meta)).ToString() : "0";
                }

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
                                     Meta = a.Meta,
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
                res = await (from a in db.dOR_Meta_Proyecto
                             join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                             join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
                             join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Mes, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Mes, d.Año } into dJoin
                             from dItem in dJoin.DefaultIfEmpty()
                             where a.NoProyecto == proyecto
                             && b.Nivel == nivel.ToString().Trim()
                             group new Dor_ObjetivosGenerales
                             {
                                 Id = a.Id,
                                 UnidadDeNegocio = a.UnidadDeNegocio,
                                 Concepto = a.Concepto,
                                 Descripcion = a.Descripcion,
                                 Meta = a.Meta,
                                 PorcentajeEstimado = b.Valor,
                                 //PorcentajeReal = (a.Real != null && b.Valor != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(b.Valor) / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                 //Ingreso = dItem != null ? dItem.Ingreso : 0,
                                 //Gasto = dItem != null ? dItem.Gasto : 0, 
                                 Nivel = b.Nivel,
                                 Valor = b.Valor,
                                 Tooltip = c.Tooltip,
                                 ENE = a.ENE,
                                 FEB = a.FEB,
                                 MAR = a.MAR,
                                 ABR = a.ABR,
                                 MAY = a.MAY,
                                 JUN = a.JUN,
                                 JUL = a.JUL,
                                 AGO = a.AGO,
                                 SEP = a.SEP,
                                 OCT = a.OCT,
                                 NOV = a.NOV,
                                 DIC = a.DIC
                             } by new { a.Descripcion, a.Concepto } into g
                             select new Dor_ObjetivosGenerales
                             {
                                 Id = g.First().Id,
                                 UnidadDeNegocio = g.First().UnidadDeNegocio,
                                 Concepto = g.Key.Concepto,
                                 Descripcion = g.Key.Descripcion,
                                 Meta = g.First().Meta,
                                 PromedioReal = g.Average(item => Convert.ToDecimal(item.Real)).ToString(),
                                 PorcentajeEstimado = g.First().PorcentajeEstimado,
                                 //PorcentajeReal = g.Sum(x => Convert.ToDecimal(x.PorcentajeReal)).ToString(),
                                 //Ingreso = g.Sum(x => Convert.ToDecimal(x.Ingreso)),
                                 //Gasto = g.Sum(x => Convert.ToDecimal(x.Gasto)),
                                 Nivel = g.First().Nivel,
                                 Valor = g.First().Valor,
                                 Tooltip = g.First().Tooltip,
                                 ENE = g.First().ENE,
                                 FEB = g.First().FEB,
                                 MAR = g.First().MAR,
                                 ABR = g.First().ABR,
                                 MAY = g.First().MAY,
                                 JUN = g.First().JUN,
                                 JUL = g.First().JUL,
                                 AGO = g.First().AGO,
                                 SEP = g.First().SEP,
                                 OCT = g.First().OCT,
                                 NOV = g.First().NOV,
                                 DIC = g.First().DIC,
                                 Real = mes == 0
                                     ? g.Sum(item => Convert.ToDecimal(item.ENE.GetValueOrDefault()) + Convert.ToDecimal(item.FEB.GetValueOrDefault()) + Convert.ToDecimal(item.MAR.GetValueOrDefault()) + Convert.ToDecimal(item.ABR.GetValueOrDefault()) + Convert.ToDecimal(item.MAY.GetValueOrDefault()) + Convert.ToDecimal(item.JUN.GetValueOrDefault()) + Convert.ToDecimal(item.JUL.GetValueOrDefault()) + Convert.ToDecimal(item.AGO.GetValueOrDefault()) + Convert.ToDecimal(item.SEP.GetValueOrDefault()) + Convert.ToDecimal(item.OCT.GetValueOrDefault()) + Convert.ToDecimal(item.NOV.GetValueOrDefault()) + Convert.ToDecimal(item.DIC.GetValueOrDefault())) / (DateTime.Now.Month - 1)
                                     : mes == 1 ? g.Sum(item => Convert.ToDecimal(item.ENE.GetValueOrDefault()))
                                     : mes == 2 ? g.Sum(item => Convert.ToDecimal(item.FEB.GetValueOrDefault()))
                                     : mes == 3 ? g.Sum(item => Convert.ToDecimal(item.MAR.GetValueOrDefault()))
                                     : mes == 4 ? g.Sum(item => Convert.ToDecimal(item.ABR.GetValueOrDefault()))
                                     : mes == 5 ? g.Sum(item => Convert.ToDecimal(item.MAY.GetValueOrDefault()))
                                     : mes == 6 ? g.Sum(item => Convert.ToDecimal(item.JUN.GetValueOrDefault()))
                                     : mes == 7 ? g.Sum(item => Convert.ToDecimal(item.JUL.GetValueOrDefault()))
                                     : mes == 8 ? g.Sum(item => Convert.ToDecimal(item.AGO.GetValueOrDefault()))
                                     : mes == 9 ? g.Sum(item => Convert.ToDecimal(item.SEP.GetValueOrDefault()))
                                     : mes == 10 ? g.Sum(item => Convert.ToDecimal(item.OCT.GetValueOrDefault()))
                                     : mes == 11 ? g.Sum(item => Convert.ToDecimal(item.NOV.GetValueOrDefault()))
                                     : mes == 12 ? g.Sum(item => Convert.ToDecimal(item.DIC.GetValueOrDefault()))
                                     : 0
                             }).ToListAsync();

                foreach (var r in res)
                {
                    r.PorcentajeReal = r.Real != null && r.Valor != null && r.Meta != null && r.Meta != 0 ? (Convert.ToDecimal(r.Real) * Convert.ToDecimal(r.Valor) / Convert.ToDecimal(r.Meta)).ToString() : "0";
                }

                return res;
            }
        }

        public async Task<List<Dor_ObjetivosEmpleado>> GetDorObjetivosDesepeno(int anio, int proyecto, int empleado, int nivel, int? acepto, int mes)
        {
            List<Dor_ObjetivosEmpleado> res = null;

            using (var db = new ConnectionDB(dbConfig))
            {
                if (acepto >= 1)
                {
                    res = await (from a in db.dOR_ObjetivosDesepenos
                                 join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion } into bJoin
                                 from bItem in bJoin.DefaultIfEmpty()
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
                                     PorcentajeEstimado = bItem != null ? bItem.Valor : "0",
                                     PorcentajeReal = (a.Real != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(bItem != null ? bItem.Valor : "0") / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                     Acepto = a.Acepto,
                                     MotivoR = a.MotivoR,
                                     FechaCarga = a.FechaCarga,
                                     FechaAceptado = a.FechaAceptado,
                                     FechaRechazo = a.FechaRechazo,
                                     Nivel = null,
                                     Valor = a.Nivel,
                                     Tooltip = null
                                 }).ToListAsync();
                }
                else
                {
                    var resBase = await (from a in db.dOR_ObjetivosDesepenos
                                         join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion } into bJoin
                                         from bItem in bJoin.DefaultIfEmpty()
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
                                             PorcentajeEstimado = bItem != null ? bItem.Valor : "0",
                                             PorcentajeReal = (a.Real != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(bItem != null ? bItem.Valor : "0") / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                             Acepto = a.Acepto,
                                             MotivoR = a.MotivoR,
                                             FechaCarga = a.FechaCarga,
                                             FechaAceptado = a.FechaAceptado,
                                             FechaRechazo = a.FechaRechazo,
                                             Nivel = null,
                                             Valor = a.Nivel,
                                             Tooltip = null
                                         }).ToListAsync();

                    res = await (from a in db.dOR_ObjetivosDesepenos
                                 join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion } into bJoin
                                 from bItem in bJoin.DefaultIfEmpty()
                                 join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion } into cJoin
                                 from cItem in cJoin.DefaultIfEmpty()
                                 where a.Empleado == empleado.ToString().Trim()
                                 && a.Anio == anio
                                 && a.Proyecto == proyecto.ToString().Trim()
                                 && bItem.Nivel == nivel.ToString().Trim()
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
                                     PorcentajeEstimado = bItem != null ? bItem.Valor : "0",
                                     PorcentajeReal = (a.Real != null && a.Meta != null) ? (Convert.ToDecimal(a.Real) * Convert.ToDecimal(bItem != null ? bItem.Valor : "0") / Convert.ToDecimal(a.Meta)).ToString() : "0",
                                     Acepto = a.Acepto,
                                     MotivoR = a.MotivoR,
                                     FechaCarga = a.FechaCarga,
                                     FechaAceptado = a.FechaAceptado,
                                     FechaRechazo = a.FechaRechazo,
                                     Nivel = bItem != null ? bItem.Nivel : "0",
                                     Valor = bItem != null ? bItem.Valor : "0",
                                     Tooltip = cItem != null ? cItem.Tooltip : string.Empty
                                 }).ToListAsync();

                    //if (resBase == null)
                    //    return res;

                    //var dict = res.ToDictionary(p => p.IdEmpOb);
                    //foreach (var item in resBase)
                    //{
                    //    dict[item.IdEmpOb] = item;
                    //}
                    //var merged = dict.Values.ToList();

                    //return merged;

                }
            }

            return res;
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
