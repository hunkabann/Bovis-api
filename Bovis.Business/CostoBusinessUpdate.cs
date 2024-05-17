using AutoMapper;
using Bovis.Common.Mapper;
using Bovis.Common.Model.DTO;
using Bovis.Common.Model.Tables;
using Bovis.Data.Interface;
using Microsoft.Win32;
using System.Runtime.Intrinsics.X86;


namespace Bovis.Business
{
    
    public static class CostoBusinessUpdate
    {
        //ATC 
        //public static decimal BonoAdicionalReubicacion = 0M;
        //public static decimal ViaticosAComprobar = 0M;
        //public static decimal Be_BonoAdicional = 0M;
        //public static decimal Be_AyudaTransporte = 0M;

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




        public class CostoLaboral
        {
            public decimal? CostoMensualEmpleado { get; set; }
            public decimal? CostoMensualProyecto { get; set; }
            public decimal? CostoAnualEmpleado { get; set; }
            public decimal? CostoSalarioBruto { get; set; } // CostoMensualEmpleado/SueldoBruto
            public decimal? CostoSalarioNeto { get; set; } // CostoMensualEmpleado/SueldoNeto

        }

        public static TB_CostoPorEmpleado ValueFields(CostoPorEmpleadoDTO source)
        {
           

        TB_CostoPorEmpleado destination = new();

            destination = MapToTbCostoEmpleado<NotNullMappingProfile>(source, destination);


            #region Seniority
            if (destination.FechaIngreso == null || destination.FechaIngreso == DateTime.MinValue)
            {
                DateTime fechaHoraActual = DateTime.Now;
                string fechaHoraFormateada = fechaHoraActual.ToString("yyyy-MM-ddTHH:mm:ss.fff");
                DateTime fechaHoraFormateadaDateTime = DateTime.ParseExact(fechaHoraFormateada, "yyyy-MM-ddTHH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
                destination.FechaIngreso = fechaHoraFormateadaDateTime;
            }
            destination.NuMes = DateTime.Now.Month;
            destination.NuAnno = DateTime.Now.Year;
            TimeSpan diferencia = (TimeSpan)(DateTime.Now - destination.FechaIngreso);
            destination.Antiguedad = diferencia.Days / 365;
            #endregion Seniority

            #region Aguinaldo
            if (source.IdCategoria == 1)
            {
                if (destination.Antiguedad < 5)
                {
                    destination.AguinaldoCantMeses = 15;
                }
                if (destination.Antiguedad >= 5 && destination.Antiguedad < 10)
                {
                    destination.AguinaldoCantMeses = 30;
                }
                if (destination.Antiguedad > 10)
                {
                    destination.AguinaldoCantMeses = 40;
                }

            }
            else
            {
                destination.AguinaldoCantMeses = 15;
            }
            #endregion Aguinaldo

            #region IMSS
            //destination.RetencionImss = 


            //ATC
            if (source.cotizacion == null)
            {


            }
            else
            {
                if (p_patron > p_3_Veces_UMA)
                {
                    p_EME2 = (double)(((source.cotizacion - p_3_Veces_UMA) * .004) * 31);
                }
                else
                {
                    p_EME2 = 0;
                }

                p_EME_GMPE = (double)(source.cotizacion * 0.00375) * p_dias_mes;

                p_EME_ED = (double)(source.cotizacion * 0.0025) * p_dias_mes;

                p_EME_ESP = (double)(source.cotizacion * 0.00625) * p_dias_mes;

                p_GP = (double)(source.cotizacion * 0.0) * p_dias_mes;

                p_CEAV = (double)(source.cotizacion * 0.01125) * p_dias_trabajados_bim;

                if (source.cotizacion < 1)
                {
                    destination.RetencionImss = 0;
                }
                else
                {
                    destination.RetencionImss = (decimal)(p_EME2 + p_EME_GMPE + p_EME_ED + p_EME_ESP + p_GP + p_CEAV);
                }
               

                p_RTP = (double)(source.cotizacion * p_dias_trabajados) * p_Prima_Riesgo;

                p_PEME = (double)(p_Patron_CF * p_UMA) * p_dias_mes;


                if (p_patron > p_3_Veces_UMA)
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
                    destination.Imss = 0;
                }
                else
                {
                    destination.Imss = (decimal)(p_RTP + p_PEME + p_PEME2 + p_EMGP + p_EMDP + p_IVDP + p_GPSP);
                }

                destination.Imss = (decimal)(p_RTP + p_PEME + p_PEME2 + p_EMGP + p_EMDP + p_IVDP + p_GPSP);

                p_SBC = (double)(source.cotizacion * p_Patron_SBC_CEAV) * p_dias_trabajados_bim;

                destination.Retiro2 = (decimal)p_SBC;


                destination.CesantesVejez = (decimal)(source.cotizacion * p_Patron_CV) * p_dias_trabajados_bim;

                destination.Infonavit = (decimal)(source.cotizacion * p_Patron_Infona) * p_dias_trabajados_bim;



                //destination.Imss = (decimal)(p_EME2 + p_EME_GMPE + p_EME_ED + p_EME_ESP + p_GP);
            }

            

            #endregion IMSS

            if (destination.SueldoBruto.HasValue && destination.SueldoBruto > 1)
            {
                #region Sueldo Neto Mensual
                destination.MontoDescuentoMensual = destination.RetencionImss + destination.Ispt;
                destination.AvgDescuentoEmpleado = destination.SueldoBruto > 0 ? destination.MontoDescuentoMensual / destination.SueldoBruto : 0;
                //ATC
                //destination.SueldoNetoPercibidoMensual = (destination.SueldoBruto + CostoBusinessConstants.BonoAdicionalReubicacion / CostoBusinessConstants.ViaticosAComprobar) - destination.MontoDescuentoMensual;
                destination.SueldoNetoPercibidoMensual = (destination.SueldoBruto ) - destination.MontoDescuentoMensual;
                #endregion Sueldo Neto Mensual

                destination.Anual = destination.SueldoBruto * 12;

                #region Aguinaldo
                // destination.AguinaldoMontoProvisionMensual = destination.AguinaldoCantMeses * destination.SueldoBruto / 12M;
                destination.AguinaldoMontoProvisionMensual = ((destination.SueldoBruto / 30) * destination.AguinaldoCantMeses) / 12M;
                #endregion Aguinaldo

                #region Indemnizacion
                destination.IndemProvisionMensual = ((destination.SueldoBruto / 30M) * 20M) / 12M;
                #endregion Indemnizacion

                #region PTU
                destination.PtuProvision = destination.SueldoBruto * 1116000M / 8053945M / 12M;
                #endregion PTU
            }
            else
            {
                #region Sueldo Neto Mensual
                destination.AvgDescuentoEmpleado = 0;
                #endregion Sueldo Neto Mensual
            }


            #region Prima Vacacional
            if (destination.SueldoBruto == 0)
            {
                destination.PvDiasVacasAnuales = 0;
            }
            else
            {
                switch (destination.Antiguedad)
                {
                    case 1:
                        destination.PvDiasVacasAnuales = 12;
                        break;
                    case 2:
                        destination.PvDiasVacasAnuales = 14;
                        break;
                    case 3:
                        destination.PvDiasVacasAnuales = 16;
                        break;
                    case 4:
                        destination.PvDiasVacasAnuales = 18;
                        break;
                    case 5:
                        destination.PvDiasVacasAnuales = 20;
                        break;
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        destination.PvDiasVacasAnuales = 22;
                        break;
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        destination.PvDiasVacasAnuales = 24;
                        break;
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                        destination.PvDiasVacasAnuales = 26;
                        break;
                    case 21:
                    case 22:
                    case 23:
                    case 24:
                    case 25:
                        destination.PvDiasVacasAnuales = 28;
                        break;
                    case 26:
                    case 27:
                    case 28:
                    case 29:
                    case 30:
                        destination.PvDiasVacasAnuales = 30;
                        break;
                    case 31:
                    case 32:
                    case 33:
                    case 34:
                    case 35:
                        destination.PvDiasVacasAnuales = 32;
                        break;
                }
            }

            if (destination.SueldoBruto > 0)
            {
                destination.PvProvisionMensual = destination.PvDiasVacasAnuales > 0 ? (destination.SueldoBruto / 30 * destination.PvDiasVacasAnuales * 0.25M / 12) * 0.75M : 0;
            }
            else
                destination.PvProvisionMensual = 0.0M;
            #endregion Prima Vacacional

            #region Provisión Bono Anual
            //destination.AvgBonoAnualEstimado = source.AvgBonoAnualEstimado;
            if(destination.AvgBonoAnualEstimado.HasValue)
            {
                //destination.
            }
            #endregion Provisión Bono Anual

            #region Gastos Médicos Mayores
            //destination.SgmmCostoTotalAnual = source.SgmmCostoTotalAnual;
            if (destination.SgmmCostoTotalAnual.HasValue)
            {
                destination.SgmmCostoMensual = destination.SgmmCostoTotalAnual / 12.0M;
            }
            #endregion Gastos Médicos Mayores

            #region Seguro de Vida
            //destination.SvCostoTotalAnual = source.SvCostoTotalAnual;
            if (destination.SvCostoTotalAnual.HasValue && destination.SvCostoTotalAnual != 0)
            {
                destination.SvCostoMensual = destination.SvCostoTotalAnual / 12.0M;
            }
            else
                destination.SvCostoMensual = 0.0M;
            #endregion Seguro de Vida

            #region Vales de Despensa
            //destination.VaidCostoMensual = source.VaidCostoMensual;
            if (destination.VaidCostoMensual.HasValue && destination.VaidCostoMensual != 0)
            {
                destination.VaidComisionCostoMensual = destination.VaidCostoMensual * 0.015M;
            }
            else
                destination.VaidComisionCostoMensual = 0.0M;
            #endregion Vales de Despensa

            #region Cargas sociales e impuestos laborales
            //destination.Impuesto3sNomina = (destination.SueldoBruto + destination.AguinaldoMontoProvisionMensual + destination.PvProvisionMensual + CostoBusinessConstants.Be_BonoAdicional + CostoBusinessConstants.Be_AyudaTransporte + destination.BonoAnualProvisionMensual + CostoBusinessConstants.BonoAdicionalReubicacion) * 0.03M; //(source.ImpuestoNomina/100); // * 0.03M;
            destination.Impuesto3sNomina = (destination.SueldoBruto + destination.AguinaldoMontoProvisionMensual + destination.PvProvisionMensual + CostoBusinessConstants.Be_BonoAdicional + CostoBusinessConstants.Be_AyudaTransporte + destination.BonoAnualProvisionMensual + CostoBusinessConstants.BonoAdicionalReubicacion) * 0.03M; //(source.ImpuestoNomina/100); // * 0.03M;
            if (source.cotizacion == null)
            {


            }
            else
            {
                if (source.cotizacion < 1)
                {
                    destination.CargasSociales = 0;
                }
                else
                {
                    destination.CargasSociales = destination.Impuesto3sNomina + destination.Imss + destination.Retiro2 + destination.CesantesVejez + destination.Infonavit;
                }
            }

              
            #endregion Cargas sociales e impuestos laborales

            #region Costo total laboral BLL
            destination.CostoMensualEmpleado = destination.SueldoBruto + destination.AguinaldoMontoProvisionMensual + destination.PvProvisionMensual + destination.IndemProvisionMensual + destination.BonoAnualProvisionMensual + destination.SgmmCostoMensual + destination.SvCostoMensual + destination.VaidCostoMensual + destination.VaidComisionCostoMensual + destination.PtuProvision + destination.CargasSociales;
            destination.CostoMensualProyecto = 0.0M;
            destination.CostoAnualEmpleado = destination.CostoMensualEmpleado * 12;
            destination.CostoSalarioBruto = destination.SueldoBruto > 0 ? destination.CostoMensualEmpleado / destination.SueldoBruto : 0;
            destination.CostoSalarioNeto = destination.SueldoNetoPercibidoMensual > 0 ? destination.CostoMensualEmpleado / destination.SueldoNetoPercibidoMensual : 0;
            #endregion Costo total laboral BLL




            destination.FechaActualizacion = DateTime.Now;
            destination.RegHistorico = false;

            //ATC
            if (source.CostoMensualProyecto == null)
            {


            }
            else
            {
                destination.CostoMensualProyecto = source.CostoMensualProyecto;
            }


            return destination;

        }

        private static TB_CostoPorEmpleado MapToTbCostoEmpleado<TProfile>(CostoPorEmpleadoDTO source, TB_CostoPorEmpleado destination) where TProfile : Profile, new()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TProfile>();
            });

            IMapper mapper = config.CreateMapper();
            destination = mapper.Map(source, destination);

            return destination;

        }

    }
}
