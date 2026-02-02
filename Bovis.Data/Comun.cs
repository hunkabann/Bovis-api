using Bovis.Common.Model.NoTable;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bovis.Data
{
    public static class Comun
    {
        public static bool EsFechaActual(this string cadena, string sCultura, out DateTime dttSalida)
        {
            dttSalida = new DateTime();

            bool esHoy = false;
            CultureInfo oCultura = CultureInfo.CreateSpecificCulture(sCultura);

            
            if (cadena == null || cadena == "")
            {
                dttSalida = DateTime.Now.Date;
                esHoy = true;
                return esHoy;
            }

            dttSalida = Convert.ToDateTime(cadena, oCultura);
            if (DateTime.Now.Date == dttSalida)
            {
                esHoy = true;
            }

            return esHoy;
        }

        public static string ObtieneCultura()
        {
            return "es-Mx";
        }
    }

    public class General
    {
        public void AsignaVaciosProyecto_Detalle(ref PCS_Proyecto_Detalle oEntrada)
        {
            oEntrada.NumProyecto = 0;
            oEntrada.NombreProyecto = "";
            oEntrada.FechaIni = DateTime.Now.Date;
            oEntrada.FechaFin = DateTime.Now.Date;


            oEntrada.Etapas = new List<PCS_Etapa_Detalle>();
            oEntrada.Etapas.Add(new PCS_Etapa_Detalle()
            {
                IdFase = 0,
                Orden = 0,
                Fase = "",
                FechaIni = DateTime.Now.Date,
                FechaFin = DateTime.Now.Date,
                Empleados = new List<PCS_Empleado_Detalle>(),
            }) ;

            oEntrada.Etapas[0].Empleados.Add(new PCS_Empleado_Detalle()
            {
                Id = 0,
                IdFase = 0,
                NumempleadoRrHh = "",
                Empleado = "",
                Cantidad = 0,
                AplicaTodosMeses = false,
                Fee = 0,
                Reembolsable = false,
                NuCostoIni = 0,
                ChAlias = "",
                EtiquetaTBD = "",
                IdPuesto = "",
                Fechas = new List<PCS_Fecha_Detalle>(),
            });

            oEntrada.Etapas[0].Empleados[0].Fechas.Add(new PCS_Fecha_Detalle()
            {
                Id = 0,
                Rubro = "",
                ClasificacionPY = "",
                RubroReembolsable = false,
                Mes = 0,
                Anio = 0,
                Porcentaje = 0,
            });
        }

        public void AsignaVaciosPCS_GanttData(ref PCS_GanttData oEntrada)
        {
            CultureInfo oCultura = CultureInfo.CreateSpecificCulture(Comun.ObtieneCultura());
            oEntrada.data = new List<PCS_GanttDataFase>();

            oEntrada.data.Add(new PCS_GanttDataFase()
            {
                X = new string[] { String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date), String.Format("{0:yyyy-MM-dd}", DateTime.Now.Date) },
                Y = "",
            });

        }

        public void AsignaVaciosListaProyectos(ref List<Proyecto_Detalle> lst)
        {
            if (lst.Count == 0)
            {
                lst.Add(new Proyecto_Detalle
                {
                    nunum_proyecto = 0,
                    chproyecto = "",
                    //chalcance = proy.Alcance,
                    chalcance = "",
                    chcp = "",
                    chciudad = "",
                    nukidpais = 0,
                    chpais = "",
                    nukidestatus = 0,
                    chestatus = "",
                    nukidsector = 0,
                    chsector = "",
                    nukidtipo_proyecto = 0,
                    chtipo_proyecto = "",
                    nukidresponsable_preconstruccion = "0",
                    chresponsable_preconstruccion = "",
                    nukidresponsable_construccion = "0",
                    chresponsable_construccion = "",
                    nukidresponsable_ehs = "0",
                    chresponsable_ehs = "",
                    nukidresponsable_supervisor = "0",
                    chresponsable_supervisor = "",
                    nukidempresa = 0,
                    chempresa = "",
                    nukiddirector_ejecutivo = "0",
                    chdirector_ejecutivo = "",
                    nucosto_promedio_m2 = 0,
                    dtfecha_ini = DateTime.Now.Date,
                    dtfecha_fin = DateTime.Now.Date,
                    impuesto_nomina = 0,
                    nukidunidadnegocio = 0,  //atc
                    chunidadnegocio = "",  //atc
                    chcontacto_nombre = "",
                    chcontacto_posicion = "",
                    chcontacto_telefono = "",
                    chcontacto_correo = "",
                    dtfecha_vigencia_ini = DateTime.Now.Date, //LineaBase
                    dtfecha_vigencia_fin = DateTime.Now.Date, //LineaBase
                });

                lst[0].Clientes = new List<InfoCliente>();
                lst[0].Clientes.Add(new InfoCliente() { IdCliente = 0, Cliente = "", Rfc = "" });

                lst[0].overheadPorcentaje = 0;
                lst[0].utilidadPorcentaje = 0;
                lst[0].contingenciaPorcentaje = 0;

            }

        }//AsignaVaciosListaProyectos

        public void AsignaVaciosListaCosto_Detalle(ref List<Costo_Detalle> lst)
        {
            lst.Add(new Costo_Detalle()
            {
                IdCostoEmpleado = 0,
                /*
                 *  Empleado
                 */
                IdPersona = 0,
                NumEmpleadoRrHh = "",
                NumEmpleadoNoi = 0,
                NombreCompletoEmpleado = "",
                ApellidoPaterno = "",
                ApellidoMaterno = "",
                NombreEmpleado = "",
                IdCiudad = 0,
                Ciudad = "",
                Reubicacion = "",
                IdPuesto = 0,
                Puesto = "",
                NumProyecto = 0,
                Proyecto = "",
                IdUnidadNegocio = 0,
                UnidadNegocio = "",
                IdEmpresa = 0,
                Empresa = "",
                Timesheet = "",
                IdEmpleadoJefe = "",
                NombreJefe = "",
                Categoria = "",
                /*
                 * Seniority
                 */
                FechaIngreso = DateTime.Now.Date,
                Antiguedad = 0M,
                /*
                 * Sueldo neto mensual (MN)
                 */
                AvgDescuentoEmpleado = 0M,
                MontoDescuentoMensual = 0M,
                SueldoNetoPercibidoMensual = 0M,
                RetencionImss = 0M,
                Ispt = 0M,
                /*
                 * Sueldo bruto MN/USD
                 */
                SueldoBrutoInflacion = 0M,
                Anual = 0M,
                /*
                 * Aguinaldo
                 */
                AguinaldoCantidadMeses = 0M,
                AguinaldoMontoProvisionMensual = 0M,
                /*
                 * Prima vacacional
                 */
                PvDiasVacasAnuales = 0M,
                PvProvisionMensual = 0M,
                /*
                 * IndemnizaciÃ³n
                 */
                IndemProvisionMensual = 0M,
                /*
                 * ProvisiÃ³n bono anual
                 */
                AvgBonoAnualEstimado = 0M,
                BonoAnualProvisionMensual = 0M,
                /*
                 * GMM
                 */
                SgmmCostoTotalAnual = 0M,
                SgmmCostoMensual = 0M,
                /*
                 * Seguro de vida
                 */
                SvCostoTotalAnual = 0M,
                SvCostoMensual = 0M,
                /*
                 * Vales de despensa
                 */
                VaidCostoMensual = 0M,
                VaidComisionCostoMensual = 0M,
                /*
                 * PTU
                 */
                PtuProvision = 0M,
                /*
                 * Beneficios
                 */
                Beneficios = new List<Beneficio_Costo_Detalle>(),

                Beneficiosproyecto = new List<Beneficio_Costo_Detalle>(),
                /*
                 * Cargas sociales e impuestos laborales
                 */
                Impuesto3sNomina = 0M,
                Imss = 0M,
                Retiro2 = 0M,
                CesantesVejez = 0M,
                Infonavit = 0M,
                CargasSociales = 0M,
                /*
                 * Costo total laboral BLL
                 */
                CostoMensualEmpleado = 0M,
                CostoMensualProyecto = 0M,
                CostoAnualEmpleado = 0M,
                CostoSalarioBruto = 0M,
                CostoSalarioNeto = 0M,


                NuAnno = 0,
                NuMes = 0,
                FechaActualizacion = DateTime.Now.Date,
                RegHistorico = false,
                SalarioDiarioIntegrado = "",

                ImpuestoNomina = 0,
            });
        }//AsignaVaciosListaCosto_Detalle
    }
}
