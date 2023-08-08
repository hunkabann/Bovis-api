using Bovis.Common.Model;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using System.Collections.Generic;
using System.Text.Json.Nodes;
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

        // CORPORATIVO
        public async Task<List<Dor_ObjetivosGenerales>> GetDorObjetivosGenerales(int nivel, string unidadNegocio, int mes, string seccion)
        {           
            List<Dor_ObjetivosGenerales> res = null;

            int anio = mes == 0 && DateTime.Now.Month > 1 ? DateTime.Now.Year
                : mes > 1 ? DateTime.Now.Year
                : DateTime.Now.Year - 1;

            using (var db = new ConnectionDB(dbConfig))
            {
                res = await (from a in db.dOR_Objetivos_Gral
                             join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion } into bJoin
                             from bItem in bJoin.DefaultIfEmpty()
                             join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion } into cJoin
                             from cItem in cJoin.DefaultIfEmpty()
                             join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Año } into dJoin
                             from dItem in dJoin.DefaultIfEmpty()
                             where a.UnidadDeNegocio == unidadNegocio
                             && bItem.Nivel == nivel
                             //&& a.Año == anio
                             orderby a.Descripcion ascending
                             group new Dor_ObjetivosGenerales
                             {
                                 Id = a.Id,
                                 UnidadDeNegocio = a.UnidadDeNegocio,
                                 Concepto = a.Concepto,
                                 Descripcion = a.Descripcion,                                 
                                 Meta = a.Meta,
                                 MetaValor = a.MetaValor,
                                 PorcentajeEstimado = bItem.Valor ?? 0,
                                 Nivel = bItem.Nivel ?? 0,
                                 Valor = bItem.Valor ?? 0,
                                 Tooltip = cItem.Tooltip ?? string.Empty,
                                 Enero = (DateTime.Now.Month > 1 || (DateTime.Now.Month == 1 && DateTime.Now.Day >= 20) ? a.Enero : 0) ?? 0,
                                 Febrero = (DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= 20) ? a.Febrero : 0) ?? 0,
                                 Marzo = (DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= 20) ? a.Marzo : 0) ?? 0,
                                 Abril = (DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= 20) ? a.Abril : 0) ?? 0,
                                 Mayo = (DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= 20) ? a.Mayo : 0) ?? 0,
                                 Junio = (DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= 20) ? a.Junio : 0) ?? 0,
                                 Julio = (DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= 20) ? a.Julio : 0) ?? 0,
                                 Agosto = (DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= 20) ? a.Agosto : 0) ?? 0,
                                 Septiembre = (DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= 20) ? a.Septiembre : 0) ?? 0,
                                 Octubre = (DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= 20) ? a.Octubre : 0) ?? 0,
                                 Noviembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= 20) ? a.Noviembre : 0) ?? 0,
                                 Diciembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 12 && DateTime.Now.Day >= 20) ? a.Diciembre : 0) ?? 0,
                                 IngresoEnero = (DateTime.Now.Month > 1 || (DateTime.Now.Month == 1 && DateTime.Now.Day >= 20) ? dItem.InEnero : 0) ?? 0,
                                 IngresoFebrero = (DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= 20) ? dItem.InFebrero : 0) ?? 0,
                                 IngresoMarzo = (DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= 20) ? dItem.InMarzo : 0) ?? 0,
                                 IngresoAbril = (DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= 20) ? dItem.InAbril : 0) ?? 0,
                                 IngresoMayo = (DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= 20) ? dItem.InMayo : 0) ?? 0,
                                 IngresoJunio = (DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= 20) ? dItem.InJunio : 0) ?? 0,
                                 IngresoJulio = (DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= 20) ? dItem.InJulio : 0) ?? 0,
                                 IngresoAgosto = (DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= 20) ? dItem.InAgosto : 0) ?? 0,
                                 IngresoSeptiembre = (DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= 20) ? dItem.InSeptiembre : 0) ?? 0,
                                 IngresoOctubre = (DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= 20) ? dItem.InOctubre : 0) ?? 0,
                                 IngresoNoviembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= 20) ? dItem.InNoviembre : 0) ?? 0,
                                 IngresoDiciembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 12 && DateTime.Now.Day >= 20) ? dItem.InDiciembre : 0) ?? 0,
                                 GastoEnero = (DateTime.Now.Month > 1 || (DateTime.Now.Month == 1 && DateTime.Now.Day >= 20) ? dItem.OutEnero : 0) ?? 0,
                                 GastoFebrero = (DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= 20) ? dItem.OutFebrero : 0) ?? 0,
                                 GastoMarzo = (DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= 20) ? dItem.OutMarzo : 0) ?? 0,
                                 GastoAbril = (DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= 20) ? dItem.OutAbril : 0) ?? 0,
                                 GastoMayo = (DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= 20) ? dItem.OutMayo : 0) ?? 0,
                                 GastoJunio = (DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= 20) ? dItem.OutJunio : 0) ?? 0,
                                 GastoJulio = (DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= 20) ? dItem.OutJulio : 0) ?? 0,
                                 GastoAgosto = (DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= 20) ? dItem.OutAgosto : 0) ?? 0,
                                 GastoSeptiembre = (DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= 20) ? dItem.OutSeptiembre : 0) ?? 0,
                                 GastoOctubre = (DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= 20) ? dItem.OutOctubre : 0) ?? 0,
                                 GastoNoviembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= 20) ? dItem.OutNoviembre : 0) ?? 0,
                                 GastoDiciembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 12 && DateTime.Now.Day >= 20) ? dItem.OutDiciembre : 0) ?? 0
                             } by new { a.Descripcion, a.Concepto } into g
                             select new Dor_ObjetivosGenerales
                             {
                                 Id = g.First().Id,
                                 UnidadDeNegocio = g.First().UnidadDeNegocio,
                                 Concepto = g.Key.Concepto,
                                 Descripcion = g.Key.Descripcion,
                                 Meta = g.First().Meta,
                                 MetaValor = g.First().MetaValor,
                                 PromedioReal = g.Average(item => Convert.ToDecimal(item.Real)).ToString(),
                                 PorcentajeEstimado = g.First().PorcentajeEstimado,
                                 Nivel = g.First().Nivel,
                                 Valor = g.First().Valor,
                                 Tooltip = g.First().Tooltip,
                                 Enero = g.First().Enero,
                                 Febrero = g.First().Febrero,
                                 Marzo = g.First().Marzo,
                                 Abril = g.First().Abril,
                                 Mayo = g.First().Mayo,
                                 Junio = g.First().Junio,
                                 Julio = g.First().Julio,
                                 Agosto = g.First().Agosto,
                                 Septiembre = g.First().Septiembre,
                                 Octubre = g.First().Octubre,
                                 Noviembre = g.First().Noviembre,
                                 Diciembre = g.First().Diciembre,                                 
                                 Real = mes == 0 ? (g.First().Enero + g.First().Febrero + g.First().Marzo + g.First().Abril + g.First().Mayo + g.First().Junio + g.First().Julio + g.First().Agosto + g.First().Septiembre + g.First().Octubre + g.First().Noviembre + g.First().Diciembre) / (DateTime.Now.Month > 1 && DateTime.Now.Day >= 20 ? DateTime.Now.Month : DateTime.Now.Month - 1)
                                 : mes == 1 ? g.First().Enero
                                 : mes == 2 ? g.First().Febrero
                                 : mes == 3 ? g.First().Marzo
                                 : mes == 4 ? g.First().Abril
                                 : mes == 5 ? g.First().Mayo
                                 : mes == 6 ? g.First().Junio
                                 : mes == 7 ? g.First().Julio
                                 : mes == 8 ? g.First().Agosto
                                 : mes == 9 ? g.First().Septiembre
                                 : mes == 10 ? g.First().Octubre
                                 : mes == 11 ? g.First().Noviembre
                                 : mes == 12 ? g.First().Diciembre
                                 : 0,
                                 IngresoEnero = g.First().IngresoEnero,
                                 IngresoFebrero = g.First().IngresoFebrero,
                                 IngresoMarzo = g.First().IngresoMarzo,
                                 IngresoAbril = g.First().IngresoAbril,
                                 IngresoMayo = g.First().IngresoMayo,
                                 IngresoJunio = g.First().IngresoJunio,
                                 IngresoJulio = g.First().IngresoJulio,
                                 IngresoAgosto = g.First().IngresoAgosto,
                                 IngresoSeptiembre = g.First().IngresoSeptiembre,
                                 IngresoOctubre = g.First().IngresoOctubre,
                                 IngresoNoviembre = g.First().IngresoNoviembre,
                                 IngresoDiciembre = g.First().IngresoDiciembre,
                                 IngresoTotal = mes == 0 ? (g.First().IngresoEnero + g.First().IngresoFebrero + g.First().IngresoMarzo + g.First().IngresoAbril + g.First().IngresoMayo + g.First().IngresoJunio + g.First().IngresoJulio + g.First().IngresoAgosto + g.First().IngresoSeptiembre + g.First().IngresoOctubre + g.First().IngresoNoviembre + g.First().IngresoDiciembre) / g.First().MetaValor
                                 : mes == 1 ? g.First().IngresoEnero / g.First().MetaValor
                                 : mes == 2 ? g.First().IngresoFebrero / g.First().MetaValor
                                 : mes == 3 ? g.First().IngresoMarzo / g.First().MetaValor
                                 : mes == 4 ? g.First().IngresoAbril / g.First().MetaValor
                                 : mes == 5 ? g.First().IngresoMayo / g.First().MetaValor
                                 : mes == 6 ? g.First().IngresoJunio / g.First().MetaValor
                                 : mes == 7 ? g.First().IngresoJulio / g.First().MetaValor
                                 : mes == 8 ? g.First().IngresoAgosto / g.First().MetaValor
                                 : mes == 9 ? g.First().IngresoSeptiembre / g.First().MetaValor
                                 : mes == 10 ? g.First().IngresoOctubre / g.First().MetaValor
                                 : mes == 11 ? g.First().IngresoNoviembre / g.First().MetaValor
                                 : mes == 12 ? g.First().IngresoDiciembre / g.First().MetaValor
                                 : 0,
                                 GastoEnero = g.First().GastoEnero,
                                 GastoFebrero = g.First().GastoFebrero,
                                 GastoMarzo = g.First().GastoMarzo,
                                 GastoAbril = g.First().GastoAbril,
                                 GastoMayo = g.First().GastoMayo,
                                 GastoJunio = g.First().GastoJunio,
                                 GastoJulio = g.First().GastoJulio,
                                 GastoAgosto = g.First().GastoAgosto,
                                 GastoSeptiembre = g.First().GastoSeptiembre,
                                 GastoOctubre = g.First().GastoOctubre,
                                 GastoNoviembre = g.First().GastoNoviembre,
                                 GastoDiciembre = g.First().GastoDiciembre,
                                 GastoTotal = mes == 0 ? (g.First().GastoEnero + g.First().GastoFebrero + g.First().GastoMarzo + g.First().GastoAbril + g.First().GastoMayo + g.First().GastoJunio + g.First().GastoJulio + g.First().GastoAgosto + g.First().GastoSeptiembre + g.First().GastoOctubre + g.First().GastoNoviembre + g.First().GastoDiciembre) / g.First().MetaValor
                                 : mes == 1 ? g.First().GastoEnero / g.First().MetaValor
                                 : mes == 2 ? g.First().GastoFebrero / g.First().MetaValor
                                 : mes == 3 ? g.First().GastoMarzo / g.First().MetaValor
                                 : mes == 4 ? g.First().GastoAbril / g.First().MetaValor
                                 : mes == 5 ? g.First().GastoMayo / g.First().MetaValor
                                 : mes == 6 ? g.First().GastoJunio / g.First().MetaValor
                                 : mes == 7 ? g.First().GastoJulio / g.First().MetaValor
                                 : mes == 8 ? g.First().GastoAgosto / g.First().MetaValor
                                 : mes == 9 ? g.First().GastoSeptiembre / g.First().MetaValor
                                 : mes == 10 ? g.First().GastoOctubre / g.First().MetaValor
                                 : mes == 11 ? g.First().GastoNoviembre / g.First().MetaValor
                                 : mes == 12 ? g.First().GastoDiciembre / g.First().MetaValor
                                 : 0,
                                 MetaMensual = g.First().Enero + g.First().Febrero + g.First().Marzo + g.First().Abril + g.First().Mayo + g.First().Junio + g.First().Julio + g.First().Agosto + g.First().Septiembre + g.First().Octubre + g.First().Noviembre + g.First().Diciembre
                             }).ToListAsync();

                var res_meta_mensual = await (from a in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs
                                              where a.Año == DateTime.Now.Year
                                              select a).ToListAsync();

                decimal? ingresos = 0;
                decimal? gastos = 0;
                decimal? resta_ingresos_gastos = 0;

                foreach (var m in res_meta_mensual)
                {
                    ingresos += m.InEnero ?? 0 + m.InFebrero ?? 0 + m.InMarzo ?? 0 + m.InAbril ?? 0 + m.InMayo ?? 0 + m.InJunio ?? 0 + m.InJulio ?? 0 + m.InAgosto ?? 0 + m.InSeptiembre ?? 0 + m.InOctubre ?? 0 + m.InNoviembre ?? 0 + m.InDiciembre ?? 0;
                    gastos += m.OutEnero ?? 0 + m.OutFebrero ?? 0 + m.OutMarzo ?? 0 + m.OutAbril ?? 0 + m.OutMayo ?? 0 + m.OutJunio ?? 0 + m.OutJulio ?? 0 + m.OutAgosto ?? 0 + m.OutSeptiembre ?? 0 + m.OutOctubre ?? 0 + m.OutNoviembre ?? 0 + m.OutDiciembre ?? 0;
                }

                resta_ingresos_gastos = ingresos - gastos;

                foreach (var r in res)
                {
                    r.PorcentajeReal = r.Real != null && r.Valor != null && r.Meta != null && r.Meta != 0 ? Convert.ToDecimal(r.Real) * Convert.ToDecimal(r.Valor) / Convert.ToDecimal(r.Meta) : 0;                    
                    //r.MetaMensual = resta_ingresos_gastos / r.MetaValor;
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

        // DE PROYECTO / AREA
        public async Task<List<Dor_ObjetivosGenerales>> GetDorMetasProyecto(int proyecto, int nivel, int mes, int empleado, string seccion)
        {
            List<Dor_ObjetivosGenerales> res = null;

            int anio = mes == 0 && DateTime.Now.Month > 1 ? DateTime.Now.Year
                : mes > 1 ? DateTime.Now.Year
                : DateTime.Now.Year - 1;

            using (var db = new ConnectionDB(dbConfig))
            {                
                res = await (from a in db.dOR_Meta_Proyectos
                             join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion } into bJoin
                             from bItem in bJoin.DefaultIfEmpty()
                             join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion } into cJoin
                             from cItem in cJoin.DefaultIfEmpty()
                             join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Año } into dJoin
                             from dItem in dJoin.DefaultIfEmpty()
                             where a.NoProyecto == proyecto
                             && bItem.Nivel == nivel
                             //&& a.Año == anio
                             orderby a.Descripcion ascending
                             group new Dor_ObjetivosGenerales
                             {
                                 Id = a.Id,
                                 UnidadDeNegocio = a.UnidadDeNegocio,
                                 Concepto = a.Concepto,
                                 Descripcion = a.Descripcion,
                                 Empleado = empleado,
                                 Meta = a.Meta,                                         
                                 PorcentajeEstimado = bItem.Valor ?? 0,
                                 RealArea = a.Real ?? 0,
                                 Nivel = bItem.Nivel ?? 0,
                                 Valor = bItem.Valor ?? 0,
                                 Tooltip = cItem.Tooltip ?? string.Empty,
                                 Enero = (DateTime.Now.Month > 1 || (DateTime.Now.Month == 1 && DateTime.Now.Day >= 20) ? a.Enero : 0) ?? 0,
                                 Febrero = (DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= 20) ? a.Febrero : 0) ?? 0,
                                 Marzo = (DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= 20) ? a.Marzo : 0) ?? 0,
                                 Abril = (DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= 20) ? a.Abril : 0) ?? 0,
                                 Mayo = (DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= 20) ? a.Mayo : 0) ?? 0,
                                 Junio = (DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= 20) ? a.Junio : 0) ?? 0,
                                 Julio = (DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= 20) ? a.Julio : 0) ?? 0,
                                 Agosto = (DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= 20) ? a.Agosto : 0) ?? 0,
                                 Septiembre = (DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= 20) ? a.Septiembre : 0) ?? 0,
                                 Octubre = (DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= 20) ? a.Octubre : 0) ?? 0,
                                 Noviembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= 20) ? a.Noviembre : 0) ?? 0,
                                 Diciembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 12 && DateTime.Now.Day >= 20) ? a.Diciembre : 0) ?? 0,
                                 IngresoEnero = (DateTime.Now.Month > 1 || (DateTime.Now.Month == 1 && DateTime.Now.Day >= 20) ? dItem.InEnero : 0) ?? 0,
                                 IngresoFebrero = (DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= 20) ? dItem.InFebrero : 0) ?? 0,
                                 IngresoMarzo = (DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= 20) ? dItem.InMarzo : 0) ?? 0,
                                 IngresoAbril = (DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= 20) ? dItem.InAbril : 0) ?? 0,
                                 IngresoMayo = (DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= 20) ? dItem.InMayo : 0) ?? 0,
                                 IngresoJunio = (DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= 20) ? dItem.InJunio : 0) ?? 0,
                                 IngresoJulio = (DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= 20) ? dItem.InJulio : 0) ?? 0,
                                 IngresoAgosto = (DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= 20) ? dItem.InAgosto : 0) ?? 0,
                                 IngresoSeptiembre = (DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= 20) ? dItem.InSeptiembre : 0) ?? 0,
                                 IngresoOctubre = (DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= 20) ? dItem.InOctubre : 0) ?? 0,
                                 IngresoNoviembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= 20) ? dItem.InNoviembre : 0) ?? 0,
                                 IngresoDiciembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 12 && DateTime.Now.Day >= 20) ? dItem.InDiciembre : 0) ?? 0,
                                 GastoEnero = (DateTime.Now.Month > 1 || (DateTime.Now.Month == 1 && DateTime.Now.Day >= 20) ? dItem.OutEnero : 0) ?? 0,
                                 GastoFebrero = (DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= 20) ? dItem.OutFebrero : 0) ?? 0,
                                 GastoMarzo = (DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= 20) ? dItem.OutMarzo : 0) ?? 0,
                                 GastoAbril = (DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= 20) ? dItem.OutAbril : 0) ?? 0,
                                 GastoMayo = (DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= 20) ? dItem.OutMayo : 0) ?? 0,
                                 GastoJunio = (DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= 20) ? dItem.OutJunio : 0) ?? 0,
                                 GastoJulio = (DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= 20) ? dItem.OutJulio : 0) ?? 0,
                                 GastoAgosto = (DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= 20) ? dItem.OutAgosto : 0) ?? 0,
                                 GastoSeptiembre = (DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= 20) ? dItem.OutSeptiembre : 0) ?? 0,
                                 GastoOctubre = (DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= 20) ? dItem.OutOctubre : 0) ?? 0,
                                 GastoNoviembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= 20) ? dItem.OutNoviembre : 0) ?? 0,
                                 GastoDiciembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 12 && DateTime.Now.Day >= 20) ? dItem.OutDiciembre : 0) ?? 0,
                                 ProyectadoEnero = (DateTime.Now.Month > 1 || (DateTime.Now.Month == 1 && DateTime.Now.Day >= 20) ? a.ProyectadoEnero : 0) ?? 0,
                                 ProyectadoFebrero = (DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= 20) ? a.ProyectadoFebrero : 0) ?? 0,
                                 ProyectadoMarzo = (DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= 20) ? a.ProyectadoMarzo : 0) ?? 0,
                                 ProyectadoAbril = (DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= 20) ? a.ProyectadoAbril : 0) ?? 0,
                                 ProyectadoMayo = (DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= 20) ? a.ProyectadoMayo : 0) ?? 0,
                                 ProyectadoJunio = (DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= 20) ? a.ProyectadoJunio : 0) ?? 0,
                                 ProyectadoJulio = (DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= 20) ? a.ProyectadoJulio : 0) ?? 0,
                                 ProyectadoAgosto = (DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= 20) ? a.ProyectadoAgosto : 0) ?? 0,
                                 ProyectadoSeptiembre = (DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= 20) ? a.ProyectadoSeptiembre : 0) ?? 0,
                                 ProyectadoOctubre = (DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= 20) ? a.ProyectadoOctubre : 0) ?? 0,
                                 ProyectadoNoviembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= 20) ? a.ProyectadoNoviembre : 0) ?? 0,
                                 ProyectadoDiciembre = (DateTime.Now.Month > 11 || (DateTime.Now.Month == 12 && DateTime.Now.Day >= 20) ? a.ProyectadoDiciembre : 0) ?? 0
                             } by new { a.Descripcion, a.Concepto } into g
                             select new Dor_ObjetivosGenerales
                             {
                                 Id = g.First().Id,
                                 UnidadDeNegocio = g.First().UnidadDeNegocio,
                                 Concepto = g.Key.Concepto,
                                 Descripcion = g.Key.Descripcion,
                                 Empleado = g.First().Empleado,
                                 Meta = g.First().Meta,                                 
                                 PromedioReal = g.Average(item => Convert.ToDecimal(item.Real)).ToString(),
                                 PorcentajeEstimado = g.First().PorcentajeEstimado,
                                 RealArea = g.First().RealArea,
                                 Nivel = g.First().Nivel,
                                 Valor = g.First().Valor,
                                 Tooltip = g.First().Tooltip,
                                 Enero = g.First().Enero,
                                 Febrero = g.First().Febrero,
                                 Marzo = g.First().Marzo,
                                 Abril = g.First().Abril,
                                 Mayo = g.First().Mayo,
                                 Junio = g.First().Junio,
                                 Julio = g.First().Julio,
                                 Agosto = g.First().Agosto,
                                 Septiembre = g.First().Septiembre,
                                 Octubre = g.First().Octubre,
                                 Noviembre = g.First().Noviembre,
                                 Diciembre = g.First().Diciembre,
                                 Real = mes == 0 ? (g.First().Enero + g.First().Febrero + g.First().Marzo + g.First().Abril + g.First().Mayo + g.First().Junio + g.First().Julio + g.First().Agosto + g.First().Septiembre + g.First().Octubre + g.First().Noviembre + g.First().Diciembre) / (DateTime.Now.Month > 1 && DateTime.Now.Day >= 20 ? DateTime.Now.Month : DateTime.Now.Month - 1)
                                 : mes == 1 ? g.First().Enero
                                 : mes == 2 ? g.First().Febrero
                                 : mes == 3 ? g.First().Marzo
                                 : mes == 4 ? g.First().Abril
                                 : mes == 5 ? g.First().Mayo
                                 : mes == 6 ? g.First().Junio
                                 : mes == 7 ? g.First().Julio
                                 : mes == 8 ? g.First().Agosto
                                 : mes == 9 ? g.First().Septiembre
                                 : mes == 10 ? g.First().Octubre
                                 : mes == 11 ? g.First().Noviembre
                                 : mes == 12 ? g.First().Diciembre
                                 : 0,
                                 //IngresoEnero = g.First().IngresoEnero,
                                 //IngresoFebrero = g.First().IngresoFebrero,
                                 //IngresoMarzo = g.First().IngresoMarzo,
                                 //IngresoAbril = g.First().IngresoAbril,
                                 //IngresoMayo = g.First().IngresoMayo,
                                 //IngresoJunio = g.First().IngresoJunio,
                                 //IngresoJulio = g.First().IngresoJulio,
                                 //IngresoAgosto = g.First().IngresoAgosto,
                                 //IngresoSeptiembre = g.First().IngresoSeptiembre,
                                 //IngresoOctubre = g.First().IngresoOctubre,
                                 //IngresoNoviembre = g.First().IngresoNoviembre,
                                 //IngresoDiciembre = g.First().IngresoDiciembre,
                                 //IngresoTotal = mes == 0 ? (g.First().IngresoEnero + g.First().IngresoFebrero + g.First().IngresoMarzo + g.First().IngresoAbril + g.First().IngresoMayo + g.First().IngresoJunio + g.First().IngresoJulio + g.First().IngresoAgosto + g.First().IngresoSeptiembre + g.First().IngresoOctubre + g.First().IngresoNoviembre + g.First().IngresoDiciembre) / g.First().MetaValor
                                 //: mes == 1 ? g.First().IngresoEnero / g.First().MetaValor
                                 //: mes == 2 ? g.First().IngresoFebrero / g.First().MetaValor
                                 //: mes == 3 ? g.First().IngresoMarzo / g.First().MetaValor
                                 //: mes == 4 ? g.First().IngresoAbril / g.First().MetaValor
                                 //: mes == 5 ? g.First().IngresoMayo / g.First().MetaValor
                                 //: mes == 6 ? g.First().IngresoJunio / g.First().MetaValor
                                 //: mes == 7 ? g.First().IngresoJulio / g.First().MetaValor
                                 //: mes == 8 ? g.First().IngresoAgosto / g.First().MetaValor
                                 //: mes == 9 ? g.First().IngresoSeptiembre / g.First().MetaValor
                                 //: mes == 10 ? g.First().IngresoOctubre / g.First().MetaValor
                                 //: mes == 11 ? g.First().IngresoNoviembre / g.First().MetaValor
                                 //: mes == 12 ? g.First().IngresoDiciembre / g.First().MetaValor
                                 //: 0,
                                 //GastoEnero = g.First().GastoEnero,
                                 //GastoFebrero = g.First().GastoFebrero,
                                 //GastoMarzo = g.First().GastoMarzo,
                                 //GastoAbril = g.First().GastoAbril,
                                 //GastoMayo = g.First().GastoMayo,
                                 //GastoJunio = g.First().GastoJunio,
                                 //GastoJulio = g.First().GastoJulio,
                                 //GastoAgosto = g.First().GastoAgosto,
                                 //GastoSeptiembre = g.First().GastoSeptiembre,
                                 //GastoOctubre = g.First().GastoOctubre,
                                 //GastoNoviembre = g.First().GastoNoviembre,
                                 //GastoDiciembre = g.First().GastoDiciembre,
                                 //GastoTotal = mes == 0 ? (g.First().GastoEnero + g.First().GastoFebrero + g.First().GastoMarzo + g.First().GastoAbril + g.First().GastoMayo + g.First().GastoJunio + g.First().GastoJulio + g.First().GastoAgosto + g.First().GastoSeptiembre + g.First().GastoOctubre + g.First().GastoNoviembre + g.First().GastoDiciembre) / g.First().MetaValor
                                 //: mes == 1 ? g.First().GastoEnero / g.First().MetaValor
                                 //: mes == 2 ? g.First().GastoFebrero / g.First().MetaValor
                                 //: mes == 3 ? g.First().GastoMarzo / g.First().MetaValor
                                 //: mes == 4 ? g.First().GastoAbril / g.First().MetaValor
                                 //: mes == 5 ? g.First().GastoMayo / g.First().MetaValor
                                 //: mes == 6 ? g.First().GastoJunio / g.First().MetaValor
                                 //: mes == 7 ? g.First().GastoJulio / g.First().MetaValor
                                 //: mes == 8 ? g.First().GastoAgosto / g.First().MetaValor
                                 //: mes == 9 ? g.First().GastoSeptiembre / g.First().MetaValor
                                 //: mes == 10 ? g.First().GastoOctubre / g.First().MetaValor
                                 //: mes == 11 ? g.First().GastoNoviembre / g.First().MetaValor
                                 //: mes == 12 ? g.First().GastoDiciembre / g.First().MetaValor
                                 //: 0,
                                 ProyectadoEnero = g.First().ProyectadoEnero,
                                 ProyectadoFebrero = g.First().ProyectadoFebrero,
                                 ProyectadoMarzo = g.First().ProyectadoMarzo,
                                 ProyectadoAbril = g.First().ProyectadoAbril,
                                 ProyectadoMayo = g.First().ProyectadoMayo,
                                 ProyectadoJunio = g.First().ProyectadoJunio,
                                 ProyectadoJulio = g.First().ProyectadoJulio,
                                 ProyectadoAgosto = g.First().ProyectadoAgosto,
                                 ProyectadoSeptiembre = g.First().ProyectadoSeptiembre,
                                 ProyectadoOctubre = g.First().ProyectadoOctubre,
                                 ProyectadoNoviembre = g.First().ProyectadoNoviembre,
                                 ProyectadoDiciembre = g.First().ProyectadoDiciembre,
                                 ProyectadoTotal = mes == 0 ? (g.First().ProyectadoEnero + g.First().ProyectadoFebrero + g.First().ProyectadoMarzo + g.First().ProyectadoAbril + g.First().ProyectadoMayo + g.First().ProyectadoJunio + g.First().ProyectadoJulio + g.First().ProyectadoAgosto + g.First().ProyectadoSeptiembre + g.First().ProyectadoOctubre + g.First().ProyectadoNoviembre + g.First().ProyectadoDiciembre) / (DateTime.Now.Month > 1 && DateTime.Now.Day >= 20 ? DateTime.Now.Month : DateTime.Now.Month - 1)
                                 : mes == 1 ? g.First().ProyectadoEnero
                                 : mes == 2 ? g.First().ProyectadoFebrero
                                 : mes == 3 ? g.First().ProyectadoMarzo
                                 : mes == 4 ? g.First().ProyectadoAbril
                                 : mes == 5 ? g.First().ProyectadoMayo
                                 : mes == 6 ? g.First().ProyectadoJunio
                                 : mes == 7 ? g.First().ProyectadoJulio
                                 : mes == 8 ? g.First().ProyectadoAgosto
                                 : mes == 9 ? g.First().ProyectadoSeptiembre
                                 : mes == 10 ? g.First().ProyectadoOctubre
                                 : mes == 11 ? g.First().ProyectadoNoviembre
                                 : mes == 12 ? g.First().ProyectadoDiciembre
                                 : 0
                             }).ToListAsync();

                foreach (var r in res)
                {
                    decimal? realArea = await (from a in db.dOR_Meta_Proyectos
                                          where a.Empleado == empleado
                                          select a.Real).FirstOrDefaultAsync();

                    r.RealArea = r.Real = r.Concepto == "AREA" && r.Descripcion == "Planes de trabajo" ? realArea : r.Real;
                    r.PorcentajeReal = r.Real != null && r.Valor != null && r.Meta != null && r.Meta != 0 ? Convert.ToDecimal(r.Real) * Convert.ToDecimal(r.Valor) / Convert.ToDecimal(r.Meta) : 0;
                }

                return res;
            }
        }

        public async Task<List<Dor_ObjetivosEmpleado>> GetDorObjetivosDesepeno(int anio, int proyecto, int empleado, int nivel, int? acepto, int mes)
        {
            List<Dor_ObjetivosEmpleado> res = null;

            using (var db = new ConnectionDB(dbConfig))
            {
                //res = await (from a in db.dOR_ObjetivosDesepenos
                //             join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto } equals new { b.UnidadDeNegocio, b.Concepto } into bJoin
                //             from bItem in bJoin.DefaultIfEmpty()
                //             join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion } into cJoin
                //             from cItem in cJoin.DefaultIfEmpty()
                //             where a.Anio == anio
                //             && a.Proyecto == proyecto
                //             && a.Empleado == empleado
                //             && bItem.Nivel == nivel
                //             orderby a.Descripcion ascending
                //             group new Dor_ObjetivosEmpleado
                //             {
                //                 IdEmpOb = a.IdEmpOb,
                //                 UnidadDeNegocio = a.UnidadDeNegocio,
                //                 Concepto = a.Concepto,
                //                 Descripcion = a.Descripcion,
                //                 Meta = a.Meta,
                //                 Real = a.Real ?? 0,
                //                 Acepto = a.Acepto,
                //                 MotivoR = a.MotivoR,
                //                 FechaCarga = a.FechaCarga,
                //                 FechaAceptado = a.FechaAceptado,
                //                 FechaRechazo = a.FechaRechazo,
                //                 Nivel = bItem.Nivel ?? 0,
                //                 Valor = bItem.Valor ?? 0,
                //                 Tooltip = cItem.Tooltip ?? string.Empty
                //             } by new { a.Concepto, a.Descripcion } into g
                //             select new Dor_ObjetivosEmpleado
                //             {
                //                 IdEmpOb = g.First().IdEmpOb,
                //                 UnidadDeNegocio = g.First().UnidadDeNegocio,
                //                 Concepto = g.Key.Concepto,
                //                 Descripcion = g.Key.Descripcion,
                //                 Meta = g.First().Meta,
                //                 Real = g.First().Real,
                //                 PorcentajeEstimado = g.Max(item => item.Valor),
                //                 PorcentajeReal = g.First().Real * g.Max(item => item.Valor) / g.First().Meta,
                //                 Acepto = g.First().Acepto,
                //                 MotivoR = g.First().MotivoR,
                //                 FechaCarga = g.First().FechaCarga,
                //                 FechaAceptado = g.First().FechaAceptado,
                //                 FechaRechazo = g.First().FechaRechazo,
                //                 Nivel = g.Max(item => item.Nivel),
                //                 Valor = g.Max(item => item.Valor),
                //                 Tooltip = g.First().Tooltip
                //             }).ToListAsync();

                res = await (from a in db.dOR_ObjetivosDesepenos
                             where a.Anio == anio
                             && a.Proyecto == proyecto
                             && a.Empleado == empleado
                             && (acepto == 0 || a.Acepto == acepto)
                             orderby a.Descripcion ascending
                             group new Dor_ObjetivosEmpleado
                             {
                                 IdEmpOb = a.IdEmpOb,
                                 UnidadDeNegocio = a.UnidadDeNegocio,
                                 Concepto = a.Concepto,
                                 Descripcion = a.Descripcion,
                                 Meta = a.Meta,
                                 Real = a.Real,
                                 Acepto = a.Acepto,
                                 MotivoR = a.MotivoR,
                                 FechaCarga = a.FechaCarga,
                                 FechaAceptado = a.FechaAceptado,
                                 FechaRechazo = a.FechaRechazo,
                                 Valor = a.Valor
                             } by new { a.IdEmpOb, a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Meta, a.Valor, a.Real, a.Acepto, a.MotivoR, a.FechaCarga, a.FechaAceptado, a.FechaRechazo } into g
                             select new Dor_ObjetivosEmpleado
                             {
                                 IdEmpOb = g.Key.IdEmpOb,
                                 UnidadDeNegocio = g.Key.UnidadDeNegocio,
                                 Concepto = g.Key.Concepto,
                                 Descripcion = g.Key.Descripcion,
                                 Meta = g.Key.Meta ?? 0,
                                 Real = g.Key.Real ?? 0,
                                 PorcentajeEstimado = g.Key.Valor ?? 0,
                                 PorcentajeReal = (g.Key.Real * Convert.ToDecimal(g.Key.Valor) / g.Key.Meta) ?? 0,
                                 Acepto = g.Key.Acepto,
                                 MotivoR = g.Key.MotivoR,
                                 FechaCarga = g.Key.FechaCarga,
                                 FechaAceptado = g.Key.FechaAceptado,
                                 FechaRechazo = g.Key.FechaRechazo,
                                 Nivel = nivel,
                                 Valor = g.Key.Valor ?? 0
                             }).ToListAsync();
            }

            return res;
        }


        public async Task<List<DOR_ObjetivosDesepeno>> GetDorObjetivosDesepeno(int anio, int proyecto, string concepto, int? empleado)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var query = await (from cat in db.dOR_ObjetivosDesepenos
                             where cat.Anio == anio
                             && cat.Proyecto == proyecto
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
                                        Valor = objetivo.Valor,
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
                                .Value(x => x.Valor, objetivo.Valor)
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

        public async Task<(bool Success, string Message)> UpdateReal(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id = Convert.ToInt32(registro["id"].ToString());
            int id_empleado = Convert.ToInt32(registro["id_empleado"].ToString());
            decimal real = Convert.ToDecimal(registro["real"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_update_real = await db.dOR_Meta_Proyectos.Where(x => x.Empleado == id_empleado)
                    .UpdateAsync(x => new DOR_Meta_Proyecto
                    {
                        Real = real
                    }) > 0;

                resp.Success = res_update_real;
                resp.Message = res_update_real == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }
            return resp;
        }
    }
}
