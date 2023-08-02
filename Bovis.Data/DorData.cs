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

            using (var db = new ConnectionDB(dbConfig))
            {
                res = await (from a in db.dOR_Objetivos_Gral
                             join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                             join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion } into cJoin
                             from cItem in cJoin.DefaultIfEmpty()
                             join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Año } into dJoin
                             from dItem in dJoin.DefaultIfEmpty()
                             where a.UnidadDeNegocio == unidadNegocio
                             && b.Nivel == nivel
                             orderby a.Descripcion ascending
                             group new Dor_ObjetivosGenerales
                             {
                                 Id = a.Id,
                                 UnidadDeNegocio = a.UnidadDeNegocio,
                                 Concepto = a.Concepto,
                                 Descripcion = a.Descripcion,
                                 Meta = a.Meta,
                                 MetaValor = a.MetaValor,
                                 PorcentajeEstimado = b.Valor,
                                 Nivel = b.Nivel,
                                 Valor = b.Valor,
                                 Tooltip = cItem.Tooltip ?? string.Empty,
                                 Enero = a.Ene ?? 0,
                                 Febrero = a.Feb ?? 0,
                                 Marzo = a.Mar ?? 0,
                                 Abril = a.Abr ?? 0,
                                 Mayo = a.May ?? 0,
                                 Junio = a.Jun ?? 0,
                                 Julio = a.Jul ?? 0,
                                 Agosto = a.Ago ?? 0,
                                 Septiembre = a.Sep ?? 0,
                                 Octubre = a.Oct ?? 0,
                                 Noviembre = a.Nov ?? 0,
                                 Diciembre = a.Dic ?? 0,
                                 IngresoEnero = dItem.InEnero ?? 0,
                                 IngresoFebrero = dItem.InFebrero ?? 0,
                                 IngresoMarzo = dItem.InMarzo ?? 0,
                                 IngresoAbril = dItem.InAbril ?? 0,
                                 IngresoMayo = dItem.InMayo ?? 0,
                                 IngresoJunio = dItem.InJunio ?? 0,
                                 IngresoJulio = dItem.InJulio ?? 0,
                                 IngresoAgosto = dItem.InAgosto ?? 0,
                                 IngresoSeptiembre = dItem.InSeptiembre ?? 0,
                                 IngresoOctubre = dItem.InOctubre ?? 0,
                                 IngresoNoviembre = dItem.InNoviembre ?? 0,
                                 IngresoDiciembre = dItem.InDiciembre ?? 0,
                                 GastoEnero = dItem.OutEnero ?? 0,
                                 GastoFebrero = dItem.OutFebrero ?? 0,
                                 GastoMarzo = dItem.OutMarzo ?? 0,
                                 GastoAbril = dItem.OutAbril ?? 0,
                                 GastoMayo = dItem.OutMayo ?? 0,
                                 GastoJunio = dItem.OutJunio ?? 0,
                                 GastoJulio = dItem.OutJulio ?? 0,
                                 GastoAgosto = dItem.OutAgosto ?? 0,
                                 GastoSeptiembre = dItem.OutSeptiembre ?? 0,
                                 GastoOctubre = dItem.OutOctubre ?? 0,
                                 GastoNoviembre = dItem.OutNoviembre ?? 0,
                                 GastoDiciembre = dItem.OutDiciembre ?? 0
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
                                 Real = mes == 0
                                     ? g.Sum(item => Convert.ToDecimal(item.Enero.GetValueOrDefault()) + Convert.ToDecimal(item.Febrero.GetValueOrDefault()) + Convert.ToDecimal(item.Marzo.GetValueOrDefault()) + Convert.ToDecimal(item.Abril.GetValueOrDefault()) + Convert.ToDecimal(item.Mayo.GetValueOrDefault()) + Convert.ToDecimal(item.Junio.GetValueOrDefault()) + Convert.ToDecimal(item.Julio.GetValueOrDefault()) + Convert.ToDecimal(item.Agosto.GetValueOrDefault()) + Convert.ToDecimal(item.Septiembre.GetValueOrDefault()) + Convert.ToDecimal(item.Octubre.GetValueOrDefault()) + Convert.ToDecimal(item.Noviembre.GetValueOrDefault()) + Convert.ToDecimal(item.Diciembre.GetValueOrDefault())) / (DateTime.Now.Month - 1)
                                     : mes == 1 ? g.Sum(item => Convert.ToDecimal(item.Enero.GetValueOrDefault()))
                                     : mes == 2 ? g.Sum(item => Convert.ToDecimal(item.Febrero.GetValueOrDefault()))
                                     : mes == 3 ? g.Sum(item => Convert.ToDecimal(item.Marzo.GetValueOrDefault()))
                                     : mes == 4 ? g.Sum(item => Convert.ToDecimal(item.Abril.GetValueOrDefault()))
                                     : mes == 5 ? g.Sum(item => Convert.ToDecimal(item.Mayo.GetValueOrDefault()))
                                     : mes == 6 ? g.Sum(item => Convert.ToDecimal(item.Junio.GetValueOrDefault()))
                                     : mes == 7 ? g.Sum(item => Convert.ToDecimal(item.Julio.GetValueOrDefault()))
                                     : mes == 8 ? g.Sum(item => Convert.ToDecimal(item.Agosto.GetValueOrDefault()))
                                     : mes == 9 ? g.Sum(item => Convert.ToDecimal(item.Septiembre.GetValueOrDefault()))
                                     : mes == 10 ? g.Sum(item => Convert.ToDecimal(item.Octubre.GetValueOrDefault()))
                                     : mes == 11 ? g.Sum(item => Convert.ToDecimal(item.Noviembre.GetValueOrDefault()))
                                     : mes == 12 ? g.Sum(item => Convert.ToDecimal(item.Diciembre.GetValueOrDefault()))
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
                                 IngresoTotal = mes == 0
                                    ? g.Sum(item => Convert.ToDecimal(item.IngresoEnero.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoFebrero.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoMarzo.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoAbril.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoMayo.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoJunio.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoJulio.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoAgosto.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoSeptiembre.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoOctubre.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoNoviembre.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoDiciembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 1 ? g.Sum(item => Convert.ToDecimal(item.IngresoEnero.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 2 ? g.Sum(item => Convert.ToDecimal(item.IngresoFebrero.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 3 ? g.Sum(item => Convert.ToDecimal(item.IngresoMarzo.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 4 ? g.Sum(item => Convert.ToDecimal(item.IngresoAbril.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 5 ? g.Sum(item => Convert.ToDecimal(item.IngresoMayo.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 6 ? g.Sum(item => Convert.ToDecimal(item.IngresoJunio.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 7 ? g.Sum(item => Convert.ToDecimal(item.IngresoJulio.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 8 ? g.Sum(item => Convert.ToDecimal(item.IngresoAgosto.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 9 ? g.Sum(item => Convert.ToDecimal(item.IngresoSeptiembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 10 ? g.Sum(item => Convert.ToDecimal(item.IngresoOctubre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 11 ? g.Sum(item => Convert.ToDecimal(item.IngresoNoviembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 12 ? g.Sum(item => Convert.ToDecimal(item.IngresoDiciembre.GetValueOrDefault())) / g.First().MetaValor
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
                                 GastoTotal = mes == 0
                                    ? g.Sum(item => Convert.ToDecimal(item.GastoEnero.GetValueOrDefault()) + Convert.ToDecimal(item.GastoFebrero.GetValueOrDefault()) + Convert.ToDecimal(item.GastoMarzo.GetValueOrDefault()) + Convert.ToDecimal(item.GastoAbril.GetValueOrDefault()) + Convert.ToDecimal(item.GastoMayo.GetValueOrDefault()) + Convert.ToDecimal(item.GastoJunio.GetValueOrDefault()) + Convert.ToDecimal(item.GastoJulio.GetValueOrDefault()) + Convert.ToDecimal(item.GastoAgosto.GetValueOrDefault()) + Convert.ToDecimal(item.GastoSeptiembre.GetValueOrDefault()) + Convert.ToDecimal(item.GastoOctubre.GetValueOrDefault()) + Convert.ToDecimal(item.GastoNoviembre.GetValueOrDefault()) + Convert.ToDecimal(item.GastoDiciembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 1 ? g.Sum(item => Convert.ToDecimal(item.GastoEnero.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 2 ? g.Sum(item => Convert.ToDecimal(item.GastoFebrero.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 3 ? g.Sum(item => Convert.ToDecimal(item.GastoMarzo.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 4 ? g.Sum(item => Convert.ToDecimal(item.GastoAbril.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 5 ? g.Sum(item => Convert.ToDecimal(item.GastoMayo.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 6 ? g.Sum(item => Convert.ToDecimal(item.GastoJunio.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 7 ? g.Sum(item => Convert.ToDecimal(item.GastoJulio.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 8 ? g.Sum(item => Convert.ToDecimal(item.GastoAgosto.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 9 ? g.Sum(item => Convert.ToDecimal(item.GastoSeptiembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 10 ? g.Sum(item => Convert.ToDecimal(item.GastoOctubre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 11 ? g.Sum(item => Convert.ToDecimal(item.GastoNoviembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 12 ? g.Sum(item => Convert.ToDecimal(item.GastoDiciembre.GetValueOrDefault())) / g.First().MetaValor
                                    : 0                                 
                             }).ToListAsync();

                var res_meta_mensual = await (from a in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs
                                              where a.Año == DateTime.Now.Year
                                              select a).ToListAsync();

                decimal? ingresos = 0;
                decimal? gastos = 0;
                decimal? resta_ingresos_gastos = 0;

                foreach(var m in res_meta_mensual)
                {
                    ingresos += m.InEnero ?? 0 + m.InFebrero ?? 0 + m.InMarzo ?? 0 + m.InAbril ?? 0 + m.InMayo ?? 0 + m.InJunio ?? 0 + m.InJulio ?? 0 + m.InAgosto ?? 0 + m.InSeptiembre ?? 0 + m.InOctubre ?? 0 + m.InNoviembre ?? 0 + m.InDiciembre ?? 0;
                    gastos += m.OutEnero ?? 0 + m.OutFebrero ?? 0 + m.OutMarzo ?? 0 + m.OutAbril ?? 0 + m.OutMayo ?? 0 + m.OutJunio ?? 0 + m.OutJulio ?? 0 + m.OutAgosto ?? 0 + m.OutSeptiembre ?? 0 + m.OutOctubre ?? 0 + m.OutNoviembre ?? 0 + m.OutDiciembre ?? 0;
                }

                resta_ingresos_gastos = ingresos - gastos;

                foreach (var r in res)
                {                    
                    r.PorcentajeReal = r.Real != null && r.Valor != null && r.Meta != null && r.Meta != 0 ? Convert.ToDecimal(r.Real) * Convert.ToDecimal(r.Valor) / Convert.ToDecimal(r.Meta) : 0;
                    r.MetaMensual = resta_ingresos_gastos / r.MetaValor;
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
        public async Task<List<Dor_ObjetivosGenerales>> GetDorMetasProyecto(int proyecto, int nivel, int mes, string seccion)
        {
            List<Dor_ObjetivosGenerales> res = null;

            using (var db = new ConnectionDB(dbConfig))
            {                
                res = await (from a in db.dOR_Meta_Proyectos
                             join b in db.dOR_Objetivos_Nivel on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { b.UnidadDeNegocio, b.Concepto, b.Descripcion }
                             join c in db.dOR_Tooltip on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion } equals new { c.UnidadDeNegocio, c.Concepto, c.Descripcion } into cJoin
                             from cItem in cJoin.DefaultIfEmpty()
                             join d in db.tB_DOR_Real_Gasto_Ingreso_Proyecto_GPMs on new { a.UnidadDeNegocio, a.Concepto, a.Descripcion, a.Año } equals new { d.UnidadDeNegocio, d.Concepto, d.Descripcion, d.Año } into dJoin
                             from dItem in dJoin.DefaultIfEmpty()
                             where a.NoProyecto == proyecto
                             && b.Nivel == nivel
                             orderby a.Descripcion ascending
                             group new Dor_ObjetivosGenerales
                             {
                                 Id = a.Id,
                                 UnidadDeNegocio = a.UnidadDeNegocio,
                                 Concepto = a.Concepto,
                                 Descripcion = a.Descripcion,
                                 Meta = a.Meta,                                 
                                 PorcentajeEstimado = b.Valor,
                                 Nivel = b.Nivel,
                                 Valor = b.Valor,
                                 Tooltip = cItem.Tooltip ?? string.Empty,
                                 Enero = a.Enero ?? 0,
                                 Febrero = a.Febrero ?? 0,
                                 Marzo = a.Marzo ?? 0,
                                 Abril = a.Abril ?? 0,
                                 Mayo = a.Mayo ?? 0,
                                 Junio = a.Junio ?? 0,
                                 Julio = a.Julio ?? 0,
                                 Agosto = a.Agosto ?? 0,
                                 Septiembre = a.Septiembre ?? 0,
                                 Octubre = a.Octubre ?? 0,
                                 Noviembre = a.Noviembre ?? 0,
                                 Diciembre = a.Diciembre ?? 0,
                                 IngresoEnero = dItem.InEnero ?? 0,
                                 IngresoFebrero = dItem.InFebrero ?? 0,
                                 IngresoMarzo = dItem.InMarzo ?? 0,
                                 IngresoAbril = dItem.InAbril ?? 0,
                                 IngresoMayo = dItem.InMayo ?? 0,
                                 IngresoJunio = dItem.InJunio ?? 0,
                                 IngresoJulio = dItem.InJulio ?? 0,
                                 IngresoAgosto = dItem.InAgosto ?? 0,
                                 IngresoSeptiembre = dItem.InSeptiembre ?? 0,
                                 IngresoOctubre = dItem.InOctubre ?? 0,
                                 IngresoNoviembre = dItem.InNoviembre ?? 0,
                                 IngresoDiciembre = dItem.InDiciembre ?? 0,
                                 GastoEnero = dItem.OutEnero ?? 0,
                                 GastoFebrero = dItem.OutFebrero ?? 0,
                                 GastoMarzo = dItem.OutMarzo ?? 0,
                                 GastoAbril = dItem.OutAbril ?? 0,
                                 GastoMayo = dItem.OutMayo ?? 0,
                                 GastoJunio = dItem.OutJunio ?? 0,
                                 GastoJulio = dItem.OutJulio ?? 0,
                                 GastoAgosto = dItem.OutAgosto ?? 0,
                                 GastoSeptiembre = dItem.OutSeptiembre ?? 0,
                                 GastoOctubre = dItem.OutOctubre ?? 0,
                                 GastoNoviembre = dItem.OutNoviembre ?? 0,
                                 GastoDiciembre = dItem.OutDiciembre ?? 0,
                                 ProyectadoEnero = a.ProyectadoEnero ?? 0,
                                 ProyectadoFebrero = a.ProyectadoFebrero ?? 0,
                                 ProyectadoMarzo = a.ProyectadoMarzo ?? 0,
                                 ProyectadoAbril = a.ProyectadoAbril ?? 0,
                                 ProyectadoMayo = a.ProyectadoMayo ?? 0,
                                 ProyectadoJunio = a.ProyectadoJunio ?? 0,
                                 ProyectadoJulio = a.ProyectadoJulio ?? 0,
                                 ProyectadoAgosto = a.ProyectadoAgosto ?? 0,
                                 ProyectadoSeptiembre = a.ProyectadoSeptiembre ?? 0,
                                 ProyectadoOctubre = a.ProyectadoOctubre ?? 0,
                                 ProyectadoNoviembre = a.ProyectadoNoviembre ?? 0,
                                 ProyectadoDiciembre = a.ProyectadoDiciembre ?? 0
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
                                 Real = mes == 0
                                     ? g.Sum(item => Convert.ToDecimal(item.Enero.GetValueOrDefault()) + Convert.ToDecimal(item.Febrero.GetValueOrDefault()) + Convert.ToDecimal(item.Marzo.GetValueOrDefault()) + Convert.ToDecimal(item.Abril.GetValueOrDefault()) + Convert.ToDecimal(item.Mayo.GetValueOrDefault()) + Convert.ToDecimal(item.Junio.GetValueOrDefault()) + Convert.ToDecimal(item.Julio.GetValueOrDefault()) + Convert.ToDecimal(item.Agosto.GetValueOrDefault()) + Convert.ToDecimal(item.Septiembre.GetValueOrDefault()) + Convert.ToDecimal(item.Octubre.GetValueOrDefault()) + Convert.ToDecimal(item.Noviembre.GetValueOrDefault()) + Convert.ToDecimal(item.Diciembre.GetValueOrDefault())) / (DateTime.Now.Month - 1)
                                     : mes == 1 ? g.Sum(item => Convert.ToDecimal(item.Enero.GetValueOrDefault()))
                                     : mes == 2 ? g.Sum(item => Convert.ToDecimal(item.Febrero.GetValueOrDefault()))
                                     : mes == 3 ? g.Sum(item => Convert.ToDecimal(item.Marzo.GetValueOrDefault()))
                                     : mes == 4 ? g.Sum(item => Convert.ToDecimal(item.Abril.GetValueOrDefault()))
                                     : mes == 5 ? g.Sum(item => Convert.ToDecimal(item.Mayo.GetValueOrDefault()))
                                     : mes == 6 ? g.Sum(item => Convert.ToDecimal(item.Junio.GetValueOrDefault()))
                                     : mes == 7 ? g.Sum(item => Convert.ToDecimal(item.Julio.GetValueOrDefault()))
                                     : mes == 8 ? g.Sum(item => Convert.ToDecimal(item.Agosto.GetValueOrDefault()))
                                     : mes == 9 ? g.Sum(item => Convert.ToDecimal(item.Septiembre.GetValueOrDefault()))
                                     : mes == 10 ? g.Sum(item => Convert.ToDecimal(item.Octubre.GetValueOrDefault()))
                                     : mes == 11 ? g.Sum(item => Convert.ToDecimal(item.Noviembre.GetValueOrDefault()))
                                     : mes == 12 ? g.Sum(item => Convert.ToDecimal(item.Diciembre.GetValueOrDefault()))
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
                                 IngresoTotal = mes == 0
                                    ? g.Sum(item => Convert.ToDecimal(item.IngresoEnero.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoFebrero.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoMarzo.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoAbril.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoMayo.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoJunio.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoJulio.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoAgosto.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoSeptiembre.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoOctubre.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoNoviembre.GetValueOrDefault()) + Convert.ToDecimal(item.IngresoDiciembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 1 ? g.Sum(item => Convert.ToDecimal(item.IngresoEnero.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 2 ? g.Sum(item => Convert.ToDecimal(item.IngresoFebrero.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 3 ? g.Sum(item => Convert.ToDecimal(item.IngresoMarzo.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 4 ? g.Sum(item => Convert.ToDecimal(item.IngresoAbril.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 5 ? g.Sum(item => Convert.ToDecimal(item.IngresoMayo.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 6 ? g.Sum(item => Convert.ToDecimal(item.IngresoJunio.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 7 ? g.Sum(item => Convert.ToDecimal(item.IngresoJulio.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 8 ? g.Sum(item => Convert.ToDecimal(item.IngresoAgosto.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 9 ? g.Sum(item => Convert.ToDecimal(item.IngresoSeptiembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 10 ? g.Sum(item => Convert.ToDecimal(item.IngresoOctubre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 11 ? g.Sum(item => Convert.ToDecimal(item.IngresoNoviembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 12 ? g.Sum(item => Convert.ToDecimal(item.IngresoDiciembre.GetValueOrDefault())) / g.First().MetaValor
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
                                 GastoTotal = mes == 0
                                    ? g.Sum(item => Convert.ToDecimal(item.GastoEnero.GetValueOrDefault()) + Convert.ToDecimal(item.GastoFebrero.GetValueOrDefault()) + Convert.ToDecimal(item.GastoMarzo.GetValueOrDefault()) + Convert.ToDecimal(item.GastoAbril.GetValueOrDefault()) + Convert.ToDecimal(item.GastoMayo.GetValueOrDefault()) + Convert.ToDecimal(item.GastoJunio.GetValueOrDefault()) + Convert.ToDecimal(item.GastoJulio.GetValueOrDefault()) + Convert.ToDecimal(item.GastoAgosto.GetValueOrDefault()) + Convert.ToDecimal(item.GastoSeptiembre.GetValueOrDefault()) + Convert.ToDecimal(item.GastoOctubre.GetValueOrDefault()) + Convert.ToDecimal(item.GastoNoviembre.GetValueOrDefault()) + Convert.ToDecimal(item.GastoDiciembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 1 ? g.Sum(item => Convert.ToDecimal(item.GastoEnero.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 2 ? g.Sum(item => Convert.ToDecimal(item.GastoFebrero.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 3 ? g.Sum(item => Convert.ToDecimal(item.GastoMarzo.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 4 ? g.Sum(item => Convert.ToDecimal(item.GastoAbril.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 5 ? g.Sum(item => Convert.ToDecimal(item.GastoMayo.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 6 ? g.Sum(item => Convert.ToDecimal(item.GastoJunio.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 7 ? g.Sum(item => Convert.ToDecimal(item.GastoJulio.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 8 ? g.Sum(item => Convert.ToDecimal(item.GastoAgosto.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 9 ? g.Sum(item => Convert.ToDecimal(item.GastoSeptiembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 10 ? g.Sum(item => Convert.ToDecimal(item.GastoOctubre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 11 ? g.Sum(item => Convert.ToDecimal(item.GastoNoviembre.GetValueOrDefault())) / g.First().MetaValor
                                    : mes == 12 ? g.Sum(item => Convert.ToDecimal(item.GastoDiciembre.GetValueOrDefault())) / g.First().MetaValor
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
                                 ProyectadoTotal = mes == 0
                                    ? g.Sum(item => Convert.ToDecimal(item.ProyectadoEnero.GetValueOrDefault()) + Convert.ToDecimal(item.ProyectadoFebrero.GetValueOrDefault()) + Convert.ToDecimal(item.ProyectadoMarzo.GetValueOrDefault()) + Convert.ToDecimal(item.ProyectadoAbril.GetValueOrDefault()) + Convert.ToDecimal(item.ProyectadoMayo.GetValueOrDefault()) + Convert.ToDecimal(item.ProyectadoJunio.GetValueOrDefault()) + Convert.ToDecimal(item.ProyectadoJulio.GetValueOrDefault()) + Convert.ToDecimal(item.ProyectadoAgosto.GetValueOrDefault()) + Convert.ToDecimal(item.ProyectadoSeptiembre.GetValueOrDefault()) + Convert.ToDecimal(item.ProyectadoOctubre.GetValueOrDefault()) + Convert.ToDecimal(item.ProyectadoNoviembre.GetValueOrDefault()) + Convert.ToDecimal(item.ProyectadoDiciembre.GetValueOrDefault())) / (DateTime.Now.Month - 1)
                                    : mes == 1 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoEnero.GetValueOrDefault()))
                                    : mes == 2 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoFebrero.GetValueOrDefault()))
                                    : mes == 3 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoMarzo.GetValueOrDefault()))
                                    : mes == 4 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoAbril.GetValueOrDefault()))
                                    : mes == 5 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoMayo.GetValueOrDefault()))
                                    : mes == 6 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoJunio.GetValueOrDefault()))
                                    : mes == 7 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoJulio.GetValueOrDefault()))
                                    : mes == 8 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoAgosto.GetValueOrDefault()))
                                    : mes == 9 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoSeptiembre.GetValueOrDefault()))
                                    : mes == 10 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoOctubre.GetValueOrDefault()))
                                    : mes == 11 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoNoviembre.GetValueOrDefault()))
                                    : mes == 12 ? g.Sum(item => Convert.ToDecimal(item.ProyectadoDiciembre.GetValueOrDefault()))
                                    : 0
                             }).ToListAsync();

                foreach (var r in res)
                {
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

            int id_record = Convert.ToInt32(registro["id"].ToString());
            decimal real = Convert.ToDecimal(registro["real"].ToString());

            using (var db = new ConnectionDB(dbConfig))
            {
                var res_update_real = await db.dOR_Meta_Proyectos.Where(x => x.Id == id_record)
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
