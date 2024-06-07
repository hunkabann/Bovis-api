using Azure;
using Bovis.Common;
using Bovis.Common.Model;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.NoTable;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Bovis.Data.Repository;
using LinqToDB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;


namespace Bovis.Data
{
    public class CostoData : RepositoryLinq2DB<ConnectionDB>, ICostoData
    {

        //ATC

        public static decimal BonoAdicionalReubicacion = 0M; 
        public static decimal ViaticosAComprobar = 0M;
        public static decimal Be_BonoAdicional = 0M;
        public static decimal Be_AyudaTransporte = 0M;

        //Cuota Fija del Trabajador 3 veces UMA (PATRON) = 686.60
        private static double p_patron = 686.60;
        //3 Veces UMA = 325.71
        private static double p_3_Veces_UMA = 325.71;
        //Prima Riesgo = 0.5
        private static double p_Prima_Riesgo = 0.005;

        //Factor SBC CEAV
        private static double p_Patron_SBC_CEAV = 0.02;

        //Factor cesantia y vejez del patron
        private static double p_Patron_CV = 0.0424;

        //Factor Infonavit del patron
        private static double p_Patron_Infona = 0.05;

        //Factor CuotaFija Patron
        private static double p_Patron_CF = 0.204;

        //Factor Enfermedades maternidad gastos medicos patron
        private static double p_Patron_EMGM = 0.0105;

        //Factor Enfermedades Maternidad de dinero del patron
        private static double p_Patron_EMDM = 0.007;

        //Factor Validez y vida en  dinero del patron
        private static double p_Patron_IVDP = 0.0175;

        //Factor Guarderias y Prestaciones sociales del patron
        private static double p_Patron_GPSP = 0.01;

        //UMA 2024 = 108.7
        private static double p_UMA = 108.7;
        //Dias Trabajados = 31
        private static int p_dias_trabajados = 31;
        //Dias del mes = 31
        private static int p_dias_mes = 31;
        //Dias trabajados bim = 31
        private static int p_dias_trabajados_bim = 31;


        //Enfermedad y maternidad, en especie
        private static double p_EME2;

        //Gastos medicos para pencionados
        private static double p_EME_GMPE;

        //En Dinero
        private static double p_EME_ED;

        //En Especie
        private static double p_EME_ESP;

        //Guarderias y prestaciones
        private static double p_GP;

        //Cesantia y Vejes
        private static double p_CEAV;

        //riesgo trabajo patron
        private static double p_RTP;

        //Patron en Enfermedades paternidad en especie
        private static double p_PEME;

        //Enfermedades y maternidad en especie2
        private static double p_PEME2;

        //Enfermedades y maternidad Gastos medicos para pencionados del patron
        private static double p_EMGP;

        //Enfermedades y maternidad en dinero del patron
        private static double p_EMDP;

        //Invalidez de Vida de dinero del patron
        private static double p_IVDP;

        //Guarderias y Prestaciones sociales del patron
        private static double p_GPSP;

        // SBC
        private static double p_SBC;




        #region base
        private readonly string dbConfig = "DBConfig";


        public CostoData()
        {
            this.ConfigurationDB = dbConfig;
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.Collect();
        }
        #endregion base

        #region AddCosto
        public async Task<Common.Response<decimal>> AddCosto(TB_CostoPorEmpleado registro)
        {
            var registro_anterior = await GetCostoEmpleado(registro.NumEmpleadoRrHh, registro.NuAnno, registro.NuMes, false); //Verifica que no existe registro previo de costos para este empleado en el año y mes solicitados.

            if (!registro_anterior.Success) // Puede llevarse a cabo la inserción de un nuevo registro.
            {
                //ATC
                decimal? sueldo_gravable = registro.SueldoBruto + registro.AvgBonoAnualEstimado;

                using (var db = new ConnectionDB(dbConfig))
                {
                    var isr_record = await (from isr in db.tB_Cat_Tabla_ISRs
                                            where isr.Anio == registro.NuAnno
                                            && isr.Mes == registro.NuMes
                                            && (isr.LimiteInferior <= registro.SueldoBruto && isr.LimiteSuperior >= registro.SueldoBruto)
                                            select isr).FirstOrDefaultAsync();

                    if (isr_record != null)
                    {
                        //decimal? sueldoBruto = registro.AvgBonoAnualEstimado != 0 ? (registro.SueldoBruto * registro.AvgBonoAnualEstimado * 10) : registro.SueldoBruto;
                        //registro.Ispt = ((sueldoBruto - isr_record.LimiteInferior) * isr_record.PorcentajeAplicable) + isr_record.CuotaFija;

                        //ATC
                        decimal? sueldoBruto = registro.AvgBonoAnualEstimado != 0 ? sueldo_gravable : registro.SueldoBruto;
                        registro.Ispt = ((sueldo_gravable - isr_record.LimiteInferior) * isr_record.PorcentajeAplicable) + isr_record.CuotaFija;
                    }
                }

                var resultado = (decimal)await InsertEntityAsync<TB_CostoPorEmpleado>(registro);
                return new Common.Response<decimal>()
                {
                    Data = resultado,
                    Success = true,
                    Message = $"Se creó nuevo registro con id: {resultado}"
                };
            }

            return new Common.Response<decimal>()
            {
                Success = false,
                Message = $"Error: Ya existe el registro: {registro_anterior.Data[0].IdCostoEmpleado} en tabla costos para el empleado {registro.NumEmpleadoRrHh}."
            };

        }
        #endregion

