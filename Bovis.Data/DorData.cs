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

        private readonly int dia_corte = 20;

        public DorData()
        {
            this.ConfigurationDB = dbConfig;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }

        public async Task<TB_Dor_Empleados?> GetDorEjecutivoCorreo(string email)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                return await (from cat in db.tB_Dor_Empleados
                              where cat.CorreoElec == email
                              select cat).FirstOrDefaultAsync();
            }
        }
        public async Task<Dor_Subordinados> GetDorEmpleadoCorreo(string email)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from a in db.tB_Dor_Empleados
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
                var res = await (from a in db.tB_Dor_Empleados
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
        public async Task<List<Dor_ObjetivosGenerales>> GetDorObjetivosGenerales(int nivel, string unidadNegocio, int mes, int anio, string seccion)
        {
            List<Dor_ObjetivosGenerales> res = null;

            int mes_para_promedio = DateTime.Now.Month <= 2 ? 1
                : DateTime.Now.Month > 2 && DateTime.Now.Day >= dia_corte ? DateTime.Now.Month - 1
                : DateTime.Now.Month - 2;

            using (var db = new ConnectionDB(dbConfig))
            {
                res = await (from a in db.tB_Dor_Objetivos_Gral
                             join b in db.tB_Dor_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion } into bJoin
                             from bItem in bJoin.DefaultIfEmpty()
                             join c in db.tB_Dor_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion } into cJoin
                             from cItem in cJoin.DefaultIfEmpty()
                             join d in db.tB_Dor_Real_Gasto_Ingreso_Proyecto_Gpms on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Año } into dJoin
                             from dItem in dJoin.DefaultIfEmpty()
                             where a.UnidadDeNegocio == unidadNegocio
                             && bItem.Nivel == nivel
                             && a.Año == anio
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
                                 Real = a.Real,
                                 Enero = (anio < DateTime.Now.Year ? a.Enero : DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= dia_corte) ? a.Enero : 0) ?? 0,
                                 Febrero = (anio < DateTime.Now.Year ? a.Febrero : DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= dia_corte) ? a.Febrero : 0) ?? 0,
                                 Marzo = (anio < DateTime.Now.Year ? a.Marzo : DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= dia_corte) ? a.Marzo : 0) ?? 0,
                                 Abril = (anio < DateTime.Now.Year ? a.Abril : DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= dia_corte) ? a.Abril : 0) ?? 0,
                                 Mayo = (anio < DateTime.Now.Year ? a.Mayo : DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= dia_corte) ? a.Mayo : 0) ?? 0,
                                 Junio = (anio < DateTime.Now.Year ? a.Junio : DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= dia_corte) ? a.Junio : 0) ?? 0,
                                 Julio = (anio < DateTime.Now.Year ? a.Julio : DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= dia_corte) ? a.Julio : 0) ?? 0,
                                 Agosto = (anio < DateTime.Now.Year ? a.Agosto : DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= dia_corte) ? a.Agosto : 0) ?? 0,
                                 Septiembre = (anio < DateTime.Now.Year ? a.Septiembre : DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= dia_corte) ? a.Septiembre : 0) ?? 0,
                                 Octubre = (anio < DateTime.Now.Year ? a.Octubre : DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= dia_corte) ? a.Octubre : 0) ?? 0,
                                 Noviembre = (anio < DateTime.Now.Year ? a.Noviembre : (DateTime.Now.Month == 12 && DateTime.Now.Day >= dia_corte) ? a.Noviembre : 0) ?? 0,
                                 Diciembre = (anio < DateTime.Now.Year ? a.Diciembre : 0) ?? 0,
                                 IngresoEnero = (anio < DateTime.Now.Year ? dItem.InEnero : DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= dia_corte) ? dItem.InEnero : 0) ?? 0,
                                 IngresoFebrero = (anio < DateTime.Now.Year ? dItem.InFebrero : DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= dia_corte) ? dItem.InFebrero : 0) ?? 0,
                                 IngresoMarzo = (anio < DateTime.Now.Year ? dItem.InMarzo : DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= dia_corte) ? dItem.InMarzo : 0) ?? 0,
                                 IngresoAbril = (anio < DateTime.Now.Year ? dItem.InAbril : DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= dia_corte) ? dItem.InAbril : 0) ?? 0,
                                 IngresoMayo = (anio < DateTime.Now.Year ? dItem.InMayo : DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= dia_corte) ? dItem.InMayo : 0) ?? 0,
                                 IngresoJunio = (anio < DateTime.Now.Year ? dItem.InJunio : DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= dia_corte) ? dItem.InJunio : 0) ?? 0,
                                 IngresoJulio = (anio < DateTime.Now.Year ? dItem.InJulio : DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= dia_corte) ? dItem.InJulio : 0) ?? 0,
                                 IngresoAgosto = (anio < DateTime.Now.Year ? dItem.InAgosto : DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= dia_corte) ? dItem.InAgosto : 0) ?? 0,
                                 IngresoSeptiembre = (anio < DateTime.Now.Year ? dItem.InSeptiembre : DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= dia_corte) ? dItem.InSeptiembre : 0) ?? 0,
                                 IngresoOctubre = (anio < DateTime.Now.Year ? dItem.InOctubre : DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= dia_corte) ? dItem.InOctubre : 0) ?? 0,
                                 IngresoNoviembre = (anio < DateTime.Now.Year ? dItem.InNoviembre : (DateTime.Now.Month == 12 && DateTime.Now.Day >= dia_corte) ? dItem.InNoviembre : 0) ?? 0,
                                 IngresoDiciembre = (anio < DateTime.Now.Year ? dItem.InDiciembre : 0) ?? 0,
                                 GastoEnero = (anio < DateTime.Now.Year ? dItem.OutEnero : DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= dia_corte) ? dItem.OutEnero : 0) ?? 0,
                                 GastoFebrero = (anio < DateTime.Now.Year ? dItem.OutFebrero : DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= dia_corte) ? dItem.OutFebrero : 0) ?? 0,
                                 GastoMarzo = (anio < DateTime.Now.Year ? dItem.OutMarzo : DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= dia_corte) ? dItem.OutMarzo : 0) ?? 0,
                                 GastoAbril = (anio < DateTime.Now.Year ? dItem.OutAbril : DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= dia_corte) ? dItem.OutAbril : 0) ?? 0,
                                 GastoMayo = (anio < DateTime.Now.Year ? dItem.OutMayo : DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= dia_corte) ? dItem.OutMayo : 0) ?? 0,
                                 GastoJunio = (anio < DateTime.Now.Year ? dItem.OutJunio : DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= dia_corte) ? dItem.OutJunio : 0) ?? 0,
                                 GastoJulio = (anio < DateTime.Now.Year ? dItem.OutJulio : DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= dia_corte) ? dItem.OutJulio : 0) ?? 0,
                                 GastoAgosto = (anio < DateTime.Now.Year ? dItem.OutAgosto : DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= dia_corte) ? dItem.OutAgosto : 0) ?? 0,
                                 GastoSeptiembre = (anio < DateTime.Now.Year ? dItem.OutSeptiembre : DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= dia_corte) ? dItem.OutSeptiembre : 0) ?? 0,
                                 GastoOctubre = (anio < DateTime.Now.Year ? dItem.OutOctubre : DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= dia_corte) ? dItem.OutOctubre : 0) ?? 0,
                                 GastoNoviembre = (anio < DateTime.Now.Year ? dItem.OutNoviembre : (DateTime.Now.Month == 12 && DateTime.Now.Day >= dia_corte) ? dItem.OutNoviembre : 0) ?? 0,
                                 GastoDiciembre = (anio < DateTime.Now.Year ? dItem.OutDiciembre : 0) ?? 0
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
                                 Real = g.Key.Descripcion == "Seguridad" && mes == 0 ? g.First().Real
                                 : mes == 0 ? (g.First().Enero + g.First().Febrero + g.First().Marzo + g.First().Abril + g.First().Mayo + g.First().Junio + g.First().Julio + g.First().Agosto + g.First().Septiembre + g.First().Octubre + g.First().Noviembre + g.First().Diciembre) / mes_para_promedio
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

                foreach (var r in res)
                {
                    r.PorcentajeReal = r.Real != null && r.Valor != null && r.Meta != null && r.Meta != 0 ? Convert.ToDecimal(r.Real) * Convert.ToDecimal(r.Valor) / Convert.ToDecimal(r.Meta) : 0;
                }

                return res;
            }
        }

        public async Task<List<Dor_ObjetivosGenerales>> GetDorGpmProyecto(int proyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var res = await (from a in db.tB_Dor_Gpm_Proyecto
                                 join c in db.tB_Dor_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion }
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
        public async Task<List<Dor_ObjetivosGenerales>> GetDorMetasProyecto(int proyecto, int nivel, int mes, int anio, int empleado, string seccion)
        {
            List<Dor_ObjetivosGenerales> res = null;

            int mes_para_promedio = DateTime.Now.Month <= 2 ? 1
                : DateTime.Now.Month > 2 && DateTime.Now.Day >= dia_corte ? DateTime.Now.Month - 1
                : DateTime.Now.Month - 2;

            using (var db = new ConnectionDB(dbConfig))
            {
                res = await (from a in db.tB_Dor_Meta_Proyectos
                             join b in db.tB_Dor_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion } into bJoin
                             from bItem in bJoin.DefaultIfEmpty()
                             join c in db.tB_Dor_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion } into cJoin
                             from cItem in cJoin.DefaultIfEmpty()
                             join d in db.tB_Dor_Real_Gasto_Ingreso_Proyecto_Gpms on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Año } into dJoin
                             from dItem in dJoin.DefaultIfEmpty()
                             where a.NoProyecto == proyecto
                             && bItem.Nivel == nivel
                             && a.Año == anio
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
                                 Nivel = bItem.Nivel ?? 0,
                                 Valor = bItem.Valor ?? 0,
                                 Tooltip = cItem.Tooltip ?? string.Empty,
                                 Real = a.Real,
                                 Enero = (anio < DateTime.Now.Year ? a.Enero : DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= dia_corte) ? a.Enero : 0) ?? 0,
                                 Febrero = (anio < DateTime.Now.Year ? a.Febrero : DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= dia_corte) ? a.Febrero : 0) ?? 0,
                                 Marzo = (anio < DateTime.Now.Year ? a.Marzo : DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= dia_corte) ? a.Marzo : 0) ?? 0,
                                 Abril = (anio < DateTime.Now.Year ? a.Abril : DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= dia_corte) ? a.Abril : 0) ?? 0,
                                 Mayo = (anio < DateTime.Now.Year ? a.Mayo : DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= dia_corte) ? a.Mayo : 0) ?? 0,
                                 Junio = (anio < DateTime.Now.Year ? a.Junio : DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= dia_corte) ? a.Junio : 0) ?? 0,
                                 Julio = (anio < DateTime.Now.Year ? a.Julio : DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= dia_corte) ? a.Julio : 0) ?? 0,
                                 Agosto = (anio < DateTime.Now.Year ? a.Agosto : DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= dia_corte) ? a.Agosto : 0) ?? 0,
                                 Septiembre = (anio < DateTime.Now.Year ? a.Septiembre : DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= dia_corte) ? a.Septiembre : 0) ?? 0,
                                 Octubre = (anio < DateTime.Now.Year ? a.Octubre : DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= dia_corte) ? a.Octubre : 0) ?? 0,
                                 Noviembre = (anio < DateTime.Now.Year ? a.Noviembre : (DateTime.Now.Month == 12 && DateTime.Now.Day >= dia_corte) ? a.Noviembre : 0) ?? 0,
                                 Diciembre = (anio < DateTime.Now.Year ? a.Diciembre : 0) ?? 0,
                                 ProyectadoEnero = (anio < DateTime.Now.Year ? a.ProyectadoEnero : DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= dia_corte) ? a.ProyectadoEnero : 0) ?? 0,
                                 ProyectadoFebrero = (anio < DateTime.Now.Year ? a.ProyectadoFebrero : DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= dia_corte) ? a.ProyectadoFebrero : 0) ?? 0,
                                 ProyectadoMarzo = (anio < DateTime.Now.Year ? a.ProyectadoMarzo : DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= dia_corte) ? a.ProyectadoMarzo : 0) ?? 0,
                                 ProyectadoAbril = (anio < DateTime.Now.Year ? a.ProyectadoAbril : DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= dia_corte) ? a.ProyectadoAbril : 0) ?? 0,
                                 ProyectadoMayo = (anio < DateTime.Now.Year ? a.ProyectadoMayo : DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= dia_corte) ? a.ProyectadoMayo : 0) ?? 0,
                                 ProyectadoJunio = (anio < DateTime.Now.Year ? a.ProyectadoJunio : DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= dia_corte) ? a.ProyectadoJunio : 0) ?? 0,
                                 ProyectadoJulio = (anio < DateTime.Now.Year ? a.ProyectadoJulio : DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= dia_corte) ? a.ProyectadoJulio : 0) ?? 0,
                                 ProyectadoAgosto = (anio < DateTime.Now.Year ? a.ProyectadoAgosto : DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= dia_corte) ? a.ProyectadoAgosto : 0) ?? 0,
                                 ProyectadoSeptiembre = (anio < DateTime.Now.Year ? a.ProyectadoSeptiembre : DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= dia_corte) ? a.ProyectadoSeptiembre : 0) ?? 0,
                                 ProyectadoOctubre = (anio < DateTime.Now.Year ? a.ProyectadoOctubre : DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= dia_corte) ? a.ProyectadoOctubre : 0) ?? 0,
                                 ProyectadoNoviembre = (anio < DateTime.Now.Year ? a.ProyectadoNoviembre : (DateTime.Now.Month == 12 && DateTime.Now.Day >= dia_corte) ? a.ProyectadoNoviembre : 0) ?? 0,
                                 ProyectadoDiciembre = (anio < DateTime.Now.Year ? a.ProyectadoDiciembre : 0) ?? 0
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
                                 Real = g.Key.Descripcion == "Seguridad" && mes == 0 ? g.First().Real
                                 : mes == 0 ? (g.First().Enero + g.First().Febrero + g.First().Marzo + g.First().Abril + g.First().Mayo + g.First().Junio + g.First().Julio + g.First().Agosto + g.First().Septiembre + g.First().Octubre + g.First().Noviembre + g.First().Diciembre) / mes_para_promedio
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
                                 ProyectadoTotal = mes == 0 ? (g.First().ProyectadoEnero + g.First().ProyectadoFebrero + g.First().ProyectadoMarzo + g.First().ProyectadoAbril + g.First().ProyectadoMayo + g.First().ProyectadoJunio + g.First().ProyectadoJulio + g.First().ProyectadoAgosto + g.First().ProyectadoSeptiembre + g.First().ProyectadoOctubre + g.First().ProyectadoNoviembre + g.First().ProyectadoDiciembre) / mes_para_promedio
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
                                 : 0,
                                 MetaMensual = g.First().Meta,
                             }).ToListAsync();

                decimal? real, ingresos, gastos, IngresoEnero, IngresoFebrero, IngresoMarzo, IngresoAbril, IngresoMayo, IngresoJunio, IngresoJulio, IngresoAgosto, IngresoSeptiembre,
                    IngresoOctubre, IngresoNoviembre, IngresoDiciembre, GastoEnero, GastoFebrero, GastoMarzo, GastoAbril, GastoMayo, GastoJunio, GastoJulio, GastoAgosto, GastoSeptiembre,
                    GastoOctubre, GastoNoviembre, GastoDiciembre;

                foreach (var r in res)
                {
                    real = 0;
                    if ((r.Descripcion == "GASTO" || r.Descripcion == "GPM PROYECTO") && mes == 0)
                    {
                        ingresos = 0;
                        gastos = 0;

                        var res_meta_mensual = await (from a in db.tB_Dor_Real_Gasto_Ingreso_Proyecto_Gpms
                                                      where a.Año == anio
                                                      && a.NoProyecto == proyecto
                                                      && a.UnidadDeNegocio == r.UnidadDeNegocio
                                                      && a.Concepto == r.Concepto
                                                      && a.Descripcion == r.Descripcion
                                                      select a).ToListAsync();

                        foreach (var m in res_meta_mensual)
                        {
                            IngresoEnero = (anio < DateTime.Now.Year ? m.InEnero : DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= dia_corte) ? m.InEnero : 0) ?? 0;
                            IngresoFebrero = (anio < DateTime.Now.Year ? m.InFebrero : DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= dia_corte) ? m.InFebrero : 0) ?? 0;
                            IngresoMarzo = (anio < DateTime.Now.Year ? m.InMarzo : DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= dia_corte) ? m.InMarzo : 0) ?? 0;
                            IngresoAbril = (anio < DateTime.Now.Year ? m.InAbril : DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= dia_corte) ? m.InAbril : 0) ?? 0;
                            IngresoMayo = (anio < DateTime.Now.Year ? m.InMayo : DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= dia_corte) ? m.InMayo : 0) ?? 0;
                            IngresoJunio = (anio < DateTime.Now.Year ? m.InJunio : DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= dia_corte) ? m.InJunio : 0) ?? 0;
                            IngresoJulio = (anio < DateTime.Now.Year ? m.InJulio : DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= dia_corte) ? m.InJulio : 0) ?? 0;
                            IngresoAgosto = (anio < DateTime.Now.Year ? m.InAgosto : DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= dia_corte) ? m.InAgosto : 0) ?? 0;
                            IngresoSeptiembre = (anio < DateTime.Now.Year ? m.InSeptiembre : DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= dia_corte) ? m.InSeptiembre : 0) ?? 0;
                            IngresoOctubre = (anio < DateTime.Now.Year ? m.InOctubre : DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= dia_corte) ? m.InOctubre : 0) ?? 0;
                            IngresoNoviembre = (anio < DateTime.Now.Year ? m.InNoviembre : (DateTime.Now.Month == 12 && DateTime.Now.Day >= dia_corte) ? m.InNoviembre : 0) ?? 0;
                            IngresoDiciembre = (anio < DateTime.Now.Year ? m.InDiciembre : 0) ?? 0;
                            GastoEnero = (anio < DateTime.Now.Year ? m.OutEnero : DateTime.Now.Month > 2 || (DateTime.Now.Month == 2 && DateTime.Now.Day >= dia_corte) ? m.OutEnero : 0) ?? 0;
                            GastoFebrero = (anio < DateTime.Now.Year ? m.OutFebrero : DateTime.Now.Month > 3 || (DateTime.Now.Month == 3 && DateTime.Now.Day >= dia_corte) ? m.OutFebrero : 0) ?? 0;
                            GastoMarzo = (anio < DateTime.Now.Year ? m.OutMarzo : DateTime.Now.Month > 4 || (DateTime.Now.Month == 4 && DateTime.Now.Day >= dia_corte) ? m.OutMarzo : 0) ?? 0;
                            GastoAbril = (anio < DateTime.Now.Year ? m.OutAbril : DateTime.Now.Month > 5 || (DateTime.Now.Month == 5 && DateTime.Now.Day >= dia_corte) ? m.OutAbril : 0) ?? 0;
                            GastoMayo = (anio < DateTime.Now.Year ? m.OutMayo : DateTime.Now.Month > 6 || (DateTime.Now.Month == 6 && DateTime.Now.Day >= dia_corte) ? m.OutMayo : 0) ?? 0;
                            GastoJunio = (anio < DateTime.Now.Year ? m.OutJunio : DateTime.Now.Month > 7 || (DateTime.Now.Month == 7 && DateTime.Now.Day >= dia_corte) ? m.OutJunio : 0) ?? 0;
                            GastoJulio = (anio < DateTime.Now.Year ? m.OutJulio : DateTime.Now.Month > 8 || (DateTime.Now.Month == 8 && DateTime.Now.Day >= dia_corte) ? m.OutJulio : 0) ?? 0;
                            GastoAgosto = (anio < DateTime.Now.Year ? m.OutAgosto : DateTime.Now.Month > 9 || (DateTime.Now.Month == 9 && DateTime.Now.Day >= dia_corte) ? m.OutAgosto : 0) ?? 0;
                            GastoSeptiembre = (anio < DateTime.Now.Year ? m.OutSeptiembre : DateTime.Now.Month > 10 || (DateTime.Now.Month == 10 && DateTime.Now.Day >= dia_corte) ? m.OutSeptiembre : 0) ?? 0;
                            GastoOctubre = (anio < DateTime.Now.Year ? m.OutOctubre : DateTime.Now.Month > 11 || (DateTime.Now.Month == 11 && DateTime.Now.Day >= dia_corte) ? m.OutOctubre : 0) ?? 0;
                            GastoNoviembre = (anio < DateTime.Now.Year ? m.OutNoviembre : (DateTime.Now.Month == 12 && DateTime.Now.Day >= dia_corte) ? m.OutNoviembre : 0) ?? 0;
                            GastoDiciembre = (anio < DateTime.Now.Year ? m.OutDiciembre : 0) ?? 0;

                            ingresos += IngresoEnero + IngresoFebrero + IngresoMarzo + IngresoAbril + IngresoMayo + IngresoJunio + IngresoJulio + IngresoAgosto + IngresoSeptiembre + IngresoOctubre + IngresoNoviembre + IngresoDiciembre;
                            gastos += GastoEnero + GastoFebrero + GastoMarzo + GastoAbril + GastoMayo + GastoJunio + GastoJulio + GastoAgosto + GastoSeptiembre + GastoOctubre + GastoNoviembre + GastoDiciembre;
                        }

                        if (ingresos != 0)
                        {
                            real = r.Descripcion == "GASTO" ? (gastos / ingresos) * 100
                                    : ((ingresos - gastos) / ingresos) * 100;
                        }
                    }
                    else if (r.Concepto == "AREA" && r.Descripcion == "Planes de trabajo")
                    {
                        real = await (from a in db.tB_Dor_Meta_Proyectos
                                      where a.Empleado == empleado
                                      select a.Real).FirstOrDefaultAsync();
                    }
                    else
                    {
                        real = r.Real;
                    }                    

                    r.Real = real;
                    r.PorcentajeReal = r.Real != null && r.Valor != null && r.Meta != null && r.Meta != 0 ? Convert.ToDecimal(r.Real) * Convert.ToDecimal(r.Valor) / Convert.ToDecimal(r.Meta) : 0;
                }
            }

            return res;
        }    

        public async Task<List<Dor_ObjetivosEmpleado>> GetDorObjetivosDesepeno(int anio, int proyecto, int empleado, int nivel, int? acepto, int mes)
        {
            List<Dor_ObjetivosEmpleado> res = null;

            using (var db = new ConnectionDB(dbConfig))
            {
                res = await (from a in db.tB_Dor_Objetivos_Desepenos
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
                                 Valor = a.Valor,
                                 EsPersonal = a.EsPersonal
                             } by new { a.IdEmpOb, a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Meta, a.Valor, a.Real, a.Acepto, a.MotivoR, a.FechaCarga, a.FechaAceptado, a.FechaRechazo, a.EsPersonal } into g
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
                                 Valor = g.Key.Valor ?? 0,
                                 EsPersonal = g.Key.EsPersonal
                             }).ToListAsync();
            }

            return res;
        }


        public async Task<List<TB_Dor_Objetivos_Desepeno>> GetDorObjetivosDesepeno(int anio, int proyecto, string concepto, int? empleado)
        {
            using (var db = new ConnectionDB(dbConfig))
            {
                var query = await (from cat in db.tB_Dor_Objetivos_Desepenos
                             where cat.Anio == anio
                             && cat.Proyecto == proyecto
                             && cat.Concepto == concepto
                             //&& cat.Empleado == (empleado.HasValue ? empleado.ToString() : default(string))
                             //|| cat.Empleado == null
                             select cat).ToListAsync();

                return query;
            }
        }


        public async Task<(bool Success, string Message)> UpdObjetivo(TB_Dor_Objetivos_Desepeno objetivo)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                var objetivoDB = await db.tB_Dor_Objetivos_Desepenos.Where(x => x.IdEmpOb == objetivo.IdEmpOb)
                                    .UpdateAsync(x => new TB_Dor_Objetivos_Desepeno
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
        public async Task<(bool Success, string Message)> AddObjetivo(TB_Dor_Objetivos_Desepeno objetivo)
        {
            (bool Success, string Message) resp = (true, string.Empty);
            using (var db = new ConnectionDB(dbConfig))
            {
                //var objetivoDB = await db.dOR_ObjetivosDesepenos.Where(x => x.Anio == objetivo.Anio && x.Empleado == objetivo.Empleado && x.Proyecto == objetivo.Proyecto && x.Descripcion == objetivo.Descripcion).FirstOrDefaultAsync();
                //if(objetivoDB is  null)
                //{
                var inseert = await db.tB_Dor_Objetivos_Desepenos
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
                var res_update_real = await db.tB_Dor_Meta_Proyectos.Where(x => x.Empleado == id_empleado)
                    .UpdateAsync(x => new TB_Dor_Meta_Proyecto
                    {
                        Real = real
                    }) > 0;

                resp.Success = res_update_real;
                resp.Message = res_update_real == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }

            return resp;
        }
        
        public async Task<(bool Success, string Message)> UpdateObjetivoPersonal(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id = Convert.ToInt32(registro["id"].ToString());
            decimal real = Convert.ToDecimal(registro["real"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_update_real = await db.tB_Dor_Objetivos_Desepenos.Where(x => x.IdEmpOb == id)
                    .UpdateAsync(x => new TB_Dor_Objetivos_Desepeno
                    {
                        Real = real
                    }) > 0;

                resp.Success = res_update_real;
                resp.Message = res_update_real == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }
            return resp;
        }
        
        public async Task<(bool Success, string Message)> UpdateAcepto(JsonObject registro)
        {
            (bool Success, string Message) resp = (true, string.Empty);

            int id = Convert.ToInt32(registro["id"].ToString());
            int acepto = Convert.ToInt32(registro["acepto"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_update_real = await db.tB_Dor_Objetivos_Desepenos.Where(x => x.IdEmpOb == id)
                    .UpdateAsync(x => new TB_Dor_Objetivos_Desepeno
                    {
                        Acepto = acepto
                    }) > 0;

                resp.Success = res_update_real;
                resp.Message = res_update_real == default ? "Ocurrio un error al actualizar registro." : string.Empty;
            }
            return resp;
        }
    }
}