        #region GetCostos

        public async Task<List<Costo_Detalle>> GetCostos(bool? hist)
        {
            CostoQueries QueryBase = new(dbConfig);
            var costos = await QueryBase.CostosEmpleados();

            if ((bool)hist)
            {
                return costos;
            }
            else
                return costos.Where(reg => reg.RegHistorico == false).ToList();
        }
        #endregion

        #region GetCosto
        public async Task<Costo_Detalle> GetCosto(int IdCosto)
        {
            CostoQueries QueryBase = new(dbConfig);
            var costos = await QueryBase.CostosEmpleados();
            var resp = costos.SingleOrDefault(costo => costo.IdCostoEmpleado == IdCosto);

            return resp;
        }
        #endregion

        #region GetCostosEmpleado
        public async Task<Common.Response<List<Costo_Detalle>>> GetCostosEmpleado(string NumEmpleadoRrHh, bool hist)
        {
            CostoQueries QueryBase = new(dbConfig);
            var costos = await QueryBase.CostosEmpleados();
            var resp = costos.Where(costo => costo.NumEmpleadoRrHh == NumEmpleadoRrHh).ToList<Costo_Detalle>();

            if (hist)
            {
                if (resp.Count > 0)
                {
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = true,
                        Data = resp,
                        Message = "Ok"
                    };

                }
                else
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = false,
                        Message = $"No se encontraron históricos de costos del empleado: {NumEmpleadoRrHh}"

                    };


            }
            else
            {
                var listaCostos = resp.Where(reg => reg.RegHistorico == false).ToList();
                if (listaCostos.Count > 0)
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = true,
                        Data = listaCostos,
                        Message = "Ok"
                    };
                else
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = false,
                        Message = $"No se encontraron registros de costos para el empleado: {NumEmpleadoRrHh}"
                    };
            }


        }
        #endregion

        #region GetCostoEmpleado
        public async Task<Common.Response<List<Costo_Detalle>>> GetCostoEmpleado(string NumEmpleadoRrHh, int anno, int mes, bool hist)
        {
            CostoQueries QueryBase = new(dbConfig);
            var costos = await QueryBase.CostosEmpleados();
            var result = costos.Where(costo => costo.NumEmpleadoRrHh == NumEmpleadoRrHh && costo.NuAnno == anno && costo.NuMes == mes).ToList();

            if (result.Count > 0)
            {
                if (hist)
                {
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = true,
                        Data = result,
                        Message = "Ok"
                    };

                }
                else
                {
                    var costoEmpleado = result.Where(reg => reg.RegHistorico == false).ToList();
                    if (costoEmpleado.Count > 0)
                    {
                        return new Common.Response<List<Costo_Detalle>>()
                        {
                            Success = true,
                            Data = costoEmpleado,
                            Message = "Ok"

                        };

                    }
                    else
                        return new Common.Response<List<Costo_Detalle>>()
                        {
                            Success = false,
                            Message = $"No existe registro de costo para el empleado {NumEmpleadoRrHh} en el año y mes solicitados"

                        };

                }
            }
            else
            {
                return new Common.Response<List<Costo_Detalle>>()
                {
                    Success = false,
                    Message = $"No existen históricos de costos para el empleado: {NumEmpleadoRrHh}."
                };
            }
        }
        #endregion

        #region GetCostoLaborable
        public async Task<Common.Response<decimal>> GetCostoLaborable(string NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max)
        {
            CostoQueries QueryBase = new(dbConfig);
            var result = await QueryBase.CostosEmpleados();

            var costosEmpleado = result.Where(reg => (reg.NumEmpleadoRrHh == NumEmpleadoRrHh) && (reg.RegHistorico == false) && ((reg.NuAnno > anno_min && reg.NuAnno < anno_max) || (reg.NuAnno == anno_min && reg.NuMes >= mes_min) || (reg.NuAnno == anno_max && reg.NuMes <= mes_max))).ToList();

            if (costosEmpleado.Count > 0)
            {

                decimal costoTotalLaborable = 0.0M;
                foreach (var costo in costosEmpleado)
                    costoTotalLaborable = (decimal)costo.CostoMensualEmpleado + costoTotalLaborable;
                return new Common.Response<decimal>()
                {
                    Success = true,
                    Data = (decimal)costoTotalLaborable,
                    Message = $"El CTL - Costo Total Laborable es de: {costoTotalLaborable.ToString("C2")}"
                };
            }
            else
                return new Common.Response<decimal>()
                {
                    Success = false,
                    Data = 0.0M,
                    Message = $"No se encontraron históricos de costos para el empleado: {NumEmpleadoRrHh}"
                };

        }
        #endregion

        #region GetCostosBetweenDates
        public async Task<Common.Response<List<Costo_Detalle>>> GetCostosBetweenDates(string NumEmpleadoRrHh, int anno_min, int mes_min, int anno_max, int mes_max, bool hist)
        {
            CostoQueries QueryBase = new(dbConfig);
            var costos = await QueryBase.CostosEmpleados();
            if (costos.Count > 0)
            {
                if (hist)
                {
                    var costosEmpleado = costos.Where(reg => ((reg.NumEmpleadoRrHh == NumEmpleadoRrHh) && (reg.NuAnno > anno_min && reg.NuAnno < anno_max) || (reg.NuAnno == anno_min && reg.NuMes >= mes_min) || (reg.NuAnno == anno_max && reg.NuMes <= mes_max))).ToList();
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = true,
                        Data = costosEmpleado,
                        Message = "Ok"
                    };
                }
                else
                {
                    var costosEmpleado = costos.Where(reg => (reg.NumEmpleadoRrHh == NumEmpleadoRrHh) && (reg.RegHistorico == false) && ((reg.NuAnno > anno_min && reg.NuAnno < anno_max) || (reg.NuAnno == anno_min && reg.NuMes >= mes_min) || (reg.NuAnno == anno_max && reg.NuMes <= mes_max))).ToList();
                    return new Common.Response<List<Costo_Detalle>>()
                    {
                        Success = true,
                        Data = costosEmpleado,
                        Message = "Ok"
                    };
                }

            }

            return new Common.Response<List<Costo_Detalle>>()
            {
                Success = false,
                Message = $"No se encontraron registros históricos de costos para el Empleado: {NumEmpleadoRrHh} en las fechas proporcionadas"
            };


        }
        #endregion

        #region UpdateCostos
        public async Task<Common.Response<TB_CostoPorEmpleado>> UpdateCostos(CostoPorEmpleadoDTO source,int costoId, TB_CostoPorEmpleado registro)
        {
            if (costoId == registro.IdCostoEmpleado)
            {
                int numero_proyecto = registro.NumProyecto;
                decimal? sueldo_bruto = registro.SueldoBruto;
                decimal? avg_bono_anual_estimado = registro.AvgBonoAnualEstimado;
                decimal? sgmm_costo_total_anual = registro.SgmmCostoTotalAnual;
                decimal? sv_costo_total_anual = registro.SvCostoTotalAnual;
                decimal? vaid_costo_mensual = registro.VaidCostoMensual;
                double? cotizacion = source.cotizacion;//1540.5;
                decimal? bonoproyect_sueldobruto = source.bonoproyect_sueldobruto;

                using (var db = new ConnectionDB(dbConfig))
                {
                    var registro_anterior = await (from c in db.tB_Costo_Por_Empleados
                                                   where c.IdCostoEmpleado == registro.IdCostoEmpleado
                                                   && c.NumEmpleadoRrHh == registro.NumEmpleadoRrHh
                                                   && c.RegHistorico == false
                                                   select c).FirstOrDefaultAsync();

                    //ATC
                    decimal? sueldo_gravable = 0; 
                    
                    if (bonoproyect_sueldobruto != null && bonoproyect_sueldobruto > 0)
                    {
                        //ATC
                        sueldo_gravable = registro.SueldoBruto + registro.AvgBonoAnualEstimado + bonoproyect_sueldobruto;
                    }
                    else
                    {
                        //ATC
                       sueldo_gravable = registro.SueldoBruto + registro.AvgBonoAnualEstimado; 
                    }

                    if (cotizacion != null && cotizacion > 0)
                    {

                        if (p_patron > p_3_Veces_UMA)
                        {
                            p_EME2 = (double)(((cotizacion - p_3_Veces_UMA) * .004) * 31);
                        }
                        else
                        {
                            p_EME2 = 0;
                        }

                        p_EME_GMPE = (double)(cotizacion * 0.00375) * p_dias_mes;

                        p_EME_ED = (double)(cotizacion * 0.0025) * p_dias_mes;

                        p_EME_ESP = (double)(cotizacion * 0.00625) * p_dias_mes;

                        p_GP = (double)(cotizacion * 0.0) * p_dias_mes;

                        p_CEAV = (double)(source.cotizacion * 0.01125) * p_dias_trabajados_bim;

                        if (source.cotizacion < 1)
                        {
                            registro.RetencionImss = 0;
                        }
                        else
                        {
                            registro.RetencionImss = (decimal)(p_EME2 + p_EME_GMPE + p_EME_ED + p_EME_ESP + p_GP + p_CEAV);
                        }

                           


                        p_RTP = (double)(source.cotizacion * p_dias_trabajados) * p_Prima_Riesgo ;

                        p_PEME = (double)(p_Patron_CF * p_UMA) * p_dias_mes;


                        if(p_patron > p_3_Veces_UMA)
                        {
                            p_PEME2 = (double)((source.cotizacion - p_3_Veces_UMA) * 0.011) * p_dias_mes;
                        }
                        else
                        {
                            p_PEME2 = 0;
                        }

                        p_EMGP = (double)(source.cotizacion * p_Patron_EMGM) * p_dias_mes;

                        p_EMDP = (double)(source.cotizacion * p_Patron_EMDM) * p_dias_mes;

                        p_IVDP = (double)(source.cotizacion * p_Patron_IVDP) * p_dias_trabajados;

                        p_GPSP = (double)(source.cotizacion * p_Patron_GPSP) * p_dias_trabajados;

                        if (source.cotizacion < 1)
                        {
                            registro.Imss = 0;
                        }
                        else
                        {
                            registro.Imss = (decimal)(p_RTP + p_PEME + p_PEME2 + p_EMGP + p_EMDP + p_IVDP + p_GPSP);
                        }

                        

                        p_SBC = (double)(source.cotizacion * p_Patron_SBC_CEAV) * p_dias_trabajados_bim;

                        registro.Retiro2 = (decimal)p_SBC;


                        registro.CesantesVejez = (decimal)(source.cotizacion * p_Patron_CV) * p_dias_trabajados_bim; 

                        registro.Infonavit = (decimal)(source.cotizacion * p_Patron_Infona) * p_dias_trabajados_bim;



                    }




                    if (registro_anterior != null)
                    {
                        registro_anterior.RegHistorico = true;
                        var resBool = await UpdateEntityAsync<TB_CostoPorEmpleado>(registro_anterior);

                        registro = registro_anterior;
                        registro.RegHistorico = false;
                        registro.SueldoBruto = sueldo_bruto;
                        registro.NumProyecto = numero_proyecto;
                        registro.NuMes = DateTime.Now.Month;
                        registro.NuAnno = DateTime.Now.Year;
                        registro.FechaActualizacion = DateTime.Now;
                        if (avg_bono_anual_estimado != 0 || sgmm_costo_total_anual != 0 ||
                            sv_costo_total_anual != 0 || vaid_costo_mensual != 0)
                        {
                            
                            registro.AvgBonoAnualEstimado = avg_bono_anual_estimado;

                            //ATC
                           if (sgmm_costo_total_anual != 0)
                            {
                                registro.SgmmCostoMensual = sgmm_costo_total_anual / 12;
                            }

                            registro.SgmmCostoTotalAnual = sgmm_costo_total_anual;

                            //ATC
                            if (sv_costo_total_anual != 0)
                            {
                                registro.SvCostoMensual = sv_costo_total_anual / 12;
                            }

                            
                            registro.SvCostoTotalAnual = sv_costo_total_anual;

                            //ATC
                            if (vaid_costo_mensual != 0)
                            {
                                registro.VaidComisionCostoMensual = vaid_costo_mensual / 12;
                            }
                            registro.VaidCostoMensual = vaid_costo_mensual;
                           
                        }
                        //ATC
                        if (cotizacion != null && cotizacion > 0)
                        {

                           
                            registro.RetencionImss = (decimal)(p_EME2 + p_EME_GMPE + p_EME_ED + p_EME_ESP + p_GP);
                        }
                        else
                        {
                            registro.RetencionImss = 0;
                        }


                        

                       var isr_record = await (from isr in db.tB_Cat_Tabla_ISRs
                        where isr.Anio == registro.NuAnno
                        && isr.Mes == registro.NuMes
                        && (isr.LimiteInferior <= sueldo_gravable && isr.LimiteSuperior >= sueldo_gravable)
                        select isr).FirstOrDefaultAsync();

                        registro.Impuesto3sNomina = (registro.SueldoBruto + registro.AguinaldoMontoProvisionMensual + registro.PvProvisionMensual + Be_BonoAdicional + Be_AyudaTransporte + registro.BonoAnualProvisionMensual + BonoAdicionalReubicacion) * 0.03M;//(source.ImpuestoNomina/100); // * 0.03M;

                        if (source.cotizacion == null)
                        {


                        }
                        else
                        {
                            if (source.cotizacion < 1)
                            {
                                registro.CargasSociales = 0;
                            }
                            else
                            {
                                registro.CargasSociales = (registro.Impuesto3sNomina + registro.Imss + registro.Retiro2 + registro.CesantesVejez + registro.Infonavit);
                            }
                        }
                        

                        if (isr_record != null)
                        {
                            //decimal? sueldoBruto = registro.AvgBonoAnualEstimado != 0 ? (registro.SueldoBruto * registro.AvgBonoAnualEstimado * 10) : registro.SueldoBruto;
                            //registro.Ispt = ((sueldoBruto - isr_record.LimiteInferior) * isr_record.PorcentajeAplicable) + isr_record.CuotaFija;

                            //decimal? sueldoBruto = registro.AvgBonoAnualEstimado != 0 ? sueldo_gravable : registro.SueldoBruto;
                            //decimal? sueldoBruto = sueldo_gravable;
                            registro.Ispt = 0;
                            registro.Ispt = ((sueldo_gravable - isr_record.LimiteInferior) * isr_record.PorcentajeAplicable) + isr_record.CuotaFija;
                        }

                        //ATC

                        var BeneficioCostoProy = await (from eb in db.tB_EmpleadoProyectoBeneficios
                                                        join b in db.tB_Cat_Beneficios on eb.IdBeneficio equals b.IdBeneficio into bJoin
                                                        from bItem in bJoin.DefaultIfEmpty()
                                                        where eb.NumEmpleadoRrHh == registro.NumEmpleadoRrHh
                                                        select eb).ToListAsync();
                        decimal? costobeneProy = 0;
                        foreach (var r in BeneficioCostoProy)
                        {
                            costobeneProy = costobeneProy + r.nucostobeneficio;
                        }

                        if (BeneficioCostoProy != null)
                        {

                            registro.CostoMensualProyecto = costobeneProy;
                        }
                        //ATC
                        //Falta sumar viaticos a comprobar del proyecto 
                        registro.SueldoNetoPercibidoMensual = (registro.SueldoBruto + bonoproyect_sueldobruto) - registro.MontoDescuentoMensual;

                        var BeneficioCosto = await (from eb in db.tB_EmpleadoBeneficios
                                                        join b in db.tB_Cat_Beneficios on eb.IdBeneficio equals b.IdBeneficio into bJoin
                                                        from bItem in bJoin.DefaultIfEmpty()
                                                        where eb.NumEmpleadoRrHh == registro.NumEmpleadoRrHh
                                                        select eb).ToListAsync();
                        decimal? costobene = 0;
                        foreach (var r in BeneficioCosto)
                        {
                            costobene = costobene + r.Costo;
                        }

                        if (BeneficioCosto != null)
                        {

                            registro.CostoMensualEmpleado = registro.CostoMensualEmpleado+costobene;
                        }

                        //ATC
                        /* if (source.CostoMensualProyecto == null)
                         {


                         }
                         else
                         {
                             registro.CostoMensualProyecto = source.CostoMensualProyecto;
                         }*/

                        var resDecimal = (decimal)await InsertEntityAsync<TB_CostoPorEmpleado>(registro);

                        return new Common.Response<TB_CostoPorEmpleado>
                        {
                            Data = registro,
                            Success = true,
                            Message = $"Actualización del registro de costos: {costoId} por el {resDecimal}"
                        };

                    }
                    else
                    {
                        return new Common.Response<TB_CostoPorEmpleado>
                        {
                            Success = false,
                            Message = $"No se encontró el registro de costos: {costoId}."
                        };
                    }
                }
            }

            return new Common.Response<TB_CostoPorEmpleado>()
            {
                Success = false,
                Message = $"Identificador del Costo {costoId} no coincide con registro {registro.IdCostoEmpleado}!"
            };

        }

        public async Task<Common.Response<TB_CostoPorEmpleado>> UpdateCostoEmpleado(TB_CostoPorEmpleado registro)
        {
            int numero_proyecto = registro.NumProyecto;
            string numEmpleadoRrHh = registro.NumEmpleadoRrHh;
            decimal? sueldo_bruto = registro.SueldoBruto;
            decimal? avg_bono_anual_estimado = registro.AvgBonoAnualEstimado;
            decimal? sgmm_costo_total_anual = registro.SgmmCostoTotalAnual;
            decimal? sv_costo_total_anual = registro.SvCostoTotalAnual;
            decimal? vaid_costo_mensual = registro.VaidCostoMensual;
            using (var db = new ConnectionDB(dbConfig))
            {
                //ATC
                decimal? sueldo_gravable = registro.SueldoBruto + registro.AvgBonoAnualEstimado;

                var registro_anterior = await (from c in db.tB_Costo_Por_Empleados
                                               where c.NumEmpleadoRrHh == registro.NumEmpleadoRrHh
                                               && c.RegHistorico == false
                                               select c).FirstOrDefaultAsync();


                if (registro_anterior != null)
                {
                    registro_anterior.RegHistorico = true;
                    registro_anterior.FechaActualizacion = DateTime.Now;
                    var resBool = await UpdateEntityAsync<TB_CostoPorEmpleado>(registro_anterior);

                    registro = registro_anterior;
                    registro.RegHistorico = false;
                    registro.SueldoBruto = sueldo_bruto;
                    registro.NumProyecto = numero_proyecto;
                    registro.NuMes = DateTime.Now.Month;
                    registro.NuAnno = DateTime.Now.Year;
                    registro.FechaActualizacion = DateTime.Now;
                    //registro.AvgBonoAnualEstimado = avg_bono_anual_estimado;
                    //registro.SgmmCostoTotalAnual = sgmm_costo_total_anual;
                    //registro.SvCostoTotalAnual = sv_costo_total_anual;
                    //registro.VaidCostoMensual = vaid_costo_mensual;
                    

                    var isr_record = await (from isr in db.tB_Cat_Tabla_ISRs
                                            where isr.Anio == registro.NuAnno
                                            && isr.Mes == registro.NuMes
                                            && (isr.LimiteInferior <= sueldo_gravable && isr.LimiteSuperior >= sueldo_gravable)
                                            select isr).FirstOrDefaultAsync();

                    if (isr_record != null)
                    {
                        //ATC

                        //decimal? sueldoBruto = registro.AvgBonoAnualEstimado != 0 ? (registro.SueldoBruto * registro.AvgBonoAnualEstimado * 10) : registro.SueldoBruto;
                        //registro.Ispt = ((sueldoBruto - isr_record.LimiteInferior) * isr_record.PorcentajeAplicable) + isr_record.CuotaFija;

                        // decimal? sueldoBruto = registro.AvgBonoAnualEstimado != 0 ? sueldo_gravable : registro.SueldoBruto;
                        registro.Ispt = 0;
                        registro.Ispt = ((sueldo_gravable - isr_record.LimiteInferior) * isr_record.PorcentajeAplicable) + isr_record.CuotaFija;

                        
                    }

                    //ATC

                    //ATC

                   var BeneficioCostoProy = await (from eb in db.tB_EmpleadoProyectoBeneficios
                                                    join b in db.tB_Cat_Beneficios on eb.IdBeneficio equals b.IdBeneficio into bJoin
                                                    from bItem in bJoin.DefaultIfEmpty()
                                                    where eb.NumEmpleadoRrHh == registro.NumEmpleadoRrHh
                                                    select eb).ToListAsync();
                    decimal? costobene = 0;
                    foreach (var r in BeneficioCostoProy)
                    {
                        costobene = +r.nucostobeneficio;
                    }
                        
                    if (BeneficioCostoProy != null)
                    {

                        registro.CostoMensualProyecto = costobene;
                    }

                    var resDecimal = (decimal)await InsertEntityAsync<TB_CostoPorEmpleado>(registro);

                    return new Common.Response<TB_CostoPorEmpleado>
                    {
                        Data = registro,
                        Success = true,
                        Message = $"Actualización del registro de costos: {resDecimal}"
                    };

                }
                else
                {
                    return new Common.Response<TB_CostoPorEmpleado>
                    {
                        Success = false,
                        Message = $"No se encontró el registro de costos para el empleado: {numEmpleadoRrHh}."
                    };
                }
            }
        }


        #endregion

        #region DeleteCosto
        public async Task<Common.Response<bool>> DeleteCosto(int costoId)
        {
            var entity = await GetEntityByPKAsync<TB_CostoPorEmpleado>(costoId);
            if (entity != null)
            {
                var respuesta = await DeleteEntityAsync<TB_CostoPorEmpleado>(entity);
                if (respuesta)
                {
                    return new Common.Response<bool>
                    {
                        Success = true,
                        Data = respuesta,
                        Message = $"Registro {costoId} borrado exitosamente."
                    };
                }
                else
                {
                    return new Common.Response<bool>()
                    {
                        Success = false,
                        Data = respuesta,
                        Message = $"Error: Se encontró una falla al intentar borrar el registro {costoId}."
                    };
                }
            }
            else
            {
                return new Common.Response<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Error: Registro de costo no existe!"
                };
            }

        }
        #endregion



        #region Empleado
        public async Task<TB_Empleado> GetEmpleado(string numEmpleadoRrHh)
        {
            using (var db = new ConnectionDB(dbConfig))
            {

                var empleado = await (from emp in db.tB_Empleados
                                      where emp.NumEmpleadoRrHh == numEmpleadoRrHh
                                      select emp).FirstOrDefaultAsync();

                return empleado;
            }
        }
        #endregion Empleado

        #region Proyecto
        public async Task<TB_Proyecto> GetProyecto(int numProyecto)
        {
            using (var db = new ConnectionDB(dbConfig))
            {

                var proyecto = await (from proy in db.tB_Proyectos
                                      where proy.NumProyecto == numProyecto
                                      select proy).FirstOrDefaultAsync();

                return proyecto;
            }
        }

        #endregion Proyecto
    }
}
public class CostoQueries : RepositoryLinq2DB<ConnectionDB>
{
    private readonly string _dbConfig = "DBConfig";
    public CostoQueries(string dbConfig)
    {
        _dbConfig = dbConfig;
    }
    public async Task<List<Costo_Detalle>> CostosEmpleados()
    {
        using (var db = new ConnectionDB(_dbConfig))
        {
            var result = await (from costos in db.tB_Costo_Por_Empleados
                                join personaEmp in db.tB_Personas on costos.IdPersona equals personaEmp.IdPersona into personaEmpJoin
                                from personaEmpItem in personaEmpJoin.DefaultIfEmpty()
                                join empleadoCosto in db.tB_Empleados on costos.NumEmpleadoRrHh equals empleadoCosto.NumEmpleadoRrHh into empleadoCostoJoin
                                from empleadoCostoItem in empleadoCostoJoin.DefaultIfEmpty()
                                join ciudad in db.tB_Ciudads on empleadoCostoItem.IdCiudad equals ciudad.IdCiudad into ciudadJoin
                                from ciudadItem in ciudadJoin.DefaultIfEmpty()
                                join puesto in db.tB_Cat_Puestos on costos.IdPuesto equals puesto.IdPuesto into puestoJoin
                                from puestoItem in puestoJoin.DefaultIfEmpty()
                                join proyecto in db.tB_Proyectos on costos.NumProyecto equals proyecto.NumProyecto into proyectoJoin
                                from proyectoItem in proyectoJoin.DefaultIfEmpty()
                                join unidadN in db.tB_Cat_UnidadNegocios on costos.IdUnidadNegocio equals unidadN.IdUnidadNegocio into unidadNJoin
                                from unidadNItem in unidadNJoin.DefaultIfEmpty()
                                join empresa in db.tB_Empresas on costos.IdEmpresa equals empresa.IdEmpresa into empresaJoin
                                from empresaItem in empresaJoin.DefaultIfEmpty()
                                join empleadoJefe in db.tB_Empleados on costos.IdEmpleadoJefe equals empleadoJefe.NumEmpleadoRrHh into empleadoJefeJoin
                                from empleadoJefeItem in empleadoJefeJoin.DefaultIfEmpty()
                                join personaJefe in db.tB_Personas on empleadoJefeItem.IdPersona equals personaJefe.IdPersona into personaJefeJoin
                                from personaJefeItem in personaJefeJoin.DefaultIfEmpty()
                                join categoriaEmp in db.tB_Cat_Categorias on empleadoCostoItem.IdCategoria equals categoriaEmp.IdCategoria into categoriaEmpJoin
                                from categoriaEmpItem in categoriaEmpJoin.DefaultIfEmpty()
                                select new Costo_Detalle
                                {
                                    IdCostoEmpleado = costos.IdCostoEmpleado,
                                    // Empleado
                                    IdPersona = costos.IdPersona,
                                    NumEmpleadoRrHh = costos.NumEmpleadoRrHh,
                                    NumEmpleadoNoi = costos.NumEmpleadoNoi,
                                    NombreCompletoEmpleado = personaEmpItem != null ? personaEmpItem.Nombre + " " + personaEmpItem.ApPaterno + " " + personaEmpItem.ApMaterno : string.Empty,
                                    ApellidoPaterno = personaEmpItem != null ? personaEmpItem.ApPaterno : string.Empty,
                                    ApellidoMaterno = personaEmpItem != null ? personaEmpItem.ApMaterno : string.Empty,
                                    NombreEmpleado = personaEmpItem != null ? personaEmpItem.Nombre : string.Empty,
                                    IdCiudad = ciudadItem != null ? ciudadItem.IdCiudad : 0,
                                    Ciudad = ciudadItem != null ? ciudadItem.Ciudad : string.Empty,
                                    Reubicacion = costos.Reubicacion,
                                    IdPuesto = costos.IdPuesto,
                                    Puesto = puestoItem != null ? puestoItem.Puesto : string.Empty,
                                    NumProyecto = costos.NumProyecto,
                                    Proyecto = proyectoItem != null ? proyectoItem.Proyecto : string.Empty,
                                    IdUnidadNegocio = costos.IdUnidadNegocio,
                                    UnidadNegocio = unidadNItem != null ? unidadNItem.UnidadNegocio : string.Empty,
                                    IdEmpresa = costos.IdEmpresa,
                                    Empresa = empresaItem != null ? empresaItem.Empresa : string.Empty,
                                    Timesheet = costos.Timesheet,
                                    IdEmpleadoJefe = costos.IdEmpleadoJefe,
                                    NombreJefe = personaJefeItem != null ? personaJefeItem.Nombre + " " + personaJefeItem.ApPaterno + " " + personaJefeItem.ApMaterno : string.Empty,
                                    // Seniority
                                    FechaIngreso = costos.FechaIngreso,
                                    Antiguedad = costos.Antiguedad,
                                    // Sueldo neto mensual (MN)
                                    AvgDescuentoEmpleado = costos.AvgDescuentoEmpleado,
                                    MontoDescuentoMensual = costos.MontoDescuentoMensual,
                                    SueldoNetoPercibidoMensual = costos.SueldoNetoPercibidoMensual,
                                    RetencionImss = costos.RetencionImss,
                                    Ispt = costos.Ispt,
                                    // Sueldo bruto MN/USD
                                    SueldoBrutoInflacion = costos.SueldoBruto,
                                    Anual = costos.Anual,
                                    // Aguinaldo
                                    AguinaldoCantidadMeses = costos.AguinaldoCantMeses,
                                    AguinaldoMontoProvisionMensual = costos.AguinaldoMontoProvisionMensual,
                                    // Prima vacacional
                                    PvDiasVacasAnuales = costos.PvDiasVacasAnuales,
                                    PvProvisionMensual = costos.PvProvisionMensual,
                                    // Indemnización
                                    IndemProvisionMensual = costos.IndemProvisionMensual,
                                    // Provisión bono anual
                                    AvgBonoAnualEstimado = costos.AvgBonoAnualEstimado,
                                    BonoAnualProvisionMensual = costos.BonoAnualProvisionMensual,
                                    // GMM
                                    SgmmCostoTotalAnual = costos.SgmmCostoTotalAnual,
                                    SgmmCostoMensual = costos.SgmmCostoMensual,
                                    // Seguro de vida
                                    SvCostoTotalAnual = costos.SvCostoTotalAnual,
                                    SvCostoMensual = costos.SvCostoMensual,
                                    // Vales de despensa
                                    VaidCostoMensual = costos.VaidCostoMensual,
                                    VaidComisionCostoMensual = costos.VaidComisionCostoMensual,
                                    // PTU
                                    PtuProvision = costos.PtuProvision,
                                    // Cargas sociales e impuestos laborales
                                    Impuesto3sNomina = costos.Impuesto3sNomina,
                                    Imss = costos.Imss,
                                    Retiro2 = costos.Retiro2,
                                    CesantesVejez = costos.CesantesVejez,
                                    Infonavit = costos.Infonavit,
                                    CargasSociales = costos.CargasSociales,
                                    // Costo total laboral BLL
                                    CostoMensualEmpleado = costos.CostoMensualEmpleado,
                                    CostoMensualProyecto = costos.CostoMensualProyecto,
                                    CostoAnualEmpleado = costos.CostoAnualEmpleado,
                                    CostoSalarioBruto = costos.CostoSalarioBruto,
                                    CostoSalarioNeto = costos.CostoSalarioBruto,

                                    NuAnno = costos.NuAnno,
                                    NuMes = costos.NuMes,
                                    FechaActualizacion = costos.FechaActualizacion,
                                    RegHistorico = costos.RegHistorico,
                                    Categoria = categoriaEmpItem.Categoria,
                                    SalarioDiarioIntegrado = empleadoCostoItem.Cotizacion,
                                    //ATC
                                    ImpuestoNomina = proyectoItem.ImpuestoNomina,
                                }).ToListAsync();

            foreach (var r in result)
            {
                r.Beneficios = new List<Beneficio_Costo_Detalle>();
                r.Beneficios.AddRange(await (from eb in db.tB_EmpleadoBeneficios
                                             join b in db.tB_Cat_Beneficios on eb.IdBeneficio equals b.IdBeneficio into bJoin
                                             from bItem in bJoin.DefaultIfEmpty()
                                             where eb.NumEmpleadoRrHh == r.NumEmpleadoRrHh
                                             && eb.RegHistorico == false
                                             select new Beneficio_Costo_Detalle
                                             {
                                                 Id = eb.Id,
                                                 IdBeneficio = eb.IdBeneficio,
                                                 Beneficio = bItem != null ? bItem.Beneficio : string.Empty,
                                                 NumEmpleadoRrHh = eb.NumEmpleadoRrHh,
                                                 Costo = eb.Costo,
                                                 Mes = eb.Mes,
                                                 Anio = eb.Anno,
                                                 FechaActualizacion = eb.FechaActualizacion,
                                                 RegHistorico = eb.RegHistorico
                                             }).ToListAsync());
            }

            foreach (var r in result)
            {
                r.Beneficiosproyecto = new List<Beneficio_Costo_Detalle>();
                r.Beneficiosproyecto.AddRange(await (from eb in db.tB_EmpleadoProyectoBeneficios
                                             join b in db.tB_Cat_Beneficios on eb.IdBeneficio equals b.IdBeneficio into bJoin
                                             from bItem in bJoin.DefaultIfEmpty()
                                             where eb.NumEmpleadoRrHh == r.NumEmpleadoRrHh
                                             
                                                     select new Beneficio_Costo_Detalle
                                             {
                                                 //Id = eb.Id,
                                                 IdBeneficio = eb.IdBeneficio,
                                                 Beneficio = bItem != null ? bItem.Beneficio : string.Empty,
                                                 NumEmpleadoRrHh = eb.NumEmpleadoRrHh,
                                                 nucostobeneficio = eb.nucostobeneficio,
                                                 NumProyecto = eb.NumProyecto
                                                 //Costo = eb.Costo,
                                                 //Mes = eb.Mes,
                                                 //Anio = eb.Anno,
                                                 //FechaActualizacion = eb.FechaActualizacion,
                                                 //RegHistorico = eb.RegHistorico
                                             }).ToListAsync());
            }

            return result;

        }

    }

}

